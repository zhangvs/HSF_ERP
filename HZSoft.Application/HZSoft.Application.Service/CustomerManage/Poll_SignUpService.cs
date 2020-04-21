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
    /// �� �ڣ�2018-10-20 09:13
    /// �� ����������
    /// </summary>
    public class Poll_SignUpService : RepositoryFactory<Poll_SignUpEntity>, Poll_SignUpIService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<Poll_SignUpEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<Poll_SignUpEntity>();
            var queryParam = queryJson.ToJObject();

            //�ؼ���
            if (!queryParam["keyword"].IsEmpty())
            {
                string keyword = queryParam["keyword"].ToString();//����
                int ikeyword = queryParam["keyword"].ToInt();//���
                if (ikeyword != 0)
                {
                    expression = expression.And(t => t.Id == ikeyword);
                }
                else
                {
                    expression = expression.And(t => t.FullName.Contains(keyword));
                }

            }
            //��������
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                expression = expression.And(t => t.CreateDate >= startTime && t.CreateDate <= endTime);
            }
            //���
            if (!queryParam["Id"].IsEmpty())
            {
                int Id = queryParam["Id"].ToInt();
                expression = expression.And(t => t.Id== Id);
            }
            //����
            if (!queryParam["FullName"].IsEmpty())
            {
                string FullName = queryParam["FullName"].ToString();
                expression = expression.And(t => t.FullName.Contains(FullName));
            }
            //΢��id
            if (!queryParam["OpenId"].IsEmpty())
            {
                string OpenId = queryParam["OpenId"].ToString();
                expression = expression.And(t => t.OpenId== OpenId);
            }
            //����
            if (!queryParam["GroupId"].IsEmpty())
            {
                int GroupId = queryParam["GroupId"].ToInt();
                expression = expression.And(t => t.GroupId == GroupId);
            }
            //���״̬
            if (!queryParam["CheckMark"].IsEmpty())
            {
                int CheckMark = queryParam["CheckMark"].ToInt();
                expression = expression.And(t => t.CheckMark == CheckMark);
            }
            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<Poll_SignUpEntity> GetList(string queryJson)
        {
            var expression = LinqExtensions.True<Poll_SignUpEntity>();
            var queryParam = queryJson.ToJObject();
            //��������
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                expression = expression.And(t => t.CreateDate >= startTime && t.CreateDate <= endTime);
            }
            //���
            if (!queryParam["Id"].IsEmpty())
            {
                int Id = queryParam["Id"].ToInt();
                expression = expression.And(t => t.Id == Id);
            }
            //����
            if (!queryParam["FullName"].IsEmpty())
            {
                string FullName = queryParam["FullName"].ToString();
                expression = expression.And(t => t.FullName.Contains(FullName));
            }
            //�ؼ���
            if (!queryParam["keyword"].IsEmpty())
            {
                string keyword = queryParam["keyword"].ToString();//����
                int ikeyword = queryParam["keyword"].ToInt();//���
                if (ikeyword != 0)
                {
                    expression = expression.And(t => t.Id == ikeyword);
                }
                else
                {
                    expression = expression.And(t => t.FullName.Contains(keyword));
                }
                
            }
            //΢��id
            if (!queryParam["OpenId"].IsEmpty())
            {
                string OpenId = queryParam["OpenId"].ToString();
                expression = expression.And(t => t.OpenId == OpenId);
            }
            //����
            if (!queryParam["GroupId"].IsEmpty())
            {
                int GroupId = queryParam["GroupId"].ToInt();
                expression = expression.And(t => t.GroupId == GroupId);
            }
            //���״̬
            if (!queryParam["CheckMark"].IsEmpty())
            {
                int CheckMark = queryParam["CheckMark"].ToInt();
                expression = expression.And(t => t.CheckMark == CheckMark);
            }
            return this.BaseRepository().IQueryable(expression);
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public Poll_SignUpEntity GetEntity(int? keyValue)
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
        /// �˵�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="State">״̬��1-������0-����</param>
        public void UpdateCheckState(int? keyValue, int State)
        {
            Poll_SignUpEntity reserveEntity = new Poll_SignUpEntity();
            reserveEntity.Modify(keyValue);
            reserveEntity.CheckMark = State;
            this.BaseRepository().Update(reserveEntity);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="State">״̬��1-������0-����</param>
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
