using HZSoft.Application.Code;
using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.IService.CustomerManage;
using HZSoft.Application.IService.SystemManage;
using HZSoft.Application.Service.SystemManage;
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
    /// �� ��������
    /// �� �ڣ�2020-03-15 11:09
    /// �� ����������
    /// </summary>
    public class Client_CompanyService : RepositoryFactory<Client_CompanyEntity>, Client_CompanyIService
    {
        private ICodeRuleService coderuleService = new CodeRuleService();
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<Client_CompanyEntity> GetPageList(Pagination pagination, string queryJson)
        {

            var queryParam = queryJson.ToJObject();
            string strSql = $"select * from Client_Company where (DeleteMark<>1 or DeleteMark IS NULL) ";
            //��������
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and CreateDate >= '" + startTime + "' and CreateDate < '" + endTime + "'";
            }
            //�ͻ����
            if (!queryParam["EnCode"].IsEmpty())
            {
                string EnCode = queryParam["EnCode"].ToString();
                strSql += " and EnCode like '%" + EnCode + "%'";
            }

            //�ͻ�����
            if (!queryParam["FullName"].IsEmpty())
            {
                string FullName = queryParam["FullName"].ToString();
                strSql += " and FullName like '%" + FullName + "%'";
            }
            //���ʦ
            if (!queryParam["Contact"].IsEmpty())
            {
                string Contact = queryParam["Contact"].ToString();
                strSql += " and Contact like '%" + Contact + "%'";
            }

            //�ͻ�����
            if (!queryParam["CustLevelId"].IsEmpty())
            {
                int CustLevelId = queryParam["CustLevelId"].ToInt();
                strSql += " and CustLevelId  = " + CustLevelId;
            }
            //�ͻ��̶�
            if (!queryParam["CustDegreeId"].IsEmpty())
            {
                int CustDegreeId = queryParam["CustDegreeId"].ToInt();
                strSql += " and CustDegreeId  = " + CustDegreeId;
            }

            //������
            if (!queryParam["CreateUserId"].IsEmpty())
            {
                string CreateUserId = queryParam["CreateUserId"].ToString();
                strSql += " and CreateUserId = '" + CreateUserId + "'";
            }
            else
            {
                if (!OperatorProvider.Provider.Current().IsSystem)
                {
                    string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
                    strSql += " and CreateUserId in (" + dataAutor + ")";
                }
            }
            return this.BaseRepository().FindList(strSql.ToString(), pagination);
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<Client_CompanyEntity> GetList(string queryJson)
        {
            string strSql = "select * from Client_Company where (DeleteMark<>1 or DeleteMark IS NULL)  ";
            string strOrder = " ORDER BY CreateDate desc";
            if (!OperatorProvider.Provider.Current().IsSystem)
            {
                string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
                strSql += " and CreateUserId in (" + dataAutor + ")";
            }

            strSql += strOrder;
            return this.BaseRepository().FindList(strSql.ToString());
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public Client_CompanyEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion


        #region ��֤����
        /// <summary>
        /// �ͻ����Ʋ����ظ�
        /// </summary>
        /// <param name="fullName">����</param>
        /// <param name="keyValue">����</param>
        /// <returns></returns>
        public bool ExistFullName(string fullName, string keyValue)
        {
            var expression = LinqExtensions.True<Client_CompanyEntity>();
            expression = expression.And(t => t.FullName == fullName);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.CompanyId != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        /// <summary>
        /// �ֻ������ظ�
        /// </summary>
        /// <param name="Mobile">����</param>
        /// <param name="keyValue">����</param>
        /// <returns></returns>
        public bool ExistMobile(string Mobile, string keyValue)
        {
            var expression = LinqExtensions.True<Client_CompanyEntity>();
            expression = expression.And(t => t.Mobile == Mobile);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.CompanyId != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
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
        public void SaveForm(string keyValue, Client_CompanyEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
                try
                {
                    entity.Create();
                    //���ָ��ģ����߱�ŵĵ��ݺ�
                    entity.EnCode = coderuleService.SetBillCode(entity.CreateUserId, SystemInfo.CurrentModuleId, "", db);
                    db.Insert(entity);
                    db.Commit();
                }
                catch (Exception)
                {
                    db.Rollback();
                    throw;
                }
            }
        }
        #endregion
    }
}
