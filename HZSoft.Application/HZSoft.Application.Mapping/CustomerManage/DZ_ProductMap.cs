using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ��������
    /// �� �ڣ�2020-06-02 17:53
    /// �� �������ϱ�
    /// </summary>
    public class DZ_ProductMap : EntityTypeConfiguration<DZ_ProductEntity>
    {
        public DZ_ProductMap()
        {
            #region ������
            //��
            this.ToTable("DZ_Product");
            //����
            this.HasKey(t => t.Id);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
