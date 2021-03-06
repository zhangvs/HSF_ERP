using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Util.WebControl;
using System.Collections.Generic;
using System.Data;

namespace HZSoft.Application.IService.CustomerManage
{
    /// <summary>
    /// 版 本 6.1
    /// 
    /// 创 建：超级管理员
    /// 日 期：2019-11-17 08:11
    /// 描 述：DZ_Order
    /// </summary>
    public interface DZ_OrderIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<DZ_OrderEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<DZ_OrderEntity> GetList(string queryJson);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        DZ_OrderEntity GetEntity(string keyValue);
        #endregion

        #region 提交数据
        /// <summary>
        /// 一键驳回
        /// </summary>
        /// <param name="keyValue">主键</param>
        void BackForm(string keyValue, DZ_OrderEntity entity);
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
        void SaveForm(string keyValue, DZ_OrderEntity entity);
        /// <summary>
        /// 报价系统保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        void SaveMoneyForm(string keyValue, DZ_OrderEntity entity, List<DZ_Money_ItemEntity> entryList);
        void SaveSigned(string keyValue, DZ_OrderEntity entity);
        void UpdateMoneyOkState(string keyValue,int? state);
        void UpdateOverState(string keyValue, int? state);
        #endregion
        /// <summary>
        /// 批量（新增）
        /// </summary>
        /// <param name="dtSource">实体对象</param>
        /// <returns></returns>
        string BatchAddEntity(string keyValue, DataTable dtSource,string dir);
        string BatchAddEntity1010(string keyValue, DataTable dtSource, string dir);
        string BatchAddEntity_cw(string keyValue, DataTable dtSource, string dir);
        string BatchAddEntity1010_cw(string keyValue, DataTable dtSource, string dir);
        /// <summary>
        /// 清空导入数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        void RemoveImportForm(string keyValue);
    }
}
