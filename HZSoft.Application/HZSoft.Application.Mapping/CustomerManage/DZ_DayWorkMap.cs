using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ��������
    /// �� �ڣ�2020-03-25 10:42
    /// �� �����չ�����
    /// </summary>
    public class DZ_DayWorkMap : EntityTypeConfiguration<DZ_DayWorkEntity>
    {
        public DZ_DayWorkMap()
        {
            #region ������
            //��
            this.ToTable("DZ_DayWork");
            //����
            this.HasKey(t => t.Id);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
