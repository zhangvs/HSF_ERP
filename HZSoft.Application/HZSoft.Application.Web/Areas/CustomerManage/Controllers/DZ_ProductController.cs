using HZSoft.Util;
using HZSoft.Util.WebControl;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;
using HZSoft.Application.Busines.CustomerManage;
using HZSoft.Application.Entity.CustomerManage;
using System.Web;
using HZSoft.Application.Code;
using System.IO;
using System;

namespace HZSoft.Application.Web.Areas.CustomerManage.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// 
    /// 创 建：超级管理员
    /// 日 期：2017-12-15 15:02
    /// 描 述：产品表
    /// </summary>
    public class DZ_ProductController : MvcControllerBase
    {
        private DZ_ProductBLL productbll = new DZ_ProductBLL();

        #region 视图功能
        /// <summary>
        /// 分类+详情
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DZ_ProductIndex()
        {
            return View();
        }
        /// <summary>
        /// 分类
        /// </summary>
        /// <returns></returns>
        public ActionResult DZ_ProductTypeIndex()
        {
            return View();
        }
        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DZ_ProductTypeForm()
        {
            return View();
        }
        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DZ_ProductForm()
        {
            return View();
        }
        /// <summary>
        /// 详细页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DZ_ProductDetail()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 分类列表 "{\"ParentId\":\"0\"}"
        /// </summary>
        /// <param name="keyword">关键字查询</param>
        /// <returns>返回树形Json</returns>
        [HttpGet]
        public ActionResult GetTreeJson(string keyword, string _parentId)
        {
            var data = productbll.GetList().OrderBy(t => t.SortCode).ToList();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.TreeWhere(t => t.Name.Contains(keyword), "");
            }
            if (!string.IsNullOrEmpty(_parentId))
            {
                data = data.TreeWhere(t => t.ParentId == _parentId, "Id");//主键Id
            }
            var treeList = new List<TreeEntity>();
            foreach (DZ_ProductEntity item in data)
            {
                TreeEntity tree = new TreeEntity();
                bool hasChildren = data.Count(t => t.ParentId == item.Id) == 0 ? false : true;
                tree.id = item.Id;
                tree.text = item.Name;
                tree.value = item.Code;
                tree.parentId = item.ParentId;
                tree.isexpand = true;
                tree.complete = true;
                tree.Attribute = "isTree";
                tree.AttributeValue = item.IsTree.ToString();
                tree.hasChildren = hasChildren;
                treeList.Add(tree);
            }
            return Content(treeList.TreeToJson());
        }
        /// <summary>
        /// 明细列表
        /// </summary>
        /// <param name="typeId">分类Id</param>
        /// <param name="keyword">关键字查询</param>
        /// <returns>返回树形列表Json</returns>
        [HttpGet]
        public ActionResult GetTreeListJson(string typeId, string condition, string keyword)
        {
            var data = productbll.GetList("{\"ParentId\":\"" + typeId + "\"}").OrderBy(t => t.SortCode).ToList();
            if (!string.IsNullOrEmpty(keyword))
            {
                #region 多条件查询
                switch (condition)
                {
                    case "Name":        //产品名
                        data = data.TreeWhere(t => t.Name.Contains(keyword), "Id");
                        break;
                    case "Code":      //产品编号
                        data = data.TreeWhere(t => t.Code.Contains(keyword), "Id");
                        break;
                    default:
                        break;
                }
                #endregion
            }
            var TreeList = new List<TreeGridEntity>();
            foreach (DZ_ProductEntity item in data)
            {
                TreeGridEntity tree = new TreeGridEntity();
                bool hasChildren = data.Count(t => t.ParentId == item.ParentId) == 0 ? false : true;
                tree.id = item.Id;
                tree.parentId = item.ParentId;
                tree.expanded = true;
                tree.hasChildren = hasChildren;
                tree.entityJson = item.ToJson();
                TreeList.Add(tree);
            }
            return Content(TreeList.TreeJson());
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = productbll.GetPageList(pagination, queryJson);
            var jsonData = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return ToJsonResult(jsonData);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = productbll.GetList(queryJson);
            return ToJsonResult(data);
        }
        /// <summary>
        /// 获取实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = productbll.GetEntity(keyValue);
            return ToJsonResult(data);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult RemoveForm(string keyValue)
        {
            productbll.RemoveForm(keyValue);
            return Success("删除成功。");
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, DZ_ProductEntity entity)
        {
            productbll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 项目值不能重复
        /// </summary>
        /// <param name="Value">项目值</param>
        /// <param name="keyValue">主键</param>
        /// <param name="itemId">分类Id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ExistValue(string Value, string keyValue, string itemId)
        {
            bool IsOk = productbll.ExistValue(Value, keyValue, itemId);
            return Content(IsOk.ToString());
        }
        /// <summary>
        /// 项目名不能重复
        /// </summary>
        /// <param name="Name">项目名</param>
        /// <param name="keyValue">主键</param>
        /// <param name="itemId">分类Id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ExistName(string Name, string keyValue, string itemId)
        {
            bool IsOk = productbll.ExistName(Name, keyValue, itemId);
            return Content(IsOk.ToString());
        }
        #endregion

        /// <summary>
        /// 上传产品
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadProductPicture()
        {
            string Message = "";
            if (Request.Files.Count > 0)
            {
                HttpPostedFile file = System.Web.HttpContext.Current.Request.Files[0];
                if (file.ContentLength > 0)
                {
                    string fileExt = file.FileName.Substring(file.FileName.LastIndexOf('.'));//后缀
                    try
                    {
                        string uploadDate = OperatorProvider.Provider.Current().Account;
                        string dir = string.Format("/Resource/Product/{0}/", uploadDate);
                        if (Directory.Exists(Server.MapPath(dir)) == false)//如果不存在就创建file文件夹
                        {
                            Directory.CreateDirectory(Server.MapPath(dir));
                        }
                        //string newfileName = DateTime.Now.ToString("yyyyMMddHHmmss");
                        //原图
                        //string fullDir1 = dir + newfileName + fileExt;
                        string fullDir1 = dir + file.FileName;
                        string imgFilePath = Request.MapPath(fullDir1);
                        file.SaveAs(imgFilePath);

                        return Content(new JsonMessage { Success = true, Code = "0", Message = fullDir1 }.ToString());

                    }
                    catch (Exception ex)
                    {
                        Message = HttpUtility.HtmlEncode(ex.Message);
                        return Content(new JsonMessage { Success = false, Code = "-1", Message = Message }.ToString());
                    }
                }
                Message = "请选择要上传的文件！";
                return Content(new JsonMessage { Success = false, Code = "-1", Message = Message }.ToString());
            }
            Message = "请选择要上传的文件！";
            return Content(new JsonMessage { Success = false, Code = "-1", Message = Message }.ToString());
        }
    }
}
