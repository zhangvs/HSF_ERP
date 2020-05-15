using System;
using System.ComponentModel.DataAnnotations.Schema;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2018-06-01 16:52
    /// �� ����������
    /// </summary>
    public class Buys_OrderEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// ����Id
        /// </summary>
        /// <returns></returns>
        [Column("Id")]
        public string Id { get; set; }
        /// <summary>
        /// ���ݱ��
        /// </summary>
        /// <returns></returns>
        [Column("Code")]
        public string Code { get; set; }
        /// <summary>
        /// ����Id
        /// </summary>
        /// <returns></returns>
        [Column("OrderId")]
        public string OrderId { get; set; }
        /// <summary>
        /// ���۱��
        /// </summary>
        /// <returns></returns>
        [Column("OrderCode")]
        public string OrderCode { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [Column("OrderTitle")]
        public string OrderTitle { get; set; }
        /// <summary>
        /// ����id
        /// </summary>
        /// <returns></returns>
        [Column("ProduceId")]
        public string ProduceId { get; set; }
        /// <summary>
        /// �������
        /// </summary>
        /// <returns></returns>
        [Column("ProduceCode")]
        public string ProduceCode { get; set; }
        /// <summary>
        /// ������Id
        /// </summary>
        /// <returns></returns>
        [Column("CompanyId")]
        public string CompanyId { get; set; }
        /// <summary>
        /// ����������
        /// </summary>
        /// <returns></returns>
        [Column("CompanyName")]
        public string CompanyName { get; set; }
        /// <summary>
        /// �ͻ�Id
        /// </summary>
        /// <returns></returns>
        [Column("CustomerId")]
        public string CustomerId { get; set; }
        /// <summary>
        /// �ͻ�����
        /// </summary>
        /// <returns></returns>
        [Column("CustomerName")]
        public string CustomerName { get; set; }
        /// <summary>
        /// �ͻ��绰
        /// </summary>
        /// <returns></returns>
        public string CustomerTelphone { get; set; }
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
        /// �ƻ���������
        /// </summary>
        /// <returns></returns>
        public DateTime? SendPlanDate { get; set; }


        /// <summary>
        /// �ܰ���
        /// </summary>
        /// <returns></returns>
        [Column("TotalQty")]
        public int? TotalQty { get; set; }
        /// <summary>
        /// �տ�����
        /// </summary>
        /// <returns></returns>
        public DateTime? PaymentDate { get; set; }
        /// <summary>
        /// ����״̬��1-δ����2-���ָ���3-ȫ�����ȫ���տ���ܷ���
        /// </summary>
        /// <returns></returns>
        [Column("PAYMENTSTATE")]
        public int? PaymentState { get; set; }


        /// <summary>
        /// �Ƿ���ȡβ��(����ǰ��ֻ����β��Ҫ��Ҫ��ȡ)
        /// </summary>
        /// <returns></returns>
        public int? AfterMark { get; set; }


        /// <summary>
        /// �������״̬��0�����ڰ�װ��1�����
        /// </summary>
        /// <returns></returns>
        public int? GuiEnterMark { get; set; }
        /// <summary>
        /// �Ű����״̬��0�����ڰ�װ��1�����
        /// </summary>
        /// <returns></returns>
        public int? MenEnterMark { get; set; }
        /// <summary>
        /// ������״̬��0�����ڰ�װ��1�����
        /// </summary>
        /// <returns></returns>
        public int? WuEnterMark { get; set; }
        /// <summary>
        /// ��Э���״̬��0�����ڰ�װ��1�����
        /// </summary>
        /// <returns></returns>
        public int? WaiEnterMark { get; set; }

        /// <summary>
        /// ��ȫ���״̬��0�����ڰ�װ��1�����
        /// </summary>
        /// <returns></returns>
        public int? AllEnterMark { get; set; }
        /// <summary>
        /// ��ȫ���ʱ��
        /// </summary>
        /// <returns></returns>
        public DateTime? AllEnterDate { get; set; }

        /// <summary>
        /// ����֪ͨ
        /// </summary>
        /// <returns></returns>
        public int? SendMark { get; set; }
        /// <summary>
        /// ����֪ͨ��Id
        /// </summary>
        /// <returns></returns>
        public string SendUserId { get; set; }
        /// <summary>
        /// ����֪ͨ��
        /// </summary>
        /// <returns></returns>
        public string SendUserName { get; set; }
        /// <summary>
        /// ����֪ͨ����
        /// </summary>
        /// <returns></returns>
        public DateTime? SendDate { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public string LogisticsName { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public string LogisticsNO { get; set; }
        /// <summary>
        /// �˷�
        /// </summary>
        /// <returns></returns>
        public decimal? LogisticsCost { get; set; }
        /// <summary>
        /// ��װ��
        /// </summary>
        /// <returns></returns>
        public string InstallUserName { get; set; }
        /// <summary>
        /// ��װ��
        /// </summary>
        /// <returns></returns>
        public decimal? InstallCost { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public int? SendOutMark { get; set; }
        /// <summary>
        /// ������Id
        /// </summary>
        /// <returns></returns>
        public string SendOutUserId { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        public string SendOutUserName { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public DateTime? SendOutDate { get; set; }

        /// <summary>
        /// ��ע
        /// </summary>
        /// <returns></returns>
        [Column("DESCRIPTION")]
        public string Description { get; set; }
        /// <summary>
        /// ɾ�����
        /// </summary>
        /// <returns></returns>
        [Column("DELETEMARK")]
        public int? DeleteMark { get; set; }
        /// <summary>
        /// ��Ч��־
        /// </summary>
        /// <returns></returns>
        [Column("ENABLEDMARK")]
        public int? EnabledMark { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [Column("CREATEDATE")]
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// �����û�����
        /// </summary>
        /// <returns></returns>
        [Column("CREATEUSERID")]
        public string CreateUserId { get; set; }
        /// <summary>
        /// �����û�
        /// </summary>
        /// <returns></returns>
        [Column("CREATEUSERNAME")]
        public string CreateUserName { get; set; }
        /// <summary>
        /// �޸�����
        /// </summary>
        /// <returns></returns>
        [Column("MODIFYDATE")]
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// �޸��û�����
        /// </summary>
        /// <returns></returns>
        [Column("MODIFYUSERID")]
        public string ModifyUserId { get; set; }
        /// <summary>
        /// �޸��û�
        /// </summary>
        /// <returns></returns>
        [Column("MODIFYUSERNAME")]
        public string ModifyUserName { get; set; }
        /// <summary>
        /// ��ɱ�ʶ
        /// </summary>
        /// <returns></returns>
        public int? OverMark { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.Id = this.Code;//Guid.NewGuid().ToString()
            this.CreateDate = DateTime.Now;
            //this.CreateUserId = OperatorProvider.Provider.Current().UserId;//��װ�ֻ�΢��ɨ�봴����û��userid
            //this.CreateUserName = OperatorProvider.Provider.Current().UserName;
            //this.PaymentState = 0;
            this.TotalQty = 0;//����ܰ���
            this.AllEnterMark = 0;//��ȫ���
            this.SendMark = 0;//����
            this.SendOutMark = 0;//ʵ�ʷ���
            this.OverMark = 0;//����
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