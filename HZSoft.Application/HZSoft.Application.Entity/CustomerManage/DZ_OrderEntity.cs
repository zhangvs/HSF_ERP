using System;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2019-11-17 08:11
    /// �� ����DZ_Order
    /// </summary>
    public class DZ_OrderEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// ID
        /// </summary>
        /// <returns></returns>
        public string Id { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string Code { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public string OrderTitle { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public int? OrderType { get; set; }
        /// <summary>
        /// ��Ʒ����
        /// </summary>
        /// <returns></returns>
        public string ProductName { get; set; }
        /// <summary>
        /// ������id
        /// </summary>
        /// <returns></returns>
        public string CompanyId { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        public string CompanyName { get; set; }
        /// <summary>
        /// �ͻ�id
        /// </summary>
        /// <returns></returns>
        public string CustomerId { get; set; }
        /// <summary>
        /// �ͻ�����
        /// </summary>
        /// <returns></returns>
        public string CustomerName { get; set; }
        /// <summary>
        /// �ͻ��绰
        /// </summary>
        /// <returns></returns>
        public string CustomerTelphone { get; set; }
        /// <summary>
        /// ���ʦ
        /// </summary>
        /// <returns></returns>
        public string DesignerUserName { get; set; }
        /// <summary>
        /// ��Ƶ绰
        /// </summary>
        /// <returns></returns>
        public string DesignerTelphone { get; set; }
        /// <summary>
        /// ������Աid
        /// </summary>
        /// <returns></returns>
        public string SalesmanUserId { get; set; }
        /// <summary>
        /// ������Ա
        /// </summary>
        /// <returns></returns>
        public string SalesmanUserName { get; set; }
        /// <summary>
        /// �ջ���ַ
        /// </summary>
        /// <returns></returns>
        public string Address { get; set; }
        /// <summary>
        /// ���䷽ʽ
        /// </summary>
        /// <returns></returns>
        public int? ShippingType { get; set; }
        /// <summary>
        /// �˷ѳе���
        /// </summary>
        /// <returns></returns>
        public int? Carrier { get; set; }
        /// <summary>
        /// ԭͼ����
        /// </summary>
        /// <returns></returns>
        public string CreatePath { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
        /// <summary>
        /// �����û�����
        /// </summary>
        /// <returns></returns>
        public string CreateUserId { get; set; }
        /// <summary>
        /// �����û�
        /// </summary>
        /// <returns></returns>
        public string CreateUserName { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// ��ͼ
        /// </summary>
        /// <returns></returns>
        public int? CheckTuMark { get; set; }
        /// <summary>
        /// ��ͼ��Id
        /// </summary>
        /// <returns></returns>
        public string CheckTuUserId { get; set; }
        /// <summary>
        /// ��ͼ��
        /// </summary>
        /// <returns></returns>
        public string CheckTuUserName { get; set; }
        /// <summary>
        /// ��ͼ����
        /// </summary>
        /// <returns></returns>
        public string CheckTuPath { get; set; }
        /// <summary>
        /// ��ͼ����
        /// </summary>
        /// <returns></returns>
        public DateTime? CheckTuDate { get; set; }
        /// <summary>
        /// ��
        /// </summary>
        /// <returns></returns>
        public int? ChaiMark { get; set; }
        /// <summary>
        /// ����Id
        /// </summary>
        /// <returns></returns>
        public string ChaiUserId { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string ChaiUserName { get; set; }
        /// <summary>
        /// �𵥸���
        /// </summary>
        /// <returns></returns>
        public string ChaiPath { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        public DateTime? ChaiDate { get; set; }
        /// <summary>
        /// ���
        /// </summary>
        /// <returns></returns>
        public int? CheckMark { get; set; }
        /// <summary>
        /// �����Id
        /// </summary>
        /// <returns></returns>
        public string CheckUserId { get; set; }
        /// <summary>
        /// �����
        /// </summary>
        /// <returns></returns>
        public string CheckUserName { get; set; }
        /// <summary>
        /// �������
        /// </summary>
        /// <returns></returns>
        public DateTime? CheckDate { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public int? MoneyMark { get; set; }
        /// <summary>
        /// ������Id
        /// </summary>
        /// <returns></returns>
        public string MoneyUserId { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        public string MoneyUserName { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public DateTime? MoneyDate { get; set; }
        /// <summary>
        /// ���۸���
        /// </summary>
        /// <returns></returns>
        public string MoneyPath { get; set; }
        /// <summary>
        /// ���۸���1010
        /// </summary>
        /// <returns></returns>
        public string MoneyPath1010 { get; set; }
        /// <summary>
        /// ���۸��������
        /// </summary>
        /// <returns></returns>
        public string MoneyPathKuJiaLe { get; set; }
        /// <summary>
        /// �𵥽��
        /// </summary>
        /// <returns></returns>
        public decimal? MoneyAccounts { get; set; }

        /// <summary>
        /// �������
        /// </summary>
        /// <returns></returns>
        public int? MoneyOkMark { get; set; }
        /// <summary>
        /// ���������Id
        /// </summary>
        /// <returns></returns>
        public string MoneyOkUserId { get; set; }
        /// <summary>
        /// ���������
        /// </summary>
        /// <returns></returns>
        public string MoneyOkUserName { get; set; }
        /// <summary>
        /// �����������
        /// </summary>
        /// <returns></returns>
        public DateTime? MoneyOkDate { get; set; }

        /// <summary>
        /// Ӧ�ս��
        /// </summary>
        /// <returns></returns>
        public decimal? Accounts { get; set; }
        /// <summary>
        /// ���ս��
        /// </summary>
        /// <returns></returns>
        public decimal? ReceivedAmount { get; set; }
        /// <summary>
        /// �տ�����
        /// </summary>
        /// <returns></returns>
        public DateTime? PaymentDate { get; set; }
        /// <summary>
        /// �տ�״̬��1-δ�տ�2-�����տ�3-ȫ���տ
        /// </summary>
        /// <returns></returns>
        public int? PaymentState { get; set; }
        /// <summary>
        /// �Ƿ���ȡԤ����
        /// </summary>
        /// <returns></returns>
        public int? FrontMark { get; set; }
        /// <summary>
        /// �Ƿ���ȡβ��
        /// </summary>
        /// <returns></returns>
        public int? AfterMark { get; set; }
        /// <summary>
        /// �Ƿ��½�
        /// </summary>
        /// <returns></returns>
        public int? MonthMark { get; set; }


        /// <summary>
        /// �µ�
        /// </summary>
        /// <returns></returns>
        public int? DownMark { get; set; }
        /// <summary>
        /// �µ�����
        /// </summary>
        /// <returns></returns>
        public string DownPath { get; set; }
        /// <summary>
        /// �µ���Id
        /// </summary>
        /// <returns></returns>
        public string DownUserId { get; set; }
        /// <summary>
        /// �µ���
        /// </summary>
        /// <returns></returns>
        public string DownUserName { get; set; }
        /// <summary>
        /// �µ�����
        /// </summary>
        /// <returns></returns>
        public DateTime? DownDate { get; set; }
        /// <summary>
        /// ����ԭ��
        /// </summary>
        /// <returns></returns>
        public string PushBackReason { get; set; }
        /// <summary>
        /// ����ԭ�򸽼�
        /// </summary>
        /// <returns></returns>
        public string PushBackPath { get; set; }
        /// <summary>
        /// ����ԭ��ͼƬ
        /// </summary>
        /// <returns></returns>
        public string PushBackImg { get; set; }
        /// <summary>
        /// ����ԭ��ע
        /// </summary>
        /// <returns></returns>
        public string PushBackDesc { get; set; }


        /// <summary>
        /// �Ƶ�
        /// </summary>
        /// <returns></returns>
        public int? PushMark { get; set; }

        /// <summary>
        /// �Ƶ�����
        /// </summary>
        /// <returns></returns>
        public DateTime? PushDate { get; set; }


        /// <summary>
        /// �Ų���ʶ
        /// </summary>
        /// <returns></returns>
        public int? PlanMark { get; set; }
        /// <summary>
        /// ����״̬
        /// </summary>
        /// <returns></returns>
        public int? StepState { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public DateTime? StepDate { get; set; }

        /// <summary>
        /// ���
        /// </summary>
        /// <returns></returns>
        public int? EnterMark { get; set; }
        /// <summary>
        /// �����Id
        /// </summary>
        /// <returns></returns>
        public string EnterUserId { get; set; }
        /// <summary>
        /// �����
        /// </summary>
        /// <returns></returns>
        public string EnterUserName { get; set; }
        /// <summary>
        /// �������
        /// </summary>
        /// <returns></returns>
        public DateTime? EnterDate { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public int? SendMark { get; set; }
        /// <summary>
        /// ������Id
        /// </summary>
        /// <returns></returns>
        public string SendUserId { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        public string SendUserName { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public DateTime? SendDate { get; set; }
        /// <summary>
        /// �ƻ���������
        /// </summary>
        /// <returns></returns>
        public DateTime? SendPlanDate { get; set; }


        /// <summary>
        /// ʵ�ʷ���
        /// </summary>
        /// <returns></returns>
        public int? SendOutMark { get; set; }
        /// <summary>
        /// ʵ�ʷ�����Id
        /// </summary>
        /// <returns></returns>
        public string SendOutUserId { get; set; }
        /// <summary>
        /// ʵ�ʷ�����
        /// </summary>
        /// <returns></returns>
        public string SendOutUserName { get; set; }
        /// <summary>
        /// ʵ�ʷ�������
        /// </summary>
        /// <returns></returns>
        public DateTime? SendOutDate { get; set; }
        /// <summary>
        /// ����ͼƬ
        /// </summary>
        /// <returns></returns>
        public string SendOutImg { get; set; }


        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public int? SendLogisticsMark { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public string SendLogisticsUserId { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public string SendLogisticsUserName { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public DateTime? SendLogisticsDate { get; set; }

        /// <summary>
        /// ������װ
        /// </summary>
        /// <returns></returns>
        public int? SendInstallMark { get; set; }
        /// <summary>
        /// ������װ
        /// </summary>
        /// <returns></returns>
        public string SendInstallUserId { get; set; }
        /// <summary>
        /// ������װ
        /// </summary>
        /// <returns></returns>
        public string SendInstallUserName { get; set; }
        /// <summary>
        /// ������װ
        /// </summary>
        /// <returns></returns>
        public DateTime? SendInstallDate { get; set; }


        /// <summary>
        /// ǩ��ȷ��
        /// </summary>
        /// <returns></returns>
        public int? SignedMark { get; set; }
        /// <summary>
        /// ǩ��ȷ������
        /// </summary>
        /// <returns></returns>
        public DateTime? SignedDate { get; set; }
        /// <summary>
        /// ǩ��ȷ��ͼƬ
        /// </summary>
        /// <returns></returns>
        public string SignedImg { get; set; }
        /// <summary>
        /// ǩ��ȷ����Id
        /// </summary>
        /// <returns></returns>
        public string SignedUserId { get; set; }
        /// <summary>
        /// ǩ��ȷ����
        /// </summary>
        /// <returns></returns>
        public string SignedUserName { get; set; }


        /// <summary>
        /// ɾ��
        /// </summary>
        /// <returns></returns>
        public int? DeleteMark { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public int? EnabledMark { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public int? SortCode { get; set; }
        /// <summary>
        /// �޸�����
        /// </summary>
        /// <returns></returns>
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// �޸��û�����
        /// </summary>
        /// <returns></returns>
        public string ModifyUserId { get; set; }
        /// <summary>
        /// �޸��û�
        /// </summary>
        /// <returns></returns>
        public string ModifyUserName { get; set; }
        /// <summary>
        /// ��ɱ�ʶ
        /// </summary>
        /// <returns></returns>
        public int? OverMark { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        /// <returns></returns>
        public string Remark { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().UserName;
            this.CheckTuMark = 0;//��ͼ
            this.ChaiMark = 0;//��
            this.CheckMark = 0;//���
            this.MoneyMark = 0;//����
            this.MoneyOkMark = 0;//�������
            this.PaymentState = 1;//δ�տ�
            this.MoneyAccounts = 0;//���۽��
            this.ReceivedAmount = 0;//�տ���
            this.DownMark = 0;//�µ�
            this.PushMark = 0;//�Ƶ�
            this.PlanMark = 0;//�Ų�
            this.EnterMark = 0;//���
            this.SendMark = 0;//����֪ͨ
            this.SendOutMark = 0;//ʵ�ʷ���
            this.SignedMark = 0;//ǩ��
            this.OverMark = 0;//�������
            this.DeleteMark = 0;
            this.EnabledMark = 1;
        }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.Id = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}