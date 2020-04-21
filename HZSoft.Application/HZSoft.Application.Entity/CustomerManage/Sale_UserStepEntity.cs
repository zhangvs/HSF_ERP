using System;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ��������
    /// �� �ڣ�2020-03-27 10:14
    /// �� ����������Ա����
    /// </summary>
    public class Sale_UserStepEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string OpenId { get; set; }
        /// <summary>
        /// �ǳ�
        /// </summary>
        /// <returns></returns>
        public string NickName { get; set; }
        /// <summary>
        /// ͷ��
        /// </summary>
        /// <returns></returns>
        public string HeadimgUrl { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string Name { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public int? Step { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public string StepName { get; set; }
        /// <summary>
        /// �绰
        /// </summary>
        /// <returns></returns>
        public string Tel { get; set; }
        /// <summary>
        /// ͷ��
        /// </summary>
        /// <returns></returns>
        public string PhotoUrl { get; set; }
        /// <summary>
        /// ��ά��
        /// </summary>
        /// <returns></returns>
        public string QRcodeUrl { get; set; }
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
        /// ��ע
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
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
            this.CreateDate = DateTime.Now;
            this.DeleteMark = 0;
            this.EnabledMark = 1;
        }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.OpenId = keyValue;
            this.ModifyDate = DateTime.Now;
        }
        #endregion
    }
}