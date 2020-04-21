using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超管
    /// 日 期：2020-03-25 10:42
    /// 描 述：日工作量
    /// </summary>
    public class DZ_DayWorkMap : EntityTypeConfiguration<DZ_DayWorkEntity>
    {
        public DZ_DayWorkMap()
        {
            #region 表、主键
            //表
            this.ToTable("DZ_DayWork");
            //主键
            this.HasKey(t => t.Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
