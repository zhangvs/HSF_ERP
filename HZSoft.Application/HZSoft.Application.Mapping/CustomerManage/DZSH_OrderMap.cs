using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ��������
    /// �� �ڣ�2020-05-18 09:28
    /// �� �������ߵ�
    /// </summary>
    public class DZSH_OrderMap : EntityTypeConfiguration<DZSH_OrderEntity>
    {
        public DZSH_OrderMap()
        {
            #region ������
            //��
            this.ToTable("DZSH_Order");
            //����
            this.HasKey(t => t.Id);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
