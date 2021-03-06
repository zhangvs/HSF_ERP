using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Data.Repository;
using HZSoft.Util.WebControl;
using System.Collections.Generic;
using System.Data;

namespace HZSoft.Application.IService.CustomerManage
{
    /// <summary>
    /// 版 本 6.1
    /// 
    /// 创 建：超级管理员
    /// 日 期：2017-12-23 14:41
    /// 描 述：余量统计
    /// </summary>
    public interface Sale_CustomerIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<Sale_CustomerEntity> GetPageList(Pagination pagination, string queryJson);
        IEnumerable<Sale_CustomerEntity> GetList(string queryJson);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        Sale_CustomerEntity GetEntity(string keyValue);
        /// <summary>
        /// 获取子表详细信息
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        IEnumerable<Sale_Customer_ItemEntity> GetDetails(string keyValue);
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
        void SaveForm(string keyValue, Sale_CustomerEntity entity,List<Sale_Customer_ItemEntity> entryList);
        /// <summary>
        /// 下单
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        void SaveDownForm(string keyValue, Sale_CustomerEntity entity);
        /// <summary>
        /// 推单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="state">状态1推单，-1撤单</param>
        /// <param name="orderId">销售单id</param>
        /// <returns></returns>
        void UpdatePushState(string keyValue, int? state, string orderId);
        /// <summary>
        /// 撤单
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        void SavePushBackForm(string keyValue, Sale_CustomerEntity entity);
        /// <summary>
        /// 排产
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        void SavePlanForm(string keyValue, Sale_CustomerEntity entity);
        /// <summary>
        /// 工序撤销
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        void SaveStepBackForm(string keyValue, int? step, string name);
        /// <summary>
        /// 工序状态修改
        /// </summary>
        /// <param name="entity"></param>
        void UpdateStepState(Sale_CustomerEntity entity);
        string BatchAddEntityLiao(string keyValue, DataTable dtSource, string dir);
        #endregion
    }
}
