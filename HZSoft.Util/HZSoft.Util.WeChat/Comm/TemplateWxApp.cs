﻿using HZSoft.Util;
using HZSoft.Util.WeChat.Model;
using Newtonsoft.Json;
using RestSharp;
using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.Entities;
using System;

namespace HZSoft.Util.WeChat.Comm
{
    public class TemplateWxApp
    {
        public static string appId = Config.GetValue("AppID");
        public static string appSecret = Config.GetValue("AppSecret");



        public static string getToken()
        {
            #region CacheFactory方式获取token
            ////全局token--基础access_token，每日限额2000
            //var cacheToken = CacheFactory.Cache().GetCache<string>("access_token");
            //if (cacheToken == null)
            //{
            //    var userInfoBase = AnalyzeHelper.Get<WeixinTokenBase>(WeixinConfig.GetTokenBaseUrl);
            //    cacheToken = userInfoBase.access_token;
            //    CacheFactory.Cache().WriteCache(cacheToken, "access_token", DateTime.Now.AddSeconds(7000));
            //}
            #endregion


            //AccessTokenResult result =null;
            ////AccessTokenContainer.cs - 一个AccessToken容器（帮助自动更新AccessToken，因为每一个AccessToken都有一个有效期）
            ////有了AccessTokenContainer，我们可以直接这样获取AccessToken：
            //if (!AccessTokenContainer.CheckRegistered(appId))//检查是否已经注册
            //{
            //    var result2 = CommonApi.GetToken(appId, appSecret);
            //    string dd = result2.access_token;


            //    AccessTokenContainer.Register(appId, appSecret);//如果没有注册则进行注册
            //    result = AccessTokenContainer.GetAccessTokenResult(appId); //获取AccessToken结果
            //    LogHelper.AddLog("盛派获取新的基础token：" + result.access_token);
            //}
            //else
            //{
            //    result = AccessTokenContainer.GetAccessTokenResult(appId); //获取AccessToken结果
            //}
            //当然也可以更加简单地一步到位：var result = AccessTokenContainer.TryGetAccessToken(appId, appSecret);//_____________这样的存在过期现象！！！！！！！所以采用上面的判断方式
            //上述获取到的result有access_token和expires_in两个属性，分别储存了AccessToken字符串和过期时间（秒），
            //如果使用AccessTokenContainer.TryGetAccessToken()方法，则可以彻底忽略的expires_in存在，如果过期，系统会自动重新获取。


            ////Global中注册，不需要再判断
            //var result = AccessTokenContainer.GetAccessTokenResult(appId); //获取AccessToken结果
            //return result.access_token;

            string token = MPAccessToken.GetToken();
            //string token = "33_Q2wZJzS8wuiieFeKGihHjwRrndkPsCBrem9JNc03-f8-05nfYbQaD5OxH_psDGZ66scu-F5NRcLMeWh_oiQbxlyQeZrTYYXbfQDVZ-3WDp6gJX2jJ7rMdrrkBQ-kVcsO2-ns_m5nZyfZEPtVJOGeAIAVAS";//MPAccessToken.GetToken();
            return token;
        }
        /// <summary>
        /// 给审图，拆单提醒
        /// TemplateWxApp.SendTemplateNew("oA-EC1WVqHl_gsBM3We2rgOHIMEQ", "您好，有新的订单需要审图!", entity.OrderTitle, entity.Code, "请进行审图。");
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="template_id"></param>
        /// <param name="keyword2">商品名称</param>
        /// <param name="keyword3">订单号</param>
        /// <returns></returns>
        public static string SendTemplateNew(string openId, string first, string keyword2, string keyword3, string remark, bool retryIfFaild = true)
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
                string token = getToken();
                SendTemplateMessageResult sendTemplateMessageResult = Senparc.Weixin.MP.AdvancedAPIs.TemplateApi.SendTemplateMessage(
                    token, openId, "Y5OqvcAap6hDfUOfDA-ffgiP8VFuISg3AogTT0Z7938", null, data, null);
                LogHelper.AddLog(first + keyword3 + "\r\n" + token + "\r\n" + openId + "\r\n" + sendTemplateMessageResult.errmsg);
                return sendTemplateMessageResult.errmsg;
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(first + ex.Message);
                if (retryIfFaild)
                {
                    retryIfFaild = false;//重试一次
                    MPAccessToken.GetNewToken();//不是最新的token获取新的token
                    string twoResult = SendTemplateNew(openId, first, keyword2, keyword3, remark, false);
                    LogHelper.AddLog("重试结果-" + twoResult);
                    return twoResult;
                }
                return ex.Message;
                //微信Post请求发生错误！错误代码：43004，说明：require subscribe hint: [ziWtva03011295]
                //此appId（365465）尚未注册，请先使用AccessTokenContainer.Register完成注册（全局执行一次即可）！模块：MP
                //微信Post请求发生错误！错误代码：40001，说明：invalid credential, access_token is invalid or not latest hints: [LGfcCbMre-bODBba!]
            }
        }

        /// <summary>
        /// 给财务报价审核提醒
        /// TemplateWxApp.SendTemplateMoney("oA-EC1bg4U16c63kR6yj51lA5AiM", "您好，有新的报价需要审核!", "研发中心", entity.OrderTitle, entity.Code, "请进行报价审核。");
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="template_id"></param>
        /// <param name="keyword2">商品名称</param>
        /// <param name="keyword3">订单号</param>
        /// <returns></returns>
        public static string SendTemplateMoney(string openId, string first, string keyword2, string keyword3, string keyword4, string remark, bool retryIfFaild = true)
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
                string token = getToken();
                SendTemplateMessageResult sendTemplateMessageResult = Senparc.Weixin.MP.AdvancedAPIs.TemplateApi.SendTemplateMessage(
                    token, openId, "pJERHW4hENanVyyzA5Kiz_fYmvAT0sc4RRLqfZE9nUM", null, data, null);
                LogHelper.AddLog(first + keyword4 + "\r\n" + token + "\r\n" + openId + "\r\n" + sendTemplateMessageResult.errmsg);
                return sendTemplateMessageResult.errmsg;
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(first + ex.Message);
                if (retryIfFaild)
                {
                    retryIfFaild = false;//重试一次
                    MPAccessToken.GetNewToken();//不是最新的token获取新的token
                    string twoResult = SendTemplateMoney(openId, first, keyword2, keyword3, keyword4, remark, false);
                    LogHelper.AddLog("重试结果-" + twoResult);
                }
                return ex.Message;
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
        public static string SendTemplateMoneyOk(string openId, string first, string keyword1, string keyword2, string keyword3, string remark, bool retryIfFaild = true)
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
                string token = getToken();
                SendTemplateMessageResult sendTemplateMessageResult = Senparc.Weixin.MP.AdvancedAPIs.TemplateApi.SendTemplateMessage(
                    token, openId, "XfKHJdlsZ66CtuQVZl5u5_K0AO2lOw0vYKsTyfSogAU", null, data, null);
                LogHelper.AddLog(first + keyword1 + "\r\n" + token + "\r\n" + openId + "\r\n" + sendTemplateMessageResult.errmsg);
                return sendTemplateMessageResult.errmsg;
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(first + ex.Message);
                if (retryIfFaild)
                {
                    retryIfFaild = false;//重试一次
                    MPAccessToken.GetNewToken();//不是最新的token获取新的token
                    string twoResult = SendTemplateMoneyOk(openId, first, keyword1, keyword2, keyword3, remark, false);
                    LogHelper.AddLog("重试结果-" + twoResult);
                }
                return ex.Message;
            }
        }



        /// <summary>
        /// 收款提醒
        /// TemplateWxApp.SendTemplateReceivable("oA-EC1bg4U16c63kR6yj51lA5AiM","您好，有新的收款需要确认!", entity.Code, entity.PaymentPrice.ToString(), entity.OrderTitle);
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="template_id"></param>
        /// <param name="keyword1">收款项目</param>
        /// <param name="keyword2">应收金额</param>
        /// <param name="keyword3">时间</param>
        /// <returns></returns>
        public static string SendTemplateReceivable(string openId, string first, string keyword1, string keyword2, string remark, bool retryIfFaild = true)
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
                string token = getToken();
                SendTemplateMessageResult sendTemplateMessageResult = Senparc.Weixin.MP.AdvancedAPIs.TemplateApi.SendTemplateMessage(
                    token, openId, "uciE_-vnHbhkdqbIHfdpmTQG5g568pDAw90qmjNXHGY", null, data, null);
                LogHelper.AddLog(first + keyword1 + "\r\n" + token + "\r\n" + openId + "\r\n" + sendTemplateMessageResult.errmsg);
                return sendTemplateMessageResult.errmsg;
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(first + ex.Message);
                if (retryIfFaild)
                {
                    retryIfFaild = false;//重试一次
                    MPAccessToken.GetNewToken();//不是最新的token获取新的token
                    string twoResult = SendTemplateReceivable(openId, first, keyword1, keyword2, remark, false);
                    LogHelper.AddLog("重试结果-" + twoResult);
                }
                return ex.Message;
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
        public static string SendTemplateReceivableOk(string openId, string first, string keyword1, string remark, bool retryIfFaild = true)
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
                string token = getToken();
                SendTemplateMessageResult sendTemplateMessageResult = Senparc.Weixin.MP.AdvancedAPIs.TemplateApi.SendTemplateMessage(
                    token, openId, "PxdaZK82LHdat5u7zYzEbt4rOmLVIVFIC90We2YDXZ8", null, data, null);
                LogHelper.AddLog(first + remark + "\r\n" + token + "\r\n" + openId + "\r\n" + sendTemplateMessageResult.errmsg);
                return sendTemplateMessageResult.errmsg;
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(first + ex.Message);
                if (retryIfFaild)
                {
                    retryIfFaild = false;//重试一次
                    MPAccessToken.GetNewToken();//不是最新的token获取新的token
                    string twoResult = SendTemplateReceivableOk(openId, first, keyword1, remark, false);
                    LogHelper.AddLog("重试结果-" + twoResult);
                }
                return ex.Message;
            }
        }

        /// <summary>
        /// 完全入库
        /// TemplateWxApp.SendTemplateAllIn(hsf_CardEntity.OpenId,"您好，您的订单已经全部入库!", buyEntity.Code, buyEntity.OrderTitle+"：共" + buyEntity.TotalQty + "包。"+ wk);
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="template_id"></param>
        /// <returns></returns>
        public static string SendTemplateAllIn(string openId, string first, string keyword1, string remark, bool retryIfFaild = true)
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
                string token = getToken();
                SendTemplateMessageResult sendTemplateMessageResult = Senparc.Weixin.MP.AdvancedAPIs.TemplateApi.SendTemplateMessage(
                    token, openId, "OWtHeoHLSNzPj8FJ1Bp6vbD4k0WfIbRqYhELB0wtMmY", null, data, null);
                LogHelper.AddLog(first + keyword1 + "\r\n" + token + "\r\n" + openId + "\r\n" + sendTemplateMessageResult.errmsg);
                return sendTemplateMessageResult.errmsg;
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(first + ex.Message);
                if (retryIfFaild)
                {
                    retryIfFaild = false;//重试一次
                    MPAccessToken.GetNewToken();//不是最新的token获取新的token
                    string twoResult = SendTemplateAllIn(openId, first, keyword1, remark, false);
                    LogHelper.AddLog("重试结果-" + twoResult);
                }
                return ex.Message;
            }
        }

        /// <summary>
        /// 配货通知
        /// 您好，您收到了一个新的配货订单，请尽快处理
        ///订单号：WX02302301
        ///时间：2018-04-10 23:00
        /// TemplateWxApp.SendTemplateSend(hsf_CardEntity.OpenId, "您好，您的订单已经发货!", entity.Code, entity.OrderTitle+"：共" + entity.TotalQty + "包。");
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="template_id"></param>
        /// <returns></returns>
        public static string SendTemplateSend(string openId, string first, string keyword1, string remark, bool retryIfFaild = true)
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
                string token = getToken();
                SendTemplateMessageResult sendTemplateMessageResult = Senparc.Weixin.MP.AdvancedAPIs.TemplateApi.SendTemplateMessage(
                    token, openId, "we_4eFusZS6KpgRM1zgtHjV5mZnSmfFPiUNUZLVnI4U", null, data, null);
                LogHelper.AddLog(first + keyword1 + "\r\n" + token + "\r\n" + openId + "\r\n" + sendTemplateMessageResult.errmsg);
                return sendTemplateMessageResult.errmsg;
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(first + ex.Message);
                if (retryIfFaild)
                {
                    retryIfFaild = false;//重试一次
                    MPAccessToken.GetNewToken();//不是最新的token获取新的token
                    string twoResult = SendTemplateSend(openId, first, keyword1, remark, false);
                    LogHelper.AddLog("重试结果-" + twoResult);
                }
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
        public static string SendTemplateSendOut(string openId, string first, string keyword1, string remark, bool retryIfFaild = true)
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
                string token = getToken();
                SendTemplateMessageResult sendTemplateMessageResult = Senparc.Weixin.MP.AdvancedAPIs.TemplateApi.SendTemplateMessage(
                    token, openId, "Pw7phZTv4ly_C9-ayUHKFBq8xPMJG-E0D5rxzBKi_xg", null, data, null);
                LogHelper.AddLog(first + keyword1 + "\r\n" + token + "\r\n" + openId + "\r\n" + sendTemplateMessageResult.errmsg);
                return sendTemplateMessageResult.errmsg;
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(first + ex.Message);
                if (retryIfFaild)
                {
                    retryIfFaild = false;//重试一次
                    MPAccessToken.GetNewToken();//不是最新的token获取新的token
                    string twoResult = SendTemplateSendOut(openId, first, keyword1, remark, false);
                    LogHelper.AddLog("重试结果-" + twoResult);
                }
                return ex.Message;
            }
        }




        /// <summary>
        /// 订单驳回通知
        /// 
        ///{{first.DATA}}
        ///订单号：{{keyword1.DATA}}
        ///驳回时间：{{keyword2.DATA}}
        ///{{remark.DATA}}
        /// TemplateWxApp.SendTemplateReject(hsf_CardEntity.OpenId, "您好，您的审图被驳回!", oldEntity.Code, oldEntity.OrderTitle);
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="template_id"></param>
        /// <returns></returns>
        public static string SendTemplateReject(string openId, string first, string keyword1, string remark, bool retryIfFaild = true)
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
                string token = getToken();
                SendTemplateMessageResult sendTemplateMessageResult = Senparc.Weixin.MP.AdvancedAPIs.TemplateApi.SendTemplateMessage(
                    token, openId, "NmIr1BdBuhhTkidUNhRe-45Ia8NTOQ4Cw-wwoaSYCAo", null, data, null);
                LogHelper.AddLog(first + keyword1 + "\r\n" + token + "\r\n" + openId + "\r\n" + sendTemplateMessageResult.errmsg);
                return sendTemplateMessageResult.errmsg;
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(first + ex.Message);
                if (retryIfFaild)
                {
                    retryIfFaild = false;//重试一次
                    MPAccessToken.GetNewToken();//不是最新的token获取新的token
                    string twoResult = SendTemplateReject(openId, first, keyword1, remark, false);
                    LogHelper.AddLog("重试结果-" + twoResult);
                }
                return ex.Message;
            }
        }



        /// <summary>
        /// 订单撤销通知
        /// 您好，您的订单已撤销
        /// 订单号：123456
        /// 订单内容：管道疏通
        /// 撤销原因：呼叫错误
        /// 撤销时间：2019年6月14日16:02
        /// 感谢您的使用
        /// TemplateWxApp.SendTemplateBack(hsf_CardEntity.OpenId, "您好，您的审图被驳回!", oldEntity.Code, oldEntity.OrderTitle);
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="template_id"></param>
        /// <returns></returns>
        public static string SendTemplateBack(string openId, string first, string keyword1, string keyword2, string keyword3, string remark, bool retryIfFaild = true)
        {
            try
            {
                var data = new
                {
                    first = new TemplateDataItem(first),
                    keyword1 = new TemplateDataItem(keyword1),//订单号
                    keyword2 = new TemplateDataItem(keyword2),//订单内容
                    keyword3 = new TemplateDataItem(keyword3),//撤销原因
                    keyword4 = new TemplateDataItem(DateTime.Now.ToString("yyyy年MM月dd日 HH:mm")),
                    remark = new TemplateDataItem(remark),
                };
                string token = getToken();
                SendTemplateMessageResult sendTemplateMessageResult = Senparc.Weixin.MP.AdvancedAPIs.TemplateApi.SendTemplateMessage(
                    token, openId, "KJPBCKatXnIVLWoBsT4PKd31jPzb3rVy3c4EQ1EG3_w", null, data, null);
                LogHelper.AddLog(first + keyword1 + "\r\n" + token + "\r\n" + openId + "\r\n" + sendTemplateMessageResult.errmsg);
                return sendTemplateMessageResult.errmsg;
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(first + ex.Message);
                if (retryIfFaild)
                {
                    retryIfFaild = false;//重试一次
                    MPAccessToken.GetNewToken();//不是最新的token获取新的token
                    string twoResult = SendTemplateReject(openId, first, keyword1, remark, false);
                    LogHelper.AddLog("重试结果-" + twoResult);
                }
                return ex.Message;
            }
        }
    }
}