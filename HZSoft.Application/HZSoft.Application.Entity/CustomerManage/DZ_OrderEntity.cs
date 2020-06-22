using System;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2019-11-17 08:11
    /// 描 述：DZ_Order
    /// </summary>
    public class DZ_OrderEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// ID
        /// </summary>
        /// <returns></returns>
        public string Id { get; set; }
        /// <summary>
        /// 单号
        /// </summary>
        /// <returns></returns>
        public string Code { get; set; }
        /// <summary>
        /// 订单名称
        /// </summary>
        /// <returns></returns>
        public string OrderTitle { get; set; }
        /// <summary>
        /// 订单类型
        /// </summary>
        /// <returns></returns>
        public int? OrderType { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        /// <returns></returns>
        public string ProductName { get; set; }
        /// <summary>
        /// 经销商id
        /// </summary>
        /// <returns></returns>
        public string CompanyId { get; set; }
        /// <summary>
        /// 经销商
        /// </summary>
        /// <returns></returns>
        public string CompanyName { get; set; }
        /// <summary>
        /// 客户id
        /// </summary>
        /// <returns></returns>
        public string CustomerId { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        /// <returns></returns>
        public string CustomerName { get; set; }
        /// <summary>
        /// 客户电话
        /// </summary>
        /// <returns></returns>
        public string CustomerTelphone { get; set; }
        /// <summary>
        /// 设计师
        /// </summary>
        /// <returns></returns>
        public string DesignerUserName { get; set; }
        /// <summary>
        /// 设计电话
        /// </summary>
        /// <returns></returns>
        public string DesignerTelphone { get; set; }
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
        /// 原图附件
        /// </summary>
        /// <returns></returns>
        public string CreatePath { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
        /// <summary>
        /// 创建用户主键
        /// </summary>
        /// <returns></returns>
        public string CreateUserId { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns></returns>
        public string CreateUserName { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 审图
        /// </summary>
        /// <returns></returns>
        public int? CheckTuMark { get; set; }
        /// <summary>
        /// 审图人Id
        /// </summary>
        /// <returns></returns>
        public string CheckTuUserId { get; set; }
        /// <summary>
        /// 审图人
        /// </summary>
        /// <returns></returns>
        public string CheckTuUserName { get; set; }
        /// <summary>
        /// 审图附件
        /// </summary>
        /// <returns></returns>
        public string CheckTuPath { get; set; }
        /// <summary>
        /// 审图日期
        /// </summary>
        /// <returns></returns>
        public DateTime? CheckTuDate { get; set; }
        /// <summary>
        /// 拆单
        /// </summary>
        /// <returns></returns>
        public int? ChaiMark { get; set; }
        /// <summary>
        /// 拆单人Id
        /// </summary>
        /// <returns></returns>
        public string ChaiUserId { get; set; }
        /// <summary>
        /// 拆单人
        /// </summary>
        /// <returns></returns>
        public string ChaiUserName { get; set; }
        /// <summary>
        /// 拆单附件
        /// </summary>
        /// <returns></returns>
        public string ChaiPath { get; set; }
        /// <summary>
        /// 拆单日期
        /// </summary>
        /// <returns></returns>
        public DateTime? ChaiDate { get; set; }
        /// <summary>
        /// 审核
        /// </summary>
        /// <returns></returns>
        public int? CheckMark { get; set; }
        /// <summary>
        /// 审核人Id
        /// </summary>
        /// <returns></returns>
        public string CheckUserId { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        /// <returns></returns>
        public string CheckUserName { get; set; }
        /// <summary>
        /// 审核日期
        /// </summary>
        /// <returns></returns>
        public DateTime? CheckDate { get; set; }
        /// <summary>
        /// 报价
        /// </summary>
        /// <returns></returns>
        public int? MoneyMark { get; set; }
        /// <summary>
        /// 报价人Id
        /// </summary>
        /// <returns></returns>
        public string MoneyUserId { get; set; }
        /// <summary>
        /// 报价人
        /// </summary>
        /// <returns></returns>
        public string MoneyUserName { get; set; }
        /// <summary>
        /// 报价日期
        /// </summary>
        /// <returns></returns>
        public DateTime? MoneyDate { get; set; }
        /// <summary>
        /// 报价附件
        /// </summary>
        /// <returns></returns>
        public string MoneyPath { get; set; }
        /// <summary>
        /// 报价附件1010
        /// </summary>
        /// <returns></returns>
        public string MoneyPath1010 { get; set; }
        /// <summary>
        /// 报价附件酷家乐
        /// </summary>
        /// <returns></returns>
        public string MoneyPathKuJiaLe { get; set; }
        /// <summary>
        /// 拆单金额
        /// </summary>
        /// <returns></returns>
        public decimal? MoneyAccounts { get; set; }

        /// <summary>
        /// 报价审核
        /// </summary>
        /// <returns></returns>
        public int? MoneyOkMark { get; set; }
        /// <summary>
        /// 报价审核人Id
        /// </summary>
        /// <returns></returns>
        public string MoneyOkUserId { get; set; }
        /// <summary>
        /// 报价审核人
        /// </summary>
        /// <returns></returns>
        public string MoneyOkUserName { get; set; }
        /// <summary>
        /// 报价审核日期
        /// </summary>
        /// <returns></returns>
        public DateTime? MoneyOkDate { get; set; }

        /// <summary>
        /// 应收金额
        /// </summary>
        /// <returns></returns>
        public decimal? Accounts { get; set; }
        /// <summary>
        /// 已收金额
        /// </summary>
        /// <returns></returns>
        public decimal? ReceivedAmount { get; set; }
        /// <summary>
        /// 收款日期
        /// </summary>
        /// <returns></returns>
        public DateTime? PaymentDate { get; set; }
        /// <summary>
        /// 收款状态（1-未收款2-部分收款3-全部收款）
        /// </summary>
        /// <returns></returns>
        public int? PaymentState { get; set; }
        /// <summary>
        /// 是否收取预付款
        /// </summary>
        /// <returns></returns>
        public int? FrontMark { get; set; }
        /// <summary>
        /// 是否收取尾款
        /// </summary>
        /// <returns></returns>
        public int? AfterMark { get; set; }
        /// <summary>
        /// 是否月结
        /// </summary>
        /// <returns></returns>
        public int? MonthMark { get; set; }


        /// <summary>
        /// 下单
        /// </summary>
        /// <returns></returns>
        public int? DownMark { get; set; }
        /// <summary>
        /// 下单附件
        /// </summary>
        /// <returns></returns>
        public string DownPath { get; set; }
        /// <summary>
        /// 下单人Id
        /// </summary>
        /// <returns></returns>
        public string DownUserId { get; set; }
        /// <summary>
        /// 下单人
        /// </summary>
        /// <returns></returns>
        public string DownUserName { get; set; }
        /// <summary>
        /// 下单日期
        /// </summary>
        /// <returns></returns>
        public DateTime? DownDate { get; set; }
        /// <summary>
        /// 撤单原因
        /// </summary>
        /// <returns></returns>
        public string PushBackReason { get; set; }
        /// <summary>
        /// 撤单原因附件
        /// </summary>
        /// <returns></returns>
        public string PushBackPath { get; set; }
        /// <summary>
        /// 撤单原因图片
        /// </summary>
        /// <returns></returns>
        public string PushBackImg { get; set; }
        /// <summary>
        /// 撤单原因备注
        /// </summary>
        /// <returns></returns>
        public string PushBackDesc { get; set; }


        /// <summary>
        /// 推单
        /// </summary>
        /// <returns></returns>
        public int? PushMark { get; set; }

        /// <summary>
        /// 推单日期
        /// </summary>
        /// <returns></returns>
        public DateTime? PushDate { get; set; }


        /// <summary>
        /// 排产标识
        /// </summary>
        /// <returns></returns>
        public int? PlanMark { get; set; }
        /// <summary>
        /// 工序状态
        /// </summary>
        /// <returns></returns>
        public int? StepState { get; set; }
        /// <summary>
        /// 工序日期
        /// </summary>
        /// <returns></returns>
        public DateTime? StepDate { get; set; }

        /// <summary>
        /// 入库
        /// </summary>
        /// <returns></returns>
        public int? EnterMark { get; set; }
        /// <summary>
        /// 入库人Id
        /// </summary>
        /// <returns></returns>
        public string EnterUserId { get; set; }
        /// <summary>
        /// 入库人
        /// </summary>
        /// <returns></returns>
        public string EnterUserName { get; set; }
        /// <summary>
        /// 入库日期
        /// </summary>
        /// <returns></returns>
        public DateTime? EnterDate { get; set; }

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
        /// 计划发货日期
        /// </summary>
        /// <returns></returns>
        public DateTime? SendPlanDate { get; set; }


        /// <summary>
        /// 实际发货
        /// </summary>
        /// <returns></returns>
        public int? SendOutMark { get; set; }
        /// <summary>
        /// 实际发货人Id
        /// </summary>
        /// <returns></returns>
        public string SendOutUserId { get; set; }
        /// <summary>
        /// 实际发货人
        /// </summary>
        /// <returns></returns>
        public string SendOutUserName { get; set; }
        /// <summary>
        /// 实际发货日期
        /// </summary>
        /// <returns></returns>
        public DateTime? SendOutDate { get; set; }
        /// <summary>
        /// 发货图片
        /// </summary>
        /// <returns></returns>
        public string SendOutImg { get; set; }


        /// <summary>
        /// 安排物流
        /// </summary>
        /// <returns></returns>
        public int? SendLogisticsMark { get; set; }
        /// <summary>
        /// 安排物流
        /// </summary>
        /// <returns></returns>
        public string SendLogisticsUserId { get; set; }
        /// <summary>
        /// 安排物流
        /// </summary>
        /// <returns></returns>
        public string SendLogisticsUserName { get; set; }
        /// <summary>
        /// 安排物流
        /// </summary>
        /// <returns></returns>
        public DateTime? SendLogisticsDate { get; set; }

        /// <summary>
        /// 发货安装
        /// </summary>
        /// <returns></returns>
        public int? SendInstallMark { get; set; }
        /// <summary>
        /// 发货安装
        /// </summary>
        /// <returns></returns>
        public string SendInstallUserId { get; set; }
        /// <summary>
        /// 发货安装
        /// </summary>
        /// <returns></returns>
        public string SendInstallUserName { get; set; }
        /// <summary>
        /// 发货安装
        /// </summary>
        /// <returns></returns>
        public DateTime? SendInstallDate { get; set; }


        /// <summary>
        /// 签收确认
        /// </summary>
        /// <returns></returns>
        public int? SignedMark { get; set; }
        /// <summary>
        /// 签收确认日期
        /// </summary>
        /// <returns></returns>
        public DateTime? SignedDate { get; set; }
        /// <summary>
        /// 签收确认图片
        /// </summary>
        /// <returns></returns>
        public string SignedImg { get; set; }
        /// <summary>
        /// 签收确认人Id
        /// </summary>
        /// <returns></returns>
        public string SignedUserId { get; set; }
        /// <summary>
        /// 签收确认人
        /// </summary>
        /// <returns></returns>
        public string SignedUserName { get; set; }


        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public int? DeleteMark { get; set; }
        /// <summary>
        /// 可用
        /// </summary>
        /// <returns></returns>
        public int? EnabledMark { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        /// <returns></returns>
        public int? SortCode { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        /// <returns></returns>
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// 修改用户主键
        /// </summary>
        /// <returns></returns>
        public string ModifyUserId { get; set; }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <returns></returns>
        public string ModifyUserName { get; set; }
        /// <summary>
        /// 完成标识
        /// </summary>
        /// <returns></returns>
        public int? OverMark { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        public string Remark { get; set; }
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
            this.CheckTuMark = 0;//审图
            this.ChaiMark = 0;//拆单
            this.CheckMark = 0;//审核
            this.MoneyMark = 0;//报价
            this.MoneyOkMark = 0;//报价审核
            this.PaymentState = 1;//未收款
            this.MoneyAccounts = 0;//报价金额
            this.ReceivedAmount = 0;//收款金额
            this.DownMark = 0;//下单
            this.PushMark = 0;//推单
            this.PlanMark = 0;//排产
            this.EnterMark = 0;//入库
            this.SendMark = 0;//发货通知
            this.SendOutMark = 0;//实际发货
            this.SignedMark = 0;//签收
            this.OverMark = 0;//结束完成
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