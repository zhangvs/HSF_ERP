using HZSoft.Application.Entity.WeChatManage;
using HZSoft.Application.Web.Utility;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HZSoft.Application.Web.Utility;
using HZSoft.Cache.Factory;

namespace HZSoft.Application.Web.Areas.WeChatManage.Controllers
{
    public class BaseWxUserController : Controller
    {
        #region [Private Method]
        /// <summary>
        /// 微信授权登录
        /// </summary>
        protected string WeixinAuth(string returnUrl)
        {
            //判断是否登录
            if (CurrentWxUser.Users == null)
            {
                //string returnUrl = Url.Action("Index", new { id = id });
                string url = string.Format(WeixinConfig.GetCodeUrl, returnUrl);
                return url;
            }
            return "";
        }
        #endregion

        //获得微信js sdk config
        protected BaseWxModel GetWxModel()
        {
            BaseWxModel model = new BaseWxModel();

            model.appid = WeixinConfig.AppID;
            model.timestamp = JSSDKHelper.GetTimestamp();
            model.nonce = JSSDKHelper.GetNoncestr();
            model.thisUrl = Request.Url.ToString();//MyCommFun.getTotalUrl();

            //string ticket = JsApiTicketContainer.TryGetJsApiTicket(WeixinConfig.AppID, WeixinConfig.AppSecret);
            //https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token=ACCESS_TOKEN&type=jsapi

            var cacheTicket = CacheFactory.Cache().GetCache<string>("jsapi_ticket");
            if (cacheTicket == null)
            {
                //全局token
                var cacheToken = CacheFactory.Cache().GetCache<string>("access_token");
                if (cacheToken == null)
                {
                    var userInfoBase = AnalyzeHelper.Get<WeixinTokenBase>(WeixinConfig.GetTokenBaseUrl);
                    cacheToken = userInfoBase.access_token;
                    CacheFactory.Cache().WriteCache(cacheToken, "access_token", DateTime.Now.AddSeconds(7000));
                }

                string ticketUrl = string.Format(WeixinConfig.GetTicketUrl, cacheToken);
                var ticketModel = AnalyzeHelper.Get<WeixinTicket>(ticketUrl);
                cacheTicket = ticketModel.ticket;
                if (!string.IsNullOrEmpty(cacheTicket))
                {
                    CacheFactory.Cache().WriteCache(cacheTicket, "jsapi_ticket", DateTime.Now.AddSeconds(7000));
                }
            }

            JSSDKHelper jsHelper = new JSSDKHelper();
            //最后一个参数url，必须为当前的网址
            var signature = JSSDKHelper.GetSignature(cacheTicket, model.nonce, model.timestamp, model.thisUrl);
            model.signature = signature;
            return model;
        }

    }
}
