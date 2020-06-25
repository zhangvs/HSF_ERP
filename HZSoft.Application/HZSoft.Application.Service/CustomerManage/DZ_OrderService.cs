using HZSoft.Application.Code;
using HZSoft.Application.Entity.BaseManage;
using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.IService.BaseManage;
using HZSoft.Application.IService.CustomerManage;
using HZSoft.Application.IService.SystemManage;
using HZSoft.Application.Service.BaseManage;
using HZSoft.Application.Service.SystemManage;
using HZSoft.Data.Repository;
using HZSoft.Util;
using HZSoft.Util.Extension;
using HZSoft.Util.WebControl;
using HZSoft.Util.WeChat.Comm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

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
        private Hsf_CardIService cardService = new Hsf_CardService();
        //private Sale_CustomerIService saleService = new Sale_CustomerService();�������ظ�������ѭ��w3wp.exe 145000

        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<DZ_OrderEntity> GetPageList(Pagination pagination, string queryJson)
        {
            string strSql = $"select * from DZ_Order where 1=1";
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
            //����ģ������
            if (!queryParam["OrderTitle"].IsEmpty())
            {
                string OrderTitle = queryParam["OrderTitle"].ToString();
                strSql += " and OrderTitle like '%" + OrderTitle + "%'";
            }
            //��������
            if (!queryParam["OrderType"].IsEmpty())
            {
                string OrderType = queryParam["OrderType"].ToString();
                if (OrderType == "-3")//�ǿ��ߵ�
                {
                    strSql += " and OrderType <> 3";
                }
                else
                {
                    strSql += " and OrderType = " + OrderType;
                }
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
                if (CheckTuMark == 0)
                {
                    strSql += " and (CheckTuMark = 0 or CheckTuMark = -1 or ChaiMark = -1)";//1.��δ����ģ�2.����ǰһ���ģ�3.��һ�����ص�
                }
                else
                {
                    strSql += " and (CheckTuMark  = 1 or CheckTuMark  = 2) ";//�������
                }
            }
            //��
            if (!queryParam["ChaiMark"].IsEmpty())
            {
                int ChaiMark = queryParam["ChaiMark"].ToInt();
                if (ChaiMark == 0)
                {
                    strSql += " and (ChaiMark = 0 or ChaiMark = -1 or CheckMark = -1)";//1.��δ����ģ�2.����ǰһ���ģ�3.��һ�����ص�
                }
                else
                {
                    strSql += " and ChaiMark = 1 ";//�������
                }
            }
            //���
            if (!queryParam["CheckMark"].IsEmpty())
            {
                int CheckMark = queryParam["CheckMark"].ToInt();
                if (CheckMark == 0)
                {
                    strSql += " and (CheckMark = 0 or CheckMark = -1 or MoneyMark = -1)";//1.��δ����ģ�2.����ǰһ���ģ�3.��һ�����ص�
                }
                else
                {
                    strSql += " and CheckMark = 1 ";//�������
                }
            }
            //����
            if (!queryParam["MoneyMark"].IsEmpty())
            {
                int MoneyMark = queryParam["MoneyMark"].ToInt();
                if (MoneyMark == 0)
                {
                    strSql += " and (MoneyMark = 0 or MoneyMark = -1 or MoneyOkMark = -1)";//1.��δ����ģ�2.����ǰһ���ģ�3.��һ�����ص�
                }
                else
                {
                    strSql += " and MoneyMark = 1 ";//�������
                }
            }
            //�������
            if (!queryParam["MoneyOkMark"].IsEmpty())
            {
                int MoneyOkMark = queryParam["MoneyOkMark"].ToInt();
                if (MoneyOkMark == 0)
                {
                    strSql += " and (MoneyOkMark = 0 or MoneyOkMark = -1 or DownMark = -1)";//1.��δ����ģ�2.����ǰһ���ģ�3.��һ�����ص�
                }
                else
                {
                    strSql += " and MoneyOkMark = 1 ";//�������
                }
            }
            //�µ�
            if (!queryParam["DownMark"].IsEmpty())
            {
                int DownMark = queryParam["DownMark"].ToInt();
                if (DownMark == 0)
                {
                    strSql += " and (DownMark = 0 or DownMark = -1 or PushMark = -1)";//1.��δ����ģ�2.����ǰһ���ģ�3.��һ�����ص�
                }
                else
                {
                    strSql += " and DownMark = 1 ";//�������
                }
            }
            //�Ƶ�
            if (!queryParam["PushMark"].IsEmpty())
            {
                int PushMark = queryParam["PushMark"].ToInt();
                if (PushMark == 0)
                {
                    strSql += " and (PushMark = 0 or PushMark = -1)";//1.��δ����ģ�2.����ǰһ���ģ�3.��һ�����ص�
                }
                else
                {
                    strSql += " and PushMark = 1 ";//�������
                }
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
            //����֪ͨ
            if (!queryParam["SendMark"].IsEmpty())
            {
                int SendMark = queryParam["SendMark"].ToInt();
                strSql += " and SendMark  = " + SendMark;
            }
            //����
            if (!queryParam["SendOutMark"].IsEmpty())
            {
                int SendOutMark = queryParam["SendOutMark"].ToInt();
                strSql += " and SendOutMark  = " + SendOutMark;
            }
            //����
            if (!queryParam["OverMark"].IsEmpty())
            {
                int OverMark = queryParam["OverMark"].ToInt();
                strSql += " and OverMark  = " + OverMark;
            }
            //����
            if (!queryParam["DeleteMark"].IsEmpty())
            {
                int DeleteMark = queryParam["DeleteMark"].ToInt();
                strSql += " and DeleteMark  = " + DeleteMark;
            }
            else
            {
                strSql += " and DeleteMark  = 0";//Ĭ�ϲ�ɾ����
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

            //��ͼ
            if (!queryParam["CheckTuMark"].IsEmpty())
            {
                int CheckTuMark = queryParam["CheckTuMark"].ToInt();
                if (CheckTuMark == 0)
                {
                    strSql += " and (CheckTuMark = 0 or CheckTuMark = -1 or ChaiMark = -1)";//1.��δ����ģ�2.����ǰһ���ģ�3.��һ�����ص�
                }
                else
                {
                    strSql += " and (CheckTuMark  = 1 or CheckTuMark  = 2) ";//�������
                }
            }
            //��
            if (!queryParam["ChaiMark"].IsEmpty())
            {
                int ChaiMark = queryParam["ChaiMark"].ToInt();
                if (ChaiMark == 0)
                {
                    strSql += " and (ChaiMark = 0 or ChaiMark = -1 or CheckMark = -1)";//1.��δ����ģ�2.����ǰһ���ģ�3.��һ�����ص�
                }
                else
                {
                    strSql += " and ChaiMark = 1 ";//�������
                }
            }
            //���
            if (!queryParam["CheckMark"].IsEmpty())
            {
                int CheckMark = queryParam["CheckMark"].ToInt();
                if (CheckMark == 0)
                {
                    strSql += " and (CheckMark = 0 or CheckMark = -1 or MoneyMark = -1)";//1.��δ����ģ�2.����ǰһ���ģ�3.��һ�����ص�
                }
                else
                {
                    strSql += " and CheckMark = 1 ";//�������
                }
            }
            //����
            if (!queryParam["MoneyMark"].IsEmpty())
            {
                int MoneyMark = queryParam["MoneyMark"].ToInt();
                if (MoneyMark == 0)
                {
                    strSql += " and (MoneyMark = 0 or MoneyMark = -1 or MoneyOkMark = -1)";//1.��δ����ģ�2.����ǰһ���ģ�3.��һ�����ص�
                }
                else
                {
                    strSql += " and MoneyMark = 1 ";//�������
                }
            }
            //�������
            if (!queryParam["MoneyOkMark"].IsEmpty())
            {
                int MoneyOkMark = queryParam["MoneyOkMark"].ToInt();
                if (MoneyOkMark == 0)
                {
                    strSql += " and (MoneyOkMark = 0 or MoneyOkMark = -1 or DownMark = -1)";//1.��δ����ģ�2.����ǰһ���ģ�3.��һ�����ص�
                }
                else
                {
                    strSql += " and MoneyOkMark = 1 ";//�������
                }
            }
            //�µ�
            if (!queryParam["DownMark"].IsEmpty())
            {
                int DownMark = queryParam["DownMark"].ToInt();
                if (DownMark == 0)
                {
                    strSql += " and (DownMark = 0 or DownMark = -1 or PushMark = -1)";//1.��δ����ģ�2.����ǰһ���ģ�3.��һ�����ص�
                }
                else
                {
                    strSql += " and DownMark = 1 ";//�������
                }
            }
            //�Ƶ�
            if (!queryParam["PushMark"].IsEmpty())
            {
                int PushMark = queryParam["PushMark"].ToInt();
                if (PushMark == 0)
                {
                    strSql += " and (PushMark = 0 or PushMark = -1)";//1.��δ����ģ�2.����ǰһ���ģ�3.��һ�����ص�
                }
                else
                {
                    strSql += " and PushMark = 1 ";//�������
                }
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
        public void CardTempBack(string name, string code, string title, string reason, string desc)
        {
            var hsf_Card = cardService.GetEntityByName(name);//���͸������������ˣ��곤�����Ա���������Ե곤�ܿ����𵥱���
            if (hsf_Card != null)
            {
                //��ֱ�Ӹ�����Ա���ۣ�ֻ�е곤����֪������
                string backMsg = TemplateWxApp.SendTemplateBack(hsf_Card.OpenId, "���ã���������֪ͨ!", code, title, reason, desc + "����֪����");//������"+ OperatorProvider.Provider.Current().UserName
                if (backMsg != "ok")
                {
                    //ҵ��Աû�й�ע���ںţ�����΢��Post���������󣡴�����룺43004��˵����require subscribe hint: [ziWtva03011295]
                    LogHelper.AddLog(name + "û�й�ע���ں�");//��¼��־
                }
            }
            else
            {
                LogHelper.AddLog(name + "û�й�ע���ں�");//��¼��־
            }
        }

        /// <summary>
        /// һ������
        /// ���ã����Ķ����ѳ���
        /// �����ţ�123456
        /// �������ݣ��ܵ���ͨ
        /// ����ԭ�򣺺��д���
        /// ����ʱ�䣺2019��6��14��16:02
        /// ��л����ʹ��
        /// </summary>
        /// <param name="keyValue">����</param>
        /// <param name="entity">����ԭ��ҳ��</param>
        public void BackForm(string keyValue, DZ_OrderEntity entity)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();

            DZ_OrderEntity oldEntity = GetEntity(keyValue);
            if (oldEntity.PushMark > 0)
            {
                entity.PushMark = -1;//�Ƶ�����
                Sale_CustomerEntity sale_CustomerEntity = db.FindEntity<Sale_CustomerEntity>(t => t.ProduceId == oldEntity.Code);//�����۵�code������������id
                if (sale_CustomerEntity != null)
                {
                    //���������ڵĻ����޸��Ƶ�״̬
                    sale_CustomerEntity.PushMark = -1;
                    db.Update<Sale_CustomerEntity>(sale_CustomerEntity);
                }
                TemplateWxApp.SendTemplateBack("oA-EC1W1BQZ46Wc8HPCZZUUFbE9M", "���ã���������֪ͨ!", entity.Code, entity.OrderTitle, entity.PushBackReason, entity.PushBackDesc + "����֪����");

                RecordHelp.AddRecord(4, keyValue, "�Ƶ�����");
            }
            if (oldEntity.DownMark > 0)
            {
                entity.DownMark = -1;//�µ�����
                Sale_CustomerEntity sale_CustomerEntity = db.FindEntity<Sale_CustomerEntity>(t => t.ProduceId == oldEntity.Code);//�����۵�code������������id
                if (sale_CustomerEntity != null)
                {
                    //���������ڵĻ����޸��µ�״̬
                    sale_CustomerEntity.DownMark = -1;
                    db.Update<Sale_CustomerEntity>(sale_CustomerEntity);
                }
                CardTempBack(oldEntity.DownUserName, entity.Code, entity.OrderTitle, entity.PushBackReason, entity.PushBackDesc);
                RecordHelp.AddRecord(4, keyValue, "�µ�����");
            }
            if (oldEntity.MoneyOkMark > 0)
            {
                entity.MoneyOkMark = -1;//������˹���
                Sale_CustomerEntity sale_CustomerEntity = db.FindEntity<Sale_CustomerEntity>(t => t.ProduceId == oldEntity.Code);//�����۵�code������������id
                if (sale_CustomerEntity != null)
                {
                    //���������ڵĻ����޸ı������״̬
                    sale_CustomerEntity.MoneyOkMark = -1;
                    db.Update<Sale_CustomerEntity>(sale_CustomerEntity);
                }
                TemplateWxApp.SendTemplateBack("oA-EC1bg4U16c63kR6yj51lA5AiM", "���ã���������֪ͨ!", entity.Code, entity.OrderTitle, entity.PushBackReason, entity.PushBackDesc + "����֪����");
                RecordHelp.AddRecord(4, keyValue, "������˳���");
            }
            if (oldEntity.MoneyMark > 0)
            {
                entity.MoneyMark = -1;//���۹���
                CardTempBack(oldEntity.MoneyUserName, entity.Code, entity.OrderTitle, entity.PushBackReason, entity.PushBackDesc);
                RecordHelp.AddRecord(4, keyValue, "���۳���");
            }
            if (oldEntity.CheckMark > 0)
            {
                entity.CheckMark = -1;//��˹���
                CardTempBack(oldEntity.CheckUserName, entity.Code, entity.OrderTitle, entity.PushBackReason, entity.PushBackDesc);
                RecordHelp.AddRecord(4, keyValue, "��˳���");
            }
            if (oldEntity.ChaiMark > 0)
            {
                entity.ChaiMark = -1;//�𵥹���
                CardTempBack(oldEntity.ChaiUserName, entity.Code, entity.OrderTitle, entity.PushBackReason, entity.PushBackDesc);
                RecordHelp.AddRecord(4, keyValue, "�𵥳���");
            }
            if (oldEntity.CheckTuMark > 0)
            {
                entity.CheckTuMark = -1;//��ͼ����
                CardTempBack(oldEntity.CheckTuUserName, entity.Code, entity.OrderTitle, entity.PushBackReason, entity.PushBackDesc);
                RecordHelp.AddRecord(4, keyValue, "��ͼ����");
            }

            entity.Modify(keyValue);
            db.Update<DZ_OrderEntity>(entity);
            db.Commit();
        }
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

                    #region 2��ͼ
                    //��ͼͨ��֮�󣬸����˷���Ϣ����
                    if (entity.CheckTuMark > 0 && oldEntity.CheckTuMark != 1)
                    {
                        //��΢��ģ����Ϣ---�ӵ�֮�󣬸���ͼ������--���oA-EC1X6RWfW1_DNJ_VNiA3uhOYg
                        //��������֪ͨ�������ѣ�
                        TemplateWxApp.SendTemplateNew("oA-EC1X6RWfW1_DNJ_VNiA3uhOYg", "���ã����µĶ�����Ҫ��!", entity.OrderTitle, entity.Code, "����в𵥡�");
                        RecordHelp.AddRecord(4, keyValue, "��ͼ");
                    }
                    //��ͼ����֮�󣬸������˷���Ϣ����
                    if (entity.CheckTuMark == -1 && oldEntity.CheckTuMark != -1)
                    {
                        var hsf_CardList = db.IQueryable<Hsf_CardEntity>(t => t.Name.Equals(oldEntity.CreateUserName));//���͸������������ˣ��곤�����Ա���������Ե곤�ܿ����𵥱���
                        if (hsf_CardList.Count() != 0)
                        {
                            var hsf_CardEntity = hsf_CardList.First();
                            //��ֱ�Ӹ�����Ա���ۣ�ֻ�е곤����֪������
                            string backMsg = TemplateWxApp.SendTemplateReject(hsf_CardEntity.OpenId, "���ã���ͼ�˲��ض���!", oldEntity.Code, oldEntity.OrderTitle);
                            if (backMsg != "ok")
                            {
                                //ҵ��Աû�й�ע���ںţ�����΢��Post���������󣡴�����룺43004��˵����require subscribe hint: [ziWtva03011295]
                                LogHelper.AddLog(entity.CreateUserName + "û�й�ע���ں�");//��¼��־
                            }
                        }
                        else
                        {
                            LogHelper.AddLog(entity.CreateUserName + "û�й�ע���ں�");//��¼��־
                        }
                        RecordHelp.AddRecord(4, keyValue, "��ͼ����");
                    }
                    #endregion

                    #region 3��
                    if (entity.ChaiMark > 0 && oldEntity.ChaiMark != 1)
                    {
                        RecordHelp.AddRecord(4, keyValue, "��");
                    }
                    if (entity.ChaiMark == -1 && oldEntity.ChaiMark != -1)
                    {
                        RecordHelp.AddRecord(4, keyValue, "�𵥲���");
                        //���͸���ͼ�˲���֪ͨ
                        var hsf_CardList = db.IQueryable<Hsf_CardEntity>(t => t.Name.Equals(oldEntity.CheckTuUserName));
                        if (hsf_CardList.Count() != 0)
                        {
                            var hsf_CardEntity = hsf_CardList.First();
                            string backMsg = TemplateWxApp.SendTemplateReject(hsf_CardEntity.OpenId, "���ã����˲��ض���!", oldEntity.Code, oldEntity.OrderTitle);
                            if (backMsg != "ok")
                            {
                                LogHelper.AddLog(entity.CheckTuUserName + "û�й�ע���ں�");
                            }
                        }
                        else
                        {
                            LogHelper.AddLog(entity.CheckTuUserName + "û�й�ע���ں�");//��¼��־
                        }
                    }
                    #endregion


                    #region 4���
                    if (entity.CheckMark > 0 && oldEntity.CheckMark != 1)
                    {
                        RecordHelp.AddRecord(4, keyValue, "���");
                    }
                    if (entity.CheckMark == -1 && oldEntity.CheckMark != -1)
                    {
                        RecordHelp.AddRecord(4, keyValue, "��˲���");
                        //���͸����˲���֪ͨ
                        var hsf_CardList = db.IQueryable<Hsf_CardEntity>(t => t.Name.Equals(oldEntity.ChaiUserName));
                        if (hsf_CardList.Count() != 0)
                        {
                            var hsf_CardEntity = hsf_CardList.First();
                            string backMsg = TemplateWxApp.SendTemplateReject(hsf_CardEntity.OpenId, "���ã�����˲��ض���!", oldEntity.Code, oldEntity.OrderTitle);
                            if (backMsg != "ok")
                            {
                                LogHelper.AddLog(entity.ChaiUserName + "û�й�ע���ں�");//��¼��־
                            }
                        }
                        else
                        {
                            LogHelper.AddLog(entity.ChaiUserName + "û�й�ע���ں�");//��¼��־
                        }
                    }
                    #endregion

                    #region 5����
                    //����֮�󣬸�������Ϣ����
                    if (entity.MoneyMark == 1 && oldEntity.MoneyMark != 1)
                    {
                        RecordHelp.AddRecord(4, keyValue, "����");
                        if (entity.OrderType != 3)//���ǿ��ߵ�
                        {
                            //��΢��ģ����Ϣ---�з�����֮�󣬸���������--��һ���������oA-EC1bg4U16c63kR6yj51lA5AiM
                            //��������֪ͨ���������ѣ�
                            TemplateWxApp.SendTemplateMoney("oA-EC1bg4U16c63kR6yj51lA5AiM", "���ã����µı�����Ҫ���!", "�з�����", entity.OrderTitle, entity.Code, "����б�����ˡ�");
                        }
                        else
                        {
                            //���ߵ������߲���
                            //�������Ԥ������һ�û�µ��ģ�����������֮�����ֱ�Ӵ�������������ʼ�����µ�
                            if (oldEntity.DownMark != 1)
                            {
                                oldEntity.MoneyOkMark = 1;
                                oldEntity.MoneyOkDate = DateTime.Now;
                                entity.MoneyOkMark = 1;//���ߵ���Ĭ�ϱ�����ˣ����߲���
                                entity.MoneyOkDate = DateTime.Now;
                                RecordHelp.AddRecord(4, keyValue, "���ߵ������������");
                                Sale_Customer_Main.SaveSaleMain(db, oldEntity);//����µ�����ʱ�������ظ�����

                                //��΢��ģ����Ϣ---�����Ѿ�������˲��տ�ȷ��֮�󣬸��ű�������Ϣ����oA-EC1bJnd0KFBuOy0joJvUOGwwk
                                //��������֪ͨ��7�µ����ѣ�
                                TemplateWxApp.SendTemplateNew("oA-EC1bJnd0KFBuOy0joJvUOGwwk", "���ã����µĶ�����Ҫ�µ�!", oldEntity.OrderTitle, oldEntity.Code, "����������µ���");
                            }
                        }
                    }
                    if (entity.MoneyMark == -1 && oldEntity.MoneyMark != -1)
                    {
                        RecordHelp.AddRecord(4, keyValue, "���۲���");
                        //���͸�����˲���֪ͨ
                        var hsf_CardList = db.IQueryable<Hsf_CardEntity>(t => t.Name.Equals(oldEntity.CheckUserName));
                        if (hsf_CardList.Count() != 0)
                        {
                            var hsf_CardEntity = hsf_CardList.First();
                            string backMsg = TemplateWxApp.SendTemplateReject(hsf_CardEntity.OpenId, "���ã������˲��ض���!", oldEntity.Code, oldEntity.OrderTitle);
                            if (backMsg != "ok")
                            {
                                LogHelper.AddLog(entity.CheckUserName + "û�й�ע���ں�");//��¼��־
                            }
                        }
                        else
                        {
                            LogHelper.AddLog(entity.CheckUserName + "û�й�ע���ں�");//��¼��־
                        }
                    }
                    #endregion

                    #region �޸ĸ��ʽ������Ҫ��ȡԤ����
                    //����޸�Ϊ����Ҫ��ȡԤ������ұ���������
                    if (entity.FrontMark == 0 && oldEntity.FrontMark == 1 && oldEntity.MoneyOkMark == 1)
                    {
                        //��ʼ��������
                        Sale_CustomerEntity sale_CustomerEntity = db.FindEntity<Sale_CustomerEntity>(t => t.ProduceId == oldEntity.Code);//�����۵�code������������id
                        if (sale_CustomerEntity == null)
                        {
                            //�Զ�����������������������*****************���ص㣺ͬ�����±������״̬�ͱ������ʱ��
                            Sale_Customer_Main.SaveSaleMain(db, oldEntity);//����µ�����ʱ�������ظ�����
                        }
                        RecordHelp.AddRecord(4, keyValue, "�޸�Ϊ����Ҫ��Ԥ����");
                    }
                    #endregion

                    #region �޸ĸ��ʽ������Ҫ��ȡβ�ͬ������ⵥAfterMarkβ��״̬
                    if (entity.AfterMark == 0 && oldEntity.AfterMark == 1 && oldEntity.PushMark == 1)//֮ǰ��Ҫ��β����ڲ���Ҫβ��,�Ƶ�֮����п���������ⵥ
                    {
                        Buys_OrderEntity buys_CustomerEntity = db.FindEntity<Buys_OrderEntity>(t => t.Id == oldEntity.Code);//�����۵�code������������id
                        if (buys_CustomerEntity != null)
                        {
                            buys_CustomerEntity.AfterMark = 0;
                            db.Update<Buys_OrderEntity>(buys_CustomerEntity);
                            RecordHelp.AddRecord(4, keyValue, "�޸�Ϊ����Ҫ��β��");
                        }
                    }
                    #endregion

                    entity.Modify(keyValue);
                    db.Update<DZ_OrderEntity>(entity);//Ҫ����oldEntity�����޸Ĳſ��ԣ�����oldEntity��entity����һ������
                    db.Commit();
                }
                else
                {
                    entity.Create();
                    //this.BaseRepository().Insert(entity);
                    db.Insert<DZ_OrderEntity>(entity);
                    coderuleService.UseRuleSeed(entity.CreateUserId, "", ((int)CodeRuleEnum.DZ_Order).ToString(), db);//ռ�õ��ݺ�
                    db.Commit();

                    if (entity.OrderType != 3)//���ǿ��ߵ�
                    {
                        //��΢��ģ����Ϣ---�ӵ�֮�󣬸���ͼ������--������oA-EC1WVqHl_gsBM3We2rgOHIMEQ
                        //��������֪ͨ����ͼ���ѣ�
                        TemplateWxApp.SendTemplateNew("oA-EC1WVqHl_gsBM3We2rgOHIMEQ", "���ã����µĶ�����Ҫ��ͼ!", entity.OrderTitle, entity.Code, "�������ͼ��");
                    }
                    else
                    {
                        //��΢��ģ����Ϣ---���ߵ��ӵ�֮�󣬸���ͼ������--���oA-EC1X6RWfW1_DNJ_VNiA3uhOYg
                        //��������֪ͨ����ͼ���ѣ�
                        TemplateWxApp.SendTemplateNew("oA-EC1X6RWfW1_DNJ_VNiA3uhOYg", "���ã����µĶ�����Ҫ��ͼ!", entity.OrderTitle, entity.Code, "�������ͼ��");
                    }

                    RecordHelp.AddRecord(4, entity.Id, "�ӵ�");
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
                    //��ȡ�ϵ���ǰ
                    DZ_OrderEntity oldEntity = GetEntity(keyValue);
                    if (oldEntity != null)
                    {
                        //��һ�α�����˵�֪ͨ�����ظ����� && oldEntity.MoneyOkMark != 1
                        if (state == 1)
                        {
                            RecordHelp.AddRecord(4, keyValue, "�������");
                            //��΢��ģ����Ϣ������������ѣ�
                            if (!string.IsNullOrEmpty(oldEntity.CreateUserName))
                            {
                                //���͸������������ˣ��곤�����Ա���������Ե곤�ܿ����𵥱���
                                var hsf_CardList = db.IQueryable<Hsf_CardEntity>(t => t.Name.Equals(oldEntity.CreateUserName));
                                if (hsf_CardList.Count() != 0)
                                {
                                    var hsf_CardEntity = hsf_CardList.First();
                                    //��ֱ�Ӹ�����Ա���ۣ�ֻ�е곤����֪������
                                    string backMsg = TemplateWxApp.SendTemplateMoneyOk(hsf_CardEntity.OpenId, "���ã����Ķ���������������!", oldEntity.Code, oldEntity.OrderTitle, oldEntity.MoneyAccounts.ToString(), "");
                                    if (backMsg != "ok")
                                    {
                                        //ҵ��Աû�й�ע���ںţ�����΢��Post���������󣡴�����룺43004��˵����require subscribe hint: [ziWtva03011295]
                                        LogHelper.AddLog(oldEntity.SalesmanUserName + "û�й�ע���ں�");//��¼��־
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
                                TemplateWxApp.SendTemplateNew("oA-EC1bJnd0KFBuOy0joJvUOGwwk", "���ã����µĶ�����Ҫ�µ�!", oldEntity.OrderTitle, oldEntity.Code, "����������µ���");
                            }
                            else
                            {
                                //�������Ԥ������һ�û�µ��ģ�����������֮�����ֱ�Ӵ�������������ʼ�����µ�
                                if (oldEntity.FrontMark == 0 && oldEntity.DownMark != 1)
                                {
                                    //�Զ�����������������������*****************���ص㣺ͬ�����±������״̬�ͱ������ʱ��
                                    oldEntity.MoneyOkMark = state;
                                    oldEntity.MoneyOkDate = DateTime.Now;
                                    Sale_Customer_Main.SaveSaleMain(db, oldEntity);//����µ�����ʱ�������ظ�����
                                }
                            }
                        }

                        //��һ�α��۲��ص�֪ͨ
                        if (state == -1 && oldEntity.MoneyOkMark != -1)
                        {
                            //��΢��ģ����Ϣ��������˲������ѣ�-������������
                            var hsf_CardList = db.IQueryable<Hsf_CardEntity>(t => t.Name.Equals(oldEntity.MoneyUserName));
                            if (hsf_CardList.Count() != 0)
                            {
                                var hsf_CardEntity = hsf_CardList.First();
                                string backMsg = TemplateWxApp.SendTemplateReject(hsf_CardEntity.OpenId, "���ã���������˲��ض���!", oldEntity.Code, oldEntity.OrderTitle);
                                if (backMsg != "ok")
                                {
                                    LogHelper.AddLog(oldEntity.SalesmanUserName + "û�й�ע���ں�");//��¼��־
                                }
                            }

                            //�޸�����������ı�����˲���
                            Sale_CustomerEntity sale_CustomerEntity = db.FindEntity<Sale_CustomerEntity>(t => t.ProduceId == oldEntity.Code);//�����۵�code������������id
                            if (sale_CustomerEntity != null)
                            {
                                sale_CustomerEntity.MoneyOkMark = state;
                                sale_CustomerEntity.MoneyOkDate = DateTime.Now;
                                db.Update<Sale_CustomerEntity>(sale_CustomerEntity);
                            }

                            RecordHelp.AddRecord(4, keyValue, "������˲���");
                        }


                        //���۵�����ı������״̬����Ҫ�޸ĵ�
                        DZ_OrderEntity entity = new DZ_OrderEntity()
                        {
                            MoneyOkMark = state,
                            MoneyOkDate = DateTime.Now,
                            MoneyOkUserId = OperatorProvider.Provider.Current().UserId,
                            MoneyOkUserName = OperatorProvider.Provider.Current().UserName
                        };
                        entity.Modify(keyValue);
                        db.Update<DZ_OrderEntity>(entity);

                        db.Commit();//�������
                    }
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
                    RecordHelp.AddRecord(4, keyValue, "ǩ��ȷ��");
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
                    db.Update<DZ_OrderEntity>(entity);


                    //���������״̬
                    Sale_CustomerEntity sale_CustomerEntity = db.FindEntity<Sale_CustomerEntity>(t => t.OrderId == keyValue);
                    if (sale_CustomerEntity != null)
                    {
                        sale_CustomerEntity.OverMark = state;
                        db.Update<Sale_CustomerEntity>(sale_CustomerEntity);
                    }

                    //��ⵥ���״̬
                    Buys_OrderEntity buysEntity = db.FindEntity<Buys_OrderEntity>(t => t.OrderId == keyValue);
                    if (buysEntity != null)
                    {
                        buysEntity.OverMark = state;
                        db.Update<Buys_OrderEntity>(buysEntity);
                    }
                    db.Commit();
                    RecordHelp.AddRecord(4, keyValue, "�ᵥ");

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="dtSource">ʵ�����</param>
        /// <returns></returns>
        public string BatchAddEntity(string keyValue, DataTable dtSource, string dir)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                DZ_OrderEntity oldEntity = GetEntity(keyValue);
                if (!string.IsNullOrEmpty(oldEntity.MoneyPathKuJiaLe))
                {
                    //�ظ����룬ɾ��֮ǰ��������У������ǿ���ֻ���1010
                    //��ɾ��keyValue֮ǰ�����room��item
                    db.Delete<DZ_Money_RoomEntity>(t => t.OrderId == keyValue);
                    db.Delete<DZ_Money_ItemEntity>(t => t.OrderId == keyValue);
                    oldEntity.MoneyPathKuJiaLe = "";
                    oldEntity.MoneyPath1010 = "";
                }
                //��ȡ���۶�����
                int p = 0;
                if (oldEntity.CompanyName == "�����ֺ�˹")
                {
                    p = 2;
                }
                else if (oldEntity.CompanyName == "�ൺ�ֺ�˹")
                {
                    p = 3;
                }
                else
                {
                    p = 1;
                }

                int rowsCount = dtSource.Rows.Count;
                string productName = "";

                for (int r = 4; r < rowsCount; r++)
                {
                    string firstName = dtSource.Rows[r][0].ToString();
                    if (firstName.Contains("�������ƣ�"))
                    {
                        string roomName = firstName.Replace("�������ƣ�", "");
                        DZ_Money_RoomEntity roomEntity = AddDbRoom(roomName, keyValue, oldEntity.Code, "KuJiaLe");
                        roomEntity.RoomAmount = 0;

                        //�ӵ�ǰ���俪ʼ��������ȡ��ǰ���������
                        for (r = r + 3; r < rowsCount; r++)
                        {
                            firstName = dtSource.Rows[r][0].ToString();


                            if (firstName.Contains("�����嵥"))
                            {
                                decimal width = 0;
                                decimal height = 0;
                                int shan = 0;
                                string[] keys = { "��ɫխ��", "��ɫխ��", "35��", "65��" };
                                int keysIndex = 0;
                                string keyWord = "";
                                for (r = r + 3; r < rowsCount; r++)
                                {
                                    string secondName = dtSource.Rows[r][1].ToString();//D:���Ŷ��죨DG��-GU��
                                    if (secondName.Contains("���Ŷ���"))//ֻȡһ����
                                    {
                                        string widthStr = dtSource.Rows[r][6].ToString();//��2244
                                        width = Convert.ToDecimal(widthStr);
                                    }

                                    if (secondName.Contains("��ɫխ��") || secondName.Contains("��ɫխ��") || secondName.Contains("35��") || secondName.Contains("65��"))//ֻȡһ����
                                    {
                                        string heightStr = dtSource.Rows[r][9].ToString();//�ߣ�2,261	
                                        height = Convert.ToDecimal(heightStr);
                                        shan++;
                                        for (int i = 0; i < keys.Length; i++)
                                        {
                                            if (secondName.Contains(keys[i]))
                                            {
                                                keysIndex = i;
                                                keyWord = keys[keysIndex];
                                            }
                                        }
                                    }

                                    if (string.IsNullOrEmpty(secondName))
                                    {
                                        if (width != 0 && height != 0)
                                        {
                                            var product = db.FindEntity<DZ_ProductEntity>(t => t.Name.Contains(keyWord));//������ͬ��
                                            if (product != null)
                                            {
                                                decimal? _place = GetPlanPrice(p, product);
                                                decimal _area = width * height / 1000000;
                                                decimal? _amount = _place * _area;
                                                roomEntity.RoomAmount += _amount;
                                                AddDbItem(roomEntity.RoomId, roomName, product.Id, product.Code, product.Name, $"�Ŷ�H{height}mm*W{width}mm����{shan}��", 1, _area, product.Unit, _place, _amount, keyValue, oldEntity.Code, "KuJiaLe", db);

                                            }
                                        }
                                        break;//ֻ����һ�Ρ�Ĭ�ϵ�ǰ����ֻ��һ������
                                    }
                                }
                            }
                            if (firstName.Contains("����嵥"))
                            {
                                List<DZ_Money_ItemEntity> ItemList = new List<DZ_Money_ItemEntity>();
                                for (r = r + 3; r < rowsCount; r++)
                                {
                                    string secondName = dtSource.Rows[r][1].ToString();//����Ƹ�
                                    if (secondName.Contains("Բ���߽Ű�"))
                                    {
                                        var product = db.FindEntity<DZ_ProductEntity>(t => t.Name.Contains("Բ���߽Ű�"));//������ͬ��
                                        if (product != null)
                                        {
                                            decimal? _place = GetPlanPrice(p, product);
                                            decimal? _amount = _place;
                                            roomEntity.RoomAmount += _amount;
                                            var itemEntity = GetDbItem(roomEntity.RoomId, roomName, product.Id, product.Code, secondName, product.Guige, 1, 1, product.Unit, _place, _amount, keyValue, oldEntity.Code, "KuJiaLe", db);

                                            var itemFrist = ItemList.Find(t => t.ProductId == product.Id);
                                            if (itemFrist == null)
                                            {
                                                ItemList.Add(itemEntity);
                                            }
                                            else
                                            {
                                                //ͬһ��Ʒ��ͬһ���ʣ��ۼӽ����������
                                                itemFrist.Count += itemEntity.Count;
                                                itemFrist.Area += itemEntity.Area;
                                                itemFrist.Amount = itemFrist.Area * _place;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        foreach (var item in ItemList)
                                        {
                                            db.Insert<DZ_Money_ItemEntity>(item);
                                        }
                                        break;
                                    }
                                }
                            }

                            if (firstName.Contains("����嵥"))
                            {
                                List<DZ_Money_ItemEntity> ItemList = new List<DZ_Money_ItemEntity>();
                                for (r = r + 3; r < rowsCount; r++)
                                {
                                    string secondName = dtSource.Rows[r][1].ToString();//����Ƹ�
                                    if (!string.IsNullOrEmpty(secondName))
                                    {
                                        if (secondName.Contains("���") || secondName.Contains("���") || secondName.Contains("���") || secondName.Contains("���̳�")
                                        || secondName.Contains("�����") || secondName.Contains("����") || secondName.Contains("�Ƹ�") || secondName.Contains("�Ʊ���") || secondName.Contains("��ƿ��"))
                                        {
                                            string[] keys = { "���", "���", "���", "���̳�", "�����", "����", "�Ƹ�", "�Ʊ���", "��ƿ��" };
                                            int keysIndex = 0;
                                            string keyWord = "";
                                            for (int i = 0; i < keys.Length; i++)
                                            {
                                                if (secondName.Contains(keys[i]))
                                                {
                                                    keysIndex = i;
                                                    keyWord = keys[keysIndex];
                                                }
                                            }
                                            if (!string.IsNullOrEmpty(keyWord))
                                            {
                                                var product = db.FindEntity<DZ_ProductEntity>(t => t.Name.Contains(keyWord));//������ͬ��
                                                if (product != null)
                                                {
                                                    decimal? _place = GetPlanPrice(p, product);
                                                    decimal? _amount = _place;
                                                    roomEntity.RoomAmount += _amount;
                                                    var itemEntity = GetDbItem(roomEntity.RoomId, roomName, product.Id, product.Code, secondName, product.Guige, 1, 1, product.Unit, _place, _amount, keyValue, oldEntity.Code, "KuJiaLe", db);

                                                    var itemFrist = ItemList.Find(t => t.ProductId == product.Id);
                                                    if (itemFrist == null)
                                                    {
                                                        ItemList.Add(itemEntity);
                                                    }
                                                    else
                                                    {
                                                        //ͬһ��Ʒ��ͬһ���ʣ��ۼӽ����������
                                                        itemFrist.Count += itemEntity.Count;
                                                        itemFrist.Area += itemEntity.Area;
                                                        itemFrist.Amount = itemFrist.Area * _place;
                                                    }
                                                }
                                            }
                                        }

                                        if (secondName.Contains("45������1#") || secondName.Contains("45������2#") || secondName.Contains("80������1#"))
                                        {
                                            //Y:80������1#(����ͷ1#)	
                                            string[] keys = {"45������1#", "45������2#", "80������1#"};
                                            int keysIndex = 0;
                                            string keyWord = "";
                                            for (int i = 0; i < keys.Length; i++)
                                            {
                                                if (secondName.Contains(keys[i]))
                                                {
                                                    keysIndex = i;
                                                    keyWord = keys[keysIndex];
                                                }
                                            }
                                            if (!string.IsNullOrEmpty(keyWord))
                                            {
                                                var product = db.FindEntity<DZ_ProductEntity>(t => t.Name.Contains(keyWord));//������ͬ��
                                                if (product != null)
                                                {
                                                    string heightStr = dtSource.Rows[r][9].ToString();//�ߣ�2,261	
                                                    decimal length = Convert.ToDecimal(heightStr) / 1000;

                                                    decimal? _place = GetPlanPrice(p, product);
                                                    decimal? _amount = _place * length;

                                                    roomEntity.RoomAmount += _amount;
                                                    var itemEntity = GetDbItem(roomEntity.RoomId, roomName, product.Id, product.Code, product.Name, product.Guige, 1, length, product.Unit, _place, _amount, keyValue, oldEntity.Code, "KuJiaLe", db);

                                                    var itemFrist = ItemList.Find(t => t.ProductId == product.Id);
                                                    if (itemFrist == null)
                                                    {
                                                        ItemList.Add(itemEntity);
                                                    }
                                                    else
                                                    {
                                                        //ͬһ��Ʒ��ͬһ���ʣ��ۼӽ����������
                                                        itemFrist.Count += itemEntity.Count;
                                                        itemFrist.Area += itemEntity.Area;
                                                        itemFrist.Amount = itemFrist.Area * _place;
                                                    }
                                                }

                                                //����ͷ��
                                                string[] keys2 = { "��ͷ1#", "��ͷ2#", };
                                                int keysIndex2 = 0;
                                                string keyWord2 = "";
                                                for (int i = 0; i < keys2.Length; i++)
                                                {
                                                    if (secondName.Contains(keys2[i]))
                                                    {
                                                        keysIndex2 = i;
                                                        keyWord2 = keys2[keysIndex2];
                                                    }
                                                }
                                                if (!string.IsNullOrEmpty(keyWord2))
                                                {
                                                    string keyWordNo = keyWord.Substring(0, 2);

                                                    var product2 = db.FindEntity<DZ_ProductEntity>(t => t.Name.Contains(keyWordNo+keyWord2));//������ͬ��
                                                    if (product2 != null)
                                                    {
                                                        decimal? _place = GetPlanPrice(p, product2);
                                                        decimal? _amount = _place*2;//����*2
                                                        roomEntity.RoomAmount += _amount;
                                                        var itemEntity = GetDbItem(roomEntity.RoomId, roomName, product2.Id, product2.Code, product2.Name, product2.Guige, 1, 2, product2.Unit, _place, _amount, keyValue, oldEntity.Code, "KuJiaLe", db);

                                                        var itemFrist = ItemList.Find(t => t.ProductId == product2.Id);
                                                        if (itemFrist == null)
                                                        {
                                                            ItemList.Add(itemEntity);
                                                        }
                                                        else
                                                        {
                                                            //ͬһ��Ʒ��ͬһ���ʣ��ۼӽ����������
                                                            itemFrist.Count += itemEntity.Count;
                                                            itemFrist.Area += itemEntity.Area;
                                                            itemFrist.Amount = itemFrist.Area * _place;
                                                        }
                                                    }
                                                }
                                            }
                                        }



                                        if (secondName.Contains("��ͨ"))
                                        {
                                            //P:�ߵ���ͨ��	
                                            string keyWord = secondName.Substring(2);//ȥ��ǰ��λP:
                                            var product = db.FindEntity<DZ_ProductEntity>(t => t.Name.Contains(keyWord));//������ͬ��
                                            if (product != null)
                                            {
                                                decimal? _place = GetPlanPrice(p, product);

                                                string widthStr = dtSource.Rows[r][6].ToString();//��2244
                                                decimal width = Convert.ToDecimal(widthStr);
                                                decimal length = 0;
                                                if (keyWord == "�ߵ���ͨ")
                                                {
                                                    length = (width - 6)/1000;//�ߵ���ͨ����= ceil�������-6��/1000��
                                                }
                                                else
                                                {
                                                    length = (width - 10) / 1000;//��ͨ����=ceil�������-10��/1000��
                                                }
                                                if (length<1)
                                                {
                                                    length = 1;//С��1�ף���1��
                                                }

                                                decimal? _amount = _place * length;
                                                roomEntity.RoomAmount += _amount;
                                                var itemEntity = GetDbItem(roomEntity.RoomId, roomName, product.Id, product.Code, keyWord, product.Guige, 1, length, product.Unit, _place, _amount, keyValue, oldEntity.Code, "KuJiaLe", db);

                                                var itemFrist = ItemList.Find(t => t.ProductId == product.Id);
                                                if (itemFrist == null)
                                                {
                                                    ItemList.Add(itemEntity);
                                                }
                                                else
                                                {
                                                    //ͬһ��Ʒ��ͬһ���ʣ��ۼӽ����������
                                                    itemFrist.Count += itemEntity.Count;
                                                    itemFrist.Area += itemEntity.Area;
                                                    itemFrist.Amount = itemFrist.Area * _place;
                                                }
                                            }
                                        }

                                    }
                                    else
                                    {
                                        foreach (var item in ItemList)
                                        {
                                            db.Insert<DZ_Money_ItemEntity>(item);
                                        }
                                        break;
                                    }

                                }
                            }
                            if (firstName.Contains("�Ű��嵥"))
                            {
                                List<DZ_Money_ItemEntity> ItemList = new List<DZ_Money_ItemEntity>();
                                for (r = r + 3; r < rowsCount; r++)
                                {
                                    string secondName = dtSource.Rows[r][1].ToString();//Y:km-xs-502	
                                    if (secondName.Contains("T:")) //ͬ������ϵ��Ű壨T���������㣬�ѵ������1010�嵥��
                                    {
                                        continue;
                                    }
                                    string caizhi = dtSource.Rows[r][5].ToString();//���ƾ����ľ
                                    string area = dtSource.Rows[r][12].ToString();//0.340312

                                    if (secondName.Contains("Y:") || secondName.Contains("G:"))//����ƴ����(Y��)��Ƕ����(Y��)��������(Y��)��չ��������㣻//��Э�Ű尴չ��������㣬����0.3�O��0.3�O�����ݲ����ţ�ʯӢ�ţ����ݲ�����(��ʯӢ��ר��)���������ţ�������Ʋ����ţ�
                                    {
                                        secondName = secondName.Substring(2);//ȥ��ǰ��λY:

                                        bool isDcb = false;//Ĭ�ϲ��Ƕ��壬Ĭ�Ͽ�����
                                        if (secondName.Contains("����"))//����+30
                                        {
                                            secondName = secondName.Replace("����", "");
                                            isDcb = true;
                                        }
                                        var product = db.FindEntity<DZ_ProductEntity>(t => t.Name.ToUpper().Contains(secondName.ToUpper()));//������ͬ��
                                        if (product != null)
                                        {
                                            decimal? _place = GetPlanPrice(p, product);
                                            if (_place == 0 || _place == null)
                                            {
                                                return "��Ʒ���۲����ڣ�" + secondName;
                                            }

                                            if (secondName.Contains("km-xs"))
                                            {
                                                if (caizhi.Contains("����") && caizhi.Contains("�߹�") && caizhi.Contains("����"))//Ĥ��һ��
                                                {
                                                    _place += 50;//����+50
                                                }
                                                if (caizhi.Contains("����"))
                                                {
                                                    _place += 80;//����+50
                                                }
                                            }
                                            if (isDcb)//����+30
                                            {
                                                _place += 30;//���۶���+30
                                            }

                                            decimal? _area = Convert.ToDecimal(area);
                                            if (secondName.Contains("G:"))//��Э�Ű�,����0.3�O��0.3�O
                                            {
                                                _area = _area > 0.3M ? _area : 0.3M;
                                            }
                                            decimal? _amount = _place * _area;
                                            roomEntity.RoomAmount += _amount;
                                            var itemEntity= GetDbItem(roomEntity.RoomId, roomName, product.Id, product.Code, secondName, caizhi, 1, _area, product.Unit, _place, _amount, keyValue, oldEntity.Code, "KuJiaLe", db);

                                            var itemFrist = ItemList.Find(t => t.ProductId == product.Id && t.Guige == caizhi);
                                            if(itemFrist == null)
                                            {
                                                ItemList.Add(itemEntity);
                                            }
                                            else
                                            {
                                                //ͬһ��Ʒ��ͬһ���ʣ��ۼӽ����������
                                                itemFrist.Count += itemEntity.Count;
                                                itemFrist.Area += itemEntity.Area;
                                                itemFrist.Amount = itemFrist.Area * _place;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        foreach (var item in ItemList)
                                        {
                                            db.Insert<DZ_Money_ItemEntity>(item);
                                        }
                                        break;
                                    }
                                }
                            }
                            if (firstName.Contains("���"))
                            {
                                for (r = r + 3; r < rowsCount; r++)
                                {
                                    string secondName = dtSource.Rows[r][1].ToString();//P:1370��ֱ��
                                    string count = dtSource.Rows[r][14].ToString();//6
                                    if (!secondName.IsEmpty())
                                    {
                                        secondName = secondName.Substring(2);//ȥ��ǰ��λY:
                                        var product = db.FindEntity<DZ_ProductEntity>(t => t.Name.Contains(secondName));//������ͬ��
                                        if (product != null)
                                        {
                                            decimal? _place = GetPlanPrice(p, product);
                                            if (_place == 0 || _place == null)
                                            {
                                                return "��Ʒ���۲����ڣ�" + secondName;
                                            }

                                            decimal? _count = Convert.ToDecimal(count);
                                            decimal? _amount = _place * _count;
                                            roomEntity.RoomAmount += _amount;
                                            AddDbItem(roomEntity.RoomId, roomName, product.Id, product.Code, secondName, product.Guige, 1, _count, product.Unit, _place, _amount, keyValue, oldEntity.Code, "KuJiaLe", db);
                                        }
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                            if (firstName.Contains("����"))
                            {
                                List<DZ_Money_ItemEntity> ItemList = new List<DZ_Money_ItemEntity>();
                                for (r = r + 3; r < rowsCount; r++)
                                {
                                    string secondName = dtSource.Rows[r][1].ToString();//P:����-95����1#	
                                    if (!secondName.IsEmpty())
                                    {
                                        if (secondName.Contains("����"))
                                        {
                                            secondName = secondName.Substring(3);//ȥ��ǰ��λ����-
                                            var product = db.FindEntity<DZ_ProductEntity>(t => t.Name.Contains(secondName));//������ͬ��
                                            if (product != null)
                                            {
                                                decimal? _place = GetPlanPrice(p, product);
                                                if (_place == 0 || _place == null)
                                                {
                                                    return "��Ʒ���۲����ڣ�" + secondName;
                                                }

                                                string widthStr = dtSource.Rows[r][6].ToString();//��3,571	
                                                                                                 //95����1#/75����1#���׼���      95����1#/75����1#����=����� >2400������=���/2000+0.2������=ceil�����/2400��������=���/1000+0.2��
                                                decimal width = Convert.ToDecimal(widthStr);
                                                decimal length;

                                                if (width > 2400)
                                                {
                                                    length = width / 2000 + 0.2M * width / 2400;
                                                }
                                                else
                                                {
                                                    length = width / 1000 + 0.2M;
                                                }

                                                decimal? _amount = _place * length;
                                                roomEntity.RoomAmount += _amount;

                                                var itemEntity = GetDbItem(roomEntity.RoomId, roomName, product.Id, product.Code, secondName, product.Guige, 1, length, product.Unit, _place, _amount, keyValue, oldEntity.Code, "KuJiaLe", db);

                                                var itemFrist = ItemList.Find(t => t.ProductId == product.Id);
                                                if (itemFrist == null)
                                                {
                                                    ItemList.Add(itemEntity);
                                                }
                                                else
                                                {
                                                    //ͬһ��Ʒ��ͬһ���ʣ��ۼӽ����������
                                                    itemFrist.Count += itemEntity.Count;
                                                    itemFrist.Area += itemEntity.Area;
                                                    itemFrist.Amount = itemFrist.Area * _place;
                                                }
                                            }
                                        }

                                        if (secondName.Contains("����"))
                                        {
                                            //����-���Ͻ��߽Ű�	
                                            string amountStr = dtSource.Rows[r][18].ToString();//0.00
                                            if (amountStr== "0")
                                            {
                                                secondName = secondName.Substring(3);//ȥ��ǰ��λ����-
                                                var product = db.FindEntity<DZ_ProductEntity>(t => t.Name.Contains(secondName));//������ͬ��
                                                if (product != null)
                                                {
                                                    decimal? _place = GetPlanPrice(p, product);
                                                    if (_place == 0 || _place == null)
                                                    {
                                                        return "��Ʒ���۲����ڣ�" + secondName;
                                                    }

                                                    string widthStr = dtSource.Rows[r][6].ToString();
                                                    decimal width = Convert.ToDecimal(widthStr);
                                                    decimal count = count = (width + 600) / 2000 *2;//����=ceil�����Ϊ0���߽Ű���+600��/2000��������Ҫ����Ա�˶ԣ�   �̶�����2.0��/��

                                                    decimal? _amount = _place * count;
                                                    roomEntity.RoomAmount += _amount;

                                                    var itemEntity = GetDbItem(roomEntity.RoomId, roomName, product.Id, product.Code, secondName, product.Guige, 1, count, product.Unit, _place, _amount, keyValue, oldEntity.Code, "KuJiaLe", db);

                                                    var itemFrist = ItemList.Find(t => t.ProductId == product.Id);
                                                    if (itemFrist == null)
                                                    {
                                                        ItemList.Add(itemEntity);
                                                    }
                                                    else
                                                    {
                                                        //ͬһ��Ʒ��ͬһ���ʣ��ۼӽ����������
                                                        itemFrist.Count += itemEntity.Count;
                                                        itemFrist.Area += itemEntity.Area;
                                                        itemFrist.Amount = itemFrist.Area * _place;
                                                    }
                                                }
                                            }
                                            
                                        }
                                    }
                                    else
                                    {
                                        foreach (var item in ItemList)
                                        {
                                            db.Insert<DZ_Money_ItemEntity>(item);
                                        }
                                        break;
                                    }
                                }
                            }

                            if (firstName.Contains("�������ƣ�"))
                            {
                                var oldRoom = db.FindEntity<DZ_Money_RoomEntity>(t => t.RoomName == roomName && t.OrderId == keyValue);
                                if (oldRoom != null)
                                {
                                    oldRoom.RoomAmount += roomEntity.RoomAmount;
                                    oldRoom.Source = "KuJiaLe";
                                    db.Update<DZ_Money_RoomEntity>(oldRoom);
                                }
                                else
                                {
                                    db.Insert<DZ_Money_RoomEntity>(roomEntity);
                                    productName += roomName + "��";
                                }
                                r = r - 1;
                                break;//��ǰroom������ϣ�����ѭ��
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(productName))
                {
                    oldEntity.ProductName = productName.Substring(0, productName.Length - 1);
                }

                oldEntity.MoneyPathKuJiaLe = dir;
                db.Update<DZ_OrderEntity>(oldEntity);
                db.Commit();
                return "�������ֱ����ļ��ɹ���";
            }
            catch (Exception ex)
            {
                db.Rollback();
                throw;
                //LogHelper.AddLog(ex.Message);
                //return ex.Message;
            }
        }

        /// <summary>
        /// ������������1010
        /// </summary>
        /// <param name="dtSource">ʵ�����</param>
        /// <returns></returns>
        public string BatchAddEntity1010(string keyValue, DataTable dtSource, string dir)
        {
            try
            {
                IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
                DZ_OrderEntity oldEntity = GetEntity(keyValue);
                if (!string.IsNullOrEmpty(oldEntity.MoneyPath1010))
                {
                    //�ظ����룬ɾ��֮ǰ��������У������ǿ���ֻ���1010
                    //��ɾ��keyValue֮ǰ�����room��item
                    db.Delete<DZ_Money_RoomEntity>(t => t.OrderId == keyValue);
                    db.Delete<DZ_Money_ItemEntity>(t => t.OrderId == keyValue);
                    oldEntity.MoneyPathKuJiaLe = "";
                    oldEntity.MoneyPath1010 = "";
                }
                int p = 0;
                if (oldEntity.CompanyName == "�����ֺ�˹")
                {
                    p = 2;
                }
                else if (oldEntity.CompanyName == "�ൺ�ֺ�˹")
                {
                    p = 3;
                }
                else
                {
                    p = 1;
                }

                int rowsCount = dtSource.Rows.Count;
                string productName = "";

                for (int r = 4; r < rowsCount; r++)
                {
                    string c1 = dtSource.Rows[r][0].ToString();//�ֶζ��Ź�3915*2020*500
                    if (!string.IsNullOrEmpty(c1))
                    {
                        if (c1 == "�ܼƣ�")
                        {
                            break;
                        }
                        string roomName = Regex.Match(c1, "[\u4e00-\u9fa5]+").Value;
                        //��������db
                        DZ_Money_RoomEntity roomEntity = AddDbRoom(roomName, keyValue, oldEntity.Code, "1010");
                        roomEntity.RoomAmount = 0;
                        for (int i = r + 1; i < rowsCount; i++)
                        {
                            string c2 = dtSource.Rows[i][1].ToString();//18-������Ȼԭ�������
                            if (!string.IsNullOrEmpty(c2))
                            {
                                string[] c22 = c2.Split('-');
                                if (c22.Length == 2)
                                {
                                    string houdu = c22[0];
                                    string caizhiNMame = c22[1];
                                    decimal? sumArea = 0;
                                    int? sumCount = 0;
                                    var product = db.FindEntity<DZ_ProductEntity>(t => t.Guige.Contains(houdu) && t.Name.Contains(caizhiNMame.Substring(caizhiNMame.Length - 3)));//������+18
                                    if (product != null)
                                    {
                                        decimal? _place = GetPlanPrice(p, product);
                                        if (_place == 0 || _place == null)
                                        {
                                            return "��Ʒ���۲����ڣ�" + caizhiNMame + houdu;
                                        }
                                        else
                                        {
                                            for (int j = i + 1; j < rowsCount; j++)
                                            {

                                                string c11 = dtSource.Rows[i][1].ToString();//18-������Ȼԭ�������
                                                if (c11 == "��")//�������10Ԫ/��	
                                                {
                                                    _place += 10;
                                                }
                                                string kuan = dtSource.Rows[j][3].ToString();//��
                                                string gao = dtSource.Rows[j][4].ToString();//��
                                                if (!string.IsNullOrEmpty(kuan))
                                                {
                                                    sumArea += Math.Round(((decimal)Convert.ToDecimal(kuan) * Convert.ToDecimal(gao) / 1000000), 2);

                                                }
                                                else
                                                {
                                                    //���գ�˵����ǰ�����Ѿ�������ɣ�������
                                                    decimal? _amount = _place * sumArea;
                                                    roomEntity.RoomAmount += _amount;
                                                    //���������ڵĲ���db
                                                    AddDbItem(roomEntity.RoomId, roomName, product.Id, product.Code, caizhiNMame, product.Guige, sumCount, sumArea, product.Unit, _place, _amount, keyValue, oldEntity.Code, "1010", db);
                                                    i = j - 1;//��ǰ���У�-1��ȥ��i������1
                                                    break;//��ǰitem������ϣ�����ѭ��
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        return "��Ʒ�����ڣ�" + caizhiNMame + houdu;
                                    }
                                }
                            }
                            else
                            {
                                var oldRoom = db.FindEntity<DZ_Money_RoomEntity>(t => t.RoomName == roomName && t.OrderId == keyValue);
                                if (oldRoom != null)
                                {
                                    oldRoom.RoomAmount += roomEntity.RoomAmount;
                                    oldRoom.Source = "1010";
                                    db.Update<DZ_Money_RoomEntity>(oldRoom);
                                }
                                else
                                {
                                    db.Insert<DZ_Money_RoomEntity>(roomEntity);
                                    productName += roomName+"��";
                                }
                                r = i - 1;
                                break;//��ǰroom������ϣ�����ѭ��
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(productName))
                {
                    oldEntity.ProductName = productName.Substring(0, productName.Length - 1);
                }
                oldEntity.MoneyPath1010 = dir;
                db.Update<DZ_OrderEntity>(oldEntity);
                db.Commit();
                return "����1010�����ļ��ɹ�";
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(ex.Message);
                return ex.Message;
            }

        }

        /// <summary>
        /// �༭���۱���
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <param name="entryList">ʵ���Ӷ���</param>
        /// <returns></returns>
        public void SaveMoneyForm(string keyValue, DZ_OrderEntity entity, List<DZ_Money_ItemEntity> entryList)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                //����ɨ��֮��༭
                DZ_OrderEntity oldEntity = GetEntity(keyValue);
                if (oldEntity != null)
                {
                    //����
                    entity.Modify(keyValue);
                    db.Update(entity);
                    //��ϸ
                    db.Delete<DZ_Money_ItemEntity>(t => t.OrderId.Equals(keyValue));

                    foreach (DZ_Money_ItemEntity item in entryList)
                    {
                        item.Create();
                        item.OrderId = keyValue;
                        db.Insert(entryList);
                    }

                    db.Commit();
                    RecordHelp.AddRecord(4, keyValue, "�༭����");
                }
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }



        public decimal? GetPlanPrice(int p, DZ_ProductEntity entity)
        {
            decimal? price = null;
            switch (p)
            {
                case 1:
                    price = entity.Plan1;
                    break;
                case 2:
                    price = entity.Plan2;
                    break;
                case 3:
                    price = entity.Plan3;
                    break;
                default:
                    break;
            }
            return price;
        }

        public DZ_Money_RoomEntity AddDbRoom(string roomName, string orderId, string orderCode, string source)
        {
            DZ_Money_RoomEntity roomEntity = new DZ_Money_RoomEntity()
            {
                RoomName = roomName,
                OrderId = orderId,
                OrderCode = orderCode,
                Source = source
            };
            roomEntity.Create();
            return roomEntity;
        }
        public void AddDbItem(string roomId, string roomName, string productId, string productCode, string productName, string guige, decimal? count, decimal? area, string unit, decimal? price, decimal? amount, string orderId, string orderCode, string source, IRepository db)
        {
            DZ_Money_ItemEntity itemEntity = new DZ_Money_ItemEntity()
            {
                RoomId = roomId,
                RoomName = roomName,
                ProductId = productId,
                ProductCode = productCode,
                ProductName = productName,
                Guige = guige,
                Count = count,
                Area = area,
                Unit = unit,
                Price = price,
                Amount = amount,
                OrderId = orderId,
                OrderCode = orderCode,
                Source = source
            };
            itemEntity.Create();
            db.Insert<DZ_Money_ItemEntity>(itemEntity);//������岿��item
        }

        public DZ_Money_ItemEntity GetDbItem(string roomId, string roomName, string productId, string productCode, string productName, string guige, decimal? count, decimal? area, string unit, decimal? price, decimal? amount, string orderId, string orderCode, string source, IRepository db)
        {
            DZ_Money_ItemEntity itemEntity = new DZ_Money_ItemEntity()
            {
                RoomId = roomId,
                RoomName = roomName,
                ProductId = productId,
                ProductCode = productCode,
                ProductName = productName,
                Guige = guige,
                Count = count,
                Area = area,
                Unit = unit,
                Price = price,
                Amount = amount,
                OrderId = orderId,
                OrderCode = orderCode,
                Source = source
            };
            itemEntity.Create();
            return itemEntity;
        }
        
    }
}
