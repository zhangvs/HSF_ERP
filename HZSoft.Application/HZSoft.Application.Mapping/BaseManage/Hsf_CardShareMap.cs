using HZSoft.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.BaseManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2019-01-30 10:23
    /// �� ������Ƭ����
    /// </summary>
    public class Hsf_CardShareMap : EntityTypeConfiguration<Hsf_CardShareEntity>
    {
        public Hsf_CardShareMap()
        {
            #region ������
            //��
            this.ToTable("Hsf_CardShare");
            //����
            this.HasKey(t => t.Id);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
