using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ��������
    /// �� �ڣ�2020-06-11 11:28
    /// �� �������۷���
    /// </summary>
    public class DZ_Money_RoomMap : EntityTypeConfiguration<DZ_Money_RoomEntity>
    {
        public DZ_Money_RoomMap()
        {
            #region ������
            //��
            this.ToTable("DZ_Money_Room");
            //����
            this.HasKey(t => t.RoomId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
