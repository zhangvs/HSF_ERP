using HZSoft.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.BaseManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2019-01-28 14:42
    /// �� ����������Ƭ
    /// </summary>
    public class Hsf_CardMap : EntityTypeConfiguration<Hsf_CardEntity>
    {
        public Hsf_CardMap()
        {
            #region ������
            //��
            this.ToTable("Hsf_Card");
            //����
            this.HasKey(t => t.Id);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
