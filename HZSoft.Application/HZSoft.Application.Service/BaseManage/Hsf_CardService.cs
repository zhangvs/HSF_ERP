using HZSoft.Application.Entity.BaseManage;
using HZSoft.Application.IService.BaseManage;
using HZSoft.Data.Repository;
using HZSoft.Util;
using HZSoft.Util.Extension;
using HZSoft.Util.WebControl;
using System.Collections.Generic;
using System.Linq;

namespace HZSoft.Application.Service.BaseManage
{
    /// <summary>
    /// 版 本 6.1
    /// 
    /// 创 建：超级管理员
    /// 日 期：2019-01-28 14:42
    /// 描 述：电子名片
    /// </summary>
    public class Hsf_CardService : RepositoryFactory<Hsf_CardEntity>, Hsf_CardIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<Hsf_CardEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<Hsf_CardEntity>();
            expression = expression.And(t => t.DeleteMark != 1);
            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<Hsf_CardEntity> GetList(string queryJson)
        {
            var expression = LinqExtensions.True<Hsf_CardEntity>();
            expression = expression.And(t => t.DeleteMark != 1);
            //var queryParam = queryJson.ToJObject();
            //if (!queryParam["OpenId"].IsEmpty())
            //{
            //    expression = expression.And(t => t.OpenId == queryParam["OpenId"].ToString());
            //}
            return this.BaseRepository().IQueryable(expression).OrderByDescending(t => t.CreateDate).ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Hsf_CardEntity GetEntity(string keyValue)
        {
            var expression = LinqExtensions.True<Hsf_CardEntity>();
            expression = expression.And(t => t.Id == keyValue);
            return this.BaseRepository().FindEntity(expression);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="OpenId">主键值</param>
        /// <returns></returns>
        public Hsf_CardEntity GetEntityByOpenId(string OpenId)
        {
            var expression = LinqExtensions.True<Hsf_CardEntity>();
            expression = expression.And(t => t.OpenId == OpenId);
            return this.BaseRepository().FindEntity(expression);
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
        public string SaveForm(string keyValue, Hsf_CardEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
                return entity.Id;
            }
            else
            {
                entity.Create();
                entity.Description = Config.GetValue("Domain") + "/WeChatManage/Card/Skl?Id=" + entity.Id;
                this.BaseRepository().Insert(entity);
                return entity.Id;
            }
        }
        #endregion
    }
}
