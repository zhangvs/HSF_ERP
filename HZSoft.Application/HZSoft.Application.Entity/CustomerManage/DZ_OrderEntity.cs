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
        /// ���۸���
        /// </summary>
        /// <returns></returns>
        public string MoneyPath { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public DateTime? MoneyDate { get; set; }
        /// <summary>
        /// �𵥽��
        /// </summary>
        /// <returns></returns>
        public decimal? MoneyAccounts { get; set; }

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
            this.CheckTuMark = 0;
            this.ChaiMark = 0;
            this.CheckMark = 0;
            this.MoneyMark = 0;
            this.PaymentState = 1;//δ�տ�
            this.MoneyAccounts = 0;//���۽��
            this.ReceivedAmount = 0;//�տ���
            this.DownMark = 0;//�µ�
            this.EnterMark = 0;
            this.SendMark = 0;
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