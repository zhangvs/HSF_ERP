using System;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.BaseManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2017-06-30 10:28
    /// 描 述：供应商
    /// </summary>
    public class Supplier_InfoEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 唯一标识符
        /// </summary>
        /// <returns></returns>
        public string ID { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        /// <returns></returns>
        public string Code { get; set; }
        /// <summary>
        /// 全名
        /// </summary>
        /// <returns></returns>
        public string FullName { get; set; }
        /// <summary>
        /// 简称
        /// </summary>
        /// <returns></returns>
        public string ShortName { get; set; }
        /// <summary>
        /// 助记码
        /// </summary>
        /// <returns></returns>
        public string Namepy { get; set; }
        /// <summary>
        /// 单位地址
        /// </summary>
        /// <returns></returns>
        public string Address { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        /// <returns></returns>
        public string Contract { get; set; }
        /// <summary>
        /// 证件号
        /// </summary>
        /// <returns></returns>
        public string IDNo { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        /// <returns></returns>
        public string Mobile { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        /// <returns></returns>
        public string Telephone { get; set; }
        /// <summary>
        /// 传真
        /// </summary>
        /// <returns></returns>
        public string Fax { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        /// <returns></returns>
        public string Province { get; set; }
        /// <summary>
        /// 市
        /// </summary>
        /// <returns></returns>
        public string City { get; set; }
        /// <summary>
        /// 县
        /// </summary>
        /// <returns></returns>
        public string Country { get; set; }
        /// <summary>
        /// 税号
        /// </summary>
        /// <returns></returns>
        public string TaxNo { get; set; }
        /// <summary>
        /// 电子信箱
        /// </summary>
        /// <returns></returns>
        public string Email { get; set; }
        /// <summary>
        /// 开户行
        /// </summary>
        /// <returns></returns>
        public string Opeingbank { get; set; }
        /// <summary>
        /// 开户行账号
        /// </summary>
        /// <returns></returns>
        public string Account { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        public string Remark { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 创建人Id
        /// </summary>
        /// <returns></returns>
        public string CreatorId { get; set; }
        /// <summary>
        /// 创建人名称
        /// </summary>
        /// <returns></returns>
        public string CreatorName { get; set; }
        /// <summary>
        /// 上级客户Id
        /// </summary>
        /// <returns></returns>
        public string ParentId { get; set; }
        /// <summary>
        /// 是否需要质检
        /// </summary>
        /// <returns></returns>
        public int? IsNeedCheck { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        /// <returns></returns>
        public int? Status { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.ID = Guid.NewGuid().ToString();
            this.CreateTime = DateTime.Now;
            this.CreatorId = OperatorProvider.Provider.Current().UserId;
            this.CreatorName = OperatorProvider.Provider.Current().UserName;
                                            }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.ID = keyValue;
            this.CreateTime = DateTime.Now;
            this.CreatorId = OperatorProvider.Provider.Current().UserId;
            this.CreatorName = OperatorProvider.Provider.Current().UserName;
                                            }
        #endregion
    }
}