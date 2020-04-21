using System;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2019-05-23 14:36
    /// �� ����Ʒ����ѡ
    /// </summary>
    public class Poll_ToEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        public int? Id { get; set; }
        /// <summary>
        /// ΢�ű�ʶ
        /// </summary>
        /// <returns></returns>
        public string OpenId { get; set; }
        /// <summary>
        /// ΢���ǳ�
        /// </summary>
        /// <returns></returns>
        public string NickName { get; set; }
        /// <summary>
        /// ΢��ͷ��
        /// </summary>
        /// <returns></returns>
        public string HeadimgUrl { get; set; }
        /// <summary>
        /// ҳ��·��
        /// </summary>
        /// <returns></returns>
        public string ShareUrl { get; set; }
        /// <summary>
        /// �������
        /// </summary>
        /// <returns></returns>
        public string ShareTitle { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public string ShareContent { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public string ShareTo { get; set; }
        /// <summary>
        /// ѡ�ֱ��
        /// </summary>
        /// <returns></returns>
        public int? PlayerId { get; set; }
        /// <summary>
        /// ѡ������
        /// </summary>
        /// <returns></returns>
        public string PlayerName { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateDate { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.CreateDate = DateTime.Now;
        }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(int? keyValue)
        {
            this.Id = keyValue;
        }
        #endregion
    }
}