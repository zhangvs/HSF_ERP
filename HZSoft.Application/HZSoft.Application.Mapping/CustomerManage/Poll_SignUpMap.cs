using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2018-10-20 09:13
    /// �� ����������
    /// </summary>
    public class Poll_SignUpMap : EntityTypeConfiguration<Poll_SignUpEntity>
    {
        public Poll_SignUpMap()
        {
            #region ������
            //��
            this.ToTable("Poll_SignUp");
            //����
            this.HasKey(t => t.Id);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
