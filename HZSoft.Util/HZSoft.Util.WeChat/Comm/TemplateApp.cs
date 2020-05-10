using HZSoft.Util;
using Newtonsoft.Json;
using RestSharp;
using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;
using System;

namespace HZSoft.Util.WeChat.Comm
{
    public class TemplateApp
    {
        public static string OpenId = "";
        public static string Template_id = "";
        public static string AccessToken = GetACCESS_TOKEN();
        static void Main(string[] args)
        {
            //网页跳转
            //SendTemplateMessageResult T = SendTemplateURL(AccessToken, OpenId, Template_id);
            //小程序跳转
            SendTemplateMessageResult T1 = SendTemplatMiniProgram(AccessToken, OpenId, Template_id);
            //Console.WriteLine(T + "\n" + T1);
            Console.ReadKey();
        }

        /// <summary>
        /// 给审图，拆单提醒
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="template_id"></param>
        /// <param name="keyword2">商品名称</param>
        /// <param name="keyword3">订单号</param>
        /// <returns></returns>
        public static string SendTemplateNew(string accessToken, string openId, string template_id, string first, string keyword2, string keyword3, string remark)
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
                //string url = "https://baidu.com";
                SendTemplateMessageResult sendTemplateMessageResult = Senparc.Weixin.MP.AdvancedAPIs.TemplateApi.SendTemplateMessage(accessToken, openId, template_id, null, data, null);
                return sendTemplateMessageResult.errmsg;
            }
            catch (Exception ex)
            {
                return ex.Message;//微信Post请求发生错误！错误代码：43004，说明：require subscribe hint: [ziWtva03011295]
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
        public static string SendTemplateMoney(string accessToken, string openId, string template_id, string first, string keyword2, string keyword3, string keyword4, string remark)
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
                //string url = "https://baidu.com";
                SendTemplateMessageResult sendTemplateMessageResult = Senparc.Weixin.MP.AdvancedAPIs.TemplateApi.SendTemplateMessage(accessToken, openId, template_id, null, data, null);
                return sendTemplateMessageResult.errmsg;
            }
            catch (Exception ex)
            {
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
        public static string SendTemplateMoneyOk(string accessToken, string openId, string template_id, string first, string keyword1, string keyword2, string keyword3, string remark)
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
                //string url = "https://baidu.com";
                SendTemplateMessageResult sendTemplateMessageResult = Senparc.Weixin.MP.AdvancedAPIs.TemplateApi.SendTemplateMessage(accessToken, openId, template_id, null, data, null);
                return sendTemplateMessageResult.errmsg;
            }
            catch (Exception ex)
            {
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
        public static string SendTemplateReceivable(string accessToken, string openId, string template_id, string first, string keyword1, string keyword2, string remark)
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
                SendTemplateMessageResult sendTemplateMessageResult = Senparc.Weixin.MP.AdvancedAPIs.TemplateApi.SendTemplateMessage(accessToken, openId, template_id, null, data, null);
                return sendTemplateMessageResult.errmsg;
            }
            catch (Exception ex)
            {
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
        public static string SendTemplateReceivableOk(string accessToken, string openId, string template_id, string first, string keyword1, string remark)
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
                SendTemplateMessageResult sendTemplateMessageResult = Senparc.Weixin.MP.AdvancedAPIs.TemplateApi.SendTemplateMessage(accessToken, openId, template_id, null, data, null);
                return sendTemplateMessageResult.errmsg;
            }
            catch (Exception ex)
            {
                return ex.Message;//微信Post请求发生错误！错误代码：43004，说明：require subscribe hint: [ziWtva03011295]
            }
        }

        /// <summary>
        /// 完全入库
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="template_id"></param>
        /// <returns></returns>
        public static string SendTemplateAllIn(string accessToken, string openId, string template_id, string first, string keyword1, string remark)
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
                SendTemplateMessageResult sendTemplateMessageResult = Senparc.Weixin.MP.AdvancedAPIs.TemplateApi.SendTemplateMessage(accessToken, openId, template_id, null, data, null);
                return sendTemplateMessageResult.errmsg;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



        /// <summary>
        /// 发货通知，实际发货
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="template_id"></param>
        /// <returns></returns>
        public static string SendTemplateSend(string accessToken, string openId, string template_id, string first, string keyword1, string remark)
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
                SendTemplateMessageResult sendTemplateMessageResult = Senparc.Weixin.MP.AdvancedAPIs.TemplateApi.SendTemplateMessage(accessToken, openId, template_id, null, data, null);
                return sendTemplateMessageResult.errmsg;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }





        /// <summary>
        /// 小程序跳转
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="template_id"></param>
        /// <returns></returns>
        public static SendTemplateMessageResult SendTemplatMiniProgram(string accessToken, string openId, string template_id)
        {
            var data = new
            {
                first = new TemplateDataItem("小程序跳转"),
                keyword1 = new TemplateDataItem("keyword1"),
                keyword2 = new TemplateDataItem(DateTime.Now.ToString("yyyy年MM月dd日 HH:mm")),
                remark = new TemplateDataItem("remark"),
            };
            // 小程序
            TempleteModel_MiniProgram miniProgram = new TempleteModel_MiniProgram
            {
                appid = "",
                pagepath = ""
            };
            string url = string.Empty;
            return Senparc.Weixin.MP.AdvancedAPIs.TemplateApi.SendTemplateMessage(accessToken, openId, template_id, url, data, miniProgram);
        }
        /// <summary>
        /// 获取 AccessToken 需保存 有次数限制
        /// </summary>
        /// <returns></returns>
        public static string GetACCESS_TOKEN()
        {
            string appid = Config.GetValue("AppID");
            string secret = Config.GetValue("AppSecret");
            string apiurl = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + appid + "&secret=" + secret;
            var request = new RestRequest("", Method.GET);
            RestClient restClient = new RestClient(apiurl);
            //return restClient.Execute(request).Content;
            var jd = JsonConvert.DeserializeObject<WXApi>(restClient.Execute(request).Content);
            string token = (String)jd.access_token;
            return token;
        }
        public class WXApi
        {
            public string access_token { set; get; }
        }
    }
}