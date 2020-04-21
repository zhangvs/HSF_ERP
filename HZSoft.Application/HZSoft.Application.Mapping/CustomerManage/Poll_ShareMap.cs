using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2018-10-26 17:42
    /// 描 述：分享记录
    /// </summary>
    public class Poll_ShareMap : EntityTypeConfiguration<Poll_ShareEntity>
    {
        public Poll_ShareMap()
        {
            #region 表、主键
            //表
            this.ToTable("Poll_Share");
            //主键
            this.HasKey(t => t.Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
