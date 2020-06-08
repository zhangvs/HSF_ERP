using HZSoft.Application.Entity.BaseManage;
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
    /// 日 期：2017-12-18 13:59
    /// 描 述：产品分类
    /// </summary>
    public interface DZ_ProductIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<DZ_ProductEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<DZ_ProductEntity> GetList(string queryJson);
        IEnumerable<DZ_ProductEntity> GetList();
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        DZ_ProductEntity GetEntity(string keyValue);
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
        void SaveForm(string keyValue, DZ_ProductEntity entity);
        /// <summary>
        /// 批量（新增）
        /// </summary>
        /// <param name="dtSource">实体对象</param>
        /// <returns></returns>
        string BatchAddEntity(DataTable dtSource);
        #endregion

        #region 验证数据
        /// <summary>
        /// 项目值不能重复
        /// </summary>
        /// <param name="Value">项目值</param>
        /// <param name="keyValue">主键</param>
        /// <param name="Id">分类Id</param>
        /// <returns></returns>
        bool ExistCode(string Code, string keyValue, string Id);
        /// <summary>
        /// 项目名不能重复
        /// </summary>
        /// <param name="Name">项目名</param>
        /// <param name="keyValue">主键</param>
        /// <param name="Id">分类Id</param>
        /// <returns></returns>
        bool ExistName(string Name, string keyValue, string Id);
        #endregion
    }
}
