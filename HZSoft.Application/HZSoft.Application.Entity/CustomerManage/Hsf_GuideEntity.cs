using System;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ��������
    /// �� �ڣ�2020-03-11 11:26
    /// �� ����ԤԼ��
    /// </summary>
    public class Hsf_GuideEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// guide_id
        /// </summary>
        /// <returns></returns>
        public string guide_id { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string name { get; set; }
        /// <summary>
        /// �绰
        /// </summary>
        /// <returns></returns>
        public string phone { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string city { get; set; }
        /// <summary>
        /// design_uid
        /// </summary>
        /// <returns></returns>
        public string design_uid { get; set; }
        /// <summary>
        /// design_account
        /// </summary>
        /// <returns></returns>
        public string design_account { get; set; }
        /// <summary>
        /// design_name
        /// </summary>
        /// <returns></returns>
        public string design_name { get; set; }
        /// <summary>
        /// design_phone
        /// </summary>
        /// <returns></returns>
        public string design_phone { get; set; }
        /// <summary>
        /// design_face
        /// </summary>
        /// <returns></returns>
        public string design_face { get; set; }
        /// <summary>
        /// unionid
        /// </summary>
        /// <returns></returns>
        public string unionid { get; set; }
        /// <summary>
        /// guide_name
        /// </summary>
        /// <returns></returns>
        public string guide_name { get; set; }
        /// <summary>
        /// brand
        /// </summary>
        /// <returns></returns>
        public string brand { get; set; }
        /// <summary>
        /// meijia_id
        /// </summary>
        /// <returns></returns>
        public string meijia_id { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string share_from { get; set; }
        /// <summary>
        /// verifycode
        /// </summary>
        /// <returns></returns>
        public string verifycode { get; set; }
        /// <summary>
        /// test
        /// </summary>
        /// <returns></returns>
        public string test { get; set; }
        /// <summary>
        /// DeleteMark
        /// </summary>
        /// <returns></returns>
        public int? DeleteMark { get; set; }
        /// <summary>
        /// EnabledMark
        /// </summary>
        /// <returns></returns>
        public int? EnabledMark { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
        /// <summary>
        /// CreateDate
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// ModifyDate
        /// </summary>
        /// <returns></returns>
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// ModifyUserId
        /// </summary>
        /// <returns></returns>
        public string ModifyUserId { get; set; }
        /// <summary>
        /// ModifyUserName
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
            this.guide_id = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
                                }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.guide_id = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}