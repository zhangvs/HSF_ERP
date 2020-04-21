using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.IService.CustomerManage;
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
    /// 创 建：超级管理员
    /// 日 期：2018-10-20 09:13
    /// 描 述：报名表
    /// </summary>
    public class Poll_SignUpService : RepositoryFactory<Poll_SignUpEntity>, Poll_SignUpIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<Poll_SignUpEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<Poll_SignUpEntity>();
            var queryParam = queryJson.ToJObject();

            //关键字
            if (!queryParam["keyword"].IsEmpty())
            {
                string keyword = queryParam["keyword"].ToString();//姓名
                int ikeyword = queryParam["keyword"].ToInt();//编号
                if (ikeyword != 0)
                {
                    expression = expression.And(t => t.Id == ikeyword);
                }
                else
                {
                    expression = expression.And(t => t.FullName.Contains(keyword));
                }

            }
            //报名日期
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                expression = expression.And(t => t.CreateDate >= startTime && t.CreateDate <= endTime);
            }
            //编号
            if (!queryParam["Id"].IsEmpty())
            {
                int Id = queryParam["Id"].ToInt();
                expression = expression.And(t => t.Id== Id);
            }
            //姓名
            if (!queryParam["FullName"].IsEmpty())
            {
                string FullName = queryParam["FullName"].ToString();
                expression = expression.And(t => t.FullName.Contains(FullName));
            }
            //微信id
            if (!queryParam["OpenId"].IsEmpty())
            {
                string OpenId = queryParam["OpenId"].ToString();
                expression = expression.And(t => t.OpenId== OpenId);
            }
            //分组
            if (!queryParam["GroupId"].IsEmpty())
            {
                int GroupId = queryParam["GroupId"].ToInt();
                expression = expression.And(t => t.GroupId == GroupId);
            }
            //审核状态
            if (!queryParam["CheckMark"].IsEmpty())
            {
                int CheckMark = queryParam["CheckMark"].ToInt();
                expression = expression.And(t => t.CheckMark == CheckMark);
            }
            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<Poll_SignUpEntity> GetList(string queryJson)
        {
            var expression = LinqExtensions.True<Poll_SignUpEntity>();
            var queryParam = queryJson.ToJObject();
            //报名日期
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                expression = expression.And(t => t.CreateDate >= startTime && t.CreateDate <= endTime);
            }
            //编号
            if (!queryParam["Id"].IsEmpty())
            {
                int Id = queryParam["Id"].ToInt();
                expression = expression.And(t => t.Id == Id);
            }
            //姓名
            if (!queryParam["FullName"].IsEmpty())
            {
                string FullName = queryParam["FullName"].ToString();
                expression = expression.And(t => t.FullName.Contains(FullName));
            }
            //关键字
            if (!queryParam["keyword"].IsEmpty())
            {
                string keyword = queryParam["keyword"].ToString();//姓名
                int ikeyword = queryParam["keyword"].ToInt();//编号
                if (ikeyword != 0)
                {
                    expression = expression.And(t => t.Id == ikeyword);
                }
                else
                {
                    expression = expression.And(t => t.FullName.Contains(keyword));
                }
                
            }
            //微信id
            if (!queryParam["OpenId"].IsEmpty())
            {
                string OpenId = queryParam["OpenId"].ToString();
                expression = expression.And(t => t.OpenId == OpenId);
            }
            //分组
            if (!queryParam["GroupId"].IsEmpty())
            {
                int GroupId = queryParam["GroupId"].ToInt();
                expression = expression.And(t => t.GroupId == GroupId);
            }
            //审核状态
            if (!queryParam["CheckMark"].IsEmpty())
            {
                int CheckMark = queryParam["CheckMark"].ToInt();
                expression = expression.And(t => t.CheckMark == CheckMark);
            }
            return this.BaseRepository().IQueryable(expression);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Poll_SignUpEntity GetEntity(int? keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(int? keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(int? keyValue, Poll_SignUpEntity entity)
        {
            if (keyValue != null)
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                entity.Create();
                this.BaseRepository().Insert(entity);
            }
        }

        /// <summary>
        /// 核单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="State">状态：1-启动；0-禁用</param>
        public void UpdateCheckState(int? keyValue, int State)
        {
            Poll_SignUpEntity reserveEntity = new Poll_SignUpEntity();
            reserveEntity.Modify(keyValue);
            reserveEntity.CheckMark = State;
            this.BaseRepository().Update(reserveEntity);
        }

        /// <summary>
        /// 作废
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="State">状态：1-启动；0-禁用</param>
        public void UpdateDeleteState(int? keyValue, int State)
        {
            Poll_SignUpEntity reserveEntity = new Poll_SignUpEntity();
            reserveEntity.Modify(keyValue);
            reserveEntity.DeleteMark = State;
            this.BaseRepository().Update(reserveEntity);
        }
        #endregion
    }
}
