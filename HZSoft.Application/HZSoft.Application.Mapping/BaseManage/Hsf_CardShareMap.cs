using HZSoft.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.BaseManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2019-01-30 10:23
    /// 描 述：名片分享
    /// </summary>
    public class Hsf_CardShareMap : EntityTypeConfiguration<Hsf_CardShareEntity>
    {
        public Hsf_CardShareMap()
        {
            #region 表、主键
            //表
            this.ToTable("Hsf_CardShare");
            //主键
            this.HasKey(t => t.Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
