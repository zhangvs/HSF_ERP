using HZSoft.Application.Code;
using HZSoft.Application.Entity.BaseManage;
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
using System.Linq.Expressions;

namespace HZSoft.Application.Service.CustomerManage
{
    /// <summary>
    /// �� �� 6.1
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2019-11-17 08:11
    /// �� ����DZ_Order
    /// </summary>
    public class DZ_OrderService : RepositoryFactory<DZ_OrderEntity>, DZ_OrderIService
    {
        private ICodeRuleService coderuleService = new CodeRuleService();

        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<DZ_OrderEntity> GetPageList(Pagination pagination, string queryJson)
        {
            string strSql = $"select * from DZ_Order where DeleteMark=0 ";
            var expression = LinqExtensions.True<DZ_OrderEntity>();
            var queryParam = queryJson.ToJObject();
            //��������
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and CreateDate >= '" + startTime + "' and CreateDate < '" + endTime + "'";
            }
            //��˾��
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "Code":              //�ͻ����
                        strSql += " and Code  like '%" + keyword + "%' ";
                        break;
                    case "CompanyName":            //������
                        strSql += " and CompanyName  like '%" + keyword + "%' ";
                        break;
                    case "CustomerName":            //�ͻ�
                        strSql += " and CustomerName  like '%" + keyword + "%' ";
                        break;
                    case "CheckTuUserName":             //��ͼ��
                        strSql += " and CheckTuUserName  like '%" + keyword + "%' ";
                        break;
                    case "ChaiUserName":       //����
                        strSql += " and ChaiUserName  like '%" + keyword + "%' ";
                        break;
                    case "CheckUserName":       //�����
                        strSql += " and CheckUserName  like '%" + keyword + "%' ";
                        break;
                    default:
                        break;
                }
            }
            //���۱��
            if (!queryParam["Code"].IsEmpty())
            {
                string Code = queryParam["Code"].ToString();
                strSql += " and Code like '%" + Code + "%'";
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
            //��ͼ��
            if (!queryParam["CheckTuUserName"].IsEmpty())
            {
                string CheckTuUserName = queryParam["CheckTuUserName"].ToString();
                strSql += " and CheckTuUserName like '%" + CheckTuUserName + "%'";
            }
            //����
            if (!queryParam["ChaiUserName"].IsEmpty())
            {
                string ChaiUserName = queryParam["ChaiUserName"].ToString();
                strSql += " and ChaiUserName like '%" + ChaiUserName + "%'";
            }
            //�����
            if (!queryParam["CheckUserName"].IsEmpty())
            {
                string CheckUserName = queryParam["CheckUserName"].ToString();
                strSql += " and CheckUserName like '%" + CheckUserName + "%'";
            }

            //���
            if (!queryParam["CheckMark"].IsEmpty())
            {
                int CheckMark = queryParam["CheckMark"].ToInt();
                strSql += " and CheckMark  = " + CheckMark ;
            }
            //��ͼ
            if (!queryParam["CheckTuMark"].IsEmpty())
            {
                int CheckTuMark = queryParam["CheckTuMark"].ToInt();
                if (CheckTuMark == 1)
                {
                    strSql += " and (CheckTuMark  = 1 or CheckTuMark  = 2) ";
                }
                else
                {
                    strSql += " and CheckTuMark  = " + CheckTuMark;
                }
            }
            //��ͼ
            if (!queryParam["ChaiMark"].IsEmpty())
            {
                int ChaiMark = queryParam["ChaiMark"].ToInt();
                strSql += " and ChaiMark  = " + ChaiMark;
            }
            //����
            if (!queryParam["MoneyMark"].IsEmpty())
            {
                int MoneyMark = queryParam["MoneyMark"].ToInt();
                strSql += " and MoneyMark  = " + MoneyMark;
            }
            //�������
            if (!queryParam["MoneyOkMark"].IsEmpty())
            {
                int MoneyOkMark = queryParam["MoneyOkMark"].ToInt();
                strSql += " and MoneyOkMark  = " + MoneyOkMark;
            }
            //�µ�
            if (!queryParam["DownMark"].IsEmpty())
            {
                int DownMark = queryParam["DownMark"].ToInt();
                strSql += " and DownMark  = " + DownMark;
            }
            //δ����
            if (!queryParam["NoMoneyMark"].IsEmpty())
            {
                int noMoneyMark = queryParam["NoMoneyMark"].ToInt();
                //noMoneyMark = noMoneyMark == 1 ? 0 : 1;
                //ѡ����δ���۰�ť
                if (noMoneyMark==1)
                {
                    strSql += " and MoneyMark  != 1";
                }
            }
            //δ�µ�
            if (!queryParam["NoDownMark"].IsEmpty())
            {
                int noDownMark = queryParam["NoDownMark"].ToInt();
                if (noDownMark == 1)
                {
                    strSql += " and DownMark  != 1";
                }
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
            //���
            if (!queryParam["EnterMark"].IsEmpty())
            {
                int EnterMark = queryParam["EnterMark"].ToInt();
                strSql += " and EnterMark  = " + EnterMark;
            }
            //����
            if (!queryParam["SendMark"].IsEmpty())
            {
                int SendMark = queryParam["SendMark"].ToInt();
                strSql += " and SendMark  = " + SendMark;
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
        public IEnumerable<DZ_OrderEntity> GetList(string queryJson)
        {
            string strSql = $"select * from DZ_Order where DeleteMark=0 ";
            var expression = LinqExtensions.True<DZ_OrderEntity>();
            var queryParam = queryJson.ToJObject();
            //��������
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and CreateDate >= '" + startTime + "' and CreateDate < '" + endTime + "'";
            }
            //��˾��
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "Code":              //�ͻ����
                        strSql += " and Code  like '%" + keyword + "%' ";
                        break;
                    case "CompanyName":            //������
                        strSql += " and CompanyName  like '%" + keyword + "%' ";
                        break;
                    case "CustomerName":            //�ͻ�
                        strSql += " and CustomerName  like '%" + keyword + "%' ";
                        break;
                    case "CheckTuUserName":             //��ͼ��
                        strSql += " and CheckTuUserName  like '%" + keyword + "%' ";
                        break;
                    case "ChaiUserName":       //����
                        strSql += " and ChaiUserName  like '%" + keyword + "%' ";
                        break;
                    case "CheckUserName":       //�����
                        strSql += " and CheckUserName  like '%" + keyword + "%' ";
                        break;
                    default:
                        break;
                }
            }
            //���۱��
            if (!queryParam["Code"].IsEmpty())
            {
                string Code = queryParam["Code"].ToString();
                strSql += " and Code like '%" + Code + "%'";
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
            //��ͼ��
            if (!queryParam["CheckTuUserName"].IsEmpty())
            {
                string CheckTuUserName = queryParam["CheckTuUserName"].ToString();
                strSql += " and CheckTuUserName like '%" + CheckTuUserName + "%'";
            }
            //����
            if (!queryParam["ChaiUserName"].IsEmpty())
            {
                string ChaiUserName = queryParam["ChaiUserName"].ToString();
                strSql += " and ChaiUserName like '%" + ChaiUserName + "%'";
            }
            //�����
            if (!queryParam["CheckUserName"].IsEmpty())
            {
                string CheckUserName = queryParam["CheckUserName"].ToString();
                strSql += " and CheckUserName like '%" + CheckUserName + "%'";
            }

            //��ͼ
            if (!queryParam["CheckTuMark"].IsEmpty())
            {
                int CheckTuMark = queryParam["CheckTuMark"].ToInt();
                if (CheckTuMark == 1)
                {
                    strSql += " and (CheckTuMark  = 1 or CheckTuMark  = 2) ";
                }
                else
                {
                    strSql += " and CheckTuMark  = " + CheckTuMark;
                }
            }
            //���
            if (!queryParam["CheckMark"].IsEmpty())
            {
                int CheckMark = queryParam["CheckMark"].ToInt();
                strSql += " and CheckMark  = " + CheckMark;
            }
            //��ͼ
            if (!queryParam["ChaiMark"].IsEmpty())
            {
                int ChaiMark = queryParam["ChaiMark"].ToInt();
                strSql += " and ChaiMark  = " + ChaiMark;
            }
            //����
            if (!queryParam["MoneyMark"].IsEmpty())
            {
                int MoneyMark = queryParam["MoneyMark"].ToInt();
                strSql += " and MoneyMark  = " + MoneyMark;
            }
            //�µ�
            if (!queryParam["DownMark"].IsEmpty())
            {
                int DownMark = queryParam["DownMark"].ToInt();
                strSql += " and DownMark  = " + DownMark;
            }
            //δ����
            if (!queryParam["NoMoneyMark"].IsEmpty())
            {
                int noMoneyMark = queryParam["NoMoneyMark"].ToInt();
                //noMoneyMark = noMoneyMark == 1 ? 0 : 1;
                //ѡ����δ���۰�ť
                if (noMoneyMark == 1)
                {
                    strSql += " and MoneyMark  != 1";
                }
            }
            //δ�µ�
            if (!queryParam["NoDownMark"].IsEmpty())
            {
                int noDownMark = queryParam["NoDownMark"].ToInt();
                if (noDownMark == 1)
                {
                    strSql += " and DownMark  != 1";
                }
            }

            //֧��״̬
            if (!queryParam["PaymentState"].IsEmpty())
            {
                int PaymentState = queryParam["PaymentState"].ToInt();
                if (PaymentState==2)
                {
                    strSql += " and PaymentState >= 2 ";
                }
                else
                {
                    strSql += " and PaymentState  = " + PaymentState;
                }
                
            }
            //���
            if (!queryParam["EnterMark"].IsEmpty())
            {
                int EnterMark = queryParam["EnterMark"].ToInt();
                strSql += " and EnterMark  = " + EnterMark;
            }
            //����
            if (!queryParam["SendMark"].IsEmpty())
            {
                int SendMark = queryParam["SendMark"].ToInt();
                strSql += " and SendMark  = " + SendMark;
            }
            return this.BaseRepository().FindList(strSql.ToString());
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public DZ_OrderEntity GetEntity(string keyValue)
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
            DZ_OrderEntity entity = GetEntity(keyValue);
            entity.Modify(keyValue);
            entity.DeleteMark = 1;
            this.BaseRepository().Update(entity);

            //this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, DZ_OrderEntity entity)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    //���õ��ϵ����۵�״̬
                    DZ_OrderEntity oldEntity = GetEntity(keyValue);

                    //��ͼͨ��֮�󣬸����˷���Ϣ����
                    if (entity.CheckTuMark >0 && oldEntity.CheckTuMark == 0)
                    {
                        //��΢��ģ����Ϣ---�ӵ�֮�󣬸���ͼ������--���oA-EC1X6RWfW1_DNJ_VNiA3uhOYg
                        //��������֪ͨ�������ѣ�
                        TemplateWxApp.SendTemplateNew("oA-EC1X6RWfW1_DNJ_VNiA3uhOYg","���ã����µĶ�����Ҫ��!", entity.OrderTitle, entity.Code, "����в𵥡�");
                    }


                    //����֮�󣬸�������Ϣ����
                    if (entity.MoneyMark == 1 && oldEntity.MoneyMark == 0)
                    {
                        //��΢��ģ����Ϣ---�з�����֮�󣬸���������--��һ��oA-EC1X0OoVmzyowOqxYHlY5NHX4
                        //��������֪ͨ���������ѣ�
                        TemplateWxApp.SendTemplateMoney("oA-EC1X0OoVmzyowOqxYHlY5NHX4", "���ã����µı�����Ҫ���!", "�з�����", entity.OrderTitle, entity.Code, "����б�����ˡ�");
                    }

                    entity.Modify(keyValue);
                    this.BaseRepository().Update(entity);//Ҫ����oldEntity�����޸Ĳſ��ԣ�����oldEntity��entity����һ������
                }
                else
                {
                    entity.Create();
                    this.BaseRepository().Insert(entity);
                    coderuleService.UseRuleSeed(entity.CreateUserId, "", ((int)CodeRuleEnum.DZ_Order).ToString(), db);//ռ�õ��ݺ�
                    db.Commit();

                    //��΢��ģ����Ϣ---�ӵ�֮�󣬸���ͼ������--������oA-EC1WVqHl_gsBM3We2rgOHIMEQ
                    //��������֪ͨ����ͼ���ѣ�
                    TemplateWxApp.SendTemplateNew("oA-EC1WVqHl_gsBM3We2rgOHIMEQ", "���ã����µĶ�����Ҫ��ͼ!", entity.OrderTitle, entity.Code, "�������ͼ��");
                }
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }


        /// <summary>
        /// ���񱨼����
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public void UpdateMoneyOkState(string keyValue, int? state)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    DZ_OrderEntity entity = new DZ_OrderEntity()
                    {
                        MoneyOkMark = state,
                        MoneyOkDate = DateTime.Now,
                        MoneyOkUserId = OperatorProvider.Provider.Current().UserId,
                        MoneyOkUserName = OperatorProvider.Provider.Current().UserName
                    };
                    entity.Modify(keyValue);
                    this.BaseRepository().Update(entity);

                    DZ_OrderEntity oldEntity = GetEntity(keyValue);
                    //��΢��ģ����Ϣ������������ѣ�
                    if (!string.IsNullOrEmpty(oldEntity.CreateUserName))
                    {
                        var hsf_CardList = db.IQueryable<Hsf_CardEntity>(t => t.Name.Equals(oldEntity.CreateUserName));//���͸������������ˣ��곤�����Ա���������Ե곤�ܿ����𵥱���
                        if (hsf_CardList.Count() != 0)
                        {
                            var hsf_CardEntity = hsf_CardList.First();
                            //��ֱ�Ӹ�����Ա���ۣ�ֻ�е곤����֪������
                            string backMsg = TemplateWxApp.SendTemplateMoneyOk(hsf_CardEntity.OpenId, "���ã����Ķ���������������!", oldEntity.Code, oldEntity.OrderTitle, entity.MoneyAccounts.ToString(), "");
                            if (backMsg != "ok")
                            {
                                //ҵ��Աû�й�ע���ںţ�����΢��Post���������󣡴�����룺43004��˵����require subscribe hint: [ziWtva03011295]
                                LogHelper.AddLog(entity.SalesmanUserName + "û�й�ע���ں�");//��¼��־
                            }
                        }
                    }


                    //������˸ı��������������״̬
                    Sale_CustomerEntity sale_CustomerEntity = db.FindEntity<Sale_CustomerEntity>(t => t.ProduceId == oldEntity.Code);//�����۵�code������������id
                    if (sale_CustomerEntity != null)
                    {
                        //���������ڵĻ���˵���Ѿ��տ��
                        sale_CustomerEntity.MoneyOkMark = state;
                        sale_CustomerEntity.MoneyOkDate = DateTime.Now;
                        db.Update<Sale_CustomerEntity>(sale_CustomerEntity);

                        //�տ�+�������=�����µ�

                        //��΢��ģ����Ϣ---�����Ѿ�������˲��տ�ȷ��֮�󣬸��ű�������Ϣ����oA-EC1bJnd0KFBuOy0joJvUOGwwk
                        //��������֪ͨ��7�µ����ѣ�
                        TemplateWxApp.SendTemplateNew("oA-EC1bJnd0KFBuOy0joJvUOGwwk",
                            "���ã����µĶ�����Ҫ�µ�!", oldEntity.OrderTitle, oldEntity.Code, "����������µ���");
                    }
                    else
                    {
                        //�������Ԥ������һ�û�µ��ģ�����������֮�����ֱ�Ӵ�������������ʼ�����µ�
                        if (oldEntity.FrontMark == 0 && oldEntity.DownMark != 1)
                        {
                            //�Զ�����������������������*****************
                            Sale_Customer_Main.SaveSaleMain(db, oldEntity);//����µ�����ʱ�������ظ�����
                        }
                    }
                    db.Commit();//�������
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// ǩ��ȷ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        public void SaveSigned(string keyValue, DZ_OrderEntity entity)
        {
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.Modify(keyValue);
                    entity.SignedMark = 1;
                    entity.SignedDate = DateTime.Now;
                    entity.SignedUserId = OperatorProvider.Provider.Current().UserId;
                    entity.SignedUserName = OperatorProvider.Provider.Current().UserName;
                    this.BaseRepository().Update(entity);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }




        /// <summary>
        /// �ᵥ
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public void UpdateOverState(string keyValue, int? state)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    DZ_OrderEntity entity = new DZ_OrderEntity()
                    {
                        OverMark = state
                    };
                    entity.Modify(keyValue);
                    this.BaseRepository().Update(entity);


                    //���������״̬
                    Sale_CustomerEntity sale_CustomerEntity = db.FindEntity<Sale_CustomerEntity>(t => t.OrderId == keyValue);
                    if (sale_CustomerEntity != null)
                    {
                        sale_CustomerEntity.OverMark = state;
                        db.Update<Sale_CustomerEntity>(sale_CustomerEntity);
                        db.Commit();
                    }

                    //��ⵥ���״̬
                    Buys_OrderEntity buysEntity = db.FindEntity<Buys_OrderEntity>(t => t.OrderId == keyValue);
                    if (buysEntity != null)
                    {
                        buysEntity.OverMark = state;
                        db.Update<Buys_OrderEntity>(buysEntity);
                        db.Commit();
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        #endregion
    }
}
