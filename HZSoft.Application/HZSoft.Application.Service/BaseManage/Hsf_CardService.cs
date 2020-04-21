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
    /// �� �� 6.1
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2019-01-28 14:42
    /// �� ����������Ƭ
    /// </summary>
    public class Hsf_CardService : RepositoryFactory<Hsf_CardEntity>, Hsf_CardIService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<Hsf_CardEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<Hsf_CardEntity>();
            expression = expression.And(t => t.DeleteMark != 1);
            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
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
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public Hsf_CardEntity GetEntity(string keyValue)
        {
            var expression = LinqExtensions.True<Hsf_CardEntity>();
            expression = expression.And(t => t.Id == keyValue);
            return this.BaseRepository().FindEntity(expression);
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="OpenId">����ֵ</param>
        /// <returns></returns>
        public Hsf_CardEntity GetEntityByOpenId(string OpenId)
        {
            var expression = LinqExtensions.True<Hsf_CardEntity>();
            expression = expression.And(t => t.OpenId == OpenId);
            return this.BaseRepository().FindEntity(expression);
        }
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
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
