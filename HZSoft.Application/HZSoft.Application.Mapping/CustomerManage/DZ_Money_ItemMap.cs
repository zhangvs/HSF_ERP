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
    public class DZ_Money_ItemMap : EntityTypeConfiguration<DZ_Money_ItemEntity>
    {
        public DZ_Money_ItemMap()
        {
            #region ������
            //��
            this.ToTable("DZ_Money_Item");
            //����
            this.HasKey(t => t.ItemId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
