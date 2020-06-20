using HZSoft.Application.Code;
using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.IService.CustomerManage;
using HZSoft.Application.IService.SystemManage;
using HZSoft.Application.Service.SystemManage;
using HZSoft.Data.Repository;
using HZSoft.Util;
using HZSoft.Util.Extension;
using HZSoft.Util.WebControl;
using HZSoft.Util.WeChat.Comm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HZSoft.Application.Service.CustomerManage
{
    /// <summary>
    /// �� �� 6.1
    /// 
    /// �� ��������
    /// �� �ڣ�2020-05-18 09:28
    /// �� �������ߵ�
    /// </summary>
    public class DZSH_OrderService : RepositoryFactory<DZSH_OrderEntity>, DZSH_OrderIService
    {
        private ICodeRuleService coderuleService = new CodeRuleService();
        private DZ_OrderIService dzService = new DZ_OrderService();
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<DZSH_OrderEntity> GetPageList(Pagination pagination, string queryJson)
        {
            string strSql = $"select * from DZSH_Order where DeleteMark=0 ";
            var expression = LinqExtensions.True<DZSH_OrderEntity>();
            var queryParam = queryJson.ToJObject();
            //��������
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and CreateDate >= '" + startTime + "' and CreateDate < '" + endTime + "'";
            }
            //���۱��
            if (!queryParam["Code"].IsEmpty())
            {
                string Code = queryParam["Code"].ToString();
                strSql += " and Code like '%" + Code + "%'";
            }
            //����ģ������
            if (!queryParam["OrderTitle"].IsEmpty())
            {
                string OrderTitle = queryParam["OrderTitle"].ToString();
                strSql += " and OrderTitle like '%" + OrderTitle + "%'";
            }

            //��˾��
            if (!queryParam["CompanyName"].IsEmpty())
            {
                string CompanyName = queryParam["CompanyName"].ToString();
                strSql += " and CompanyName like '%" + CompanyName + "%'";
            }
            //�ͻ���
            if (!queryParam["CustomerName"].IsEmpty())
            {
                string CustomerName = queryParam["CustomerName"].ToString();
                strSql += " and CustomerName like '%" + CustomerName + "%'";
            }
            //������
            if (!queryParam["SalesmanUserName"].IsEmpty())
            {
                string SalesmanUserName = queryParam["SalesmanUserName"].ToString();
                strSql += " and SalesmanUserName like '%" + SalesmanUserName + "%'";
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
            //֧��״̬
            if (!queryParam["PaymentState"].IsEmpty())
            {
                int PaymentState = queryParam["PaymentState"].ToInt();
                if (PaymentState == 2)
                {
                    strSql += " and PaymentState >= 2 ";
                }
                else
                {
                    strSql += " and PaymentState  = " + PaymentState;
                }

            }
            //����
            if (!queryParam["OverMark"].IsEmpty())
            {
                int OverMark = queryParam["OverMark"].ToInt();
                strSql += " and OverMark  = " + OverMark;
            }
            return this.BaseRepository().FindList(strSql.ToString(), pagination);
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<DZSH_OrderEntity> GetList(string queryJson)
        {
            string strSql = $"select * from DZSH_Order where DeleteMark=0 ";
            var expression = LinqExtensions.True<DZSH_OrderEntity>();
            var queryParam = queryJson.ToJObject();
            //��������
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and CreateDate >= '" + startTime + "' and CreateDate < '" + endTime + "'";
            }
            //���۱��
            if (!queryParam["Code"].IsEmpty())
            {
                string Code = queryParam["Code"].ToString();
                strSql += " and Code like '%" + Code + "%'";
            }
            //����ģ������
            if (!queryParam["OrderTitle"].IsEmpty())
            {
                string OrderTitle = queryParam["OrderTitle"].ToString();
                strSql += " and OrderTitle like '%" + OrderTitle + "%'";
            }

            //��˾��
            if (!queryParam["CompanyName"].IsEmpty())
            {
                string CompanyName = queryParam["CompanyName"].ToString();
                strSql += " and CompanyName like '%" + CompanyName + "%'";
            }
            //�ͻ���
            if (!queryParam["CustomerName"].IsEmpty())
            {
                string CustomerName = queryParam["CustomerName"].ToString();
                strSql += " and CustomerName like '%" + CustomerName + "%'";
            }
            //������
            if (!queryParam["SalesmanUserName"].IsEmpty())
            {
                string SalesmanUserName = queryParam["SalesmanUserName"].ToString();
                strSql += " and SalesmanUserName like '%" + SalesmanUserName + "%'";
            }

            //֧��״̬
            if (!queryParam["PaymentState"].IsEmpty())
            {
                int PaymentState = queryParam["PaymentState"].ToInt();
                if (PaymentState == 2)
                {
                    strSql += " and PaymentState >= 2 ";
                }
                else
                {
                    strSql += " and PaymentState  = " + PaymentState;
                }

            }
            return this.BaseRepository().FindList(strSql.ToString());
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public DZSH_OrderEntity GetEntity(string keyValue)
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
        public void SaveForm(string keyValue, DZSH_OrderEntity entity)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    //���õ��ϵĿ��ߵ�
                    DZSH_OrderEntity oldEntity = GetEntity(keyValue);

                    if (entity.NeedSale == 1 && oldEntity.NeedSale == 0)
                    {
                        //��Ϊ����֮�󣬴������۵�
                        CreateDZOrder.SaveDZOrder(db, entity);
                    }
                    else
                    {
                        //���õ��ϵ����۵�
                        DZ_OrderEntity oldDZEntity = dzService.GetEntity(keyValue);
                        if (oldDZEntity!=null)
                        {
                            //����ϴ����ļ�·���仯���ж����۵��е���ͼ״̬�Ƿ��Ѿ���ͼ���Ѿ���ͼ�������޸��ļ�����Ҫ�з����أ�
                            if (entity.CreatePath!=oldEntity.CreatePath && oldDZEntity.CheckTuMark == 1)
                            {
                                throw new Exception("����ϵ�з���ͼ���أ��������ϴ��ļ���");
                            }
                            DZ_OrderEntity dzEntity = new DZ_OrderEntity
                            {
                                OrderTitle = entity.OrderTitle,
                                OrderType = entity.OrderType,
                                CompanyId = entity.CompanyId,
                                CompanyName = entity.CompanyName,
                                CustomerId = entity.CustomerId,
                                CustomerName = entity.CustomerName,
                                CustomerTelphone = entity.CustomerTelphone,
                                DesignerUserName = entity.DesignerUserName,
                                DesignerTelphone = entity.DesignerTelphone,
                                Address = entity.Address,
                                SalesmanUserId = entity.SalesmanUserId,
                                SalesmanUserName = entity.SalesmanUserName,
                                ShippingType = entity.ShippingType,
                                Carrier = entity.Carrier,
                                CreatePath = entity.CreatePath,
                                Description = entity.Description,
                            };
                            oldDZEntity.Modify(keyValue);
                            //ͬ�����۵�
                            db.Update<DZ_OrderEntity>(dzEntity);//�ص��ע����·���Ƿ�ͬ��
                        }
                    }

                    entity.Modify(keyValue);
                    //����
                    db.Update<DZSH_OrderEntity>(entity);
                    db.Commit();
                    RecordHelp.AddRecord(4, entity.Id, "���ߵ��༭");
                }
                else
                {
                    entity.Create();
                    db.Insert<DZSH_OrderEntity>(entity);
                    coderuleService.UseRuleSeed(entity.CreateUserId, "", ((int)CodeRuleEnum.DZSH_Order).ToString(), db);//ռ�õ��ݺ�
                    if (entity.NeedSale == 1)
                    {
                        //�����µ����۵�
                        CreateDZOrder.SaveDZOrder(db, entity);
                    }

                    db.Commit();
                    RecordHelp.AddRecord(4, entity.Id, "���߽ӵ�");
                    //��³³
                    TemplateWxApp.SendTemplateNew("oA-EC1aaKOSNdW2wL8lHSsr3R4Dg", "���ã����µĿ��ߵ�!", entity.OrderTitle, entity.Code, "����д���");

                    //��΢��ģ����Ϣ---���ߵ��ӵ�֮�󣬸���ͼ������--���oA-EC1X6RWfW1_DNJ_VNiA3uhOYg
                    //��������֪ͨ����ͼ���ѣ�
                    TemplateWxApp.SendTemplateNew("oA-EC1X6RWfW1_DNJ_VNiA3uhOYg", "���ã����µĶ�����Ҫ��ͼ!", entity.OrderTitle, entity.Code, "�������ͼ��");
                }
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
