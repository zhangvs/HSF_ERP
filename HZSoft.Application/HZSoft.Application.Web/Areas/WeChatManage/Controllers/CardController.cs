using HZSoft.Application.Busines.BaseManage;
using HZSoft.Application.Busines.CustomerManage;
using HZSoft.Application.Code;
using HZSoft.Application.Entity.BaseManage;
using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.Web.Utility;
using HZSoft.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HZSoft.Application.Web.Areas.WeChatManage.Controllers
{
    [HandlerWXAuthorizeAttribute(LoginMode.Enforce)]
    public class CardController : BaseWxUserController
    {
        Hsf_CardBLL hsf_CardBLL = new Hsf_CardBLL();
        Hsf_GuideBLL hsf_GuideBLL = new Hsf_GuideBLL();
        //
        // GET: /WeChatManage/Card/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Hsf(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var entity = hsf_CardBLL.GetEntity(id);
                if (entity != null)
                {
                    return View(entity);
                }
                else
                {
                    return RedirectToAction("Error", new { msg = "没有任何结果！"+ id });
                }

            }
            else
            {
                return RedirectToAction("Error", new { msg = "没有任何结果！" + id });
            }
        }

        public ActionResult Skl(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var entity = hsf_CardBLL.GetEntity(id);
                if (entity != null)
                {
                    return View(entity);
                }
                else
                {
                    return RedirectToAction("Error", new { msg = "没有任何结果！" + id });
                }

            }
            else
            {
                return RedirectToAction("Error", new { msg = "没有任何结果！" + id });
            }
        }
        public ActionResult Skljm(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var entity = hsf_CardBLL.GetEntity(id);
                if (entity != null)
                {
                    return View(entity);
                }
                else
                {
                    return RedirectToAction("Error", new { msg = "没有任何结果！" + id });
                }

            }
            else
            {
                return RedirectToAction("Error", new { msg = "没有任何结果！" + id });
            }
        }

        public ActionResult Guide(string id,string name)
        {
            ViewBag.meijia_id = id;
            ViewBag.share_from = name;
            return View();
        }
        /// <summary>
        /// 制作电子名片
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ActionResult MakeCard(Hsf_CardEntity entity)
        {
            return View();
        }
        /// <summary>
        /// 个人资料补充
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ActionResult MakeCard2(Hsf_CardEntity entity)
        {
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
            var entity = hsf_CardBLL.GetEntityByOpenId(OpenId);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 提交报名
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddGuide(Hsf_GuideEntity entity)
        {
            entity.design_uid = CurrentWxUser.OpenId;
            entity.design_name = CurrentWxUser.NickName;
            entity.design_phone = CurrentWxUser.HeadimgUrl;
            ////插入报名表
            hsf_GuideBLL.SaveForm(null, entity);
            return Content("1");
        }


        /// <summary>
        /// 提交报名
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveCard(Hsf_CardEntity entity)
        {
            string OpenId = CurrentWxUser.OpenId;
            if (!string.IsNullOrEmpty(OpenId))
            {
                var cardentity = hsf_CardBLL.GetEntityByOpenId(OpenId);
                if (cardentity != null)
                {
                    entity.OpenId = CurrentWxUser.OpenId;
                    entity.NickName = CurrentWxUser.NickName;
                    entity.HeadimgUrl = CurrentWxUser.HeadimgUrl;
                    //插入报名表
                    string id = hsf_CardBLL.SaveForm(cardentity.Id, entity);
                    return Content(id);
                }
                else
                {
                    entity.OpenId = CurrentWxUser.OpenId;
                    entity.NickName = CurrentWxUser.NickName;
                    entity.HeadimgUrl = CurrentWxUser.HeadimgUrl;
                    string id = hsf_CardBLL.SaveForm(null, entity);
                    return Content(id);
                }
            }
            else
            {
                return Content("不存在微信id！");
            }
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

        #region 分享代码
        /// <summary>
        /// 分享
        /// </summary>
        /// <param name="cardId"></param>
        /// <param name="cardName"></param>
        /// <param name="photoUrl"></param>
        /// <returns></returns>
        public PartialViewResult _PartialShare(string cardId, string cardName, string photoUrl)
        {
            //获得微信基础类
            BaseWxModel baseModel = GetWxModel();
            ShareModel fxModel = new ShareModel()
            {
                appid = baseModel.appid,
                nonce = baseModel.nonce,
                signature = baseModel.signature,
                thisUrl = baseModel.thisUrl,
                timestamp = baseModel.timestamp,
                fxTitle = $"{cardName}-电子名片",
                fxContent = Config.GetValue("CompanyProfile"),
                fxImg = Config.GetValue("Domain") + Url.Content(photoUrl),
            };
            ViewBag.userId = cardId;
            ViewBag.userName = cardName;
            return PartialView(fxModel);
        }
        #endregion


        #region
        Hsf_CardShareBLL shareBLL = new Hsf_CardShareBLL();
        /// <summary>
        /// 添加分享记录
        /// </summary>
        /// <param name="shareUrl"></param>
        /// <param name="shareTitle"></param>
        /// <param name="shareContent"></param>
        /// <param name="shareTo"></param>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AddShare(string shareUrl, string shareTitle, string shareContent, string shareTo, string userId, string userName)
        {
            try
            {
                Hsf_CardShareEntity entity = new Hsf_CardShareEntity()
                {
                    CreateDate = DateTime.Now,
                    //ShareContent = shareContent,
                    ShareTitle = shareTitle,
                    ShareTo = shareTo,
                    ShareUrl = shareUrl,
                    OpenId = CurrentWxUser.OpenId,
                    NickName = CurrentWxUser.NickName,
                    HeadimgUrl = CurrentWxUser.HeadimgUrl,
                    UserId = userId,
                    UserName = userName
                };
                shareBLL.SaveForm(null, entity);
                return Json(new { iserror = false, message = "分享成功" });
            }
            catch (Exception ex)
            {
                return Json(new { iserror = true, message = ex.Message });
            }
        }
        #endregion
    }
}
