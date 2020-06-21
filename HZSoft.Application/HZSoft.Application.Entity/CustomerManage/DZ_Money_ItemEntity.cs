using System;
using System.ComponentModel.DataAnnotations.Schema;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超管
    /// 日 期：2020-06-11 11:28
    /// 描 述：报价房间
    /// </summary>
    public class DZ_Money_ItemEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        /// <returns></returns>
        [Column("ITEMID")]
        public string ItemId { get; set; }
        /// <summary>
        /// 订单Id
        /// </summary>
        /// <returns></returns>
        [Column("ORDERID")]
        public string OrderId { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        /// <returns></returns>
        [Column("ORDERCODE")]
        public string OrderCode { get; set; }
        /// <summary>
        /// 房间Id
        /// </summary>
        /// <returns></returns>
        [Column("ROOMID")]
        public string RoomId { get; set; }
        /// <summary>
        /// 房间名称
        /// </summary>
        /// <returns></returns>
        [Column("ROOMNAME")]
        public string RoomName { get; set; }
        /// <summary>
        /// 产品Id
        /// </summary>
        /// <returns></returns>
        [Column("PRODUCTID")]
        public string ProductId { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        /// <returns></returns>
        [Column("PRODUCTCODE")]
        public string ProductCode { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        /// <returns></returns>
        [Column("PRODUCTNAME")]
        public string ProductName { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        /// <returns></returns>
        [Column("Guige")]
        public string Guige { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        /// <returns></returns>
        [Column("COUNT")]
        public int? Count { get; set; }
        /// <summary>
        /// 面积
        /// </summary>
        /// <returns></returns>
        [Column("Area")]
        public decimal? Area { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        /// <returns></returns>
        [Column("UNIT")]
        public string Unit { get; set; }
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
        /// 排序
        /// </summary>
        /// <returns></returns>
        [Column("SORT")]
        public int? Sort { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        /// <returns></returns>
        [Column("DESCRIPTION")]
        public string Description { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        /// <returns></returns>
        [Column("CREATEDATE")]
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        /// <returns></returns>
        [Column("MODIFYDATE")]
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        /// <returns></returns>
        [Column("Source")]
        public string Source { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.ItemId = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
                                }
        /// <summary>
        /// 编辑调用
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