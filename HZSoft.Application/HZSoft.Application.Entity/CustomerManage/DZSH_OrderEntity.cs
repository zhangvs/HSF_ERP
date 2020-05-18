using System;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ��������
    /// �� �ڣ�2020-05-18 09:28
    /// �� �������ߵ�
    /// </summary>
    public class DZSH_OrderEntity : BaseEntity
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
        /// ����
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
        /// ����������
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
        /// �ջ���ַ
        /// </summary>
        /// <returns></returns>
        public string Address { get; set; }
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
        /// ���۵绰
        /// </summary>
        /// <returns></returns>
        public string SalesmanTelphone { get; set; }
        /// <summary>
        /// ���۽��
        /// </summary>
        /// <returns></returns>
        public decimal? Accounts { get; set; }
        /// <summary>
        /// Ԥ����
        /// </summary>
        /// <returns></returns>
        public int? FrontMark { get; set; }
        /// <summary>
        /// β��
        /// </summary>
        /// <returns></returns>
        public int? AfterMark { get; set; }
        /// <summary>
        /// �½�
        /// </summary>
        /// <returns></returns>
        public int? MonthMark { get; set; }
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
        /// �տ�״̬
        /// </summary>
        /// <returns></returns>
        public int? PaymentState { get; set; }
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
        /// �ӵ�����
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
        /// ���
        /// </summary>
        /// <returns></returns>
        public int? OverMark { get; set; }
        /// <summary>
        /// ��Ҫ����
        /// </summary>
        /// <returns></returns>
        public int? NeedSale { get; set; }
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