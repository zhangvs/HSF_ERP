using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2018-06-01 16:52
    /// 描 述：进货单
    /// </summary>
    public class Buys_OrderItemMap : EntityTypeConfiguration<Buys_OrderItemEntity>
    {
        public Buys_OrderItemMap()
        {
            #region 表、主键
            //表
            this.ToTable("Buys_OrderItem");
            //主键
            this.HasKey(t => t.OrderEntryId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
