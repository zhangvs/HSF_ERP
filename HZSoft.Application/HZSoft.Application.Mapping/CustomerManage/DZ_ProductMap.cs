using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超管
    /// 日 期：2020-06-02 17:53
    /// 描 述：物料表
    /// </summary>
    public class DZ_ProductMap : EntityTypeConfiguration<DZ_ProductEntity>
    {
        public DZ_ProductMap()
        {
            #region 表、主键
            //表
            this.ToTable("DZ_Product");
            //主键
            this.HasKey(t => t.Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
