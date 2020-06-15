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
    /// 创 建：超级管理员
    /// 日 期：2017-12-23 14:41
    /// 描 述：余量统计
    /// </summary>
    public class Sale_CustomerBLL
    {
        private Sale_CustomerIService service = new Sale_CustomerService();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<Sale_CustomerEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<Sale_CustomerEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Sale_CustomerEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        /// <summary>
        /// 获取子表详细信息
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IEnumerable<Sale_Customer_ItemEntity> GetDetails(string keyValue)
        {
            return service.GetDetails(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            try
            {
                service.RemoveForm(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 生产单编辑
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, Sale_CustomerEntity entity,List<Sale_Customer_ItemEntity> entryList)
        {
            try
            {
                service.SaveForm(keyValue, entity, entryList);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void UpdateStepState(Sale_CustomerEntity entity)
        {
            try
            {
                service.UpdateStepState(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void SaveDownForm(string keyValue, Sale_CustomerEntity entity)
        {
            try
            {
                service.SaveDownForm(keyValue, entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void SavePlanForm(string keyValue, Sale_CustomerEntity entity)
        {
            try
            {
                service.SavePlanForm(keyValue, entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void SaveStepBackForm(string keyValue, int? step, string name)
        {
            try
            {
                service.SaveStepBackForm(keyValue, step, name);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 推单,撤单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="state">状态1推单-1撤单</param>
        /// <param name="orderId">销售单id</param>
        /// <returns></returns>
        public void UpdatePushState(string keyValue, int? state, string orderId)
        {
            try
            {
                service.UpdatePushState(keyValue, state, orderId);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public void SavePushBackForm(string keyValue, Sale_CustomerEntity entity)
        {
            try
            {
                service.SavePushBackForm(keyValue, entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
