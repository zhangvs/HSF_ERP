using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2018-10-24 16:44
    /// �� ����ͶƱ��¼
    /// </summary>
    public class Poll_RecordMap : EntityTypeConfiguration<Poll_RecordEntity>
    {
        public Poll_RecordMap()
        {
            #region ������
            //��
            this.ToTable("Poll_Record");
            //����
            this.HasKey(t => t.Id);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
