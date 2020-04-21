using HZSoft.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.BaseManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2017-06-30 10:28
    /// 描 述：供应商
    /// </summary>
    public class Supplier_InfoMap : EntityTypeConfiguration<Supplier_InfoEntity>
    {
        public Supplier_InfoMap()
        {
            #region 表、主键
            //表
            this.ToTable("Supplier_Info");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
