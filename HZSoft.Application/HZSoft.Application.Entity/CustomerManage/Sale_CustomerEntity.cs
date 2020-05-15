using System;
using System.ComponentModel.DataAnnotations.Schema;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2017-12-23 14:41
    /// �� ��������ͳ��
    /// </summary>
    public class Sale_CustomerEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// ����id
        /// </summary>
        /// <returns></returns>
        [Column("ProduceId")]
        public string ProduceId { get; set; }
        /// <summary>
        /// �������
        /// </summary>
        /// <returns></returns>
        [Column("ProduceCode")]
        public string ProduceCode { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [Column("OrderType")]
        public int? OrderType { get; set; }
        /// <summary>
        /// ����id
        /// </summary>
        /// <returns></returns>
        [Column("OrderId")]
        public string OrderId { get; set; }
        /// <summary>
        /// ���۱��
        /// </summary>
        /// <returns></returns>
        [Column("OrderCode")]
        public string OrderCode { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [Column("OrderTitle")]
        public string OrderTitle { get; set; }
        /// <summary>
        /// ������Id
        /// </summary>
        /// <returns></returns>
        [Column("CompanyId")]
        public string CompanyId { get; set; }
        /// <summary>
        /// ����������
        /// </summary>
        /// <returns></returns>
        [Column("CompanyName")]
        public string CompanyName { get; set; }
        /// <summary>
        /// �ͻ�Id
        /// </summary>
        /// <returns></returns>
        [Column("CustomerId")]
        public string CustomerId { get; set; }
        /// <summary>
        /// �ͻ�����
        /// </summary>
        /// <returns></returns>
        [Column("CustomerName")]
        public string CustomerName { get; set; }
        /// <summary>
        /// ����id
        /// </summary>
        /// <returns></returns>
        [Column("SalesmanUserId")]
        public string SalesmanUserId { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [Column("SalesmanUserName")]
        public string SalesmanUserName { get; set; }
        /// <summary>
        /// �ƻ���ʼ����
        /// </summary>
        /// <returns></returns>
        [Column("StatePlanDate")]
        public DateTime? StatePlanDate { get; set; }
        /// <summary>
        /// �ƻ���������
        /// </summary>
        /// <returns></returns>
        [Column("EndPlanDate")]
        public DateTime? EndPlanDate { get; set; }
        /// <summary>
        /// ��ٽ�������
        /// </summary>
        /// <returns></returns>
        [Column("EndDate")]
        public DateTime? EndDate { get; set; }
        
        /// <summary>
        /// �ջ���ַ
        /// </summary>
        /// <returns></returns>
        public string Address { get; set; }
        /// <summary>
        /// ���䷽ʽ
        /// </summary>
        /// <returns></returns>
        public int? ShippingType { get; set; }
        /// <summary>
        /// �˷ѳе���
        /// </summary>
        /// <returns></returns>
        public int? Carrier { get; set; }
        /// <summary>
        /// �ͻ��绰
        /// </summary>
        /// <returns></returns>
        public string CustomerTelphone { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [Column("KaiLiaoMark")]
        public int? KaiLiaoMark { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [Column("KaiLiaoDate")]
        public DateTime? KaiLiaoDate { get; set; }
        /// <summary>
        /// �����û�
        /// </summary>
        /// <returns></returns>
        [Column("KaiLiaoUserId")]
        public string KaiLiaoUserId { get; set; }
        /// <summary>
        /// �����û�
        /// </summary>
        /// <returns></returns>
        [Column("KaiLiaoUserName")]
        public string KaiLiaoUserName { get; set; }

        /// <summary>
        /// ���
        /// </summary>
        /// <returns></returns>
        [Column("FengBianMark")]
        public int? FengBianMark { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [Column("FengBianDate")]
        public DateTime? FengBianDate { get; set; }
        /// <summary>
        /// �����û�
        /// </summary>
        /// <returns></returns>
        [Column("FengBianUserId")]
        public string FengBianUserId { get; set; }
        /// <summary>
        /// �����û�
        /// </summary>
        /// <returns></returns>
        [Column("FengBianUserName")]
        public string FengBianUserName { get; set; }


        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [Column("PaiZuanMark")]
        public int? PaiZuanMark { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [Column("PaiZuanDate")]
        public DateTime? PaiZuanDate { get; set; }
        /// <summary>
        /// �����û�
        /// </summary>
        /// <returns></returns>
        [Column("PaiZuanUserId")]
        public string PaiZuanUserId { get; set; }
        /// <summary>
        /// �����û�
        /// </summary>
        /// <returns></returns>
        [Column("PaiZuanUserName")]
        public string PaiZuanUserName { get; set; }


        /// <summary>
        /// ��װ
        /// </summary>
        /// <returns></returns>
        [Column("ShiZhuangMark")]
        public int? ShiZhuangMark { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [Column("ShiZhuangDate")]
        public DateTime? ShiZhuangDate { get; set; }
        /// <summary>
        /// �����û�
        /// </summary>
        /// <returns></returns>
        [Column("ShiZhuangUserId")]
        public string ShiZhuangUserId { get; set; }
        /// <summary>
        /// �����û�
        /// </summary>
        /// <returns></returns>
        [Column("ShiZhuangUserName")]
        public string ShiZhuangUserName { get; set; }


        /// <summary>
        /// ��װ
        /// </summary>
        /// <returns></returns>
        [Column("BaoZhuangMark")]
        public int? BaoZhuangMark { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [Column("BaoZhuangDate")]
        public DateTime? BaoZhuangDate { get; set; }
        /// <summary>
        /// �����û�
        /// </summary>
        /// <returns></returns>
        [Column("BaoZhuangUserId")]
        public string BaoZhuangUserId { get; set; }
        /// <summary>
        /// �����û�
        /// </summary>
        /// <returns></returns>
        [Column("BaoZhuangUserName")]
        public string BaoZhuangUserName { get; set; }


        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [Column("XiSuMark")]
        public int? XiSuMark { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [Column("XiSuDate")]
        public DateTime? XiSuDate { get; set; }
        /// <summary>
        /// �����û�
        /// </summary>
        /// <returns></returns>
        [Column("XiSuUserId")]
        public string XiSuUserId { get; set; }
        /// <summary>
        /// �����û�
        /// </summary>
        /// <returns></returns>
        [Column("XiSuUserName")]
        public string XiSuUserName { get; set; }


        /// <summary>
        /// ����--����
        /// </summary>
        /// <returns></returns>
        [Column("GuiTiMark")]
        public int? GuiTiMark { get; set; }
        /// <summary>
        /// ����--�Ű�
        /// </summary>
        /// <returns></returns>
        [Column("MenBanMark")]
        public int? MenBanMark { get; set; }
        /// <summary>
        /// ����--���
        /// </summary>
        /// <returns></returns>
        [Column("WuJinMark")]
        public int? WuJinMark { get; set; }
        /// <summary>
        /// ����--��Э
        /// </summary>
        /// <returns></returns>
        [Column("WaiXieMark")]
        public int? WaiXieMark { get; set; }



        /// <summary>
        /// ����״̬
        /// </summary>
        /// <returns></returns>
        public int? StepState { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public DateTime? StepDate { get; set; }

        /// <summary>
        /// ���
        /// </summary>
        /// <returns></returns>
        [Column("EnterMark")]
        public int? EnterMark { get; set; }
        /// <summary>
        /// �������
        /// </summary>
        /// <returns></returns>
        [Column("EnterDate")]
        public DateTime? EnterDate { get; set; }
        /// <summary>
        /// ����û�
        /// </summary>
        /// <returns></returns>
        [Column("EnterUserId")]
        public string EnterUserId { get; set; }
        /// <summary>
        /// ����û�
        /// </summary>
        /// <returns></returns>
        [Column("EnterUserName")]
        public string EnterUserName { get; set; }
        


        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [Column("SendMark")]
        public int? SendMark { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [Column("SendDate")]
        public DateTime? SendDate { get; set; }
        /// <summary>
        /// �����û�
        /// </summary>
        /// <returns></returns>
        [Column("SendUserId")]
        public string SendUserId { get; set; }
        /// <summary>
        /// �����û�
        /// </summary>
        /// <returns></returns>
        [Column("SendUserName")]
        public string SendUserName { get; set; }
        /// <summary>
        /// �ƻ���������
        /// </summary>
        /// <returns></returns>
        public DateTime? SendPlanDate { get; set; }

        /// <summary>
        /// ʵ�ʷ���
        /// </summary>
        /// <returns></returns>
        public int? SendOutMark { get; set; }
        /// <summary>
        /// ʵ�ʷ�����Id
        /// </summary>
        /// <returns></returns>
        public string SendOutUserId { get; set; }
        /// <summary>
        /// ʵ�ʷ�����
        /// </summary>
        /// <returns></returns>
        public string SendOutUserName { get; set; }
        /// <summary>
        /// ʵ�ʷ�������
        /// </summary>
        /// <returns></returns>
        public DateTime? SendOutDate { get; set; }

        /// <summary>
        /// �����
        /// </summary>
        /// <returns></returns>
        [Column("SumTotalArea")]
        public decimal? SumTotalArea { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        [Column("SUMTOTALCOUNT")]
        public decimal? SumTotalCount { get; set; }
        /// <summary>
        /// �������
        /// </summary>
        /// <returns></returns>
        [Column("SumEndCount")]
        public decimal? SumEndCount { get; set; }
        /// <summary>
        /// ��ʣ����
        /// </summary>
        /// <returns></returns>
        [Column("SUMYUCOUNT")]
        public decimal? SumYuCount { get; set; }


        /// <summary>
        /// �������
        /// </summary>
        /// <returns></returns>
        public int? MoneyOkMark { get; set; }
        /// <summary>
        /// �����������
        /// </summary>
        /// <returns></returns>
        public DateTime? MoneyOkDate { get; set; }
        /// <summary>
        /// �µ�
        /// </summary>
        /// <returns></returns>
        public int? DownMark { get; set; }
        /// <summary>
        /// �µ���Id
        /// </summary>
        /// <returns></returns>
        public string DownUserId { get; set; }
        /// <summary>
        /// �µ���
        /// </summary>
        /// <returns></returns>
        public string DownUserName { get; set; }
        /// <summary>
        /// �µ�����
        /// </summary>
        /// <returns></returns>
        public DateTime? DownDate { get; set; }
        /// <summary>
        /// �µ�����
        /// </summary>
        /// <returns></returns>
        public string DownPath { get; set; }

        /// <summary>
        /// �Ƶ�
        /// </summary>
        /// <returns></returns>
        public int? PushMark { get; set; }
        /// <summary>
        /// �Ƶ���Id
        /// </summary>
        /// <returns></returns>
        public string PushUserId { get; set; }
        /// <summary>
        /// �Ƶ���
        /// </summary>
        /// <returns></returns>
        public string PushUserName { get; set; }
        /// <summary>
        /// �Ƶ�����
        /// </summary>
        /// <returns></returns>
        public DateTime? PushDate { get; set; }
        /// <summary>
        /// ����ԭ��
        /// </summary>
        /// <returns></returns>
        public string PushBackReason { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public string PushBackPath { get; set; }

        /// <summary>
        /// ���ʱ��
        /// </summary>
        /// <returns></returns>
        [Column("CREATEDATE")]
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// �����Id
        /// </summary>
        /// <returns></returns>
        [Column("CREATEUSERID")]
        public string CreateUserId { get; set; }
        /// <summary>
        /// �����
        /// </summary>
        /// <returns></returns>
        [Column("CREATEUSERNAME")]
        public string CreateUserName { get; set; }
        /// <summary>
        /// ״̬
        /// </summary>
        /// <returns></returns>
        [Column("STATUS")]
        public int? Status { get; set; }
        /// <summary>
        /// �Ƿ�ɾ��
        /// </summary>
        /// <returns></returns>
        [Column("ISDEL")]
        public int? IsDel { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [Column("SPAREFIELD")]
        public string SpareField { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        /// <returns></returns>
        [Column("REMARK")]
        public string Remark { get; set; }
        /// <summary>
        /// �޸�����
        /// </summary>
        /// <returns></returns>
        [Column("MODIFYDATE")]
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// �޸��û�����
        /// </summary>
        /// <returns></returns>
        [Column("MODIFYUSERID")]
        public string ModifyUserId { get; set; }
        /// <summary>
        /// �޸��û�
        /// </summary>
        /// <returns></returns>
        [Column("MODIFYUSERNAME")]
        public string ModifyUserName { get; set; }


        /// <summary>
        /// ɾ��
        /// </summary>
        /// <returns></returns>
        public int? DeleteMark { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public int? EnabledMark { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public int? SortCode { get; set; }
        /// <summary>
        /// ��ɱ�ʶ
        /// </summary>
        /// <returns></returns>
        public int? OverMark { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.ProduceId = this.ProduceCode;//�����۱��һ��
            this.CreateDate = DateTime.Now;
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().UserName;

            this.DownMark = 0;//�µ�
            this.PushMark = 0;//�Ƶ�

            this.EndDate = DateTime.Now.AddDays(30).Date;//��ٽ�������
            this.KaiLiaoMark = 1;//Ĭ��ѡ��5����
            this.FengBianMark = 1;
            this.PaiZuanMark = 1;
            this.ShiZhuangMark = 1;
            this.BaoZhuangMark = 1;
            this.EnterMark = 0;//���
            this.SendMark = 0;//����
            this.SendOutMark = 0;//ʵ�ʷ���
            this.OverMark = 0;//����
            this.DeleteMark = 0;
            this.EnabledMark = 1;
        }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.ProduceId = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}