using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ��������
    /// �� �ڣ�2020-03-15 11:09
    /// �� ����������
    /// </summary>
    public class Client_CompanyMap : EntityTypeConfiguration<Client_CompanyEntity>
    {
        public Client_CompanyMap()
        {
            #region ������
            //��
            this.ToTable("Client_Company");
            //����
            this.HasKey(t => t.CompanyId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
