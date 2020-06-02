using HZSoft.Application.Entity.BaseManage;
using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.IService.BaseManage;
using HZSoft.Application.IService.CustomerManage;
using HZSoft.Data.Repository;
using HZSoft.Util;
using HZSoft.Util.Extension;
using HZSoft.Util.WebControl;
using System.Collections.Generic;
using System.Linq;

namespace HZSoft.Application.Service.CustomerManage
{
    /// <summary>
    /// �� �� 6.1
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2017-12-18 13:59
    /// �� ������Ʒ����
    /// </summary>
    public class DZ_ProductService : RepositoryFactory<DZ_ProductEntity>, DZ_ProductIService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<DZ_ProductEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<DZ_ProductEntity>();
            var queryParam = queryJson.ToJObject();
            string strSql = "select * from DZ_Product where 1 = 1";

            //����id
            if (!queryParam["Id"].IsEmpty())
            {
                string Id = queryParam["Id"].ToString();
                strSql += " and Id = '" + Id + "'";
            }
            //������id
            if (!queryParam["PId"].IsEmpty())
            {
                string PId = queryParam["PId"].ToString();
                strSql += " and PId = '" + PId + "'";
            }
            else
            {
                strSql += " and PId <> '0'";
            }

            if (!queryParam["keyword"].IsEmpty())
            {
                string keyword = queryParam["keyword"].ToString();
                strSql += " and (Name like '%" + keyword + "%' or Code like '%" + keyword + "%')";
            }

            return this.BaseRepository().FindList(strSql.ToString(), pagination);
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<DZ_ProductEntity> GetList(string queryJson)
        {
            var expression = LinqExtensions.True<DZ_ProductEntity>();
            var queryParam = queryJson.ToJObject();
            string strSql = "select * from DZ_Product where 1 = 1";

            //����id
            if (!queryParam["Id"].IsEmpty())
            {
                string Id = queryParam["Id"].ToString();
                strSql += " and Id = '" + Id + "'";
            }
            //������id
            if (!queryParam["PId"].IsEmpty())
            {
                string PId = queryParam["PId"].ToString();
                strSql += " and PId = '" + PId + "'";
            }
            else
            {
                strSql += " and PId <> '0'";
            }

            if (!queryParam["keyword"].IsEmpty())
            {
                string keyword = queryParam["keyword"].ToString();
                strSql += " and (Name like '%" + keyword + "%' or Code like '%" + keyword + "%')";
            }

            return this.BaseRepository().FindList(strSql.ToString());
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <returns>�����б�</returns>
        public IEnumerable<DZ_ProductEntity> GetList()
        {
            return this.BaseRepository().IQueryable().ToList();
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public DZ_ProductEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
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
        public void SaveForm(string keyValue, DZ_ProductEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
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
        #endregion

        #region ��֤����
        /// <summary>
        /// ��Ŀֵ�����ظ�
        /// </summary>
        /// <param name="Value">��Ŀֵ</param>
        /// <param name="keyValue">����</param>
        /// <param name="Id">����Id</param>
        /// <returns></returns>
        public bool ExistValue(string Value, string keyValue, string Id)
        {
            var expression = LinqExtensions.True<DZ_ProductEntity>();
            expression = expression.And(t => t.Code == Value).And(t => t.PId == Id);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.Id != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        /// <summary>
        /// ��Ŀ�������ظ�
        /// </summary>
        /// <param name="Name">��Ŀ��</param>
        /// <param name="keyValue">����</param>
        /// <param name="Id">����Id</param>
        /// <returns></returns>
        public bool ExistName(string Name, string keyValue, string Id)
        {
            var expression = LinqExtensions.True<DZ_ProductEntity>();
            expression = expression.And(t => t.Name == Name).And(t => t.PId == Id);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.Id != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        #endregion
    }
}
