using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ��������
    /// �� �ڣ�2020-03-11 11:26
    /// �� ����ԤԼ��
    /// </summary>
    public class Hsf_GuideMap : EntityTypeConfiguration<Hsf_GuideEntity>
    {
        public Hsf_GuideMap()
        {
            #region ������
            //��
            this.ToTable("Hsf_Guide");
            //����
            this.HasKey(t => t.guide_id);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
