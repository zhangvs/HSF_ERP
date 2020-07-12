using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超管
    /// 日 期：2020-07-12 10:31
    /// 描 述：备料清单
    /// </summary>
    public class Produce_OutMap : EntityTypeConfiguration<Produce_OutEntity>
    {
        public Produce_OutMap()
        {
            #region 表、主键
            //表
            this.ToTable("Produce_Out");
            //主键
            this.HasKey(t => t.Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
