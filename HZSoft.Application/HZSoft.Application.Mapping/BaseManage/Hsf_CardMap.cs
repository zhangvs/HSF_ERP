using HZSoft.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.BaseManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2019-01-28 14:42
    /// 描 述：电子名片
    /// </summary>
    public class Hsf_CardMap : EntityTypeConfiguration<Hsf_CardEntity>
    {
        public Hsf_CardMap()
        {
            #region 表、主键
            //表
            this.ToTable("Hsf_Card");
            //主键
            this.HasKey(t => t.Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
