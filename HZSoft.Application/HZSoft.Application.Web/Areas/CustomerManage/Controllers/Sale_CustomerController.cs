using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.Busines.CustomerManage;
using HZSoft.Util;
using HZSoft.Util.WebControl;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HZSoft.Application.Web.Areas.CustomerManage.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// 
    /// 创 建：超级管理员
    /// 日 期：2017-12-23 14:41
    /// 描 述：余量统计
    /// </summary>
    public class Sale_CustomerController : MvcControllerBase
    {
        private Sale_CustomerBLL sale_customerbll = new Sale_CustomerBLL();

        #region 视图功能
        /// <summary>
        /// 生产下单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OrderDownIndex()
        {
            return View();
        }
        /// <summary>
        /// 生产下单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OrderDownForm()
        {
            return View();
        }

        /// <summary>
        /// 生产推单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OrderPushIndex()
        {
            return View();
        }
        /// <summary>
        /// 撤单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OrderPushBackForm()
        {
            return View();
        }

        /// <summary>
        /// 生产单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OrderIndex()
        {
            return View();
        }
        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Sale_CustomerForm()
        {
            return View();
        }
        /// <summary>
        /// 排产计划
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PlanForm()
        {
            return View();
        }
        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Detail()
        {
            return View();
        }

        /// <summary>
        /// 完成
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OverIndex()
        {
            return View();
        }
        #endregion

        #region 获取数据
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
            var data = sale_customerbll.GetPageList(pagination, queryJson);
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
        // <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = sale_customerbll.GetList(queryJson);
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
            var data = sale_customerbll.GetEntity(keyValue);
            var childData = sale_customerbll.GetDetails(keyValue);
            var jsonData = new
            {
                entity = data,
                childEntity = childData
            };
            return ToJsonResult(jsonData);
        }
        /// <summary>
        /// 获取子表详细信息 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetDetailsJson(string keyValue)
        {
            var data = sale_customerbll.GetDetails(keyValue);
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
            sale_customerbll.RemoveForm(keyValue);
            return Success("删除成功。");
        }
        /// <summary>
        /// 生产单编辑
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="strEntity">实体对象</param>
        /// <param name="strChildEntitys">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, string strEntity,string strChildEntitys)
        {
            strEntity = strEntity.Replace("&nbsp;", "");//生日日期错误
            var entity = strEntity.ToObject<Sale_CustomerEntity>();
            var childEntitys = strChildEntitys.ToList<Sale_Customer_ItemEntity>();
            sale_customerbll.SaveForm(keyValue, entity, childEntitys);
            return Success("操作成功。");
        }


        /// <summary>
        /// 推单,撤单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="state">状态1推单-1撤单</param>
        /// <param name="orderId">销售单id</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult UpdatePushState(string keyValue, int? state, string orderId)
        {
            sale_customerbll.UpdatePushState(keyValue, state, orderId);
            return Success("操作成功。");
        }

        /// <summary>
        /// 下单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveDownForm(string keyValue, Sale_CustomerEntity entity)
        {
            sale_customerbll.SaveDownForm(keyValue,entity);
            return Success("操作成功。");
        }

        /// <summary>
        /// 排产
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SavePlanForm(string keyValue, Sale_CustomerEntity entity)
        {
            sale_customerbll.SavePlanForm(keyValue, entity);
            return Success("操作成功。");
        }


        /// <summary>
        /// 撤单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SavePushBackForm(string keyValue, Sale_CustomerEntity entity)
        {
            sale_customerbll.SavePushBackForm(keyValue, entity);
            return Success("操作成功。");
        }
        
        #endregion


    }
}
