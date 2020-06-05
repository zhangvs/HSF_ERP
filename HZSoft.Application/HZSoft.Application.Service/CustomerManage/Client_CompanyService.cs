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
    /// 日 期：2020-03-15 11:09
    /// 描 述：经销商
    /// </summary>
    public class Client_CompanyService : RepositoryFactory<Client_CompanyEntity>, Client_CompanyIService
    {
        private ICodeRuleService coderuleService = new CodeRuleService();
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<Client_CompanyEntity> GetPageList(Pagination pagination, string queryJson)
        {

            var queryParam = queryJson.ToJObject();
            string strSql = $"select * from Client_Company where (DeleteMark<>1 or DeleteMark IS NULL) ";
            //成立日期
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and CreateDate >= '" + startTime + "' and CreateDate < '" + endTime + "'";
            }
            //客户编号
            if (!queryParam["EnCode"].IsEmpty())
            {
                string EnCode = queryParam["EnCode"].ToString();
                strSql += " and EnCode like '%" + EnCode + "%'";
            }

            //客户名称
            if (!queryParam["FullName"].IsEmpty())
            {
                string FullName = queryParam["FullName"].ToString();
                strSql += " and FullName like '%" + FullName + "%'";
            }
            //设计师
            if (!queryParam["Contact"].IsEmpty())
            {
                string Contact = queryParam["Contact"].ToString();
                strSql += " and Contact like '%" + Contact + "%'";
            }

            //客户级别
            if (!queryParam["CustLevelId"].IsEmpty())
            {
                int CustLevelId = queryParam["CustLevelId"].ToInt();
                strSql += " and CustLevelId  = " + CustLevelId;
            }
            //客户程度
            if (!queryParam["CustDegreeId"].IsEmpty())
            {
                int CustDegreeId = queryParam["CustDegreeId"].ToInt();
                strSql += " and CustDegreeId  = " + CustDegreeId;
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
            return this.BaseRepository().FindList(strSql.ToString(), pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<Client_CompanyEntity> GetList(string queryJson)
        {
            string strSql = "select * from Client_Company where (DeleteMark<>1 or DeleteMark IS NULL)  ";
            string strOrder = " ORDER BY CreateDate desc";
            if (!OperatorProvider.Provider.Current().IsSystem)
            {
                string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
                strSql += " and CreateUserId in (" + dataAutor + ")";
            }

            strSql += strOrder;
            return this.BaseRepository().FindList(strSql.ToString());
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Client_CompanyEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion


        #region 验证数据
        /// <summary>
        /// 客户名称不能重复
        /// </summary>
        /// <param name="fullName">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistFullName(string fullName, string keyValue)
        {
            var expression = LinqExtensions.True<Client_CompanyEntity>();
            expression = expression.And(t => t.FullName == fullName);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.CompanyId != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        /// <summary>
        /// 手机不能重复
        /// </summary>
        /// <param name="Mobile">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistMobile(string Mobile, string keyValue)
        {
            var expression = LinqExtensions.True<Client_CompanyEntity>();
            expression = expression.And(t => t.Mobile == Mobile);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.CompanyId != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
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
        public void SaveForm(string keyValue, Client_CompanyEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
                try
                {
                    entity.Create();
                    //获得指定模块或者编号的单据号
                    entity.EnCode = coderuleService.SetBillCode(entity.CreateUserId, SystemInfo.CurrentModuleId, "", db);
                    db.Insert(entity);
                    db.Commit();
                }
                catch (Exception)
                {
                    db.Rollback();
                    throw;
                }
            }
        }
        #endregion
    }
}
