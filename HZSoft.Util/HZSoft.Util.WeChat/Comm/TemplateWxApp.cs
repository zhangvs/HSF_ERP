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
                SendTemplateMessageResult sendTemplateMessageResult = Senparc.Weixin.MP.AdvancedAPIs.TemplateApi.SendTemplateMessage(
                    AccessTokenContainer.TryGetAccessToken(appId, appSecret), openId, "Y5OqvcAap6hDfUOfDA-ffgiP8VFuISg3AogTT0Z7938", null, data, null);
                return sendTemplateMessageResult.errmsg;
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(ex.Message);
                return ex.Message;//微信Post请求发生错误！错误代码：43004，说明：require subscribe hint: [ziWtva03011295]
                //此appId（365465）尚未注册，请先使用AccessTokenContainer.Register完成注册（全局执行一次即可）！模块：MP
                //微信Post请求发生错误！错误代码：40001，说明：invalid credential, access_token is invalid or not latest hints: [LGfcCbMre-bODBba!]
            }
        }

        /// <summary>
        /// 给财务报价审核提醒
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
                SendTemplateMessageResult sendTemplateMessageResult = Senparc.Weixin.MP.AdvancedAPIs.TemplateApi.SendTemplateMessage(
                    AccessTokenContainer.TryGetAccessToken(appId, appSecret), openId, "pJERHW4hENanVyyzA5Kiz_fYmvAT0sc4RRLqfZE9nUM", null, data, null);
                return sendTemplateMessageResult.errmsg;
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(ex.Message);
                return ex.Message;//微信Post请求发生错误！错误代码：43004，说明：require subscribe hint: [ziWtva03011295]
            }
        }


        /// <summary>
        /// 报价成功
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
                SendTemplateMessageResult sendTemplateMessageResult = Senparc.Weixin.MP.AdvancedAPIs.TemplateApi.SendTemplateMessage(
                    AccessTokenContainer.TryGetAccessToken(appId, appSecret), openId, "XfKHJdlsZ66CtuQVZl5u5_K0AO2lOw0vYKsTyfSogAU", null, data, null);
                return sendTemplateMessageResult.errmsg;
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(ex.Message);
                return ex.Message;//微信Post请求发生错误！错误代码：43004，说明：require subscribe hint: [ziWtva03011295]
            }
        }



        /// <summary>
        /// 收款提醒
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
                SendTemplateMessageResult sendTemplateMessageResult = Senparc.Weixin.MP.AdvancedAPIs.TemplateApi.SendTemplateMessage(
                    AccessTokenContainer.TryGetAccessToken(appId, appSecret), openId, "uciE_-vnHbhkdqbIHfdpmTQG5g568pDAw90qmjNXHGY", null, data, null);
                return sendTemplateMessageResult.errmsg;
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(ex.Message);
                return ex.Message;//微信Post请求发生错误！错误代码：43004，说明：require subscribe hint: [ziWtva03011295]
            }
        }


        /// <summary>
        /// 收款确认提醒
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
                SendTemplateMessageResult sendTemplateMessageResult = Senparc.Weixin.MP.AdvancedAPIs.TemplateApi.SendTemplateMessage(
                    AccessTokenContainer.TryGetAccessToken(appId, appSecret), openId, "PxdaZK82LHdat5u7zYzEbt4rOmLVIVFIC90We2YDXZ8", null, data, null);
                return sendTemplateMessageResult.errmsg;
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(ex.Message);
                return ex.Message;//微信Post请求发生错误！错误代码：43004，说明：require subscribe hint: [ziWtva03011295]
            }
        }

        /// <summary>
        /// 完全入库
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
                SendTemplateMessageResult sendTemplateMessageResult = Senparc.Weixin.MP.AdvancedAPIs.TemplateApi.SendTemplateMessage(
                    AccessTokenContainer.TryGetAccessToken(appId, appSecret), openId, "OWtHeoHLSNzPj8FJ1Bp6vbD4k0WfIbRqYhELB0wtMmY", null, data, null);
                return sendTemplateMessageResult.errmsg;
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(ex.Message);
                return ex.Message;
            }
        }

        /// <summary>
        /// 发货通知，实际发货
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
                SendTemplateMessageResult sendTemplateMessageResult = Senparc.Weixin.MP.AdvancedAPIs.TemplateApi.SendTemplateMessage(
                    AccessTokenContainer.TryGetAccessToken(appId, appSecret), openId, "Pw7phZTv4ly_C9-ayUHKFBq8xPMJG-E0D5rxzBKi_xg", null, data, null);
                return sendTemplateMessageResult.errmsg;
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(ex.Message);
                return ex.Message;
            }
        }
        
    }
}