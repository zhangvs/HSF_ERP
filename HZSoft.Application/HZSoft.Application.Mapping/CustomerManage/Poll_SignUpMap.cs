using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2018-10-20 09:13
    /// 描 述：报名表
    /// </summary>
    public class Poll_SignUpMap : EntityTypeConfiguration<Poll_SignUpEntity>
    {
        public Poll_SignUpMap()
        {
            #region 表、主键
            //表
            this.ToTable("Poll_SignUp");
            //主键
            this.HasKey(t => t.Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
