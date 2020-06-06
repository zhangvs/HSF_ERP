using HZSoft.Application.Busines.BaseManage;
using HZSoft.Application.Busines.CustomerManage;
using HZSoft.Application.Code;
using HZSoft.Application.Entity.BaseManage;
using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.Web.Utility;
using HZSoft.Util;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HZSoft.Application.Web.Areas.WeChatManage.Controllers
{
    [HandlerWXAuthorizeAttribute(LoginMode.Enforce)]
    public class ProduceController : BaseWxUserController
    {
        private Sale_CustomerBLL yuBLL = new Sale_CustomerBLL();
        private DZ_OrderBLL dz_orderbll = new DZ_OrderBLL();
        private Hsf_CardBLL hsf_cardbll = new Hsf_CardBLL();
        private Sale_CustomerBLL sale_customerbll = new Sale_CustomerBLL();
        private Sale_UserStepBLL userStepbll = new Sale_UserStepBLL();
        private Buys_OrderBLL buy_orderbll = new Buys_OrderBLL();

        /// <summary>
        /// 生产人员资料补充
        /// </summary>
        /// <returns></returns>
        public ActionResult MakeCard()
        {
            return View();
        }

        /// <summary>
        /// 没有任何结果
        /// </summary>
        /// <returns></returns>
        public ActionResult Error(string msg)
        {
            ViewBag.Msg = msg;
            return View();
        }
        /// <summary>
        /// 没有任何结果
        /// </summary>
        /// <returns></returns>
        public ActionResult Success(string msg)
        {
            ViewBag.Msg = msg;
            return View();
        }
        
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetFormJson()
        {
            string OpenId = CurrentWxUser.OpenId;
            var entity = userStepbll.GetEntity(OpenId);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 生产工序扫码修改
        /// </summary>
        /// <param name="id">工序id</param>
        /// <returns></returns>
        public ActionResult StepUpdate(int? id)
        {
            string OpenId = CurrentWxUser.OpenId;
            var entity = userStepbll.GetEntity(OpenId);
            if (entity!=null)
            {
                if (entity.Step != id)
                {
                    switch (id)
                    {
                        case 1:
                            entity.StepName = "开料";
                            break;
                        case 2:
                            entity.StepName = "封边";
                            break;
                        case 3:
                            entity.StepName = "排钻";
                            break;
                        case 4:
                            entity.StepName = "试装";
                            break;
                        case 5:
                            entity.StepName = "包装";
                            break;
                        case 6:
                            entity.StepName = "仓库";
                            break;
                        default:
                            break;
                    }
                    entity.Step = id;
                    userStepbll.SaveForm(entity.OpenId, entity);
                    return RedirectToAction("Success", new { msg = "工序修改成功！" + entity.Name + "，您现在的工序为：" + entity.StepName });
                }
                else
                {
                    return RedirectToAction("Error", new { msg = "无需重复修改！" + entity.Name + "，您现在的工序就是：" + entity.StepName + "" });
                }
            }
            else
            {
                return RedirectToAction("MakeCard");
            }
        }

        /// <summary>
        /// 生产工序扫码
        /// </summary>
        /// <param name="id">生产id</param>
        /// <returns></returns>
        public ActionResult StepSweepcode(string id)
        {
            //string OpenId = "oA-EC1VOfXpxFIa3K9rvSPBwvgrQ";
            string OpenId = CurrentWxUser.OpenId;
            //JObject queryJson = new JObject {
            //            { "OpenId", CurrentWxUser.OpenId }
            //        };
            //先调用看是否存在当前工人的资料信息
            var entity = userStepbll.GetEntity(OpenId);
            if (entity != null)
            {
                //生产订单
                var proEntity = sale_customerbll.GetEntity(id);
                if (proEntity == null)
                {
                    return RedirectToAction("Error", new { msg = "不存在此生产订单！" + id });
                }
                //根据当前工人的工序，执行不同的操作
                int? stepId = entity.Step;
                string stepName = entity.StepName;
                if (stepId == 0 || string.IsNullOrEmpty(stepName))
                {
                    return RedirectToAction("Error", new { msg = "请补充你的工序信息！" + id });
                }
                int? step = entity.Step;
                switch (step)
                {
                    case 1:
                        if (proEntity.KaiLiaoMark == 2)
                        {
                            //判断之前扫码的人是否包含
                            if (!proEntity.KaiLiaoUserName.Contains(entity.Name))
                            {
                                //不包含，添加当前扫码人到操作人，
                                proEntity.KaiLiaoUserName += "," + entity.Name;
                            }
                            else
                            {
                                return RedirectToAction("Error", new { msg = "已经扫码成功，无需再次扫码！" });
                            }
                        }
                        else
                        {
                            proEntity.KaiLiaoDate = DateTime.Now;
                            proEntity.KaiLiaoUserName = entity.Name;
                            proEntity.KaiLiaoMark = 2;
                        }
                        break;
                    case 2:
                        if (proEntity.KaiLiaoMark == 1)
                        {
                            return RedirectToAction("Error", new { msg = "前一道工序还未扫码！" });
                        }
                        if (proEntity.FengBianMark == 2)
                        {
                            //判断之前扫码的人是否包含
                            if (!proEntity.FengBianUserName.Contains(entity.Name))
                            {
                                //不包含，添加当前扫码人到操作人，
                                proEntity.FengBianUserName += "," + entity.Name;
                            }
                            else
                            {
                                return RedirectToAction("Error", new { msg = "已经扫码成功，无需再次扫码！" });
                            }
                        }
                        else
                        {
                            proEntity.FengBianDate = DateTime.Now;
                            proEntity.FengBianUserName = entity.Name;
                            proEntity.FengBianMark = 2;
                        }
                        break;
                    case 3:
                        if (proEntity.KaiLiaoMark == 1 || proEntity.FengBianMark == 1)
                        {
                            return RedirectToAction("Error", new { msg = "前一道工序还未扫码！" });
                        }
                        if (proEntity.PaiZuanMark == 2)
                        {
                            //判断之前扫码的人是否包含
                            if (!proEntity.PaiZuanUserName.Contains(entity.Name))
                            {
                                //不包含，添加当前扫码人到操作人，
                                proEntity.PaiZuanUserName += "," + entity.Name;
                            }
                            else
                            {
                                return RedirectToAction("Error", new { msg = "已经扫码成功，无需再次扫码！" });
                            }
                        }
                        else
                        {
                            proEntity.PaiZuanDate = DateTime.Now;
                            proEntity.PaiZuanUserName = entity.Name;
                            proEntity.PaiZuanMark = 2;
                        }
                        break;
                    case 4:
                        if (proEntity.KaiLiaoMark == 1 || proEntity.FengBianMark == 1 || proEntity.PaiZuanMark == 1)
                        {
                            return RedirectToAction("Error", new { msg = "前一道工序还未扫码！" });
                        }
                        if (proEntity.ShiZhuangMark == 2)
                        {
                            //判断之前扫码的人是否包含
                            if (!proEntity.ShiZhuangUserName.Contains(entity.Name))
                            {
                                //不包含，添加当前扫码人到操作人，
                                proEntity.ShiZhuangUserName += "," + entity.Name;
                            }
                            else
                            {
                                return RedirectToAction("Error", new { msg = "已经扫码成功，无需再次扫码！" });
                            }
                        }
                        else
                        {
                            proEntity.ShiZhuangDate = DateTime.Now;
                            proEntity.ShiZhuangUserName = entity.Name;
                            proEntity.ShiZhuangMark = 2;
                        }
                        break;
                    case 5:
                        if (proEntity.KaiLiaoMark == 1 || proEntity.FengBianMark == 1 || proEntity.PaiZuanMark == 1 || proEntity.ShiZhuangMark == 1)
                        {
                            return RedirectToAction("Error", new { msg = "前一道工序还未扫码！" });
                        }
                        if (proEntity.BaoZhuangMark == 2)
                        {
                            //判断之前扫码的人是否包含
                            if (!proEntity.BaoZhuangUserName.Contains(entity.Name))
                            {
                                //不包含，添加当前扫码人到操作人，
                                proEntity.BaoZhuangUserName += "," + entity.Name;
                            }
                            else
                            {
                                return RedirectToAction("Error", new { msg = "已经扫码成功，无需再次扫码！" });
                            }
                        }
                        else
                        {
                            //包装初次扫码，修改状态
                            proEntity.BaoZhuangDate = DateTime.Now;
                            proEntity.BaoZhuangUserName = entity.Name;
                            proEntity.BaoZhuangMark = 2;

                            //扫码初次包装之后，自动生成一个入库单主单，正在包装***************
                            buy_orderbll.SaveBuyMain(proEntity);
                        }
                        break;
                    case 6:
                        if (proEntity.XiSuMark == 2)
                        {
                            return RedirectToAction("Error", new { msg = "已经扫码成功，无需再次扫码！" });
                        }
                        proEntity.XiSuDate = DateTime.Now;
                        proEntity.XiSuUserName = entity.Name;
                        proEntity.XiSuMark = 2;
                        break;
                    default:
                        break;
                }
                //修改订单状态
                proEntity.StepState = step;
                proEntity.StepName = entity.StepName;
                proEntity.ModifyDate = DateTime.Now;
                proEntity.ModifyUserName = entity.Name;
                sale_customerbll.UpdateStepState(proEntity);
                ViewBag.Msg = "扫码成功!开始" + entity.StepName + "……";
                return View();
            }
            else
            {
                return RedirectToAction("MakeCard");
            }
        }
        /// <summary>
        /// 提交个人资料
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveCard(Sale_UserStepEntity entity)
        {
            string OpenId = CurrentWxUser.OpenId;
            if (!string.IsNullOrEmpty(OpenId))
            {
                var stepentity = userStepbll.GetEntity(OpenId);
                if (stepentity != null)
                {
                    entity.OpenId = CurrentWxUser.OpenId;
                    entity.NickName = CurrentWxUser.NickName;
                    entity.HeadimgUrl = CurrentWxUser.HeadimgUrl;
                    userStepbll.SaveForm(OpenId, entity);
                    return Content("修改成功！");
                }
                else
                {
                    entity.OpenId = CurrentWxUser.OpenId;
                    entity.NickName = CurrentWxUser.NickName;
                    entity.HeadimgUrl = CurrentWxUser.HeadimgUrl;
                    userStepbll.SaveForm(null, entity);
                    return Content("提交成功！");
                }
            }
            else
            {
                return Content("不存在微信id！");
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult YuForm()
        {
            ViewBag.productList = dz_orderbll.GetList("{\"MoneyMark\":\"1\",\"EnterMark\":\"0\"}");
            return View();
        }

        /// <summary>
        /// 获取实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        public ActionResult GetFormJson(string keyValue)
        {
            var data = yuBLL.GetEntity(keyValue);
            var childData = yuBLL.GetDetails(keyValue);
            var jsonData = new { entity = data, childEntity = childData };
            return Content(jsonData.ToJson());
        }


        /// <summary>
        /// 提交剩余库存单信息
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="strEntity"></param>
        /// <param name="strChildEntitys"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveForm(string keyValue, string strEntity, string strChildEntitys)
        {
            var entity = strEntity.ToObject<Sale_CustomerEntity>();
            var childEntitys = strChildEntitys.ToList<Sale_Customer_ItemEntity>();

            yuBLL.SaveForm(keyValue, entity, childEntitys);
            return Content("true");
        }


    }
}
