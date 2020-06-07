using System;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ��������
    /// �� �ڣ�2020-06-02 17:53
    /// �� �������ϱ�
    /// </summary>
    public class DZ_ProductEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public string Id { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public string ParentId { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string Code { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string Name { get; set; }
        /// <summary>
        /// ͼƬ
        /// </summary>
        /// <returns></returns>
        public string Picture { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string Kind { get; set; }
        /// <summary>
        /// ���
        /// </summary>
        /// <returns></returns>
        public string Guige { get; set; }
        /// <summary>
        /// ��λ
        /// </summary>
        /// <returns></returns>
        public string Unit { get; set; }
        /// <summary>
        /// ���۷���1
        /// </summary>
        /// <returns></returns>
        public decimal? Plan1 { get; set; }
        /// <summary>
        /// ���۷���2
        /// </summary>
        /// <returns></returns>
        public decimal? Plan2 { get; set; }
        /// <summary>
        /// ���۷���3
        /// </summary>
        /// <returns></returns>
        public decimal? Plan3 { get; set; }
        /// <summary>
        /// ���۷���4
        /// </summary>
        /// <returns></returns>
        public decimal? Plan4 { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
        /// <summary>
        /// ���ͽṹ
        /// </summary>
        /// <returns></returns>
        public int? IsTree { get; set; }
        /// <summary>
        /// �������
        /// </summary>
        /// <returns></returns>
        public int? IsNav { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        public int? SortCode { get; set; }
        /// <summary>
        /// ɾ�����
        /// </summary>
        /// <returns></returns>
        public int? DeleteMark { get; set; }
        /// <summary>
        /// ��Ч��־
        /// </summary>
        /// <returns></returns>
        public int? EnabledMark { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// �����û�����
        /// </summary>
        /// <returns></returns>
        public string CreateUserId { get; set; }
        /// <summary>
        /// �����û�
        /// </summary>
        /// <returns></returns>
        public string CreateUserName { get; set; }
        /// <summary>
        /// �޸�����
        /// </summary>
        /// <returns></returns>
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// �޸��û�����
        /// </summary>
        /// <returns></returns>
        public string ModifyUserId { get; set; }
        /// <summary>
        /// �޸��û�
        /// </summary>
        /// <returns></returns>
        public string ModifyUserName { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().UserName;
            this.DeleteMark = 0;
            this.EnabledMark = 1;
        }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.Id = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}