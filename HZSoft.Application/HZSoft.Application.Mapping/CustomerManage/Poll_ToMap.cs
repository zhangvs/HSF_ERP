using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2019-05-23 14:36
    /// �� ����Ʒ����ѡ
    /// </summary>
    public class Poll_ToMap : EntityTypeConfiguration<Poll_ToEntity>
    {
        public Poll_ToMap()
        {
            #region ������
            //��
            this.ToTable("Poll_To");
            //����
            this.HasKey(t => t.Id);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
