using System;
using System.ComponentModel.DataAnnotations.Schema;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2017-12-23 14:41
    /// �� ��������ͳ��
    /// </summary>
    public class Sale_Customer_ItemEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// ͳ���ӱ�Id
        /// </summary>
        /// <returns></returns>
        [Column("ID")]
        public string Id { get; set; }
        /// <summary>
        /// ����������
        /// </summary>
        /// <returns></returns>
        [Column("MainId")]
        public string MainId { get; set; }
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
        [Column("PRODUCTUNIT")]
        public string ProductUnit { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [Column("UNITPRICE")]
        public decimal? UnitPrice { get; set; }
        /// <summary>
        /// С�����
        /// </summary>
        /// <returns></returns>
        [Column("SumArea")]
        public decimal? SumArea { get; set; }
        /// <summary>
        /// �ۼ�����
        /// </summary>
        /// <returns></returns>
        [Column("SUMCOUNT")]
        public decimal? SumCount { get; set; }
        /// <summary>
        /// �깤����
        /// </summary>
        /// <returns></returns>
        [Column("EndCount")]
        public decimal? EndCount { get; set; }
        /// <summary>
        /// ʣ������
        /// </summary>
        /// <returns></returns>
        [Column("YUCOUNT")]
        public decimal? YuCount { get; set; }

        /// <summary>
        /// ����id
        /// </summary>
        /// <returns></returns>
        [Column("StepId")]
        public int? StepId { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [Column("Step")]
        public string Step { get; set; }


        /// <summary>
        /// ״̬
        /// </summary>
        /// <returns></returns>
        [Column("STATUS")]
        public int? Status { get; set; }
        /// <summary>
        /// �Ƿ�ɾ��
        /// </summary>
        /// <returns></returns>
        [Column("ISDEL")]
        public int? IsDel { get; set; }
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
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
        }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.Id = keyValue;
            this.ModifyDate = DateTime.Now;
        }
        #endregion
    }
}