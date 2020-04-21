using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HZSoft.Application.Web.Utility
{
    /// <summary>
    /// 微信返回
    /// </summary>
    public class WeixinResponse
    {
        /// <summary>
        /// 错误编码
        /// </summary>
        public int? errcode { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string errmsg { get; set; }
    }

    /// <summary>
    /// 微信Token
    /// </summary>
    public class WeixinToken : WeixinResponse
    {
        /// <summary>
        /// 网页授权接口调用凭证,注意：此access_token与基础支持的access_token不同 
        /// </summary>
        public string access_token { get; set; }
        /// <summary>
        /// access_token接口调用凭证超时时间，单位（秒） 
        /// </summary>
        public int? expires_in { get; set; }
        /// <summary>
        /// 用户刷新access_token  拥有较长的有效期（7天、30天、60天、90天）
        /// </summary>
        public string refresh_token { get; set; }
        /// <summary>
        ///  用户唯一标识，请注意，在未关注公众号时，用户访问公众号的网页，也会产生一个用户和公众号唯一的OpenID 
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 用户授权的作用域，使用逗号（,）分隔 
        /// </summary>
        public string scope { get; set; }
        /// <summary>
        /// 只有在用户将公众号绑定到微信开放平台帐号后，才会出现该字段
        /// </summary>
        public string unionid { get; set; }
    }

    /// <summary>
    /// 微信Token
    /// </summary>
    public class WeixinTokenBase : WeixinResponse
    {
        /// <summary>
        /// 网页授权接口调用凭证,注意：此access_token与基础支持的access_token不同 
        /// </summary>
        public string access_token { get; set; }
        /// <summary>
        /// access_token接口调用凭证超时时间，单位（秒） 
        /// </summary>
        public int? expires_in { get; set; }
    }

    /// <summary>
    /// 生成签名之前必须先了解一下jsapi_ticket，jsapi_ticket是公众号用于调用微信JS接口的临时票据。正常情况下，jsapi_ticket的有效期为7200秒，通过access_token来获取。
    /// 由于获取jsapi_ticket的api调用次数非常有限，频繁刷新jsapi_ticket会导致api调用受限，影响自身业务，开发者必须在自己的服务全局缓存jsapi_ticket 。
    /// </summary>
    public class WeixinTicket : WeixinResponse
    {
        /// <summary>
        /// 网页授权接口调用凭证,注意：此access_token与基础支持的access_token不同 
        /// </summary>
        public string ticket { get; set; }
        /// <summary>
        /// access_token接口调用凭证超时时间，单位（秒） 
        /// </summary>
        public int? expires_in { get; set; }
    }

    /// <summary>
    /// 微信用户信息
    /// </summary>
    public class WeixinUserInfo : WeixinResponse
    {
        /// <summary>
        ///  用户是否订阅该公众号标识，值为0时，代表此用户没有关注该公众号，拉取不到其余信息。
        /// </summary>
        public int? subscribe { get; set; }
        /// <summary>
        ///  用户唯一标识，请注意，在未关注公众号时，用户访问公众号的网页，也会产生一个用户和公众号唯一的OpenID 
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        ///  用户昵称 
        /// </summary>
        public string nickname { get; set; }
        /// <summary>
        ///  用户的性别，值为1时是男性，值为2时是女性，值为0时是未知 
        /// </summary>
        public int? sex { get; set; }
        /// <summary>
        /// 用户个人资料填写的省份
        /// </summary>
        public string province { get; set; }
        /// <summary>
        /// 普通用户个人资料填写的城市
        /// </summary>
        public string city { get; set; }
        /// <summary>
        ///  国家，如中国为CN 
        /// </summary>
        public string country { get; set; }
        /// <summary>
        ///  用户头像，最后一个数值代表正方形头像大小（有0、46、64、96、132数值可选，0代表640*640正方形头像），用户没有头像时该项为空。若用户更换头像，原有头像URL将失效。 
        /// </summary>
        public string headimgurl { get; set; }
    }
}
