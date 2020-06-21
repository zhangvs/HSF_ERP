using System;
using System.ComponentModel.DataAnnotations.Schema;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ��������
    /// �� �ڣ�2020-06-11 11:28
    /// �� �������۷���
    /// </summary>
    public class DZ_Money_ItemEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [Column("ITEMID")]
        public string ItemId { get; set; }
        /// <summary>
        /// ����Id
        /// </summary>
        /// <returns></returns>
        [Column("ORDERID")]
        public string OrderId { get; set; }
        /// <summary>
        /// �������
        /// </summary>
        /// <returns></returns>
        [Column("ORDERCODE")]
        public string OrderCode { get; set; }
        /// <summary>
        /// ����Id
        /// </summary>
        /// <returns></returns>
        [Column("ROOMID")]
        public string RoomId { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [Column("ROOMNAME")]
        public string RoomName { get; set; }
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
        /// ���
        /// </summary>
        /// <returns></returns>
        [Column("Guige")]
        public string Guige { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [Column("COUNT")]
        public int? Count { get; set; }
        /// <summary>
        /// ���
        /// </summary>
        /// <returns></returns>
        [Column("Area")]
        public decimal? Area { get; set; }
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
        [Column("PRICE")]
        public decimal? Price { get; set; }
        /// <summary>
        /// ���
        /// </summary>
        /// <returns></returns>
        [Column("AMOUNT")]
        public decimal? Amount { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [Column("SORT")]
        public int? Sort { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [Column("DESCRIPTION")]
        public string Description { get; set; }
        /// <summary>
        /// ���ʱ��
        /// </summary>
        /// <returns></returns>
        [Column("CREATEDATE")]
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// �޸�����
        /// </summary>
        /// <returns></returns>
        [Column("MODIFYDATE")]
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// ��Դ
        /// </summary>
        /// <returns></returns>
        [Column("Source")]
        public string Source { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.ItemId = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
                                }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.ItemId = keyValue;
            this.ModifyDate = DateTime.Now;
                                }
        #endregion
    }
}