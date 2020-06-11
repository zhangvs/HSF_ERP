using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超管
    /// 日 期：2020-06-11 11:28
    /// 描 述：报价房间
    /// </summary>
    public class DZ_Money_RoomMap : EntityTypeConfiguration<DZ_Money_RoomEntity>
    {
        public DZ_Money_RoomMap()
        {
            #region 表、主键
            //表
            this.ToTable("DZ_Money_Room");
            //主键
            this.HasKey(t => t.RoomId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
