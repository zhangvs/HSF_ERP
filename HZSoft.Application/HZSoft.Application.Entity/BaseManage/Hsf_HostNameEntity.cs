using System;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.BaseManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2019-02-17 10:15
    /// 描 述：智能主机管理
    /// </summary>
    public class Hsf_HostNameEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 主机id
        /// </summary>
        /// <returns></returns>
        public string Serverid { get; set; }
        /// <summary>
        /// 主机名称
        /// </summary>
        /// <returns></returns>
        public string Serverchina { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 创建mac
        /// </summary>
        /// <returns></returns>
        public string CreateUserId { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        /// <returns></returns>
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// 修改mac
        /// </summary>
        /// <returns></returns>
        public string ModifyUserId { get; set; }
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
        public override void Modify(string keyValue)
        {
            this.Serverid = keyValue;
            this.ModifyDate = DateTime.Now;
                    }
        #endregion
    }
}