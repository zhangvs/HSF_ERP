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
    public class Buys_OrderEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 主键Id
        /// </summary>
        /// <returns></returns>
        [Column("Id")]
        public string Id { get; set; }
        /// <summary>
        /// 单据编号
        /// </summary>
        /// <returns></returns>
        [Column("Code")]
        public string Code { get; set; }
        /// <summary>
        /// 销售Id
        /// </summary>
        /// <returns></returns>
        [Column("OrderId")]
        public string OrderId { get; set; }
        /// <summary>
        /// 销售编号
        /// </summary>
        /// <returns></returns>
        [Column("OrderCode")]
        public string OrderCode { get; set; }
        /// <summary>
        /// 生产id
        /// </summary>
        /// <returns></returns>
        [Column("ProduceId")]
        public string ProduceId { get; set; }
        /// <summary>
        /// 生产编号
        /// </summary>
        /// <returns></returns>
        [Column("ProduceCode")]
        public string ProduceCode { get; set; }
        /// <summary>
        /// 经销商Id
        /// </summary>
        /// <returns></returns>
        [Column("CompanyId")]
        public string CompanyId { get; set; }
        /// <summary>
        /// 经销商名称
        /// </summary>
        /// <returns></returns>
        [Column("CompanyName")]
        public string CompanyName { get; set; }
        /// <summary>
        /// 客户Id
        /// </summary>
        /// <returns></returns>
        [Column("CustomerId")]
        public string CustomerId { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        /// <returns></returns>
        [Column("CustomerName")]
        public string CustomerName { get; set; }
        /// <summary>
        /// 客户电话
        /// </summary>
        /// <returns></returns>
        public string CustomerTelphone { get; set; }
        /// <summary>
        /// 销售人员id
        /// </summary>
        /// <returns></returns>
        public string SalesmanUserId { get; set; }
        /// <summary>
        /// 销售人员
        /// </summary>
        /// <returns></returns>
        public string SalesmanUserName { get; set; }

        /// <summary>
        /// 收货地址
        /// </summary>
        /// <returns></returns>
        public string Address { get; set; }
        /// <summary>
        /// 运输方式
        /// </summary>
        /// <returns></returns>
        public int? ShippingType { get; set; }
        /// <summary>
        /// 运费承担方
        /// </summary>
        /// <returns></returns>
        public int? Carrier { get; set; }
        /// <summary>
        /// 计划发货日期
        /// </summary>
        /// <returns></returns>
        public DateTime? SendPlanDate { get; set; }


        /// <summary>
        /// 总包数
        /// </summary>
        /// <returns></returns>
        [Column("TotalQty")]
        public int? TotalQty { get; set; }
        /// <summary>
        /// 收款日期
        /// </summary>
        /// <returns></returns>
        public DateTime? PaymentDate { get; set; }
        /// <summary>
        /// 付款状态（1-未付款2-部分付款3-全部付款）
        /// </summary>
        /// <returns></returns>
        [Column("PAYMENTSTATE")]
        public int? PaymentState { get; set; }


        /// <summary>
        /// 柜体入库状态，0：正在包装，1：入库
        /// </summary>
        /// <returns></returns>
        public int? GuiEnterMark { get; set; }
        /// <summary>
        /// 门板入库状态，0：正在包装，1：入库
        /// </summary>
        /// <returns></returns>
        public int? MenEnterMark { get; set; }
        /// <summary>
        /// 五金入库状态，0：正在包装，1：入库
        /// </summary>
        /// <returns></returns>
        public int? WuEnterMark { get; set; }

        /// <summary>
        /// 完全入库状态，0：正在包装，1：入库
        /// </summary>
        /// <returns></returns>
        public int? AllEnterMark { get; set; }

        /// <summary>
        /// 发货
        /// </summary>
        /// <returns></returns>
        public int? SendMark { get; set; }
        /// <summary>
        /// 发货人Id
        /// </summary>
        /// <returns></returns>
        public string SendUserId { get; set; }
        /// <summary>
        /// 发货人
        /// </summary>
        /// <returns></returns>
        public string SendUserName { get; set; }
        /// <summary>
        /// 发货日期
        /// </summary>
        /// <returns></returns>
        public DateTime? SendDate { get; set; }

        /// <summary>
        /// 物流名称
        /// </summary>
        /// <returns></returns>
        public string LogisticsName { get; set; }
        /// <summary>
        /// 物流单号
        /// </summary>
        /// <returns></returns>
        public string LogisticsNO { get; set; }
        /// <summary>
        /// 运费
        /// </summary>
        /// <returns></returns>
        public decimal? LogisticsCost { get; set; }
        /// <summary>
        /// 安装工
        /// </summary>
        /// <returns></returns>
        public string InstallUserName { get; set; }
        /// <summary>
        /// 安装费
        /// </summary>
        /// <returns></returns>
        public decimal? InstallCost { get; set; }

        /// <summary>
        /// 发货
        /// </summary>
        /// <returns></returns>
        public int? SendOutMark { get; set; }
        /// <summary>
        /// 发货人Id
        /// </summary>
        /// <returns></returns>
        public string SendOutUserId { get; set; }
        /// <summary>
        /// 发货人
        /// </summary>
        /// <returns></returns>
        public string SendOutUserName { get; set; }
        /// <summary>
        /// 发货日期
        /// </summary>
        /// <returns></returns>
        public DateTime? SendOutDate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        [Column("DESCRIPTION")]
        public string Description { get; set; }
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
        /// 创建日期
        /// </summary>
        /// <returns></returns>
        [Column("CREATEDATE")]
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 创建用户主键
        /// </summary>
        /// <returns></returns>
        [Column("CREATEUSERID")]
        public string CreateUserId { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns></returns>
        [Column("CREATEUSERNAME")]
        public string CreateUserName { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        /// <returns></returns>
        [Column("MODIFYDATE")]
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// 修改用户主键
        /// </summary>
        /// <returns></returns>
        [Column("MODIFYUSERID")]
        public string ModifyUserId { get; set; }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <returns></returns>
        [Column("MODIFYUSERNAME")]
        public string ModifyUserName { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().UserName;
            this.PaymentState = 0;
            this.SendMark = 0;
            this.DeleteMark = 0;
            this.EnabledMark = 1;
        }
        /// <summary>
        /// 编辑调用
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