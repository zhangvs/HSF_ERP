using HZSoft.Util;
using Newtonsoft.Json;
using RestSharp;
using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;
using Senparc.Weixin.MP.Containers;
using System;

namespace HZSoft.Util.WeChat.Comm
{
    public class TemplateWxApp
    {
        public static string appId = Config.GetValue("AppID");
        public static string appSecret = Config.GetValue("AppSecret");
        /// <summary>
        /// 给审图，拆单提醒
        /// TemplateWxApp.SendTemplateNew("oA-EC1WVqHl_gsBM3We2rgOHIMEQ", "您好，有新的订单需要审图!", entity.OrderTitle, entity.Code, "请进行审图。");
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="template_id"></param>
        /// <param name="keyword2">商品名称</param>
        /// <param name="keyword3">订单号</param>
        /// <returns></returns>
        public static string SendTemplateNew( string openId, string first, string keyword2, string keyword3, string remark)
        {
            try
            {
                var data = new
                {
                    first = new TemplateDataItem(first),
                    keyword1 = new TemplateDataItem(DateTime.Now.ToString("yyyy年MM月dd日 HH:mm")),
                    keyword2 = new TemplateDataItem(keyword2),
                    keyword3 = new TemplateDataItem(keyword3),
                    remark = new TemplateDataItem(remark),
                };
                string token = AccessTokenContainer.TryGetAccessToken(appId, appSecret);
                SendTemplateMessageResult sendTemplateMessageResult = Senparc.Weixin.MP.AdvancedAPIs.TemplateApi.SendTemplateMessage(
                    token, openId, "Y5OqvcAap6hDfUOfDA-ffgiP8VFuISg3AogTT0Z7938", null, data, null);
                LogHelper.AddLog(first+ keyword3 + "\r\n"+ token + "\r\n" + openId + "\r\n" + sendTemplateMessageResult.errmsg);
                return sendTemplateMessageResult.errmsg;
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(first + ex.Message);
                return ex.Message;//微信Post请求发生错误！错误代码：43004，说明：require subscribe hint: [ziWtva03011295]
                //此appId（365465）尚未注册，请先使用AccessTokenContainer.Register完成注册（全局执行一次即可）！模块：MP
                //微信Post请求发生错误！错误代码：40001，说明：invalid credential, access_token is invalid or not latest hints: [LGfcCbMre-bODBba!]
            }
        }

        /// <summary>
        /// 给财务报价审核提醒
        /// TemplateWxApp.SendTemplateMoney("oA-EC1X0OoVmzyowOqxYHlY5NHX4", "您好，有新的报价需要审核!", "研发中心", entity.OrderTitle, entity.Code, "请进行报价审核。");
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="template_id"></param>
        /// <param name="keyword2">商品名称</param>
        /// <param name="keyword3">订单号</param>
        /// <returns></returns>
        public static string SendTemplateMoney( string openId, string first, string keyword2, string keyword3, string keyword4, string remark)
        {
            try
            {
                var data = new
                {
                    first = new TemplateDataItem(first),
                    keyword1 = new TemplateDataItem(DateTime.Now.ToString("yyyy年MM月dd日 HH:mm")),
                    keyword2 = new TemplateDataItem(keyword2),
                    keyword3 = new TemplateDataItem(keyword3),
                    keyword4 = new TemplateDataItem(keyword4),
                    remark = new TemplateDataItem(remark),
                };
                string token = AccessTokenContainer.TryGetAccessToken(appId, appSecret);
                SendTemplateMessageResult sendTemplateMessageResult = Senparc.Weixin.MP.AdvancedAPIs.TemplateApi.SendTemplateMessage(
                    token, openId, "pJERHW4hENanVyyzA5Kiz_fYmvAT0sc4RRLqfZE9nUM", null, data, null);
                LogHelper.AddLog(first+ keyword4 + "\r\n"+ token + "\r\n" + openId + "\r\n" + sendTemplateMessageResult.errmsg);
                return sendTemplateMessageResult.errmsg;
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(first + ex.Message);
                return ex.Message;//微信Post请求发生错误！错误代码：43004，说明：require subscribe hint: [ziWtva03011295]
            }
        }


        /// <summary>
        /// 报价成功
        /// TemplateWxApp.SendTemplateMoneyOk(hsf_CardEntity.OpenId,"您好，您的订单已报价成功!", oldEntity.Code, oldEntity.OrderTitle, entity.MoneyAccounts.ToString(), "");
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="keyword1">报价项目</param>
        /// <param name="keyword2">报价内容</param>
        /// <param name="keyword3">价格</param>
        /// <param name="keyword4">时间</param>
        /// <param name="template_id"></param>
        /// <returns></returns>
        public static string SendTemplateMoneyOk( string openId, string first, string keyword1, string keyword2, string keyword3, string remark)
        {
            try
            {
                var data = new
                {
                    first = new TemplateDataItem(first),
                    keyword1 = new TemplateDataItem(keyword1),
                    keyword2 = new TemplateDataItem(keyword2),
                    keyword3 = new TemplateDataItem(keyword3),
                    keyword4 = new TemplateDataItem(DateTime.Now.ToString("yyyy年MM月dd日 HH:mm")),
                    remark = new TemplateDataItem(remark),
                };
                string token = AccessTokenContainer.TryGetAccessToken(appId, appSecret);
                SendTemplateMessageResult sendTemplateMessageResult = Senparc.Weixin.MP.AdvancedAPIs.TemplateApi.SendTemplateMessage(
                    token, openId, "XfKHJdlsZ66CtuQVZl5u5_K0AO2lOw0vYKsTyfSogAU", null, data, null);
                LogHelper.AddLog(first+ keyword1 + "\r\n"+ token + "\r\n" + openId + "\r\n" + sendTemplateMessageResult.errmsg);
                return sendTemplateMessageResult.errmsg;
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(first + ex.Message);
                return ex.Message;//微信Post请求发生错误！错误代码：43004，说明：require subscribe hint: [ziWtva03011295]
            }
        }



        /// <summary>
        /// 收款提醒
        /// TemplateWxApp.SendTemplateReceivable("oA-EC1X0OoVmzyowOqxYHlY5NHX4","您好，有新的收款需要确认!", entity.Code, entity.PaymentPrice.ToString(), entity.OrderTitle);
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="template_id"></param>
        /// <param name="keyword1">收款项目</param>
        /// <param name="keyword2">应收金额</param>
        /// <param name="keyword3">时间</param>
        /// <returns></returns>
        public static string SendTemplateReceivable( string openId, string first, string keyword1, string keyword2, string remark)
        {
            try
            {
                var data = new
                {
                    first = new TemplateDataItem(first),
                    keyword1 = new TemplateDataItem(keyword1),
                    keyword2 = new TemplateDataItem(keyword2),
                    keyword3 = new TemplateDataItem(DateTime.Now.ToString("yyyy年MM月dd日 HH:mm")),
                    remark = new TemplateDataItem(remark),
                };
                string token = AccessTokenContainer.TryGetAccessToken(appId, appSecret);
                SendTemplateMessageResult sendTemplateMessageResult = Senparc.Weixin.MP.AdvancedAPIs.TemplateApi.SendTemplateMessage(
                    token, openId, "uciE_-vnHbhkdqbIHfdpmTQG5g568pDAw90qmjNXHGY", null, data, null);
                LogHelper.AddLog(first+ keyword1 + "\r\n"+ token + "\r\n" + openId + "\r\n" + sendTemplateMessageResult.errmsg);
                return sendTemplateMessageResult.errmsg;
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(first + ex.Message);
                return ex.Message;//微信Post请求发生错误！错误代码：43004，说明：require subscribe hint: [ziWtva03011295]
            }
        }


        /// <summary>
        /// 收款确认提醒
        /// TemplateWxApp.SendTemplateReceivableOk(hsf_CardEntity.OpenId,  "您好，您的收款已经确认!", "通过", oldEntity.Code+"部分收款");
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="template_id"></param>
        /// <param name="keyword1">收款项目</param>
        /// <param name="keyword2">应收金额</param>
        /// <param name="keyword3">时间</param>
        /// <returns></returns>
        public static string SendTemplateReceivableOk( string openId, string first, string keyword1, string remark)
        {
            try
            {
                var data = new
                {
                    first = new TemplateDataItem(first),
                    keyword1 = new TemplateDataItem(keyword1),
                    keyword2 = new TemplateDataItem(DateTime.Now.ToString("yyyy年MM月dd日 HH:mm")),
                    remark = new TemplateDataItem(remark),
                };
                string token = AccessTokenContainer.TryGetAccessToken(appId, appSecret);
                SendTemplateMessageResult sendTemplateMessageResult = Senparc.Weixin.MP.AdvancedAPIs.TemplateApi.SendTemplateMessage(
                    token, openId, "PxdaZK82LHdat5u7zYzEbt4rOmLVIVFIC90We2YDXZ8", null, data, null);
                LogHelper.AddLog(first+ remark + "\r\n"+ token + "\r\n" + openId + "\r\n" + sendTemplateMessageResult.errmsg);
                return sendTemplateMessageResult.errmsg;
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(first + ex.Message);
                return ex.Message;//微信Post请求发生错误！错误代码：43004，说明：require subscribe hint: [ziWtva03011295]
            }
        }

        /// <summary>
        /// 完全入库
        /// TemplateWxApp.SendTemplateAllIn(hsf_CardEntity.OpenId,"您好，您的订单已经全部入库!", buyEntity.Code, buyEntity.OrderTitle+"：共" + buyEntity.TotalQty + "包。"+ wk);
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="template_id"></param>
        /// <returns></returns>
        public static string SendTemplateAllIn( string openId, string first, string keyword1, string remark)
        {
            try
            {
                var data = new
                {
                    first = new TemplateDataItem(first),
                    keyword1 = new TemplateDataItem(keyword1),
                    keyword2 = new TemplateDataItem(DateTime.Now.ToString("yyyy年MM月dd日 HH:mm")),
                    remark = new TemplateDataItem(remark),
                };
                //string url = "https://baidu.com";
                string token = AccessTokenContainer.TryGetAccessToken(appId, appSecret);
                SendTemplateMessageResult sendTemplateMessageResult = Senparc.Weixin.MP.AdvancedAPIs.TemplateApi.SendTemplateMessage(
                    token, openId, "OWtHeoHLSNzPj8FJ1Bp6vbD4k0WfIbRqYhELB0wtMmY", null, data, null);
                LogHelper.AddLog(first+ keyword1 + "\r\n"+ token + "\r\n" + openId + "\r\n" + sendTemplateMessageResult.errmsg);
                return sendTemplateMessageResult.errmsg;
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(first + ex.Message);
                return ex.Message;
            }
        }

        /// <summary>
        /// 发货通知，实际发货
        /// TemplateWxApp.SendTemplateSend(hsf_CardEntity.OpenId, "您好，您的订单已经发货!", entity.Code, entity.OrderTitle+"：共" + entity.TotalQty + "包。");
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="template_id"></param>
        /// <returns></returns>
        public static string SendTemplateSend( string openId, string first, string keyword1, string remark)
        {
            try
            {
                var data = new
                {
                    first = new TemplateDataItem(first),
                    keyword1 = new TemplateDataItem(keyword1),
                    keyword2 = new TemplateDataItem(DateTime.Now.ToString("yyyy年MM月dd日 HH:mm")),
                    remark = new TemplateDataItem(remark),
                };
                //string url = "https://baidu.com";
                string token = AccessTokenContainer.TryGetAccessToken(appId, appSecret);
                SendTemplateMessageResult sendTemplateMessageResult = Senparc.Weixin.MP.AdvancedAPIs.TemplateApi.SendTemplateMessage(
                    token, openId, "Pw7phZTv4ly_C9-ayUHKFBq8xPMJG-E0D5rxzBKi_xg", null, data, null);
                LogHelper.AddLog(first+ keyword1 + "\r\n"+ token + "\r\n" + openId + "\r\n" + sendTemplateMessageResult.errmsg);
                return sendTemplateMessageResult.errmsg;
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(first+ex.Message);
                return ex.Message;
            }
        }
        
    }
}