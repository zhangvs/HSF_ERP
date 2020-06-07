using HZSoft.Application.Entity.BaseManage;
using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.IService.BaseManage;
using HZSoft.Application.IService.CustomerManage;
using HZSoft.Data.Repository;
using HZSoft.Util;
using HZSoft.Util.Extension;
using HZSoft.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
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
            if (!queryParam["ParentId"].IsEmpty())
            {
                string ParentId = queryParam["ParentId"].ToString();
                strSql += " and ParentId = '" + ParentId + "'";
            }
            else
            {
                strSql += " and ParentId <> '0'";
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
            if (!queryParam["ParentId"].IsEmpty())
            {
                string ParentId = queryParam["ParentId"].ToString();
                strSql += " and ParentId = '" + ParentId + "'";
            }
            else
            {
                strSql += " and ParentId <> '0'";
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
            expression = expression.And(t => t.Code == Value).And(t => t.Id == Id);
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
            expression = expression.And(t => t.Name == Name).And(t => t.Id == Id);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.Id != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        #endregion

        /// <summary>
        /// ������������
        /// </summary>
        /// <param name="dtSource">ʵ�����</param>
        /// <returns></returns>
        public string BatchAddEntity(DataTable dtSource)
        {
            int rowsCount = dtSource.Rows.Count;
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();

            int columns = dtSource.Columns.Count;
            string cf = "";
            for (int i = 0; i < rowsCount; i++)
            {
                try
                {
                    string Code = dtSource.Rows[i][0].ToString();
                    if (Code.Length == 11)
                    {
                        var liang_Data = db.FindEntity<DZ_ProductEntity>(t => t.Code == Code && t.DeleteMark != 1);//ɾ�����Ŀ����ٴε���
                        if (liang_Data != null)
                        {
                            cf += Code + ",";
                        }
                        //����
                        string Name = dtSource.Rows[i][1].ToString();
                        if (string.IsNullOrEmpty(Name))
                        {
                            return Code + "����Ϊ��";
                        }

                        //���
                        string Kind = dtSource.Rows[i][2].ToString();
                        //���
                        string Guige = dtSource.Rows[i][3].ToString();
                        //���
                        string itemName = dtSource.Rows[i][4].ToString();
                        //��λ
                        string Unit = dtSource.Rows[i][5].ToString();
                        //����1
                        string Plan1 = dtSource.Rows[i][6].ToString();
                        //����2
                        string Plan2 = dtSource.Rows[i][6].ToString();
                        //����3
                        string Plan3 = dtSource.Rows[i][6].ToString();
                        //����4
                        string Plan4 = dtSource.Rows[i][6].ToString();

                        //�������
                        DZ_ProductEntity entity = new DZ_ProductEntity()
                        {
                            Name = Name,
                            Code = Code,
                            Kind = Kind,
                            Guige = Guige,
                            Unit = Unit,
                            Plan1 = Convert.ToDecimal(Plan1),
                            Plan2 = Convert.ToDecimal(Plan2),
                            Plan3 = Convert.ToDecimal(Plan3),
                            Plan4 = Convert.ToDecimal(Plan4),
                        };
                        entity.Create();
                        db.Insert(entity);
                    }

                }
                catch (Exception ex)
                {
                    LogHelper.AddLog(ex.Message);
                    return ex.Message;
                }

            }
            db.Commit();
            if (cf != "")
            {
                LogHelper.AddLog("�����ظ����룺" + cf);
                return "�����ظ����룺" + cf;
            }
            else
            {
                return "����ɹ�";
            }

        }
    }
}
