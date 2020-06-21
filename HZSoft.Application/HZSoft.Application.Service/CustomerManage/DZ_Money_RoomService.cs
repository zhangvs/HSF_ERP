using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.IService.CustomerManage;
using HZSoft.Data.Repository;
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
    /// 日 期：2020-06-11 11:28
    /// 描 述：报价房间
    /// </summary>
    public class DZ_Money_RoomService : RepositoryFactory, DZ_Money_RoomIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<DZ_Money_RoomEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return this.BaseRepository().FindList<DZ_Money_RoomEntity>(pagination);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public DZ_Money_RoomEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<DZ_Money_RoomEntity>(keyValue);
        }
        /// <summary>
        /// 获取子表详细信息
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IEnumerable<DZ_Money_RoomEntity> GetRoomDetails(string keyValue)
        {
            return this.BaseRepository().FindList<DZ_Money_RoomEntity>("select * from DZ_Money_Room where OrderId='" + keyValue + "' ORDER BY createdate");
        }
        /// <summary>
        /// 获取子表详细信息
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IEnumerable<DZ_Money_ItemEntity> GetRoomItemDetails(string keyValue)
        {
            return this.BaseRepository().FindList<DZ_Money_ItemEntity>("select * from DZ_Money_Item where OrderId='" + keyValue + "' ORDER BY createdate");
        }
        /// <summary>
        /// 获取子表详细信息
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IEnumerable<DZ_Money_ItemEntity> GetDetails(string keyValue)
        {
            return this.BaseRepository().FindList<DZ_Money_ItemEntity>("select * from DZ_Money_Item where RoomId='"+keyValue+ "' ORDER BY createdate");        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                db.Delete<DZ_Money_RoomEntity>(keyValue);
                db.Delete<DZ_Money_ItemEntity>(t => t.RoomId.Equals(keyValue));
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, DZ_Money_RoomEntity entity,List<DZ_Money_ItemEntity> entryList)
        {
        IRepository db = this.BaseRepository().BeginTrans();
        try
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                //主表
                entity.Modify(keyValue);
                db.Update(entity);
                //明细
                db.Delete<DZ_Money_ItemEntity>(t => t.RoomId.Equals(keyValue));
                foreach (DZ_Money_ItemEntity item in entryList)
                {
                    item.Create();
                    item.RoomId = entity.RoomId;
                    db.Insert(item);
                }
            }
            else
            {
                //主表
                entity.Create();
                db.Insert(entity);
                //明细
                foreach (DZ_Money_ItemEntity item in entryList)
                {
                    item.Create();
                    item.RoomId = entity.RoomId;
                    db.Insert(item);
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
