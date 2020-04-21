using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超管
    /// 日 期：2020-03-27 10:14
    /// 描 述：车间人员资料
    /// </summary>
    public class Sale_UserStepMap : EntityTypeConfiguration<Sale_UserStepEntity>
    {
        public Sale_UserStepMap()
        {
            #region 表、主键
            //表
            this.ToTable("Sale_UserStep");
            //主键
            this.HasKey(t => t.OpenId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
