using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超管
    /// 日 期：2020-05-18 09:28
    /// 描 述：客诉单
    /// </summary>
    public class DZSH_OrderMap : EntityTypeConfiguration<DZSH_OrderEntity>
    {
        public DZSH_OrderMap()
        {
            #region 表、主键
            //表
            this.ToTable("DZSH_Order");
            //主键
            this.HasKey(t => t.Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
