using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Util.WebControl;
using System.Collections.Generic;

namespace HZSoft.Application.IService.CustomerManage
{
    /// <summary>
    /// 版 本 6.1
    /// 
    /// 创 建：超级管理员
    /// 日 期：2018-06-01 16:52
    /// 描 述：进货单
    /// </summary>
    public interface Buys_OrderIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<Buys_OrderEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        Buys_OrderEntity GetEntity(string keyValue);
        Buys_OrderEntity GetEntityByOrderId(string orderId);
        /// <summary>
        /// 获取子表详细信息
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        IEnumerable<Buys_OrderItemEntity> GetDetails(string keyValue);
        Buys_OrderItemEntity GetDetail(string keyValue, int? sortCode);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        void RemoveForm(string keyValue);
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        void SaveForm(string keyValue, Buys_OrderEntity entity,List<Buys_OrderItemEntity> entryList);
        void SaveSend(string keyValue, Buys_OrderEntity entity);
        void UpdateSendState(string keyValue);
        void SaveBuyMain(Sale_CustomerEntity entity);
        void SaveInForm(Buys_OrderItemEntity entity);
        void SaveLogisticsForm(string keyValue, Buys_OrderEntity entity);
        void SaveInstallForm(string keyValue, Buys_OrderEntity entity);
        #endregion
    }
}
