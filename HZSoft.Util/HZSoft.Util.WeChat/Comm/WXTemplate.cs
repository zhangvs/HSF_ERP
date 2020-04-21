using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HZSoft.Util.WeChat.Comm
{
    public class WXTemplate
    {
        /// <summary>
        /// 
        /// </summary>
        public class WXApi
        {
            public string access_token { set; get; }
        }
        public string GetToken()
        {
            //通知指定的微信客服
            #region 获取access_token
            string appid=Config.GetValue("AppID");
            string secret = Config.GetValue("AppSecret");
            string apiurl = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid="+ appid + "&secret="+ secret;
            WebRequest req = WebRequest.Create(@apiurl);
            req.Method = "POST";
            WebResponse response = req.GetResponse();
            Stream stream = response.GetResponseStream();
            Encoding encode = Encoding.UTF8;
            StreamReader reader = new StreamReader(stream, encode);
            string detail = reader.ReadToEnd();
            var jd = JsonConvert.DeserializeObject<WXApi>(detail);
            string token = (String)jd.access_token;
            return token;
            #endregion
        }

        /// <summary>
        /// 返回JSon数据
        /// </summary>
        /// <param name="JSONData">要处理的JSON数据</param>
        /// <param name="Url">要提交的URL</param>
        /// <returns>返回的JSON处理字符串</returns>
        public static string GetResponseData(string JSONData, string Url)
        {
            string strResult = "";
            if (JSONData != "")
            {
                byte[] bytes = Encoding.UTF8.GetBytes(JSONData);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = "POST";
                request.ContentLength = bytes.Length;
                request.ContentType = "json";
                Stream reqstream = request.GetRequestStream();
                reqstream.Write(bytes, 0, bytes.Length);
                //声明一个HttpWebRequest请求
                request.Timeout = 90000;
                //设置连接超时时间
                request.Headers.Set("Pragma", "no-cache");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream streamReceive = response.GetResponseStream();
                Encoding encoding = Encoding.UTF8;
                StreamReader streamReader = new StreamReader(streamReceive, encoding);
                strResult = streamReader.ReadToEnd();
                streamReceive.Dispose();
                streamReader.Dispose();
            }
            return strResult;
        }

        public void SendToTemplate(string orderCode)
        {
            string url = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + GetToken();

            string template = "{\"touser\":\"oA-EC1Ucth5a3bkvcJSdiTCizz_g\"," +
           "\"template_id\":\"OWtHeoHLSNzPj8FJ1Bp6vbD4k0WfIbRqYhELB0wtMmY\"," +
           "\"data\":{" +
                "\"first\": {" +
                    "\"value\":\"您好，您有生产订单已经入库!\"," +
                  " }," +
                   "\"keyword1\":{" +
                    "\"value\":\"" + orderCode + "\"," +
                   "}," +
                   "\"keyword2\": {" +
                    "\"value\":\"" + DateTime.Now.ToString("yyyy年MM月dd日 HH:mm") + "\"," +
                   "}," +
                   "\"remark\":{" +
                    "\"value\":\"请确认尾款！\"," +
                   "}" +
            "}" +
        "}";
            string str = GetResponseData(template, @url);
        }
    }
}
