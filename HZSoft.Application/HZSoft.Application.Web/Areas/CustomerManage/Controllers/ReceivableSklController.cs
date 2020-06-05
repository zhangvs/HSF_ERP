using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.Busines.CustomerManage;
using HZSoft.Util;
using HZSoft.Util.WebControl;
using System.Web.Mvc;
using System.Web;
using HZSoft.Application.Code;
using System.IO;
using System;

namespace HZSoft.Application.Web.Areas.CustomerManage.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// 
    /// 创 建：张彦山
    /// 日 期：2016-04-06 17:24
    /// 描 述：应收账款
    /// </summary>
    public class ReceivableSklController : MvcControllerBase
    {
        private ReceivableSklBLL receivablebll = new ReceivableSklBLL();

        #region 视图功能
        /// <summary>
        /// 收款
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 收款确认-折叠
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CheckIndex()
        {
            return View();
        }
        /// <summary>
        /// 收款确认-每条
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CheckItemIndex()
        {
            return View();
        }
        /// <summary>
        /// 收款确认-每条
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CheckItemOverIndex()
        {
            return View();
        }
        /// <summary>
        /// 收款页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ReceiptForm()
        {
            return View();
        }
        /// <summary>
        /// 收款详细页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ReceiptSklForm()
        {
            return View();
        }

        /// <summary>
        /// 收款详细页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ReceiptItemForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取收款单列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = receivablebll.GetPageList(pagination, queryJson);
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
        /// 获取收款单列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetPaymentPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = receivablebll.GetPaymentPageList(pagination, queryJson);
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
        /// 获取收款记录列表
        /// </summary>
        /// <param name="orderId">订单Id</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetPaymentRecordJson(string orderId)
        {
            var data = receivablebll.GetPaymentRecord(orderId);
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
            var data = receivablebll.GetEntity(keyValue);
            return ToJsonResult(data);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存表单（新增）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(ReceivableEntity entity)
        {
            receivablebll.SaveForm(entity);
            return Success("操作成功。");
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
        public ActionResult UpdateStateForm(string keyValue, ReceivableEntity entity)
        {
            receivablebll.UpdateStateForm(keyValue, entity);
            return Success("操作成功。");
        }

        /// <summary>
        /// 上传签收单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadPicture()
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
                        string dir = string.Format("/Resource/DocumentFile/Receipt/{0}/", uploadDate);
                        if (Directory.Exists(Server.MapPath(dir)) == false)//如果不存在就创建file文件夹
                        {
                            Directory.CreateDirectory(Server.MapPath(dir));
                        }
                        string newfileName = DateTime.Now.ToString("yyyyMMddHHmmss");
                        //原图
                        string fullDir1 = dir + newfileName + fileExt;
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
        #endregion
    }
}