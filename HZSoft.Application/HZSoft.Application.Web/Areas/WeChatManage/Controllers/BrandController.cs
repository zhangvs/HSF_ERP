using HZSoft.Application.Busines.CustomerManage;
using HZSoft.Application.Code;
using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.Web.Utility;
using HZSoft.Util;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.MP.TenPayLibV3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HZSoft.Application.Web.Areas.WeChatManage.Controllers
{
    
    public class BrandController : BaseWxUserController
    {
        Poll_ToBLL toBLL = new Poll_ToBLL();
        //
        // GET: /WeChatManage/Brand/
        [HandlerWXAuthorizeAttribute(LoginMode.Enforce)]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Guanzhu()
        {
            return View();
        }


        #region 分享代码
        /// <summary>
        /// 分享
        /// </summary>
        /// <returns></returns>
        public PartialViewResult _PartialShare()
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
                fxTitle = "韩师傅网络投票-生态板十大品牌！",
                fxContent = "请大家积极投票转发分享。",
                fxImg = Config.GetValue("Domain") + Url.Content("/Content/images/icon/logo2.png"),
            };
            return PartialView(fxModel);
        }

        /// <summary>
        /// 添加分享记录
        /// </summary>
        /// <param name="shareUrl"></param>
        /// <param name="shareTitle"></param>
        /// <param name="shareContent"></param>
        /// <param name="shareTo"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AddShare(string shareUrl, string shareTitle, string shareContent, string shareTo)
        {
            try
            {
                Poll_ToEntity entity = new Poll_ToEntity()
                {
                    CreateDate = DateTime.Now,
                    //ShareContent = shareContent,
                    ShareTitle = shareTitle,
                    ShareTo = shareTo,
                    ShareUrl = shareUrl,
                    OpenId = CurrentWxUser.OpenId,
                    NickName = CurrentWxUser.NickName,
                    HeadimgUrl = CurrentWxUser.HeadimgUrl
                };
                toBLL.SaveForm(null, entity);
                return Json(new { iserror = false, message = "分享成功" });

                //string nonceStr;//随机字符串
                //string paySign;//签名
                //var sendNormalRedPackResult = RedPackApi.SendNormalRedPack(
                //    "TenPayV3Info.AppId", "TenPayV3Info.MchId", "TenPayV3Info.Key",
                //    @"F:\apiclient_cert.p12",     //证书物理地址
                //    "接受收红包的用户的openId",   //接受收红包的用户的openId
                //    "红包发送者名称",             //红包发送者名称
                //    Request.UserHostAddress,      //IP
                //    100,                          //付款金额，单位分
                //    "红包祝福语",                 //红包祝福语
                //    "活动名称",                   //活动名称
                //    "备注信息",                   //备注信息
                //    out nonceStr,
                //    out paySign,
                //    null,                         //场景id（非必填）
                //    null,                         //活动信息（非必填）
                //    null                          //资金授权商户号，服务商替特约商户发放时使用（非必填）
                //    );

                //return Content(SerializerHelper.GetJsonString(sendNormalRedPackResult));

            }
            catch (Exception ex)
            {
                return Json(new { iserror = true, message = ex.Message });
            }
        }
        #endregion





    }
}
