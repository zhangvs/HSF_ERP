using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2018-10-24 16:44
    /// 描 述：投票记录
    /// </summary>
    public class Poll_RecordMap : EntityTypeConfiguration<Poll_RecordEntity>
    {
        public Poll_RecordMap()
        {
            #region 表、主键
            //表
            this.ToTable("Poll_Record");
            //主键
            this.HasKey(t => t.Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
