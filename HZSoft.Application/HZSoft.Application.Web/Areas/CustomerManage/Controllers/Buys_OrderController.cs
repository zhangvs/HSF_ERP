using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.Busines.CustomerManage;
using HZSoft.Util;
using HZSoft.Util.WebControl;
using System.Collections.Generic;
using System.Web.Mvc;
using HZSoft.Application.Busines.SystemManage;
using HZSoft.Application.Code;
using System.Web;
using System.IO;
using System;

namespace HZSoft.Application.Web.Areas.CustomerManage.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// 
    /// 创 建：超级管理员
    /// 日 期：2018-06-01 16:52
    /// 描 述：进货单
    /// </summary>
    public class Buys_OrderController : MvcControllerBase
    {
        private Buys_OrderBLL buys_orderbll = new Buys_OrderBLL();
        private CodeRuleBLL codeRuleBLL = new CodeRuleBLL();

        #region 视图功能
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Buys_OrderIndex()
        {
            return View();
        }
        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Buys_OrderForm()
        {
            if (Request["keyValue"] == null)
            {
                ViewBag.OrderCode = codeRuleBLL.GetBillCode(SystemInfo.CurrentUserId, "", ((int)CodeRuleEnum.Buy_Order).ToString());
            }
            return View();
        }
        /// <summary>
        /// 发货通知
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult KeeperIndex()
        {
            return View();
        }
        /// <summary>
        /// 发货单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SendOutIndex()
        {
            return View();
        }
        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult KeeperForm()
        {
            return View();
        }
        /// <summary>
        /// 发货页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SendForm()
        {
            return View();
        }
        /// <summary>
        /// 柜体
        /// </summary>
        /// <returns></returns>
        public ActionResult InGuiForm()
        {
            return View();
        }
        /// <summary>
        /// 门板
        /// </summary>
        /// <returns></returns>
        public ActionResult InMenForm()
        {
            return View();
        }
        /// <summary>
        /// 五金
        /// </summary>
        /// <returns></returns>
        public ActionResult InWuForm()
        {
            return View();
        }
        /// <summary>
        /// 外协
        /// </summary>
        /// <returns></returns>
        public ActionResult InWaiForm()
        {
            return View();
        }
        /// <summary>
        /// 订单详细页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Detail()
        {
            return View();
        }

        /// <summary>
        /// 入库完成
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Buys_OrderOverIndex()
        {
            return View();
        }
        /// <summary>
        /// 发货通知完成
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult KeeperOverIndex()
        {
            return View();
        }
        /// <summary>
        /// 发货完成
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SendOutOverIndex()
        {
            return View();
        }





        /// <summary>
        /// 发货物流
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SendLogisticsIndex()
        {
            return View();
        }
        /// <summary>
        /// 发货物流
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SendLogisticsOverIndex()
        {
            return View();
        }
        /// <summary>
        /// 发货物流
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SendLogisticsForm()
        {
            return View();
        }




        /// <summary>
        /// 发货安装
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SendInstallIndex()
        {
            return View();
        }
        /// <summary>
        /// 发货安装
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SendInstallOverIndex()
        {
            return View();
        }
        /// <summary>
        /// 发货安装
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SendInstallForm()
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
            var data = buys_orderbll.GetPageList(pagination, queryJson);
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
        /// 获取实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = buys_orderbll.GetEntity(keyValue);
            var childData = buys_orderbll.GetDetails(keyValue);
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
            var data = buys_orderbll.GetDetails(keyValue);
            return ToJsonResult(data);
        }
        /// <summary>
        /// 获取子表详细信息 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetDetailJson(string keyValue,int? sortCode)
        {
            var data = buys_orderbll.GetDetail(keyValue,sortCode);
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
            buys_orderbll.RemoveForm(keyValue);
            return Success("删除成功。");
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="orderEntryJson">明细对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, Buys_OrderEntity entity, string orderEntryJson)
        {
            var orderEntryList = orderEntryJson.ToList<Buys_OrderItemEntity>();
            buys_orderbll.SaveForm(keyValue, entity, orderEntryList);
            return Success("操作成功。");
        }


        /// <summary>
        /// 保存入库
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveInForm(string keyValue, Buys_OrderItemEntity entity)
        {
            buys_orderbll.SaveInForm(entity);
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
        public ActionResult SaveSend(string keyValue, Buys_OrderEntity entity)
        {
            buys_orderbll.SaveSend(keyValue, entity);
            return Success("操作成功。");
        }


        /// <summary>
        /// 实际发货
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult UpdateSendState(string keyValue)
        {
            buys_orderbll.UpdateSendState(keyValue);
            return Success("发货成功。");
        }



        /// <summary>
        /// 物流保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveLogisticsForm(string keyValue, Buys_OrderEntity entity)
        {
            buys_orderbll.SaveLogisticsForm(keyValue, entity);
            return Success("操作成功。");
        }

        /// <summary>
        /// 安装保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveInstallForm(string keyValue, Buys_OrderEntity entity)
        {
            buys_orderbll.SaveInstallForm(keyValue, entity);
            return Success("操作成功。");
        }
        #endregion
    }
}
