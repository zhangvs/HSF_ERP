using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2018-10-26 17:42
    /// �� ���������¼
    /// </summary>
    public class Poll_ShareMap : EntityTypeConfiguration<Poll_ShareEntity>
    {
        public Poll_ShareMap()
        {
            #region ������
            //��
            this.ToTable("Poll_Share");
            //����
            this.HasKey(t => t.Id);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
