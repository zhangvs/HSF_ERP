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
    public class DZ_Money_RoomEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 房间主键
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
        /// 金额
        /// </summary>
        /// <returns></returns>
        [Column("ROOMAMOUNT")]
        public decimal? RoomAmount { get; set; }
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
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.RoomId = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
                                }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.RoomId = keyValue;
            this.ModifyDate = DateTime.Now;
                                }
        #endregion
    }
}