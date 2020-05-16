﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZSoft.Application.Code
{
    /// <summary>
    /// 自动编号 规则分类枚举
    /// </summary>
    public enum CodeRuleEnum
    {
        /// <summary>
        /// 客户关系-商机编号
        /// </summary>
        Customer_ChanceCode = 10002,
        /// <summary>
        /// 客户关系-客户订单单号
        /// </summary>
        Customer_CustomerCode = 10003,
        /// <summary>
        /// 客户关系-客户编号
        /// </summary>
        Customer_OrderCode = 10004,
        /// <summary>
        /// 客户关系-接单
        /// </summary>
        Customer_DZOrder = 10007,
        /// <summary>
        /// 生产入库单
        /// </summary>
        Buy_Order = 10008,

    }
}