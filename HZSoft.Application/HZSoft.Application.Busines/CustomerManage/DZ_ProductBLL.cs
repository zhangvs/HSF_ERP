using HZSoft.Application.Entity.BaseManage;
using HZSoft.Application.IService.BaseManage;
using HZSoft.Application.Service.BaseManage;
using HZSoft.Util.WebControl;
using System.Collections.Generic;
using System;
using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.IService.CustomerManage;
using HZSoft.Application.Service.CustomerManage;
using System.Data;

namespace HZSoft.Application.Busines.CustomerManage
{
    /// <summary>
    /// 版 本 6.1
    /// 
    /// 创 建：超级管理员
    /// 日 期：2017-12-18 13:59
    /// 描 述：产品分类
    /// </summary>
    public class DZ_ProductBLL
    {
        private DZ_ProductIService service = new DZ_ProductService();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<DZ_ProductEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<DZ_ProductEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }
        /// <summary>
        /// 分类列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DZ_ProductEntity> GetList()
        {
            return service.GetList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public DZ_ProductEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
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
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, DZ_ProductEntity entity)
        {
            try
            {
                service.SaveForm(keyValue, entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 项目值不能重复
        /// </summary>
        /// <param name="Value">项目值</param>
        /// <param name="keyValue">主键</param>
        /// <param name="Id">分类Id</param>
        /// <returns></returns>
        public bool ExistCode(string Code, string keyValue, string Id)
        {
            return service.ExistCode(Code, keyValue, Id);
        }
        /// <summary>
        /// 项目名不能重复
        /// </summary>
        /// <param name="Name">项目名</param>
        /// <param name="keyValue">主键</param>
        /// <param name="Id">分类Id</param>
        /// <returns></returns>
        public bool ExistName(string Name, string keyValue, string Id)
        {
            return service.ExistName(Name, keyValue, Id);
        }
        #endregion


        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <returns></returns>
        public string BatchAddEntity(DataTable dtSource)
        {
            try
            {
                string returnMsg = service.BatchAddEntity(dtSource);
                return returnMsg;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
