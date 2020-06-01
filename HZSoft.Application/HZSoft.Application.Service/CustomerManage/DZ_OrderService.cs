using HZSoft.Application.Code;
using HZSoft.Application.Entity.BaseManage;
using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.IService.CustomerManage;
using HZSoft.Application.IService.SystemManage;
using HZSoft.Application.Service.SystemManage;
using HZSoft.Data.Repository;
using HZSoft.Util;
using HZSoft.Util.Extension;
using HZSoft.Util.WebControl;
using HZSoft.Util.WeChat.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace HZSoft.Application.Service.CustomerManage
{
    /// <summary>
    /// 版 本 6.1
    /// 
    /// 创 建：超级管理员
    /// 日 期：2019-11-17 08:11
    /// 描 述：DZ_Order
    /// </summary>
    public class DZ_OrderService : RepositoryFactory<DZ_OrderEntity>, DZ_OrderIService
    {
        private ICodeRuleService coderuleService = new CodeRuleService();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<DZ_OrderEntity> GetPageList(Pagination pagination, string queryJson)
        {
            string strSql = $"select * from DZ_Order where 1=1";
            var expression = LinqExtensions.True<DZ_OrderEntity>();
            var queryParam = queryJson.ToJObject();
            //成立日期
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and CreateDate >= '" + startTime + "' and CreateDate < '" + endTime + "'";
            }
            //公司名
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "Code":              //客户编号
                        strSql += " and Code  like '%" + keyword + "%' ";
                        break;
                    case "CompanyName":            //经销商
                        strSql += " and CompanyName  like '%" + keyword + "%' ";
                        break;
                    case "CustomerName":            //客户
                        strSql += " and CustomerName  like '%" + keyword + "%' ";
                        break;
                    case "CheckTuUserName":             //审图人
                        strSql += " and CheckTuUserName  like '%" + keyword + "%' ";
                        break;
                    case "ChaiUserName":       //拆单人
                        strSql += " and ChaiUserName  like '%" + keyword + "%' ";
                        break;
                    case "CheckUserName":       //审核人
                        strSql += " and CheckUserName  like '%" + keyword + "%' ";
                        break;
                    default:
                        break;
                }
            }
            //销售编号
            if (!queryParam["Code"].IsEmpty())
            {
                string Code = queryParam["Code"].ToString();
                strSql += " and Code like '%" + Code + "%'";
            }

            //公司名
            if (!queryParam["CompanyName"].IsEmpty())
            {
                string CompanyName = queryParam["CompanyName"].ToString();
                strSql += " and CompanyName like '%" + CompanyName + "%'";
            }
            //客户名
            if (!queryParam["CustomerName"].IsEmpty())
            {
                string CustomerName = queryParam["CustomerName"].ToString();
                strSql += " and CustomerName like '%" + CustomerName + "%'";
            }
            //销售人
            if (!queryParam["SalesmanUserName"].IsEmpty())
            {
                string SalesmanUserName = queryParam["SalesmanUserName"].ToString();
                strSql += " and SalesmanUserName like '%" + SalesmanUserName + "%'";
            }
            //审图人
            if (!queryParam["CheckTuUserName"].IsEmpty())
            {
                string CheckTuUserName = queryParam["CheckTuUserName"].ToString();
                strSql += " and CheckTuUserName like '%" + CheckTuUserName + "%'";
            }
            //拆单人
            if (!queryParam["ChaiUserName"].IsEmpty())
            {
                string ChaiUserName = queryParam["ChaiUserName"].ToString();
                strSql += " and ChaiUserName like '%" + ChaiUserName + "%'";
            }
            //审核人
            if (!queryParam["CheckUserName"].IsEmpty())
            {
                string CheckUserName = queryParam["CheckUserName"].ToString();
                strSql += " and CheckUserName like '%" + CheckUserName + "%'";
            }

            //审图
            if (!queryParam["CheckTuMark"].IsEmpty())
            {
                int CheckTuMark = queryParam["CheckTuMark"].ToInt();
                if (CheckTuMark == 0)
                {
                    strSql += " and (CheckTuMark = 0 or CheckTuMark = -1 or ChaiMark = -1)";//1.还未处理的，2.驳回前一步的，3.后一步驳回的
                }
                else
                {
                    strSql += " and (CheckTuMark  = 1 or CheckTuMark  = 2) ";//处理完的
                }
            }
            //拆单
            if (!queryParam["ChaiMark"].IsEmpty())
            {
                int ChaiMark = queryParam["ChaiMark"].ToInt();
                if (ChaiMark == 0)
                {
                    strSql += " and (ChaiMark = 0 or ChaiMark = -1 or CheckMark = -1)";//1.还未处理的，2.驳回前一步的，3.后一步驳回的
                }
                else
                {
                    strSql += " and ChaiMark = 1 ";//处理完的
                }
            }
            //审核
            if (!queryParam["CheckMark"].IsEmpty())
            {
                int CheckMark = queryParam["CheckMark"].ToInt();
                if (CheckMark == 0)
                {
                    strSql += " and (CheckMark = 0 or CheckMark = -1 or MoneyMark = -1)";//1.还未处理的，2.驳回前一步的，3.后一步驳回的
                }
                else
                {
                    strSql += " and CheckMark = 1 ";//处理完的
                }
            }
            //报价
            if (!queryParam["MoneyMark"].IsEmpty())
            {
                int MoneyMark = queryParam["MoneyMark"].ToInt();
                if (MoneyMark == 0)
                {
                    strSql += " and (MoneyMark = 0 or MoneyMark = -1 or MoneyOkMark = -1)";//1.还未处理的，2.驳回前一步的，3.后一步驳回的
                }
                else
                {
                    strSql += " and MoneyMark = 1 ";//处理完的
                }
            }
            //报价审核
            if (!queryParam["MoneyOkMark"].IsEmpty())
            {
                int MoneyOkMark = queryParam["MoneyOkMark"].ToInt();
                if (MoneyOkMark == 0)
                {
                    strSql += " and (MoneyOkMark = 0 or MoneyOkMark = -1 or DownMark = -1)";//1.还未处理的，2.驳回前一步的，3.后一步驳回的
                }
                else
                {
                    strSql += " and MoneyOkMark = 1 ";//处理完的
                }
            }
            //下单
            if (!queryParam["DownMark"].IsEmpty())
            {
                int DownMark = queryParam["DownMark"].ToInt();
                if (DownMark == 0)
                {
                    strSql += " and (DownMark = 0 or DownMark = -1 or PushMark = -1)";//1.还未处理的，2.驳回前一步的，3.后一步驳回的
                }
                else
                {
                    strSql += " and DownMark = 1 ";//处理完的
                }
            }
            //推单
            if (!queryParam["PushMark"].IsEmpty())
            {
                int PushMark = queryParam["PushMark"].ToInt();
                if (PushMark == 0)
                {
                    strSql += " and (PushMark = 0 or PushMark = -1)";//1.还未处理的，2.驳回前一步的，3.后一步驳回的
                }
                else
                {
                    strSql += " and PushMark = 1 ";//处理完的
                }
            }
            //未报价
            if (!queryParam["NoMoneyMark"].IsEmpty())
            {
                int noMoneyMark = queryParam["NoMoneyMark"].ToInt();
                //noMoneyMark = noMoneyMark == 1 ? 0 : 1;
                //选中了未报价按钮
                if (noMoneyMark == 1)
                {
                    strSql += " and MoneyMark  != 1";
                }
            }
            //未下单
            if (!queryParam["NoDownMark"].IsEmpty())
            {
                int noDownMark = queryParam["NoDownMark"].ToInt();
                if (noDownMark == 1)
                {
                    strSql += " and DownMark  != 1";
                }
            }
            //销售人
            if (!queryParam["CreateUserId"].IsEmpty())
            {
                string CreateUserId = queryParam["CreateUserId"].ToString();
                strSql += " and CreateUserId = '" + CreateUserId + "'";
            }
            else
            {
                if (!OperatorProvider.Provider.Current().IsSystem)
                {
                    string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
                    strSql += " and CreateUserId in (" + dataAutor + ")";
                }
            }
            //支付状态
            if (!queryParam["PaymentState"].IsEmpty())
            {
                int PaymentState = queryParam["PaymentState"].ToInt();
                if (PaymentState == 2)
                {
                    strSql += " and PaymentState >= 2 ";
                }
                else
                {
                    strSql += " and PaymentState  = " + PaymentState;
                }

            }
            //入库
            if (!queryParam["EnterMark"].IsEmpty())
            {
                int EnterMark = queryParam["EnterMark"].ToInt();
                strSql += " and EnterMark  = " + EnterMark;
            }
            //发货通知
            if (!queryParam["SendMark"].IsEmpty())
            {
                int SendMark = queryParam["SendMark"].ToInt();
                strSql += " and SendMark  = " + SendMark;
            }
            //发货
            if (!queryParam["SendOutMark"].IsEmpty())
            {
                int SendOutMark = queryParam["SendOutMark"].ToInt();
                strSql += " and SendOutMark  = " + SendOutMark;
            }
            //结束
            if (!queryParam["OverMark"].IsEmpty())
            {
                int OverMark = queryParam["OverMark"].ToInt();
                strSql += " and OverMark  = " + OverMark;
            }
            //作废
            if (!queryParam["DeleteMark"].IsEmpty())
            {
                int DeleteMark = queryParam["DeleteMark"].ToInt();
                strSql += " and DeleteMark  = " + DeleteMark;
            }
            else
            {
                strSql += " and DeleteMark  = 0";//默认不删除的
            }
            return this.BaseRepository().FindList(strSql.ToString(), pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<DZ_OrderEntity> GetList(string queryJson)
        {
            string strSql = $"select * from DZ_Order where DeleteMark=0 ";
            var expression = LinqExtensions.True<DZ_OrderEntity>();
            var queryParam = queryJson.ToJObject();
            //成立日期
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and CreateDate >= '" + startTime + "' and CreateDate < '" + endTime + "'";
            }
            //公司名
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "Code":              //客户编号
                        strSql += " and Code  like '%" + keyword + "%' ";
                        break;
                    case "CompanyName":            //经销商
                        strSql += " and CompanyName  like '%" + keyword + "%' ";
                        break;
                    case "CustomerName":            //客户
                        strSql += " and CustomerName  like '%" + keyword + "%' ";
                        break;
                    case "CheckTuUserName":             //审图人
                        strSql += " and CheckTuUserName  like '%" + keyword + "%' ";
                        break;
                    case "ChaiUserName":       //拆单人
                        strSql += " and ChaiUserName  like '%" + keyword + "%' ";
                        break;
                    case "CheckUserName":       //审核人
                        strSql += " and CheckUserName  like '%" + keyword + "%' ";
                        break;
                    default:
                        break;
                }
            }
            //销售编号
            if (!queryParam["Code"].IsEmpty())
            {
                string Code = queryParam["Code"].ToString();
                strSql += " and Code like '%" + Code + "%'";
            }

            //公司名
            if (!queryParam["CompanyName"].IsEmpty())
            {
                string CompanyName = queryParam["CompanyName"].ToString();
                strSql += " and CompanyName like '%" + CompanyName + "%'";
            }
            //客户名
            if (!queryParam["CustomerName"].IsEmpty())
            {
                string CustomerName = queryParam["CustomerName"].ToString();
                strSql += " and CustomerName like '%" + CustomerName + "%'";
            }
            //销售人
            if (!queryParam["SalesmanUserName"].IsEmpty())
            {
                string SalesmanUserName = queryParam["SalesmanUserName"].ToString();
                strSql += " and SalesmanUserName like '%" + SalesmanUserName + "%'";
            }
            //审图人
            if (!queryParam["CheckTuUserName"].IsEmpty())
            {
                string CheckTuUserName = queryParam["CheckTuUserName"].ToString();
                strSql += " and CheckTuUserName like '%" + CheckTuUserName + "%'";
            }
            //拆单人
            if (!queryParam["ChaiUserName"].IsEmpty())
            {
                string ChaiUserName = queryParam["ChaiUserName"].ToString();
                strSql += " and ChaiUserName like '%" + ChaiUserName + "%'";
            }
            //审核人
            if (!queryParam["CheckUserName"].IsEmpty())
            {
                string CheckUserName = queryParam["CheckUserName"].ToString();
                strSql += " and CheckUserName like '%" + CheckUserName + "%'";
            }

            //审图
            if (!queryParam["CheckTuMark"].IsEmpty())
            {
                int CheckTuMark = queryParam["CheckTuMark"].ToInt();
                if (CheckTuMark == 1)
                {
                    strSql += " and (CheckTuMark  = 1 or CheckTuMark  = 2) ";
                }
                else
                {
                    strSql += " and CheckTuMark  = " + CheckTuMark;
                }
            }

            //审图
            if (!queryParam["CheckTuMark"].IsEmpty())
            {
                int CheckTuMark = queryParam["CheckTuMark"].ToInt();
                if (CheckTuMark == 0)
                {
                    strSql += " and (CheckTuMark = 0 or CheckTuMark = -1 or ChaiMark = -1)";//1.还未处理的，2.驳回前一步的，3.后一步驳回的
                }
                else
                {
                    strSql += " and (CheckTuMark  = 1 or CheckTuMark  = 2) ";//处理完的
                }
            }
            //拆单
            if (!queryParam["ChaiMark"].IsEmpty())
            {
                int ChaiMark = queryParam["ChaiMark"].ToInt();
                if (ChaiMark == 0)
                {
                    strSql += " and (ChaiMark = 0 or ChaiMark = -1 or CheckMark = -1)";//1.还未处理的，2.驳回前一步的，3.后一步驳回的
                }
                else
                {
                    strSql += " and ChaiMark = 1 ";//处理完的
                }
            }
            //审核
            if (!queryParam["CheckMark"].IsEmpty())
            {
                int CheckMark = queryParam["CheckMark"].ToInt();
                if (CheckMark == 0)
                {
                    strSql += " and (CheckMark = 0 or CheckMark = -1 or MoneyMark = -1)";//1.还未处理的，2.驳回前一步的，3.后一步驳回的
                }
                else
                {
                    strSql += " and CheckMark = 1 ";//处理完的
                }
            }
            //报价
            if (!queryParam["MoneyMark"].IsEmpty())
            {
                int MoneyMark = queryParam["MoneyMark"].ToInt();
                if (MoneyMark == 0)
                {
                    strSql += " and (MoneyMark = 0 or MoneyMark = -1 or MoneyOkMark = -1)";//1.还未处理的，2.驳回前一步的，3.后一步驳回的
                }
                else
                {
                    strSql += " and MoneyMark = 1 ";//处理完的
                }
            }
            //报价审核
            if (!queryParam["MoneyOkMark"].IsEmpty())
            {
                int MoneyOkMark = queryParam["MoneyOkMark"].ToInt();
                if (MoneyOkMark == 0)
                {
                    strSql += " and (MoneyOkMark = 0 or MoneyOkMark = -1 or DownMark = -1)";//1.还未处理的，2.驳回前一步的，3.后一步驳回的
                }
                else
                {
                    strSql += " and MoneyOkMark = 1 ";//处理完的
                }
            }
            //下单
            if (!queryParam["DownMark"].IsEmpty())
            {
                int DownMark = queryParam["DownMark"].ToInt();
                if (DownMark == 0)
                {
                    strSql += " and (DownMark = 0 or DownMark = -1 or PushMark = -1)";//1.还未处理的，2.驳回前一步的，3.后一步驳回的
                }
                else
                {
                    strSql += " and DownMark = 1 ";//处理完的
                }
            }
            //推单
            if (!queryParam["PushMark"].IsEmpty())
            {
                int PushMark = queryParam["PushMark"].ToInt();
                if (PushMark == 0)
                {
                    strSql += " and (PushMark = 0 or PushMark = -1)";//1.还未处理的，2.驳回前一步的，3.后一步驳回的
                }
                else
                {
                    strSql += " and PushMark = 1 ";//处理完的
                }
            }
            //未报价
            if (!queryParam["NoMoneyMark"].IsEmpty())
            {
                int noMoneyMark = queryParam["NoMoneyMark"].ToInt();
                //noMoneyMark = noMoneyMark == 1 ? 0 : 1;
                //选中了未报价按钮
                if (noMoneyMark == 1)
                {
                    strSql += " and MoneyMark  != 1";
                }
            }
            //未下单
            if (!queryParam["NoDownMark"].IsEmpty())
            {
                int noDownMark = queryParam["NoDownMark"].ToInt();
                if (noDownMark == 1)
                {
                    strSql += " and DownMark  != 1";
                }
            }

            //支付状态
            if (!queryParam["PaymentState"].IsEmpty())
            {
                int PaymentState = queryParam["PaymentState"].ToInt();
                if (PaymentState == 2)
                {
                    strSql += " and PaymentState >= 2 ";
                }
                else
                {
                    strSql += " and PaymentState  = " + PaymentState;
                }

            }
            //入库
            if (!queryParam["EnterMark"].IsEmpty())
            {
                int EnterMark = queryParam["EnterMark"].ToInt();
                strSql += " and EnterMark  = " + EnterMark;
            }
            //发货
            if (!queryParam["SendMark"].IsEmpty())
            {
                int SendMark = queryParam["SendMark"].ToInt();
                strSql += " and SendMark  = " + SendMark;
            }
            return this.BaseRepository().FindList(strSql.ToString());
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public DZ_OrderEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            DZ_OrderEntity entity = GetEntity(keyValue);
            entity.Modify(keyValue);
            entity.DeleteMark = 1;
            this.BaseRepository().Update(entity);

            //this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, DZ_OrderEntity entity)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    //先拿到老的销售单状态
                    DZ_OrderEntity oldEntity = GetEntity(keyValue);

                    #region 2审图
                    //审图通过之后，给拆单人发消息提醒
                    if (entity.CheckTuMark > 0 && oldEntity.CheckTuMark != 1)
                    {
                        //发微信模板消息---接单之后，给审图人提醒--刘琛oA-EC1X6RWfW1_DNJ_VNiA3uhOYg
                        //订单生成通知（拆单提醒）
                        TemplateWxApp.SendTemplateNew("oA-EC1X6RWfW1_DNJ_VNiA3uhOYg", "您好，有新的订单需要拆单!", entity.OrderTitle, entity.Code, "请进行拆单。");
                        RecordHelp.AddRecord(4, entity.Id, "审图");
                    }
                    //审图驳回之后，给销售人发消息提醒
                    if (entity.CheckTuMark == -1 && oldEntity.CheckTuMark != -1)
                    {
                        var hsf_CardList = db.IQueryable<Hsf_CardEntity>(t => t.Name.Equals(oldEntity.CreateUserName));//发送给创建订单的人，店长代替店员创建，所以店长能看见拆单报价
                        if (hsf_CardList.Count() != 0)
                        {
                            var hsf_CardEntity = hsf_CardList.First();
                            //不直接给销售员报价，只有店长才能知道报价
                            string backMsg = TemplateWxApp.SendTemplateReject(hsf_CardEntity.OpenId, "您好，审图人驳回订单!", oldEntity.Code, oldEntity.OrderTitle);
                            if (backMsg != "ok")
                            {
                                //业务员没有关注公众号，报错：微信Post请求发生错误！错误代码：43004，说明：require subscribe hint: [ziWtva03011295]
                                LogHelper.AddLog(entity.SalesmanUserName + "没有关注公众号");//记录日志
                            }
                        }
                        RecordHelp.AddRecord(4, entity.Id, "审图驳回");
                    }
                    #endregion

                    #region 3拆单
                    if (entity.ChaiMark > 0 && oldEntity.ChaiMark != 1)
                    {
                        RecordHelp.AddRecord(4, entity.Id, "拆单");
                    }
                    if (entity.ChaiMark == -1 && oldEntity.ChaiMark != -1)
                    {
                        RecordHelp.AddRecord(4, entity.Id, "拆单驳回");
                        //发送给审图人驳回通知
                        var hsf_CardList = db.IQueryable<Hsf_CardEntity>(t => t.Name.Equals(oldEntity.CheckTuUserName));
                        if (hsf_CardList.Count() != 0)
                        {
                            var hsf_CardEntity = hsf_CardList.First();
                            string backMsg = TemplateWxApp.SendTemplateReject(hsf_CardEntity.OpenId, "您好，拆单人人驳回订单!", oldEntity.Code, oldEntity.OrderTitle);
                            if (backMsg != "ok")
                            {
                                LogHelper.AddLog(entity.SalesmanUserName + "没有关注公众号");
                            }
                        }
                    }
                    #endregion


                    #region 4审核
                    if (entity.CheckMark > 0 && oldEntity.CheckMark != 1)
                    {
                        RecordHelp.AddRecord(4, entity.Id, "审核");
                    }
                    if (entity.CheckMark == -1 && oldEntity.CheckMark != -1)
                    {
                        RecordHelp.AddRecord(4, entity.Id, "审核驳回");
                        //发送给拆单人驳回通知
                        var hsf_CardList = db.IQueryable<Hsf_CardEntity>(t => t.Name.Equals(oldEntity.ChaiUserName));
                        if (hsf_CardList.Count() != 0)
                        {
                            var hsf_CardEntity = hsf_CardList.First();
                            string backMsg = TemplateWxApp.SendTemplateReject(hsf_CardEntity.OpenId, "您好，审核人人驳回订单!", oldEntity.Code, oldEntity.OrderTitle);
                            if (backMsg != "ok")
                            {
                                LogHelper.AddLog(entity.SalesmanUserName + "没有关注公众号");//记录日志
                            }
                        }
                    }
                    #endregion

                    #region 5报价
                    //报价之后，给财务发消息提醒
                    if (entity.MoneyMark == 1 && oldEntity.MoneyMark != 1)
                    {
                        //发微信模板消息---研发报价之后，给财务提醒--刘一珠oA-EC1X0OoVmzyowOqxYHlY5NHX4
                        //订单生成通知（报价提醒）
                        TemplateWxApp.SendTemplateMoney("oA-EC1X0OoVmzyowOqxYHlY5NHX4", "您好，有新的报价需要审核!", "研发中心", entity.OrderTitle, entity.Code, "请进行报价审核。");
                        RecordHelp.AddRecord(4, entity.Id, "报价");
                    }
                    if (entity.MoneyMark == -1 && oldEntity.MoneyMark != -1)
                    {
                        RecordHelp.AddRecord(4, entity.Id, "报价驳回");
                        //发送给审核人驳回通知
                        var hsf_CardList = db.IQueryable<Hsf_CardEntity>(t => t.Name.Equals(oldEntity.CheckUserName));
                        if (hsf_CardList.Count() != 0)
                        {
                            var hsf_CardEntity = hsf_CardList.First();
                            string backMsg = TemplateWxApp.SendTemplateReject(hsf_CardEntity.OpenId, "您好，报价人驳回订单!", oldEntity.Code, oldEntity.OrderTitle);
                            if (backMsg != "ok")
                            {
                                LogHelper.AddLog(entity.SalesmanUserName + "没有关注公众号");//记录日志
                            }
                        }
                    }
                    #endregion
                    entity.Modify(keyValue);
                    this.BaseRepository().Update(entity);//要放在oldEntity后面修改才可以，否则oldEntity和entity都是一样的了
                }
                else
                {
                    entity.Create();
                    //this.BaseRepository().Insert(entity);
                    db.Insert<DZ_OrderEntity>(entity);
                    coderuleService.UseRuleSeed(entity.CreateUserId, "", ((int)CodeRuleEnum.DZ_Order).ToString(), db);//占用单据号
                    db.Commit();

                    //发微信模板消息---接单之后，给审图人提醒--刘明存oA-EC1WVqHl_gsBM3We2rgOHIMEQ
                    //订单生成通知（审图提醒）
                    TemplateWxApp.SendTemplateNew("oA-EC1WVqHl_gsBM3We2rgOHIMEQ", "您好，有新的订单需要审图!", entity.OrderTitle, entity.Code, "请进行审图。");

                    RecordHelp.AddRecord(4, entity.Id, "接单");
                }
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }


        /// <summary>
        /// 财务报价审核
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public void UpdateMoneyOkState(string keyValue, int? state)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    //获取老的在前
                    DZ_OrderEntity oldEntity = GetEntity(keyValue);
                    //当前entity
                    DZ_OrderEntity entity = new DZ_OrderEntity()
                    {
                        MoneyOkMark = state,
                        MoneyOkDate = DateTime.Now,
                        MoneyOkUserId = OperatorProvider.Provider.Current().UserId,
                        MoneyOkUserName = OperatorProvider.Provider.Current().UserName
                    };
                    entity.Modify(keyValue);
                    db.Update<DZ_OrderEntity>(entity);

                    //第一次报价审核的通知
                    if (entity.MoneyOkMark == 1 && oldEntity.MoneyOkMark != 1)
                    {
                        //发微信模板消息（报价审核提醒）
                        if (!string.IsNullOrEmpty(oldEntity.CreateUserName))
                        {
                            //发送给创建订单的人，店长代替店员创建，所以店长能看见拆单报价
                            var hsf_CardList = db.IQueryable<Hsf_CardEntity>(t => t.Name.Equals(oldEntity.CreateUserName));
                            if (hsf_CardList.Count() != 0)
                            {
                                var hsf_CardEntity = hsf_CardList.First();
                                //不直接给销售员报价，只有店长才能知道报价
                                string backMsg = TemplateWxApp.SendTemplateMoneyOk(hsf_CardEntity.OpenId, "您好，您的订单报价已审核完成!", oldEntity.Code, oldEntity.OrderTitle, entity.MoneyAccounts.ToString(), "");
                                if (backMsg != "ok")
                                {
                                    //业务员没有关注公众号，报错：微信Post请求发生错误！错误代码：43004，说明：require subscribe hint: [ziWtva03011295]
                                    LogHelper.AddLog(entity.SalesmanUserName + "没有关注公众号");//记录日志
                                }
                            }
                        }


                        //报价审核改变生产单报价审核状态
                        Sale_CustomerEntity sale_CustomerEntity = db.FindEntity<Sale_CustomerEntity>(t => t.ProduceId == oldEntity.Code);//老销售单code会生成生产单id
                        if (sale_CustomerEntity != null)
                        {
                            //生产单存在的话，说明已经收款过
                            sale_CustomerEntity.MoneyOkMark = state;
                            sale_CustomerEntity.MoneyOkDate = DateTime.Now;
                            db.Update<Sale_CustomerEntity>(sale_CustomerEntity);

                            //收款+报价审核=提醒下单

                            //发微信模板消息---财务已经报价审核并收款确认之后，给张宝莲发消息提醒oA-EC1bJnd0KFBuOy0joJvUOGwwk
                            //订单生成通知（7下单提醒）
                            TemplateWxApp.SendTemplateNew("oA-EC1bJnd0KFBuOy0joJvUOGwwk", "您好，有新的订单需要下单!", oldEntity.OrderTitle, oldEntity.Code, "请进行生产下单。");
                        }
                        else
                        {
                            //如果不收预付款，并且还没下单的，报价审核完毕之后可以直接创建生产单，开始提醒下单
                            if (oldEntity.FrontMark == 0 && oldEntity.DownMark != 1)
                            {
                                //自动创建【生产单】主单部分*****************
                                Sale_Customer_Main.SaveSaleMain(db, oldEntity);//如果下单不及时，可能重复创建
                            }
                        }
                        RecordHelp.AddRecord(4, entity.Id, "报价审核");
                    }

                    //第一次报价驳回的通知
                    if (entity.MoneyOkMark == -1 && oldEntity.MoneyOkMark != -1)
                    {
                        //发微信模板消息（报价审核驳回提醒）-给报价人提醒
                        var hsf_CardList = db.IQueryable<Hsf_CardEntity>(t => t.Name.Equals(oldEntity.MoneyUserName));
                        if (hsf_CardList.Count() != 0)
                        {
                            var hsf_CardEntity = hsf_CardList.First();
                            string backMsg = TemplateWxApp.SendTemplateReject(hsf_CardEntity.OpenId, "您好，报价审核人驳回订单!", oldEntity.Code, oldEntity.OrderTitle);
                            if (backMsg != "ok")
                            {
                                LogHelper.AddLog(entity.SalesmanUserName + "没有关注公众号");//记录日志
                            }
                        }


                        //报价审核改变生产单报价审核状态
                        Sale_CustomerEntity sale_CustomerEntity = db.FindEntity<Sale_CustomerEntity>(t => t.ProduceId == oldEntity.Code);//老销售单code会生成生产单id
                        if (sale_CustomerEntity != null)
                        {
                            //生产单存在的话，说明已经收款过
                            sale_CustomerEntity.MoneyOkMark = state;
                            sale_CustomerEntity.MoneyOkDate = DateTime.Now;
                            db.Update<Sale_CustomerEntity>(sale_CustomerEntity);
                        }

                        RecordHelp.AddRecord(4, entity.Id, "报价审核驳回");
                    }


                    db.Commit();//放在最后
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// 签收确认
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveSigned(string keyValue, DZ_OrderEntity entity)
        {
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.Modify(keyValue);
                    entity.SignedMark = 1;
                    entity.SignedDate = DateTime.Now;
                    entity.SignedUserId = OperatorProvider.Provider.Current().UserId;
                    entity.SignedUserName = OperatorProvider.Provider.Current().UserName;
                    this.BaseRepository().Update(entity);
                    RecordHelp.AddRecord(4, entity.Id, "签收确认");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }




        /// <summary>
        /// 结单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public void UpdateOverState(string keyValue, int? state)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    DZ_OrderEntity entity = new DZ_OrderEntity()
                    {
                        OverMark = state
                    };
                    entity.Modify(keyValue);
                    db.Update<DZ_OrderEntity>(entity);


                    //生产单完成状态
                    Sale_CustomerEntity sale_CustomerEntity = db.FindEntity<Sale_CustomerEntity>(t => t.OrderId == keyValue);
                    if (sale_CustomerEntity != null)
                    {
                        sale_CustomerEntity.OverMark = state;
                        db.Update<Sale_CustomerEntity>(sale_CustomerEntity);
                    }

                    //入库单完成状态
                    Buys_OrderEntity buysEntity = db.FindEntity<Buys_OrderEntity>(t => t.OrderId == keyValue);
                    if (buysEntity != null)
                    {
                        buysEntity.OverMark = state;
                        db.Update<Buys_OrderEntity>(buysEntity);
                    }
                    db.Commit();
                    RecordHelp.AddRecord(4, entity.Id, "结单");

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
