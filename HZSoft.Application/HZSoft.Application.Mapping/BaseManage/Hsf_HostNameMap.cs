using HZSoft.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.BaseManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2019-02-17 10:15
    /// �� ����������������
    /// </summary>
    public class Hsf_HostNameMap : EntityTypeConfiguration<Hsf_HostNameEntity>
    {
        public Hsf_HostNameMap()
        {
            #region ������
            //��
            this.ToTable("Hsf_HostName");
            //����
            this.HasKey(t => t.Serverid);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
