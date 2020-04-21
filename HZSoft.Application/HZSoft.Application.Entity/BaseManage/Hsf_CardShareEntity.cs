using System;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.BaseManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2019-01-30 10:23
    /// �� ������Ƭ����
    /// </summary>
    public class Hsf_CardShareEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        public string Id { get; set; }
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
        /// �����˱��
        /// </summary>
        /// <returns></returns>
        public string UserId { get; set; }
        /// <summary>
        /// ����������
        /// </summary>
        /// <returns></returns>
        public string UserName { get; set; }
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
            this.Id = DateTime.Now.ToString("yyyyMMddHHmmss");
            this.CreateDate = DateTime.Now;
                                }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.Id = keyValue;
                                            }
        #endregion
    }
}