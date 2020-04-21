using HZSoft.Application.Code;
using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.IService.CustomerManage;
using HZSoft.Data.Repository;
using HZSoft.Util;
using HZSoft.Util.Extension;
using HZSoft.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using ThoughtWorks.QRCode.Codec;

namespace HZSoft.Application.Service.CustomerManage
{
    /// <summary>
    /// 版 本 6.1
    /// 
    /// 创 建：佘赐雄
    /// 日 期：2016-04-06 17:24
    /// 描 述：应收账款
    /// </summary>
    public class ReceivableSklService : RepositoryFactory<ReceivableEntity>, IReceivableSklService
    {
        private DZ_OrderIService orderIService = new DZ_OrderService();
        private Buys_OrderIService buyIService = new Buys_OrderService();
        private Sale_CustomerService saleIService = new Sale_CustomerService();

        #region 获取数据
        /// 获取收款单列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<DZ_OrderEntity> GetPaymentPageList(Pagination pagination, string queryJson)
        {
            string strSql = $"select * from DZ_Order where DeleteMark=0 ";//报价的才显示 and MoneyMark = 1,,,不一定报价的显示，可以先收款，收款研发没有料单就不会到生产单
            var expression = LinqExtensions.True<DZ_OrderEntity>();
            var queryParam = queryJson.ToJObject();
            //成立日期
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and CreateDate >= '" + startTime + "' and CreateDate < '" + endTime + "'";
            }
            //销售编号
            if (!queryParam["Code"].IsEmpty())
            {
                string Code = queryParam["Code"].ToString();
                strSql += " and Code like '%" + Code + "%'";
            }

            //公司名
            if (!queryParam["CompanyName"].IsEmpty())
            {
                string CompanyName = queryParam["CompanyName"].ToString();
                strSql += " and CompanyName like '%" + CompanyName + "%'";
            }
            //客户名
            if (!queryParam["CustomerName"].IsEmpty())
            {
                string CustomerName = queryParam["CustomerName"].ToString();
                strSql += " and CustomerName like '%" + CustomerName + "%'";
            }
            //销售人
            if (!queryParam["SalesmanUserName"].IsEmpty())
            {
                string SalesmanUserName = queryParam["SalesmanUserName"].ToString();
                strSql += " and SalesmanUserName like '%" + SalesmanUserName + "%'";
            }

            //销售人
            if (!queryParam["CreateUserId"].IsEmpty())
            {
                string CreateUserId = queryParam["CreateUserId"].ToString();
                strSql += " and CreateUserId = '" + CreateUserId + "'";
            }
            else
            {
                if (!OperatorProvider.Provider.Current().IsSystem)
                {
                    string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
                    strSql += " and CreateUserId in (" + dataAutor + ")";
                }
            }
            return new RepositoryFactory().BaseRepository().FindList<DZ_OrderEntity>(strSql.ToString(), pagination);
        }
        /// <summary>
        /// 获取收款记录列表
        /// </summary>
        /// <param name="orderId">订单主键</param>
        /// <returns></returns>
        public IEnumerable<ReceivableEntity> GetPaymentRecord(string orderId)
        {
            return this.BaseRepository().IQueryable(t => t.OrderId.Equals(orderId)).OrderByDescending(t => t.CreateDate).ToList();
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存表单（新增）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(ReceivableEntity entity)
        {
            ICashBalanceService icashbalanceservice = new CashBalanceService();
            DZ_OrderEntity orderEntity = orderIService.GetEntity(entity.OrderId);

            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                //更改订单状态
                orderEntity.ReceivedAmount = orderEntity.ReceivedAmount + entity.PaymentPrice;//收款金额
                orderEntity.PaymentDate = DateTime.Now;//收款日期
                if (orderEntity.ReceivedAmount == orderEntity.Accounts)
                {
                    orderEntity.PaymentState = 3;//收款状态

                    //全部收款之后更新【入库单】中的尾款状态
                    Buys_OrderEntity buyEntity=buyIService.GetEntityByOrderId(orderEntity.Id);
                    if (buyEntity!=null)
                    {
                        buyEntity.PaymentDate = DateTime.Now;
                        buyEntity.PaymentState = 3;//全部收款
                        db.Update(buyEntity);
                    }
                }
                else
                {
                    orderEntity.PaymentState = 2;//部分收款
                }
                db.Update(orderEntity);

                //添加【收款】
                entity.Create();
                db.Insert(entity);

                //修改【下单状态】，第一次收款的时候才创建
                if (orderEntity.DownMark != 1)
                {
                    //收预付款，并不意味着下单，而是只是收款，客服中心下单才是真正的下单
                    //orderEntity.DownMark = 1;
                    //orderEntity.DownDate = DateTime.Now;
                    //orderEntity.DownUserId = OperatorProvider.Provider.Current().UserId;
                    //orderEntity.DownUserName = OperatorProvider.Provider.Current().UserName;
                    //orderEntity.Modify(orderEntity.Id);
                    //db.Update<DZ_OrderEntity>(orderEntity);

                    //付了预付款，自动创建【生产单】主单部分*****************
                    saleIService.SaveBuyMain(db, orderEntity);
                }

                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        #endregion
    }
}