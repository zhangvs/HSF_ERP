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
    public class Buys_OrderItemEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// ������ϸ����
        /// </summary>
        /// <returns></returns>
        [Column("ORDERENTRYID")]
        public string OrderEntryId { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [Column("ORDERID")]
        public string OrderId { get; set; }
        /// <summary>
        /// ��ƷId
        /// </summary>
        /// <returns></returns>
        [Column("PRODUCTID")]
        public string ProductId { get; set; }
        /// <summary>
        /// ��Ʒ���
        /// </summary>
        /// <returns></returns>
        [Column("PRODUCTCODE")]
        public string ProductCode { get; set; }
        /// <summary>
        /// ��Ʒ����
        /// </summary>
        /// <returns></returns>
        [Column("PRODUCTNAME")]
        public string ProductName { get; set; }
        /// <summary>
        /// ��λ
        /// </summary>
        /// <returns></returns>
        [Column("UNIT")]
        public string Unit { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [Column("QTY")]
        public int? Qty { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [Column("PRICE")]
        public decimal? Price { get; set; }
        /// <summary>
        /// ���
        /// </summary>
        /// <returns></returns>
        [Column("AMOUNT")]
        public decimal? Amount { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        [Column("SORTCODE")]
        public int? SortCode { get; set; }
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
        /// ��ע
        /// </summary>
        /// <returns></returns>
        [Column("DESCRIPTION")]
        public string Description { get; set; }

        /// <summary>
        /// �����û�
        /// </summary>
        /// <returns></returns>
        [Column("CreateItemDate")]
        public DateTime? CreateItemDate { get; set; }
        /// <summary>
        /// �����û�
        /// </summary>
        /// <returns></returns>
        [Column("CreateItemUserId")]
        public string CreateItemUserId { get; set; }
        /// <summary>
        /// �����û�
        /// </summary>
        /// <returns></returns>
        [Column("CreateItemUserName")]
        public string CreateItemUserName { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.OrderEntryId = Guid.NewGuid().ToString();
            this.CreateItemDate = DateTime.Now;
            if (string.IsNullOrEmpty(this.CreateItemUserName))
            {
                this.CreateItemUserId = OperatorProvider.Provider.Current().UserId;
                this.CreateItemUserName = OperatorProvider.Provider.Current().UserName;
            }
        }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.OrderEntryId = keyValue;
            this.CreateItemDate = DateTime.Now;
            this.CreateItemUserId = OperatorProvider.Provider.Current().UserId;
            this.CreateItemUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}