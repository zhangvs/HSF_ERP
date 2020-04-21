using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2019-05-23 14:36
    /// 描 述：品牌评选
    /// </summary>
    public class Poll_ToMap : EntityTypeConfiguration<Poll_ToEntity>
    {
        public Poll_ToMap()
        {
            #region 表、主键
            //表
            this.ToTable("Poll_To");
            //主键
            this.HasKey(t => t.Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
