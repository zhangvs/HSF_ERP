using HZSoft.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace HZSoft.Application.Web
{
    public class WeixinConfig
    {
        public static string AppID { private set; get; }
        public static string AppSecret { private set; get; }
        public static string AppName { private set; get; }
        public static string RedirectUri { private set; get; }
        public static string GetCodeUrl { private set; get; }
        public static string GetTokenUrl { private set; get; }
        public static string GetUserInfoUrl { private set; get; }

        public static string GetTokenBaseUrl { private set; get; }
        public static string biz { private set; get; }
        public static string BizUrl { private set; get; }
        
        public static string GetTicketUrl { private set; get; }

        public static void Register()
        {
            AppID = Config.GetValue("AppID");
            AppSecret = Config.GetValue("AppSecret");
            AppName = Config.GetValue("AppName");
            biz = Config.GetValue("biz");
            RedirectUri = Config.GetValue("Domain") + "/WeChatManage/WXLogin/Redirect";
            GetCodeUrl = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + AppID + "&redirect_uri=" + HttpUtility.UrlEncode(WeixinConfig.RedirectUri) + "&response_type=code&scope=snsapi_base&state={0}#wechat_redirect";//HttpUtility.UrlEncode(WeixinConfig.ReturnUri)
            GetTokenUrl = "https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + AppID + "&secret=" + AppSecret + "&code={0}&grant_type=authorization_code";
            //GetUserInfoUrl = "https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang=zh_CN";
            GetUserInfoUrl = "https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}&lang=zh_CN";


            BizUrl = "https://mp.weixin.qq.com/mp/profile_ext?action=home&__biz=" + biz + "#wechat_redirect";
            GetTokenBaseUrl = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + AppID + "&secret=" + AppSecret;
            GetTicketUrl = "https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type=jsapi";
        }
    }
}
