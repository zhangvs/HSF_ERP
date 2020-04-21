using HZSoft.Application.Entity.BaseManage;
using HZSoft.Application.IService.BaseManage;
using HZSoft.Data.Repository;
using HZSoft.Util.Extension;
using HZSoft.Util.WebControl;
using System.Collections.Generic;
using System.Linq;
using HZSoft.Util;
using HZSoft.Application.Code;
using HZSoft.Application.IService.SystemManage;
using HZSoft.Application.Service.SystemManage;
using System;

namespace HZSoft.Application.Service.BaseManage
{
    /// <summary>
    /// 版 本 6.1
    /// 
    /// 创 建：超级管理员
    /// 日 期：2017-06-30 10:28
    /// 描 述：供应商
    /// </summary>
    public class Supplier_InfoService : RepositoryFactory<Supplier_InfoEntity>, Supplier_InfoIService
    {
        private ICodeRuleService coderuleService = new CodeRuleService();
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<Supplier_InfoEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<Supplier_InfoEntity>();
            var queryParam = queryJson.ToJObject();
            //查询条件&& !queryParam["keyword"].IsEmpty()
            if (!queryParam["condition"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "Code":              //存货编号
                        expression = expression.And(t => t.Code.Contains(keyword));
                        break;
                    case "FullName":            //存货名称
                        expression = expression.And(t => t.FullName.Contains(keyword));
                        break;
                    case "ShortName":            //简称
                        expression = expression.And(t => t.ShortName.Contains(keyword));
                        break;
                    case "Namepy":              //助记码
                        expression = expression.And(t => t.Namepy.Contains(keyword));
                        break;
                    case "ParentId":                 //上级存货
                        expression = expression.And(t => t.ParentId.Contains(keyword));
                        break;
                    case "All":
                        expression = expression.And(t => t.FullName.Contains(keyword)
                            || t.Code.Contains(keyword)
                            || t.FullName.Contains(keyword)
                            || t.ShortName.Contains(keyword)
                            || t.Namepy.Contains(keyword)
                            || t.ParentId.Contains(keyword)
                            );
                        break;
                    default:
                        break;
                }
            }
            return this.BaseRepository().FindList(expression, pagination);
        }
        public IEnumerable<Supplier_InfoEntity> GetParentId()
        {
            return this.BaseRepository().FindList("SELECT * FROM Supplier_Info WHERE ParentId IS null ");
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<Supplier_InfoEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Supplier_InfoEntity GetEntity(string keyValue)
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
        public void SaveForm(string keyValue, Supplier_InfoEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                //IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
                //entity.Create();
                ////获得指定模块或者编号的单据号
                //entity.Code = coderuleService.SetBillCode(entity.CreatorId, SystemInfo.CurrentModuleId, "", db);
                //this.BaseRepository().Insert(entity);

                IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
                try
                {
                    entity.Create();
                    db.Insert(entity);
                    //占用单据号
                    coderuleService.UseRuleSeed(entity.CreatorId, SystemInfo.CurrentModuleId, "", db);
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
