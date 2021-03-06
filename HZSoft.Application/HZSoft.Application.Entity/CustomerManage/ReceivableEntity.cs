﻿using System;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：张彦山
    /// 日 期：2016-04-06 17:24
    /// 描 述：应收账款
    /// </summary>
    public class ReceivableEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 账款主键
        /// </summary>
        /// <returns></returns>
        public string ReceivableId { get; set; }
        /// <summary>
        /// 订单主键
        /// </summary>
        /// <returns></returns>
        public string OrderId { get; set; }
        /// <summary>
        /// 单据编号
        /// </summary>
        /// <returns></returns>
        public string Code { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        /// <returns></returns>
        public string OrderTitle { get; set; }
        /// <summary>
        /// 订单类型
        /// </summary>
        /// <returns></returns>
        public int? OrderType { get; set; }
        /// <summary>
        /// 经销商
        /// </summary>
        /// <returns></returns>
        public string CompanyName { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        /// <returns></returns>
        public string CustomerName { get; set; }
        /// <summary>
        /// 销售人员
        /// </summary>
        /// <returns></returns>
        public string SalesmanUserName { get; set; }
        /// <summary>
        /// 收款日期
        /// </summary>
        /// <returns></returns>
        public DateTime? PaymentTime { get; set; }
        /// <summary>
        /// 收款金额
        /// </summary>
        /// <returns></returns>
        public decimal? PaymentPrice { get; set; }
        /// <summary>
        /// 收款方式
        /// </summary>
        /// <returns></returns>
        public string PaymentMode { get; set; }
        /// <summary>
        /// 收款账户
        /// </summary>
        /// <returns></returns>
        public string PaymentAccount { get; set; }
        /// <summary>
        /// 收据
        /// </summary>
        /// <returns></returns>
        public string ReceiptPath { get; set; }

        /// <summary>
        /// 排序码
        /// </summary>
        /// <returns></returns>
        public int? SortCode { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        /// <returns></returns>
        public int? DeleteMark { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>
        /// <returns></returns>
        public int? EnabledMark { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateDate { get; set; }
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
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.ReceivableId = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().UserName;
            this.DeleteMark = 0;
            this.EnabledMark = 0;//未确认                                      数据库设置默认值是不靠谱的，不能赋值
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.ReceivableId = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}