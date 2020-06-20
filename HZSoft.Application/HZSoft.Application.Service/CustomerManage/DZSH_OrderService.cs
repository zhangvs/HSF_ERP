using HZSoft.Application.Code;
using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.IService.CustomerManage;
using HZSoft.Application.IService.SystemManage;
using HZSoft.Application.Service.SystemManage;
using HZSoft.Data.Repository;
using HZSoft.Util;
using HZSoft.Util.Extension;
using HZSoft.Util.WebControl;
using HZSoft.Util.WeChat.Comm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HZSoft.Application.Service.CustomerManage
{
    /// <summary>
    /// 版 本 6.1
    /// 
    /// 创 建：超管
    /// 日 期：2020-05-18 09:28
    /// 描 述：客诉单
    /// </summary>
    public class DZSH_OrderService : RepositoryFactory<DZSH_OrderEntity>, DZSH_OrderIService
    {
        private ICodeRuleService coderuleService = new CodeRuleService();
        private DZ_OrderIService dzService = new DZ_OrderService();
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<DZSH_OrderEntity> GetPageList(Pagination pagination, string queryJson)
        {
            string strSql = $"select * from DZSH_Order where DeleteMark=0 ";
            var expression = LinqExtensions.True<DZSH_OrderEntity>();
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
            //标题模糊搜索
            if (!queryParam["OrderTitle"].IsEmpty())
            {
                string OrderTitle = queryParam["OrderTitle"].ToString();
                strSql += " and OrderTitle like '%" + OrderTitle + "%'";
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
            //支付状态
            if (!queryParam["PaymentState"].IsEmpty())
            {
                int PaymentState = queryParam["PaymentState"].ToInt();
                if (PaymentState == 2)
                {
                    strSql += " and PaymentState >= 2 ";
                }
                else
                {
                    strSql += " and PaymentState  = " + PaymentState;
                }

            }
            //结束
            if (!queryParam["OverMark"].IsEmpty())
            {
                int OverMark = queryParam["OverMark"].ToInt();
                strSql += " and OverMark  = " + OverMark;
            }
            return this.BaseRepository().FindList(strSql.ToString(), pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<DZSH_OrderEntity> GetList(string queryJson)
        {
            string strSql = $"select * from DZSH_Order where DeleteMark=0 ";
            var expression = LinqExtensions.True<DZSH_OrderEntity>();
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
            //标题模糊搜索
            if (!queryParam["OrderTitle"].IsEmpty())
            {
                string OrderTitle = queryParam["OrderTitle"].ToString();
                strSql += " and OrderTitle like '%" + OrderTitle + "%'";
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

            //支付状态
            if (!queryParam["PaymentState"].IsEmpty())
            {
                int PaymentState = queryParam["PaymentState"].ToInt();
                if (PaymentState == 2)
                {
                    strSql += " and PaymentState >= 2 ";
                }
                else
                {
                    strSql += " and PaymentState  = " + PaymentState;
                }

            }
            return this.BaseRepository().FindList(strSql.ToString());
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public DZSH_OrderEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, DZSH_OrderEntity entity)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    //先拿到老的客诉单
                    DZSH_OrderEntity oldEntity = GetEntity(keyValue);

                    if (entity.NeedSale == 1 && oldEntity.NeedSale == 0)
                    {
                        //改为生产之后，创建销售单
                        CreateDZOrder.SaveDZOrder(db, entity);
                    }
                    else
                    {
                        //先拿到老的销售单
                        DZ_OrderEntity oldDZEntity = dzService.GetEntity(keyValue);
                        if (oldDZEntity!=null)
                        {
                            //如果上传的文件路径变化，判断销售单中的审图状态是否已经审图，已经审图，不能修改文件，需要研发驳回！
                            if (entity.CreatePath!=oldEntity.CreatePath && oldDZEntity.CheckTuMark == 1)
                            {
                                throw new Exception("请联系研发审图驳回，再重新上传文件！");
                            }
                            DZ_OrderEntity dzEntity = new DZ_OrderEntity
                            {
                                OrderTitle = entity.OrderTitle,
                                OrderType = entity.OrderType,
                                CompanyId = entity.CompanyId,
                                CompanyName = entity.CompanyName,
                                CustomerId = entity.CustomerId,
                                CustomerName = entity.CustomerName,
                                CustomerTelphone = entity.CustomerTelphone,
                                DesignerUserName = entity.DesignerUserName,
                                DesignerTelphone = entity.DesignerTelphone,
                                Address = entity.Address,
                                SalesmanUserId = entity.SalesmanUserId,
                                SalesmanUserName = entity.SalesmanUserName,
                                ShippingType = entity.ShippingType,
                                Carrier = entity.Carrier,
                                CreatePath = entity.CreatePath,
                                Description = entity.Description,
                            };
                            oldDZEntity.Modify(keyValue);
                            //同步销售单
                            db.Update<DZ_OrderEntity>(dzEntity);//重点关注的是路径是否同步
                        }
                    }

                    entity.Modify(keyValue);
                    //更新
                    db.Update<DZSH_OrderEntity>(entity);
                    db.Commit();
                    RecordHelp.AddRecord(4, entity.Id, "客诉单编辑");
                }
                else
                {
                    entity.Create();
                    db.Insert<DZSH_OrderEntity>(entity);
                    coderuleService.UseRuleSeed(entity.CreateUserId, "", ((int)CodeRuleEnum.DZSH_Order).ToString(), db);//占用单据号
                    if (entity.NeedSale == 1)
                    {
                        //创建新的销售单
                        CreateDZOrder.SaveDZOrder(db, entity);
                    }

                    db.Commit();
                    RecordHelp.AddRecord(4, entity.Id, "客诉接单");
                    //胡鲁鲁
                    TemplateWxApp.SendTemplateNew("oA-EC1aaKOSNdW2wL8lHSsr3R4Dg", "您好，有新的客诉单!", entity.OrderTitle, entity.Code, "请进行处理。");

                    //发微信模板消息---客诉单接单之后，给审图人提醒--刘琛oA-EC1X6RWfW1_DNJ_VNiA3uhOYg
                    //订单生成通知（审图提醒）
                    TemplateWxApp.SendTemplateNew("oA-EC1X6RWfW1_DNJ_VNiA3uhOYg", "您好，有新的订单需要审图!", entity.OrderTitle, entity.Code, "请进行审图。");
                }
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
