using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ��������
    /// �� �ڣ�2020-03-27 10:14
    /// �� ����������Ա����
    /// </summary>
    public class Sale_UserStepMap : EntityTypeConfiguration<Sale_UserStepEntity>
    {
        public Sale_UserStepMap()
        {
            #region ������
            //��
            this.ToTable("Sale_UserStep");
            //����
            this.HasKey(t => t.OpenId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
