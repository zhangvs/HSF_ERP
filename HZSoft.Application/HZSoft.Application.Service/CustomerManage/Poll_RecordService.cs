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
    /// �� �� 6.1
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2018-10-24 16:44
    /// �� ����ͶƱ��¼
    /// </summary>
    public class Poll_RecordService : RepositoryFactory<Poll_RecordEntity>, Poll_RecordIService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<Poll_RecordEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<Poll_RecordEntity>();
            var queryParam = queryJson.ToJObject();
            //�ؼ���
            if (!queryParam["keyword"].IsEmpty())
            {
                string keyword = queryParam["keyword"].ToString();//����
                int ikeyword = queryParam["keyword"].ToInt();//���
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
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<Poll_RecordEntity> GetList(string queryJson)
        {
            var expression = LinqExtensions.True<Poll_RecordEntity>();
            var queryParam = queryJson.ToJObject();
            //΢��id
            if (!queryParam["OpenId"].IsEmpty())
            {
                string OpenId = queryParam["OpenId"].ToString();
                expression = expression.And(t => t.OpenId == OpenId);
            }
            //һ��ͶƱһ��
            DateTime startTime = DateTime.Now.Date;
            DateTime endTime = DateTime.Now.Date.AddDays(1);
            expression = expression.And(t => t.CreateDate >= startTime && t.CreateDate <= endTime);

            return this.BaseRepository().IQueryable(expression);
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public Poll_RecordEntity GetEntity(int? keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����</param>
        public void RemoveForm(int? keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
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
                //ͶƱ��+1
                IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
                Poll_SignUpEntity poll_SignUpEntity = db.FindEntity<Poll_SignUpEntity>(entity.PlayerId);
                poll_SignUpEntity.PollCount= poll_SignUpEntity.PollCount+1;
                db.Update(poll_SignUpEntity);
                db.Commit();

                //����ͶƱ��¼
                entity.Create();
                this.BaseRepository().Insert(entity);

            }
        }
        #endregion
    }
}
