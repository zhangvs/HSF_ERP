using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ��������
    /// �� �ڣ�2020-07-12 10:31
    /// �� ���������嵥
    /// </summary>
    public class Produce_OutMap : EntityTypeConfiguration<Produce_OutEntity>
    {
        public Produce_OutMap()
        {
            #region ������
            //��
            this.ToTable("Produce_Out");
            //����
            this.HasKey(t => t.Id);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
