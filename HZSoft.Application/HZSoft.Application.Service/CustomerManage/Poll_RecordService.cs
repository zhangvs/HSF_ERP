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
    /// 日 期：2018-10-24 16:44
    /// 描 述：投票记录
    /// </summary>
    public class Poll_RecordService : RepositoryFactory<Poll_RecordEntity>, Poll_RecordIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<Poll_RecordEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<Poll_RecordEntity>();
            var queryParam = queryJson.ToJObject();
            //关键字
            if (!queryParam["keyword"].IsEmpty())
            {
                string keyword = queryParam["keyword"].ToString();//姓名
                int ikeyword = queryParam["keyword"].ToInt();//编号
                if (ikeyword != 0)
                {
                    expression = expression.And(t => t.PlayerId == ikeyword);
                }
                else
                {
                    expression = expression.And(t => t.PlayerName.Contains(keyword));
                }

            }
            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<Poll_RecordEntity> GetList(string queryJson)
        {
            var expression = LinqExtensions.True<Poll_RecordEntity>();
            var queryParam = queryJson.ToJObject();
            //微信id
            if (!queryParam["OpenId"].IsEmpty())
            {
                string OpenId = queryParam["OpenId"].ToString();
                expression = expression.And(t => t.OpenId == OpenId);
            }
            //一天投票一次
            DateTime startTime = DateTime.Now.Date;
            DateTime endTime = DateTime.Now.Date.AddDays(1);
            expression = expression.And(t => t.CreateDate >= startTime && t.CreateDate <= endTime);

            return this.BaseRepository().IQueryable(expression);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Poll_RecordEntity GetEntity(int? keyValue)
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
        public void SaveForm(int? keyValue, Poll_RecordEntity entity)
        {
            if (keyValue!=null)
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                //投票数+1
                IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
                Poll_SignUpEntity poll_SignUpEntity = db.FindEntity<Poll_SignUpEntity>(entity.PlayerId);
                poll_SignUpEntity.PollCount= poll_SignUpEntity.PollCount+1;
                db.Update(poll_SignUpEntity);
                db.Commit();

                //保存投票记录
                entity.Create();
                this.BaseRepository().Insert(entity);

            }
        }
        #endregion
    }
}
