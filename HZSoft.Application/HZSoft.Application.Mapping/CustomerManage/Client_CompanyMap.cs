using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超管
    /// 日 期：2020-03-15 11:09
    /// 描 述：经销商
    /// </summary>
    public class Client_CompanyMap : EntityTypeConfiguration<Client_CompanyEntity>
    {
        public Client_CompanyMap()
        {
            #region 表、主键
            //表
            this.ToTable("Client_Company");
            //主键
            this.HasKey(t => t.CompanyId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
