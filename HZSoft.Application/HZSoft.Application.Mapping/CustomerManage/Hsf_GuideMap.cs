using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超管
    /// 日 期：2020-03-11 11:26
    /// 描 述：预约表
    /// </summary>
    public class Hsf_GuideMap : EntityTypeConfiguration<Hsf_GuideEntity>
    {
        public Hsf_GuideMap()
        {
            #region 表、主键
            //表
            this.ToTable("Hsf_Guide");
            //主键
            this.HasKey(t => t.guide_id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
