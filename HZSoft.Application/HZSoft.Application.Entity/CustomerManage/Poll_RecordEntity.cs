using System;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2018-10-24 16:44
    /// �� ����ͶƱ��¼
    /// </summary>
    public class Poll_RecordEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// ���
        /// </summary>
        /// <returns></returns>
        public int? Id { get; set; }
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
        /// ͶƱ��id
        /// </summary>
        /// <returns></returns>
        public string OpenId { get; set; }
        /// <summary>
        /// ͶƱ��΢��
        /// </summary>
        /// <returns></returns>
        public string NickName { get; set; }
        /// <summary>
        /// ͶƱ��ͷ��
        /// </summary>
        /// <returns></returns>
        public string HeadimgUrl { get; set; }
        /// <summary>
        /// ͶƱ����
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