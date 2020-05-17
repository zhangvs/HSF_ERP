using HZSoft.Application.Web.Utility;
using HZSoft.Cache.Factory;
using HZSoft.Util;
using HZSoft.Util.WeChat.Comm;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Containers;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace HZSoft.Application.Web
{
    /// <summary>
    /// 应用程序全局设置
    /// </summary>
    public class MvcApplication : HttpApplication
    {
        /// <summary>
        /// 启动应用程序
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleTable.EnableOptimizations = true;
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            WeixinConfig.Register();

            AccessTokenContainer.Register(WeixinConfig.AppID, WeixinConfig.AppSecret);//如果没有注册则进行注册，微信基础
            var result = AccessTokenContainer.GetAccessTokenResult(WeixinConfig.AppID); //获取AccessToken结果
            LogHelper.AddLog("盛派注册基础token：" + result.access_token);

            //var result2 = AccessTokenContainer.GetAccessToken(WeixinConfig.AppID);

            //var result3 = CommonApi.GetToken(WeixinConfig.AppID, WeixinConfig.AppSecret);//这是不一样的，没有通过容器
            //string dd = result3.access_token;

            //var resul2t = CommonApi.GetMenu(dd);
            //TemplateWxApp.SendTemplateMoney("oA-EC1Ucth5a3bkvcJSdiTCizz_g", "您好，有新的报价需要审核!", "研发中心", "LS意林公馆2-1-701李先生", "SKL-20200514001", "请进行报价审核。");

            //string appId = Config.GetValue("AppID");
            //string appSecret = Config.GetValue("AppSecret");
            //当然也可以更加简单地一步到位：如果过期，系统会自动重新获取。
            //var result = AccessTokenContainer.TryGetAccessToken(appId, appSecret);
            //var result1 = AccessTokenContainer.TryGetAccessToken(appId, appSecret);
            //var result2 = AccessTokenContainer.TryGetAccessToken(appId, appSecret);//三次都是一样的

            //传统形式的不可以
            //var result = TemplateWxApp.GetACCESS_TOKEN();
            //var result2 = TemplateWxApp.GetACCESS_TOKEN();
            //var result3 = TemplateWxApp.GetACCESS_TOKEN();//传统的每次都不一样

            //发微信模板消息---接单之后，给审图人提醒--刘明存oA-EC1WVqHl_gsBM3We2rgOHIMEQ
            //订单生成通知（1审图提醒）
            //TemplateWxApp.SendTemplateNew("oA-EC1Ucth5a3bkvcJSdiTCizz_g",
            //    "您好，有新的订单需要审图!", "联动柜", "SKL-20200506003", "请进行审图。");


            //string tokenUrl = string.Format(WeixinConfig.GetTokenUrl, "snsapi_userinfo");//网页token，中间不改变普通token的获取和值
            //var token = AnalyzeHelper.Get<WeixinToken>(tokenUrl);


            TemplateWxApp.SendTemplateNew("oA-EC1Ucth5a3bkvcJSdiTCizz_g",
                "您好，有新的订单需要审图!", "联动柜", "SKL-20200506003", "请进行审图。");



            ////发微信模板消息-- - 接单之后，给审图人提醒--刘琛oA - EC1X6RWfW1_DNJ_VNiA3uhOYg
            ////订单生成通知（2拆单提醒）
            //TemplateWxApp.SendTemplateNew("oA-EC1Ucth5a3bkvcJSdiTCizz_g",
            //    "您好，有新的订单需要拆单!", "尚成汇景园_逸仙路-3508弄-7单元-11楼-03户_皇甫秋实 橱柜 加急补单货发上海", "SKL-20200506003", "请进行拆单。");




            ////发微信模板消息---研发报价之后，给财务提醒--刘一珠oA-EC1X0OoVmzyowOqxYHlY5NHX4
            ////订单生成通知（3报价提醒）
            //TemplateWxApp.SendTemplateMoney("oA-EC1Ucth5a3bkvcJSdiTCizz_g",
            //    "您好，有新的报价需要审核!", "研发中心", "联动柜", "SKL-20200506003", "请进行报价审核。");


            ////不直接给销售员报价，只有直营店店长才能知道报价（4报价确认提醒）
            //TemplateWxApp.SendTemplateMoneyOk("oA-EC1Ucth5a3bkvcJSdiTCizz_g",
            //    "您好，您的订单已报价成功!", "SKL-20200506003", "联动柜", "281", "请确认预付款。");



            ////发微信模板消息---营销收款之后，给财务提醒--刘一珠（5收款提醒）
            //TemplateWxApp.SendTemplateReceivable("oA-EC1Ucth5a3bkvcJSdiTCizz_g",
            //    "您好，有新的收款需要确认!", "联动柜", "281", "此客户为九折供货，客户打款按全款打的款，多出的28元，下次抵货款。");



            ////发微信模板消息---营销收款之后，给销售员提醒--（6收款确认提醒：已全部收款）
            //TemplateWxApp.SendTemplateReceivableOk("oA-EC1Ucth5a3bkvcJSdiTCizz_g",
            //    "您好，有的收款已经确认!", "通过", "已全部收款");


            //发微信模板消息---财务已经报价审核并收款确认之后，给张宝莲发消息提醒oA-EC1bJnd0KFBuOy0joJvUOGwwk
            //订单生成通知（7下单提醒）
            //TemplateWxApp.SendTemplateNew("oA-EC1Ucth5a3bkvcJSdiTCizz_g",
            //    "您好，有新的订单财务已经报价审核并收款确认!", "联动柜", "SKL-20200506003", "请进行生产下单。");


            ////发微信模板消息---下单之后，给程东彩发消息提醒oA-EC1W1BQZ46Wc8HPCZZUUFbE9M
            ////订单生成通知（8推单提醒）
            //TemplateWxApp.SendTemplateNew("oA-EC1Ucth5a3bkvcJSdiTCizz_g",
            //    "您好，有新的订单已经下单!", "联动柜", "SKL-20200506003", "请进行确认并推单。");


            ////给销售人提醒(9完全入库提醒)
            //TemplateWxApp.SendTemplateAllIn("oA-EC1Ucth5a3bkvcJSdiTCizz_g",
            //    "您好，您的订单已经入库!", "联动柜", "共3包。");

            ////发微信模板消息---入库+收齐尾款之后，给胡鲁鲁发消息提醒?????
            ////订单生成通知（10完全入库提醒）
            //TemplateWxApp.SendTemplateAllIn("oA-EC1Ucth5a3bkvcJSdiTCizz_g",
            //    "您好，有新的订单已经入库!", "联动柜", "共3包，请进行发货通知。");


            ////发微信模板消息---发货通知之后，给公维才发消息提醒?????
            ////订单生成通知（11发货通知提醒）
            //TemplateWxApp.SendTemplateSend("oA-EC1Ucth5a3bkvcJSdiTCizz_g",
            //    "您好，有新发货通知!", "SKL-20200506003", "联动柜，请在" + DateTime.Now + "之前安排发货。");

            ////只有关注公众号的业务员才能收到消息(12实际发货提醒)
            //TemplateWxApp.SendTemplateSend("oA-EC1Ucth5a3bkvcJSdiTCizz_g",
            //    "您好，您的订单已经发货!", "SKL-20200506003", "联动柜，共3包。");

        }

        /// <summary>
        /// 应用程序错误处理
        /// </summary>
        protected void Application_Error(object sender, EventArgs e)
        {
            var lastError = Server.GetLastError();
        }
    }
}