using System;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.BaseManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2019-01-30 10:23
    /// 描 述：名片分享
    /// </summary>
    public class Hsf_CardShareEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        public string Id { get; set; }
        /// <summary>
        /// 微信标识
        /// </summary>
        /// <returns></returns>
        public string OpenId { get; set; }
        /// <summary>
        /// 微信昵称
        /// </summary>
        /// <returns></returns>
        public string NickName { get; set; }
        /// <summary>
        /// 微信头像
        /// </summary>
        /// <returns></returns>
        public string HeadimgUrl { get; set; }
        /// <summary>
        /// 页面路径
        /// </summary>
        /// <returns></returns>
        public string ShareUrl { get; set; }
        /// <summary>
        /// 分享标题
        /// </summary>
        /// <returns></returns>
        public string ShareTitle { get; set; }
        /// <summary>
        /// 分享内容
        /// </summary>
        /// <returns></returns>
        public string ShareContent { get; set; }
        /// <summary>
        /// 分享到哪里
        /// </summary>
        /// <returns></returns>
        public string ShareTo { get; set; }
        /// <summary>
        /// 分享人编号
        /// </summary>
        /// <returns></returns>
        public string UserId { get; set; }
        /// <summary>
        /// 分享人姓名
        /// </summary>
        /// <returns></returns>
        public string UserName { get; set; }
        /// <summary>
        /// 分享时间
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateDate { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.Id = DateTime.Now.ToString("yyyyMMddHHmmss");
            this.CreateDate = DateTime.Now;
                                }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.Id = keyValue;
                                            }
        #endregion
    }
}