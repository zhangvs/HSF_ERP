using HZSoft.Application.Code;
using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.IService.CustomerManage;
using HZSoft.Application.IService.SystemManage;
using HZSoft.Application.Service.SystemManage;
using HZSoft.Data.Repository;
using HZSoft.Util;
using HZSoft.Util.Extension;
using HZSoft.Util.WebControl;
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
                    //先拿到老的销售单状态
                    DZSH_OrderEntity oldEntity = GetEntity(keyValue);

                    if (entity.NeedSale > 0 && oldEntity.NeedSale == 0)
                    {
                        CreateDZOrder.SaveDZOrder(db, entity);
                    }
                    entity.Modify(keyValue);
                    //创建新的销售单
                    db.Update<DZSH_OrderEntity>(entity);
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
