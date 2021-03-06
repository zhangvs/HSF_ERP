﻿using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Util.WebControl;
using System.Collections.Generic;

namespace HZSoft.Application.IService.CustomerManage
{
    /// <summary>
    /// 版 本 6.1
    /// 
    /// 创 建：张彦山
    /// 日 期：2016-04-06 17:24
    /// 描 述：应收账款
    /// </summary>
    public interface IReceivableSklService
    {
        #region 获取数据
        /// <summary>
        /// 获取收款单列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<ReceivableEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取收款单列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<DZ_OrderEntity> GetPaymentPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取收款记录列表
        /// </summary>
        /// <param name="orderId">订单主键</param>
        /// <returns></returns>
        IEnumerable<ReceivableEntity> GetPaymentRecord(string orderId);

        /// <summary>
        /// 获取子收款记录
        /// </summary>
        /// <param name="orderId">订单主键</param>
        ReceivableEntity GetEntity(string keyValue);
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存表单（新增）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        void SaveForm(ReceivableEntity entity);

        /// <summary>
        /// 修改收款确认
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        void UpdateStateForm(string keyValue, ReceivableEntity entity);
        #endregion
    }
}