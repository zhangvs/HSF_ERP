using System;
using System.ComponentModel.DataAnnotations.Schema;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2017-12-23 14:41
    /// 描 述：余量统计
    /// </summary>
    public class Sale_CustomerEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 生产id
        /// </summary>
        /// <returns></returns>
        [Column("ProduceId")]
        public string ProduceId { get; set; }
        /// <summary>
        /// 生产编号
        /// </summary>
        /// <returns></returns>
        [Column("ProduceCode")]
        public string ProduceCode { get; set; }
        /// <summary>
        /// 订单类型
        /// </summary>
        /// <returns></returns>
        [Column("OrderType")]
        public int? OrderType { get; set; }
        /// <summary>
        /// 销售id
        /// </summary>
        /// <returns></returns>
        [Column("OrderId")]
        public string OrderId { get; set; }
        /// <summary>
        /// 销售编号
        /// </summary>
        /// <returns></returns>
        [Column("OrderCode")]
        public string OrderCode { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        /// <returns></returns>
        [Column("OrderTitle")]
        public string OrderTitle { get; set; }
        /// <summary>
        /// 经销商Id
        /// </summary>
        /// <returns></returns>
        [Column("CompanyId")]
        public string CompanyId { get; set; }
        /// <summary>
        /// 经销商名称
        /// </summary>
        /// <returns></returns>
        [Column("CompanyName")]
        public string CompanyName { get; set; }
        /// <summary>
        /// 客户Id
        /// </summary>
        /// <returns></returns>
        [Column("CustomerId")]
        public string CustomerId { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        /// <returns></returns>
        [Column("CustomerName")]
        public string CustomerName { get; set; }
        /// <summary>
        /// 销售id
        /// </summary>
        /// <returns></returns>
        [Column("SalesmanUserId")]
        public string SalesmanUserId { get; set; }
        /// <summary>
        /// 销售名称
        /// </summary>
        /// <returns></returns>
        [Column("SalesmanUserName")]
        public string SalesmanUserName { get; set; }
        /// <summary>
        /// 计划开始日期
        /// </summary>
        /// <returns></returns>
        [Column("StatePlanDate")]
        public DateTime? StatePlanDate { get; set; }
        /// <summary>
        /// 计划结束日期
        /// </summary>
        /// <returns></returns>
        [Column("EndPlanDate")]
        public DateTime? EndPlanDate { get; set; }
        /// <summary>
        /// 最迟交付日期
        /// </summary>
        /// <returns></returns>
        [Column("EndDate")]
        public DateTime? EndDate { get; set; }
        
        /// <summary>
        /// 收货地址
        /// </summary>
        /// <returns></returns>
        public string Address { get; set; }
        /// <summary>
        /// 运输方式
        /// </summary>
        /// <returns></returns>
        public int? ShippingType { get; set; }
        /// <summary>
        /// 运费承担方
        /// </summary>
        /// <returns></returns>
        public int? Carrier { get; set; }
        /// <summary>
        /// 客户电话
        /// </summary>
        /// <returns></returns>
        public string CustomerTelphone { get; set; }

        /// <summary>
        /// 开料
        /// </summary>
        /// <returns></returns>
        [Column("KaiLiaoMark")]
        public int? KaiLiaoMark { get; set; }
        /// <summary>
        /// 开料日期
        /// </summary>
        /// <returns></returns>
        [Column("KaiLiaoDate")]
        public DateTime? KaiLiaoDate { get; set; }
        /// <summary>
        /// 开料用户
        /// </summary>
        /// <returns></returns>
        [Column("KaiLiaoUserId")]
        public string KaiLiaoUserId { get; set; }
        /// <summary>
        /// 开料用户
        /// </summary>
        /// <returns></returns>
        [Column("KaiLiaoUserName")]
        public string KaiLiaoUserName { get; set; }

        /// <summary>
        /// 封边
        /// </summary>
        /// <returns></returns>
        [Column("FengBianMark")]
        public int? FengBianMark { get; set; }
        /// <summary>
        /// 开料日期
        /// </summary>
        /// <returns></returns>
        [Column("FengBianDate")]
        public DateTime? FengBianDate { get; set; }
        /// <summary>
        /// 开料用户
        /// </summary>
        /// <returns></returns>
        [Column("FengBianUserId")]
        public string FengBianUserId { get; set; }
        /// <summary>
        /// 开料用户
        /// </summary>
        /// <returns></returns>
        [Column("FengBianUserName")]
        public string FengBianUserName { get; set; }


        /// <summary>
        /// 排钻
        /// </summary>
        /// <returns></returns>
        [Column("PaiZuanMark")]
        public int? PaiZuanMark { get; set; }
        /// <summary>
        /// 开料日期
        /// </summary>
        /// <returns></returns>
        [Column("PaiZuanDate")]
        public DateTime? PaiZuanDate { get; set; }
        /// <summary>
        /// 开料用户
        /// </summary>
        /// <returns></returns>
        [Column("PaiZuanUserId")]
        public string PaiZuanUserId { get; set; }
        /// <summary>
        /// 开料用户
        /// </summary>
        /// <returns></returns>
        [Column("PaiZuanUserName")]
        public string PaiZuanUserName { get; set; }


        /// <summary>
        /// 试装
        /// </summary>
        /// <returns></returns>
        [Column("ShiZhuangMark")]
        public int? ShiZhuangMark { get; set; }
        /// <summary>
        /// 开料日期
        /// </summary>
        /// <returns></returns>
        [Column("ShiZhuangDate")]
        public DateTime? ShiZhuangDate { get; set; }
        /// <summary>
        /// 开料用户
        /// </summary>
        /// <returns></returns>
        [Column("ShiZhuangUserId")]
        public string ShiZhuangUserId { get; set; }
        /// <summary>
        /// 开料用户
        /// </summary>
        /// <returns></returns>
        [Column("ShiZhuangUserName")]
        public string ShiZhuangUserName { get; set; }


        /// <summary>
        /// 包装
        /// </summary>
        /// <returns></returns>
        [Column("BaoZhuangMark")]
        public int? BaoZhuangMark { get; set; }
        /// <summary>
        /// 开料日期
        /// </summary>
        /// <returns></returns>
        [Column("BaoZhuangDate")]
        public DateTime? BaoZhuangDate { get; set; }
        /// <summary>
        /// 开料用户
        /// </summary>
        /// <returns></returns>
        [Column("BaoZhuangUserId")]
        public string BaoZhuangUserId { get; set; }
        /// <summary>
        /// 开料用户
        /// </summary>
        /// <returns></returns>
        [Column("BaoZhuangUserName")]
        public string BaoZhuangUserName { get; set; }


        /// <summary>
        /// 吸塑
        /// </summary>
        /// <returns></returns>
        [Column("XiSuMark")]
        public int? XiSuMark { get; set; }
        /// <summary>
        /// 吸塑日期
        /// </summary>
        /// <returns></returns>
        [Column("XiSuDate")]
        public DateTime? XiSuDate { get; set; }
        /// <summary>
        /// 吸塑用户
        /// </summary>
        /// <returns></returns>
        [Column("XiSuUserId")]
        public string XiSuUserId { get; set; }
        /// <summary>
        /// 吸塑用户
        /// </summary>
        /// <returns></returns>
        [Column("XiSuUserName")]
        public string XiSuUserName { get; set; }


        /// <summary>
        /// 材料--柜体
        /// </summary>
        /// <returns></returns>
        [Column("GuiTiMark")]
        public int? GuiTiMark { get; set; }
        /// <summary>
        /// 材料--门板
        /// </summary>
        /// <returns></returns>
        [Column("MenBanMark")]
        public int? MenBanMark { get; set; }
        /// <summary>
        /// 材料--五金
        /// </summary>
        /// <returns></returns>
        [Column("WuJinMark")]
        public int? WuJinMark { get; set; }
        /// <summary>
        /// 材料--外协
        /// </summary>
        /// <returns></returns>
        [Column("WaiXieMark")]
        public int? WaiXieMark { get; set; }



        /// <summary>
        /// 工序状态
        /// </summary>
        /// <returns></returns>
        public int? StepState { get; set; }
        /// <summary>
        /// 工序日期
        /// </summary>
        /// <returns></returns>
        public DateTime? StepDate { get; set; }

        /// <summary>
        /// 入库
        /// </summary>
        /// <returns></returns>
        [Column("EnterMark")]
        public int? EnterMark { get; set; }
        /// <summary>
        /// 入库日期
        /// </summary>
        /// <returns></returns>
        [Column("EnterDate")]
        public DateTime? EnterDate { get; set; }
        /// <summary>
        /// 入库用户
        /// </summary>
        /// <returns></returns>
        [Column("EnterUserId")]
        public string EnterUserId { get; set; }
        /// <summary>
        /// 入库用户
        /// </summary>
        /// <returns></returns>
        [Column("EnterUserName")]
        public string EnterUserName { get; set; }
        


        /// <summary>
        /// 发货
        /// </summary>
        /// <returns></returns>
        [Column("SendMark")]
        public int? SendMark { get; set; }
        /// <summary>
        /// 发货日期
        /// </summary>
        /// <returns></returns>
        [Column("SendDate")]
        public DateTime? SendDate { get; set; }
        /// <summary>
        /// 发货用户
        /// </summary>
        /// <returns></returns>
        [Column("SendUserId")]
        public string SendUserId { get; set; }
        /// <summary>
        /// 发货用户
        /// </summary>
        /// <returns></returns>
        [Column("SendUserName")]
        public string SendUserName { get; set; }
        /// <summary>
        /// 计划发货日期
        /// </summary>
        /// <returns></returns>
        public DateTime? SendPlanDate { get; set; }

        /// <summary>
        /// 实际发货
        /// </summary>
        /// <returns></returns>
        public int? SendOutMark { get; set; }
        /// <summary>
        /// 实际发货人Id
        /// </summary>
        /// <returns></returns>
        public string SendOutUserId { get; set; }
        /// <summary>
        /// 实际发货人
        /// </summary>
        /// <returns></returns>
        public string SendOutUserName { get; set; }
        /// <summary>
        /// 实际发货日期
        /// </summary>
        /// <returns></returns>
        public DateTime? SendOutDate { get; set; }

        /// <summary>
        /// 总面积
        /// </summary>
        /// <returns></returns>
        [Column("SumTotalArea")]
        public decimal? SumTotalArea { get; set; }
        /// <summary>
        /// 总数量
        /// </summary>
        /// <returns></returns>
        [Column("SUMTOTALCOUNT")]
        public decimal? SumTotalCount { get; set; }
        /// <summary>
        /// 总完成量
        /// </summary>
        /// <returns></returns>
        [Column("SumEndCount")]
        public decimal? SumEndCount { get; set; }
        /// <summary>
        /// 总剩余量
        /// </summary>
        /// <returns></returns>
        [Column("SUMYUCOUNT")]
        public decimal? SumYuCount { get; set; }


        /// <summary>
        /// 报价审核
        /// </summary>
        /// <returns></returns>
        public int? MoneyOkMark { get; set; }
        /// <summary>
        /// 报价审核日期
        /// </summary>
        /// <returns></returns>
        public DateTime? MoneyOkDate { get; set; }
        /// <summary>
        /// 下单
        /// </summary>
        /// <returns></returns>
        public int? DownMark { get; set; }
        /// <summary>
        /// 下单人Id
        /// </summary>
        /// <returns></returns>
        public string DownUserId { get; set; }
        /// <summary>
        /// 下单人
        /// </summary>
        /// <returns></returns>
        public string DownUserName { get; set; }
        /// <summary>
        /// 下单日期
        /// </summary>
        /// <returns></returns>
        public DateTime? DownDate { get; set; }
        /// <summary>
        /// 下单附件
        /// </summary>
        /// <returns></returns>
        public string DownPath { get; set; }

        /// <summary>
        /// 推单
        /// </summary>
        /// <returns></returns>
        public int? PushMark { get; set; }
        /// <summary>
        /// 推单人Id
        /// </summary>
        /// <returns></returns>
        public string PushUserId { get; set; }
        /// <summary>
        /// 推单人
        /// </summary>
        /// <returns></returns>
        public string PushUserName { get; set; }
        /// <summary>
        /// 推单日期
        /// </summary>
        /// <returns></returns>
        public DateTime? PushDate { get; set; }
        /// <summary>
        /// 撤单原因
        /// </summary>
        /// <returns></returns>
        public string PushBackReason { get; set; }
        /// <summary>
        /// 撤单附件
        /// </summary>
        /// <returns></returns>
        public string PushBackPath { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        /// <returns></returns>
        [Column("CREATEDATE")]
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 添加人Id
        /// </summary>
        /// <returns></returns>
        [Column("CREATEUSERID")]
        public string CreateUserId { get; set; }
        /// <summary>
        /// 添加人
        /// </summary>
        /// <returns></returns>
        [Column("CREATEUSERNAME")]
        public string CreateUserName { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        /// <returns></returns>
        [Column("STATUS")]
        public int? Status { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        /// <returns></returns>
        [Column("ISDEL")]
        public int? IsDel { get; set; }
        /// <summary>
        /// 备用
        /// </summary>
        /// <returns></returns>
        [Column("SPAREFIELD")]
        public string SpareField { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        [Column("REMARK")]
        public string Remark { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        /// <returns></returns>
        [Column("MODIFYDATE")]
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// 修改用户主键
        /// </summary>
        /// <returns></returns>
        [Column("MODIFYUSERID")]
        public string ModifyUserId { get; set; }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <returns></returns>
        [Column("MODIFYUSERNAME")]
        public string ModifyUserName { get; set; }


        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public int? DeleteMark { get; set; }
        /// <summary>
        /// 可用
        /// </summary>
        /// <returns></returns>
        public int? EnabledMark { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        /// <returns></returns>
        public int? SortCode { get; set; }
        /// <summary>
        /// 完成标识
        /// </summary>
        /// <returns></returns>
        public int? OverMark { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.ProduceId = this.ProduceCode;//跟销售编号一致
            this.CreateDate = DateTime.Now;
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().UserName;

            this.DownMark = 0;//下单
            this.PushMark = 0;//推单

            this.EndDate = DateTime.Now.AddDays(30).Date;//最迟交付日期
            this.KaiLiaoMark = 1;//默认选择5工序
            this.FengBianMark = 1;
            this.PaiZuanMark = 1;
            this.ShiZhuangMark = 1;
            this.BaoZhuangMark = 1;
            this.EnterMark = 0;//入库
            this.SendMark = 0;//发货
            this.SendOutMark = 0;//实际发货
            this.OverMark = 0;//结束
            this.DeleteMark = 0;
            this.EnabledMark = 1;
        }
        /// <summary>
        /// 编辑调用
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