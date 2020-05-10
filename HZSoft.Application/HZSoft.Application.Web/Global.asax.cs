using HZSoft.Application.Web.Utility;
using HZSoft.Cache.Factory;
using HZSoft.Util.WeChat.Comm;
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

            ////发微信模板消息---接单之后，给审图人提醒--刘明存oA-EC1WVqHl_gsBM3We2rgOHIMEQ
            ////订单生成通知（1审图提醒）
            //TemplateApp.SendTemplateNew(TemplateApp.AccessToken, "oA-EC1Ucth5a3bkvcJSdiTCizz_g", "Y5OqvcAap6hDfUOfDA-ffgiP8VFuISg3AogTT0Z7938",
            //    "您好，有新的订单需要审图!", "联动柜", "SKL-20200506003", "请进行审图。");


            ////发微信模板消息---接单之后，给审图人提醒--刘琛oA-EC1X6RWfW1_DNJ_VNiA3uhOYg
            ////订单生成通知（2拆单提醒）
            //TemplateApp.SendTemplateNew(TemplateApp.AccessToken, "oA-EC1Ucth5a3bkvcJSdiTCizz_g", "Y5OqvcAap6hDfUOfDA-ffgiP8VFuISg3AogTT0Z7938",
            //    "您好，有新的订单需要拆单!", "联动柜", "SKL-20200506003", "请进行拆单。");



            ////发微信模板消息---研发报价之后，给财务提醒--刘一珠oA-EC1X0OoVmzyowOqxYHlY5NHX4
            ////订单生成通知（3报价提醒）
            //TemplateApp.SendTemplateMoney(TemplateApp.AccessToken, "oA-EC1Ucth5a3bkvcJSdiTCizz_g", "pJERHW4hENanVyyzA5Kiz_fYmvAT0sc4RRLqfZE9nUM",
            //    "您好，有新的报价需要审核!", "研发中心", "联动柜", "SKL-20200506003", "请进行报价审核。");


            ////不直接给销售员报价，只有直营店店长才能知道报价（4报价确认提醒）
            //TemplateApp.SendTemplateMoneyOk(TemplateApp.AccessToken, "oA-EC1Ucth5a3bkvcJSdiTCizz_g", "XfKHJdlsZ66CtuQVZl5u5_K0AO2lOw0vYKsTyfSogAU",
            //    "您好，您的订单已报价成功!", "SKL-20200506003", "联动柜", "281", "请确认预付款。");



            ////发微信模板消息---营销收款之后，给财务提醒--刘一珠（5收款提醒）
            //TemplateApp.SendTemplateReceivable(TemplateApp.AccessToken, "oA-EC1Ucth5a3bkvcJSdiTCizz_g", "uciE_-vnHbhkdqbIHfdpmTQG5g568pDAw90qmjNXHGY",
            //    "您好，有新的收款需要确认!", "联动柜", "281", "此客户为九折供货，客户打款按全款打的款，多出的28元，下次抵货款。");



            ////发微信模板消息---营销收款之后，给销售员提醒--（6收款确认提醒：已全部收款）
            //TemplateApp.SendTemplateReceivableOk(TemplateApp.AccessToken, "oA-EC1Ucth5a3bkvcJSdiTCizz_g", "PxdaZK82LHdat5u7zYzEbt4rOmLVIVFIC90We2YDXZ8",
            //    "您好，有的收款已经确认!", "通过", "已全部收款");


            ////发微信模板消息---下单之后，给程东彩发消息提醒oA-EC1W1BQZ46Wc8HPCZZUUFbE9M
            ////订单生成通知（7下单提醒）
            //TemplateApp.SendTemplateNew(TemplateApp.AccessToken, "oA-EC1Ucth5a3bkvcJSdiTCizz_g", "Y5OqvcAap6hDfUOfDA-ffgiP8VFuISg3AogTT0Z7938",
            //    "您好，有新的订单已经下单!", "联动柜", "SKL-20200506003", "请进行确认并推单。");


            ////给销售人提醒(8完全入库提醒)
            //TemplateApp.SendTemplateAllIn(TemplateApp.AccessToken, "oA-EC1Ucth5a3bkvcJSdiTCizz_g", "OWtHeoHLSNzPj8FJ1Bp6vbD4k0WfIbRqYhELB0wtMmY",
            //    "您好，您的订单已经入库!", "联动柜", "共3包。");

            ////发微信模板消息---入库+收齐尾款之后，给胡鲁鲁发消息提醒?????
            ////订单生成通知（9完全入库提醒）
            //TemplateApp.SendTemplateAllIn(TemplateApp.AccessToken, "oA-EC1Ucth5a3bkvcJSdiTCizz_g", "OWtHeoHLSNzPj8FJ1Bp6vbD4k0WfIbRqYhELB0wtMmY",
            //    "您好，有新的订单已经入库!", "联动柜", "共3包，请进行发货通知。");


            ////发微信模板消息---发货通知之后，给公维才发消息提醒?????
            ////订单生成通知（10发货通知提醒）
            //TemplateApp.SendTemplateSend(TemplateApp.AccessToken, "oA-EC1Ucth5a3bkvcJSdiTCizz_g", "Pw7phZTv4ly_C9-ayUHKFBq8xPMJG-E0D5rxzBKi_xg",
            //    "您好，有新发货通知!", "SKL-20200506003", "联动柜，请在" + DateTime.Now+"之前安排发货。");

            ////只有关注公众号的业务员才能收到消息(11实际发货提醒)
            //TemplateApp.SendTemplateSend(TemplateApp.AccessToken, "oA-EC1Ucth5a3bkvcJSdiTCizz_g", "Pw7phZTv4ly_C9-ayUHKFBq8xPMJG-E0D5rxzBKi_xg",
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