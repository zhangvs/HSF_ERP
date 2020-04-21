using System;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.BaseManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2019-02-17 10:15
    /// �� ����������������
    /// </summary>
    public class Hsf_HostNameEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// ����id
        /// </summary>
        /// <returns></returns>
        public string Serverid { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public string Serverchina { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// ����mac
        /// </summary>
        /// <returns></returns>
        public string CreateUserId { get; set; }
        /// <summary>
        /// �޸�ʱ��
        /// </summary>
        /// <returns></returns>
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// �޸�mac
        /// </summary>
        /// <returns></returns>
        public string ModifyUserId { get; set; }
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
        public override void Modify(string keyValue)
        {
            this.Serverid = keyValue;
            this.ModifyDate = DateTime.Now;
                    }
        #endregion
    }
}