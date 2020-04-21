using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2017-12-23 14:41
    /// 描 述：余量统计
    /// </summary>
    public class Sale_CustomerMap : EntityTypeConfiguration<Sale_CustomerEntity>
    {
        public Sale_CustomerMap()
        {
            #region 表、主键
            //表
            this.ToTable("Sale_Customer");
            //主键
            this.HasKey(t => t.ProduceId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
