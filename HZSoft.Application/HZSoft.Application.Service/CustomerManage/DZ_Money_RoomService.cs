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
    /// �� �� 6.1
    /// 
    /// �� ��������
    /// �� �ڣ�2020-06-11 11:28
    /// �� �������۷���
    /// </summary>
    public class DZ_Money_RoomService : RepositoryFactory, DZ_Money_RoomIService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<DZ_Money_RoomEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return this.BaseRepository().FindList<DZ_Money_RoomEntity>(pagination);
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public DZ_Money_RoomEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<DZ_Money_RoomEntity>(keyValue);
        }
        /// <summary>
        /// ��ȡ�ӱ���ϸ��Ϣ
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public IEnumerable<DZ_Money_RoomEntity> GetRoomDetails(string keyValue)
        {
            return this.BaseRepository().FindList<DZ_Money_RoomEntity>("select * from DZ_Money_Room where OrderId='" + keyValue + "' ORDER BY createdate");
        }
        /// <summary>
        /// ��ȡ�ӱ���ϸ��Ϣ
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public IEnumerable<DZ_Money_ItemEntity> GetRoomItemDetails(string keyValue)
        {
            return this.BaseRepository().FindList<DZ_Money_ItemEntity>("select * from DZ_Money_Item where OrderId='" + keyValue + "' ORDER BY createdate");
        }
        /// <summary>
        /// ��ȡ�ӱ���ϸ��Ϣ
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public IEnumerable<DZ_Money_ItemEntity> GetDetails(string keyValue)
        {
            return this.BaseRepository().FindList<DZ_Money_ItemEntity>("select * from DZ_Money_Item where RoomId='"+keyValue+ "' ORDER BY createdate");        }
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����</param>
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
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, DZ_Money_RoomEntity entity,List<DZ_Money_ItemEntity> entryList)
        {
        IRepository db = this.BaseRepository().BeginTrans();
        try
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                //����
                entity.Modify(keyValue);
                db.Update(entity);
                //��ϸ
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
                //����
                entity.Create();
                db.Insert(entity);
                //��ϸ
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
