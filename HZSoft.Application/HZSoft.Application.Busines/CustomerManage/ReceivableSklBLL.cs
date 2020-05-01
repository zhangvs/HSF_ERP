using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.IService.CustomerManage;
using HZSoft.Application.Service.CustomerManage;
using HZSoft.Util.WebControl;
using System.Collections.Generic;
using System;

namespace HZSoft.Application.Busines.CustomerManage
{
    /// <summary>
    /// 版 本 6.1
    /// 
    /// 创 建：佘赐雄
    /// 日 期：2016-04-06 17:24
    /// 描 述：应收账款
    /// </summary>
    public class ReceivableSklBLL
    {
        private IReceivableSklService service = new ReceivableSklService();

        #region 获取数据
        /// <summary>
        /// 获取收款单列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<ReceivableEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 获取收款单列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<DZ_OrderEntity> GetPaymentPageList(Pagination pagination, string queryJson)
        {
            return service.GetPaymentPageList(pagination, queryJson);
        }
        /// <summary>
        /// 获取子收款记录
        /// </summary>
        /// <param name="orderId">订单主键</param>
        /// <returns></returns>
        public IEnumerable<ReceivableEntity> GetPaymentRecord(string orderId)
        {
            return service.GetPaymentRecord(orderId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId">订单子主键</param>
        /// <returns></returns>
        public ReceivableEntity GetEntity(string orderId)
        {
            return service.GetEntity(orderId);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存表单（新增）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(ReceivableEntity entity)
        {
            try
            {
                service.SaveForm(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// 保存表单（新增）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void UpdateStateForm(string keyValue, ReceivableEntity entity)
        {
            try
            {
                service.UpdateStateForm(keyValue, entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}