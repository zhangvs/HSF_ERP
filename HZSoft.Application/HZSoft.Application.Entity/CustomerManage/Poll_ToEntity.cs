using System;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2019-05-23 14:36
    /// 描 述：品牌评选
    /// </summary>
    public class Poll_ToEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        public int? Id { get; set; }
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
        /// 选手编号
        /// </summary>
        /// <returns></returns>
        public int? PlayerId { get; set; }
        /// <summary>
        /// 选手姓名
        /// </summary>
        /// <returns></returns>
        public string PlayerName { get; set; }
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
            this.CreateDate = DateTime.Now;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(int? keyValue)
        {
            this.Id = keyValue;
        }
        #endregion
    }
}