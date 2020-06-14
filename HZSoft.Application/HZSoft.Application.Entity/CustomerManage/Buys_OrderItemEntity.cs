using System;
using System.ComponentModel.DataAnnotations.Schema;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2018-06-01 16:52
    /// 描 述：进货单
    /// </summary>
    public class Buys_OrderItemEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 订单明细主键
        /// </summary>
        /// <returns></returns>
        [Column("ORDERENTRYID")]
        public string OrderEntryId { get; set; }
        /// <summary>
        /// 订单主键
        /// </summary>
        /// <returns></returns>
        [Column("ORDERID")]
        public string OrderId { get; set; }
        /// <summary>
        /// 商品Id
        /// </summary>
        /// <returns></returns>
        [Column("PRODUCTID")]
        public string ProductId { get; set; }
        /// <summary>
        /// 商品编号
        /// </summary>
        /// <returns></returns>
        [Column("PRODUCTCODE")]
        public string ProductCode { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        /// <returns></returns>
        [Column("PRODUCTNAME")]
        public string ProductName { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        /// <returns></returns>
        [Column("UNIT")]
        public string Unit { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        /// <returns></returns>
        [Column("QTY")]
        public int? Qty { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        /// <returns></returns>
        [Column("PRICE")]
        public decimal? Price { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        /// <returns></returns>
        [Column("AMOUNT")]
        public decimal? Amount { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>
        /// <returns></returns>
        [Column("SORTCODE")]
        public int? SortCode { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        /// <returns></returns>
        [Column("DELETEMARK")]
        public int? DeleteMark { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>
        /// <returns></returns>
        [Column("ENABLEDMARK")]
        public int? EnabledMark { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        [Column("DESCRIPTION")]
        public string Description { get; set; }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns></returns>
        [Column("CreateItemDate")]
        public DateTime? CreateItemDate { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns></returns>
        [Column("CreateItemUserId")]
        public string CreateItemUserId { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns></returns>
        [Column("CreateItemUserName")]
        public string CreateItemUserName { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
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
        /// 编辑调用
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