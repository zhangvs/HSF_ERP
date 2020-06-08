using HZSoft.Application.Entity.BaseManage;
using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.IService.BaseManage;
using HZSoft.Application.IService.CustomerManage;
using HZSoft.Data.Repository;
using HZSoft.Util;
using HZSoft.Util.Extension;
using HZSoft.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace HZSoft.Application.Service.CustomerManage
{
    /// <summary>
    /// 版 本 6.1
    /// 
    /// 创 建：超级管理员
    /// 日 期：2017-12-18 13:59
    /// 描 述：产品分类
    /// </summary>
    public class DZ_ProductService : RepositoryFactory<DZ_ProductEntity>, DZ_ProductIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<DZ_ProductEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<DZ_ProductEntity>();
            var queryParam = queryJson.ToJObject();
            string strSql = "select * from DZ_Product where 1 = 1";

            //分类id
            if (!queryParam["Id"].IsEmpty())
            {
                string Id = queryParam["Id"].ToString();
                strSql += " and Id = '" + Id + "'";
            }
            //父分类id
            if (!queryParam["ParentId"].IsEmpty())
            {
                string ParentId = queryParam["ParentId"].ToString();
                strSql += " and ParentId = '" + ParentId + "'";
            }
            //是否是分类
            if (!queryParam["IsTree"].IsEmpty())
            {
                string IsTree = queryParam["IsTree"].ToString();
                strSql += " and IsTree = " + IsTree;
            }

            if (!queryParam["keyword"].IsEmpty())
            {
                string keyword = queryParam["keyword"].ToString();
                strSql += " and (Name like '%" + keyword + "%' or Code like '%" + keyword + "%')";
            }

            return this.BaseRepository().FindList(strSql.ToString(), pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<DZ_ProductEntity> GetList(string queryJson)
        {
            var expression = LinqExtensions.True<DZ_ProductEntity>();
            var queryParam = queryJson.ToJObject();
            string strSql = "select * from DZ_Product where 1 = 1";

            //分类id
            if (!queryParam["Id"].IsEmpty())
            {
                string Id = queryParam["Id"].ToString();
                strSql += " and Id = '" + Id + "'";
            }
            //父分类id
            if (!queryParam["ParentId"].IsEmpty())
            {
                string ParentId = queryParam["ParentId"].ToString();
                strSql += " and ParentId = '" + ParentId + "'";
            }
            //是否是分类
            if (!queryParam["IsTree"].IsEmpty())
            {
                string IsTree = queryParam["IsTree"].ToString();
                strSql += " and IsTree = " + IsTree;
            }

            if (!queryParam["keyword"].IsEmpty())
            {
                string keyword = queryParam["keyword"].ToString();
                strSql += " and (Name like '%" + keyword + "%' or Code like '%" + keyword + "%')";
            }
            return this.BaseRepository().FindList(strSql.ToString());
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>返回列表</returns>
        public IEnumerable<DZ_ProductEntity> GetList()
        {
            return this.BaseRepository().IQueryable().ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public DZ_ProductEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, DZ_ProductEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                entity.Create();
                this.BaseRepository().Insert(entity);
            }
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 项目值不能重复
        /// </summary>
        /// <param name="Code">项目值</param>
        /// <param name="keyValue">主键</param>
        /// <param name="Id">分类Id</param>
        /// <returns></returns>
        public bool ExistCode(string Code, string keyValue, string Id)
        {
            var expression = LinqExtensions.True<DZ_ProductEntity>();
            expression = expression.And(t => t.Code == Code);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.Id != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
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
            var expression = LinqExtensions.True<DZ_ProductEntity>();
            expression = expression.And(t => t.Name == Name);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.Id != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        #endregion

        /// <summary>
        /// 批量（新增）
        /// </summary>
        /// <param name="dtSource">实体对象</param>
        /// <returns></returns>
        public string BatchAddEntity(DataTable dtSource)
        {
            int rowsCount = dtSource.Rows.Count;
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();

            int columns = dtSource.Columns.Count;
            string cf = "";
            for (int i = 0; i < rowsCount; i++)
            {
                try
                {
                    //类别
                    string ParentName = dtSource.Rows[i][0].ToString();
                    string ParentId = "";
                    if (string.IsNullOrEmpty(ParentName))
                    {
                        return ParentName + "类型为空";
                    }
                    var productData = db.FindEntity<DZ_ProductEntity>(t => t.Name == ParentName && t.DeleteMark != 1);
                    if (productData == null)
                    {
                        return ParentName + "：类型不存在，请先添加分类";
                    }
                    else
                    {
                        ParentId = productData.Id;
                    }
                    //名称
                    string Name = dtSource.Rows[i][1].ToString();
                    if (string.IsNullOrEmpty(Name))
                    {
                        return "名称为空：" + i;
                    }
                    //编号
                    string Code = dtSource.Rows[i][2].ToString();
                    var liang_Data = db.FindEntity<DZ_ProductEntity>(t => t.Code == Code && t.DeleteMark != 1);
                    if (liang_Data != null)
                    {
                        return "编号重复：" + Code;
                    }

                    //规格
                    string Guige = dtSource.Rows[i][3].ToString();
                    //单位
                    string Unit = dtSource.Rows[i][4].ToString();
                    //报价1
                    string Plan1 = dtSource.Rows[i][5].ToString();
                    //报价2
                    string Plan2 = dtSource.Rows[i][6].ToString();
                    //报价3
                    string Plan3 = dtSource.Rows[i][7].ToString();
                    //报价4
                    string Plan4 = dtSource.Rows[i][8].ToString();
                    //备注
                    string desc = dtSource.Rows[i][9].ToString();

                    //添加靓号
                    DZ_ProductEntity entity = new DZ_ProductEntity()
                    {
                        ParentId = ParentId,
                        ParentName = ParentName,
                        Name = Name,
                        Code = Code,
                        Guige = Guige,
                        Unit = Unit,
                        Plan1 = Plan1 == "" ? 0 : Convert.ToDecimal(Plan1),
                        Plan2 = Plan2 == "" ? 0 : Convert.ToDecimal(Plan2),
                        Plan3 = Plan3 == "" ? 0 : Convert.ToDecimal(Plan3),
                        Plan4 = Plan4 == "" ? 0 : Convert.ToDecimal(Plan4),
                        IsTree = 0,
                        SortCode = Convert.ToInt32(Code),//Covert.ToInt32()在null时不抛异常而是返回0,而num=int.Parse(str);是要抛出异常的。
                        Description = desc
                    };
                    entity.Create();
                    db.Insert(entity);
                }
                catch (Exception ex)
                {
                    LogHelper.AddLog(ex.Message);
                    return ex.Message;
                }

            }
            db.Commit();
            if (cf != "")
            {
                LogHelper.AddLog("跳过重复导入：" + cf);
                return "跳过重复导入：" + cf;
            }
            else
            {
                return "导入成功";
            }

        }
    }
}
