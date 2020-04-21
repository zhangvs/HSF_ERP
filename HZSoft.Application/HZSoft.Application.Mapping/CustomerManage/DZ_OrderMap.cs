using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2019-11-17 08:11
    /// 描 述：DZ_Order
    /// </summary>
    public class DZ_OrderMap : EntityTypeConfiguration<DZ_OrderEntity>
    {
        public DZ_OrderMap()
        {
            #region 表、主键
            //表
            this.ToTable("DZ_Order");
            //主键
            this.HasKey(t => t.Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
