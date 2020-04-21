using HZSoft.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.BaseManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2019-02-17 10:15
    /// 描 述：智能主机管理
    /// </summary>
    public class Hsf_HostNameMap : EntityTypeConfiguration<Hsf_HostNameEntity>
    {
        public Hsf_HostNameMap()
        {
            #region 表、主键
            //表
            this.ToTable("Hsf_HostName");
            //主键
            this.HasKey(t => t.Serverid);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
