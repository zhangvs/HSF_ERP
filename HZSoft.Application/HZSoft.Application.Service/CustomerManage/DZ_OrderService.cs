using HZSoft.Application.Code;
using HZSoft.Application.Entity.BaseManage;
using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.IService.BaseManage;
using HZSoft.Application.IService.CustomerManage;
using HZSoft.Application.IService.SystemManage;
using HZSoft.Application.Service.BaseManage;
using HZSoft.Application.Service.SystemManage;
using HZSoft.Data.Repository;
using HZSoft.Util;
using HZSoft.Util.Extension;
using HZSoft.Util.WebControl;
using HZSoft.Util.WeChat.Comm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

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
        private Hsf_CardIService cardService = new Hsf_CardService();
        //private Sale_CustomerIService saleService = new Sale_CustomerService();这个造成重复调用死循环w3wp.exe 145000

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
            //标题模糊搜索
            if (!queryParam["OrderTitle"].IsEmpty())
            {
                string OrderTitle = queryParam["OrderTitle"].ToString();
                strSql += " and OrderTitle like '%" + OrderTitle + "%'";
            }
            //订单类型
            if (!queryParam["OrderType"].IsEmpty())
            {
                string OrderType = queryParam["OrderType"].ToString();
                if (OrderType == "-3")//非客诉单
                {
                    strSql += " and OrderType <> 3";
                }
                else
                {
                    strSql += " and OrderType = " + OrderType;
                }
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
            //标题模糊搜索
            if (!queryParam["OrderTitle"].IsEmpty())
            {
                string OrderTitle = queryParam["OrderTitle"].ToString();
                strSql += " and OrderTitle like '%" + OrderTitle + "%'";
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
        public void CardTempBack(string name, string code, string title, string reason, string desc)
        {
            var hsf_Card = cardService.GetEntityByName(name);//发送给创建订单的人，店长代替店员创建，所以店长能看见拆单报价
            if (hsf_Card != null)
            {
                //不直接给销售员报价，只有店长才能知道报价
                string backMsg = TemplateWxApp.SendTemplateBack(hsf_Card.OpenId, "您好，订单撤销通知!", code, title, reason, desc + "，请知晓。");//撤单人"+ OperatorProvider.Provider.Current().UserName
                if (backMsg != "ok")
                {
                    //业务员没有关注公众号，报错：微信Post请求发生错误！错误代码：43004，说明：require subscribe hint: [ziWtva03011295]
                    LogHelper.AddLog(name + "没有关注公众号");//记录日志
                }
            }
            else
            {
                LogHelper.AddLog(name + "没有关注公众号");//记录日志
            }
        }

        /// <summary>
        /// 一键撤单
        /// 您好，您的订单已撤销
        /// 订单号：123456
        /// 订单内容：管道疏通
        /// 撤销原因：呼叫错误
        /// 撤销时间：2019年6月14日16:02
        /// 感谢您的使用
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">撤销原因页面</param>
        public void BackForm(string keyValue, DZ_OrderEntity entity)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();

            DZ_OrderEntity oldEntity = GetEntity(keyValue);
            if (oldEntity.PushMark > 0)
            {
                entity.PushMark = -1;//推单过了
                Sale_CustomerEntity sale_CustomerEntity = db.FindEntity<Sale_CustomerEntity>(t => t.ProduceId == oldEntity.Code);//老销售单code会生成生产单id
                if (sale_CustomerEntity != null)
                {
                    //生产单存在的话，修改推单状态
                    sale_CustomerEntity.PushMark = -1;
                    db.Update<Sale_CustomerEntity>(sale_CustomerEntity);
                }
                TemplateWxApp.SendTemplateBack("oA-EC1W1BQZ46Wc8HPCZZUUFbE9M", "您好，订单撤销通知!", entity.Code, entity.OrderTitle, entity.PushBackReason, entity.PushBackDesc + "，请知晓。");

                RecordHelp.AddRecord(4, keyValue, "推单撤销");
            }
            if (oldEntity.DownMark > 0)
            {
                entity.DownMark = -1;//下单过了
                Sale_CustomerEntity sale_CustomerEntity = db.FindEntity<Sale_CustomerEntity>(t => t.ProduceId == oldEntity.Code);//老销售单code会生成生产单id
                if (sale_CustomerEntity != null)
                {
                    //生产单存在的话，修改下单状态
                    sale_CustomerEntity.DownMark = -1;
                    db.Update<Sale_CustomerEntity>(sale_CustomerEntity);
                }
                CardTempBack(oldEntity.DownUserName, entity.Code, entity.OrderTitle, entity.PushBackReason, entity.PushBackDesc);
                RecordHelp.AddRecord(4, keyValue, "下单撤销");
            }
            if (oldEntity.MoneyOkMark > 0)
            {
                entity.MoneyOkMark = -1;//报价审核过了
                Sale_CustomerEntity sale_CustomerEntity = db.FindEntity<Sale_CustomerEntity>(t => t.ProduceId == oldEntity.Code);//老销售单code会生成生产单id
                if (sale_CustomerEntity != null)
                {
                    //生产单存在的话，修改报价审核状态
                    sale_CustomerEntity.MoneyOkMark = -1;
                    db.Update<Sale_CustomerEntity>(sale_CustomerEntity);
                }
                TemplateWxApp.SendTemplateBack("oA-EC1bg4U16c63kR6yj51lA5AiM", "您好，订单撤销通知!", entity.Code, entity.OrderTitle, entity.PushBackReason, entity.PushBackDesc + "，请知晓。");
                RecordHelp.AddRecord(4, keyValue, "报价审核撤销");
            }
            if (oldEntity.MoneyMark > 0)
            {
                entity.MoneyMark = -1;//报价过了
                CardTempBack(oldEntity.MoneyUserName, entity.Code, entity.OrderTitle, entity.PushBackReason, entity.PushBackDesc);
                RecordHelp.AddRecord(4, keyValue, "报价撤销");
            }
            if (oldEntity.CheckMark > 0)
            {
                entity.CheckMark = -1;//审核过了
                CardTempBack(oldEntity.CheckUserName, entity.Code, entity.OrderTitle, entity.PushBackReason, entity.PushBackDesc);
                RecordHelp.AddRecord(4, keyValue, "审核撤销");
            }
            if (oldEntity.ChaiMark > 0)
            {
                entity.ChaiMark = -1;//拆单过了
                CardTempBack(oldEntity.ChaiUserName, entity.Code, entity.OrderTitle, entity.PushBackReason, entity.PushBackDesc);
                RecordHelp.AddRecord(4, keyValue, "拆单撤销");
            }
            if (oldEntity.CheckTuMark > 0)
            {
                entity.CheckTuMark = -1;//审图过了
                CardTempBack(oldEntity.CheckTuUserName, entity.Code, entity.OrderTitle, entity.PushBackReason, entity.PushBackDesc);
                RecordHelp.AddRecord(4, keyValue, "审图撤销");
            }

            entity.Modify(keyValue);
            db.Update<DZ_OrderEntity>(entity);
            db.Commit();
        }
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
                        RecordHelp.AddRecord(4, keyValue, "审图");
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
                                LogHelper.AddLog(entity.CreateUserName + "没有关注公众号");//记录日志
                            }
                        }
                        else
                        {
                            LogHelper.AddLog(entity.CreateUserName + "没有关注公众号");//记录日志
                        }
                        RecordHelp.AddRecord(4, keyValue, "审图驳回");
                    }
                    #endregion

                    #region 3拆单
                    if (entity.ChaiMark > 0 && oldEntity.ChaiMark != 1)
                    {
                        RecordHelp.AddRecord(4, keyValue, "拆单");
                    }
                    if (entity.ChaiMark == -1 && oldEntity.ChaiMark != -1)
                    {
                        RecordHelp.AddRecord(4, keyValue, "拆单驳回");
                        //发送给审图人驳回通知
                        var hsf_CardList = db.IQueryable<Hsf_CardEntity>(t => t.Name.Equals(oldEntity.CheckTuUserName));
                        if (hsf_CardList.Count() != 0)
                        {
                            var hsf_CardEntity = hsf_CardList.First();
                            string backMsg = TemplateWxApp.SendTemplateReject(hsf_CardEntity.OpenId, "您好，拆单人驳回订单!", oldEntity.Code, oldEntity.OrderTitle);
                            if (backMsg != "ok")
                            {
                                LogHelper.AddLog(entity.CheckTuUserName + "没有关注公众号");
                            }
                        }
                        else
                        {
                            LogHelper.AddLog(entity.CheckTuUserName + "没有关注公众号");//记录日志
                        }
                    }
                    #endregion


                    #region 4审核
                    if (entity.CheckMark > 0 && oldEntity.CheckMark != 1)
                    {
                        RecordHelp.AddRecord(4, keyValue, "审核");
                    }
                    if (entity.CheckMark == -1 && oldEntity.CheckMark != -1)
                    {
                        RecordHelp.AddRecord(4, keyValue, "审核驳回");
                        //发送给拆单人驳回通知
                        var hsf_CardList = db.IQueryable<Hsf_CardEntity>(t => t.Name.Equals(oldEntity.ChaiUserName));
                        if (hsf_CardList.Count() != 0)
                        {
                            var hsf_CardEntity = hsf_CardList.First();
                            string backMsg = TemplateWxApp.SendTemplateReject(hsf_CardEntity.OpenId, "您好，审核人驳回订单!", oldEntity.Code, oldEntity.OrderTitle);
                            if (backMsg != "ok")
                            {
                                LogHelper.AddLog(entity.ChaiUserName + "没有关注公众号");//记录日志
                            }
                        }
                        else
                        {
                            LogHelper.AddLog(entity.ChaiUserName + "没有关注公众号");//记录日志
                        }
                    }
                    #endregion

                    #region 5报价
                    //报价之后，给财务发消息提醒
                    if (entity.MoneyMark == 1 && oldEntity.MoneyMark != 1)
                    {
                        RecordHelp.AddRecord(4, keyValue, "报价");
                        if (entity.OrderType != 3)//不是客诉单
                        {
                            //发微信模板消息---研发报价之后，给财务提醒--刘一珠改刘庆莉oA-EC1bg4U16c63kR6yj51lA5AiM
                            //订单生成通知（报价提醒）
                            TemplateWxApp.SendTemplateMoney("oA-EC1bg4U16c63kR6yj51lA5AiM", "您好，有新的报价需要审核!", "研发中心", entity.OrderTitle, entity.Code, "请进行报价审核。");
                        }
                        else
                        {
                            //客诉单，不走财务
                            //如果不收预付款，并且还没下单的，报价审核完毕之后可以直接创建生产单，开始提醒下单
                            if (oldEntity.DownMark != 1)
                            {
                                oldEntity.MoneyOkMark = 1;
                                oldEntity.MoneyOkDate = DateTime.Now;
                                entity.MoneyOkMark = 1;//客诉单，默认报价审核，不走财务
                                entity.MoneyOkDate = DateTime.Now;
                                RecordHelp.AddRecord(4, keyValue, "客诉单跳过报价审核");
                                Sale_Customer_Main.SaveSaleMain(db, oldEntity);//如果下单不及时，可能重复创建

                                //发微信模板消息---财务已经报价审核并收款确认之后，给张宝莲发消息提醒oA-EC1bJnd0KFBuOy0joJvUOGwwk
                                //订单生成通知（7下单提醒）
                                TemplateWxApp.SendTemplateNew("oA-EC1bJnd0KFBuOy0joJvUOGwwk", "您好，有新的订单需要下单!", oldEntity.OrderTitle, oldEntity.Code, "请进行生产下单。");
                            }
                        }
                    }
                    if (entity.MoneyMark == -1 && oldEntity.MoneyMark != -1)
                    {
                        RecordHelp.AddRecord(4, keyValue, "报价驳回");
                        //发送给审核人驳回通知
                        var hsf_CardList = db.IQueryable<Hsf_CardEntity>(t => t.Name.Equals(oldEntity.CheckUserName));
                        if (hsf_CardList.Count() != 0)
                        {
                            var hsf_CardEntity = hsf_CardList.First();
                            string backMsg = TemplateWxApp.SendTemplateReject(hsf_CardEntity.OpenId, "您好，报价人驳回订单!", oldEntity.Code, oldEntity.OrderTitle);
                            if (backMsg != "ok")
                            {
                                LogHelper.AddLog(entity.CheckUserName + "没有关注公众号");//记录日志
                            }
                        }
                        else
                        {
                            LogHelper.AddLog(entity.CheckUserName + "没有关注公众号");//记录日志
                        }
                    }
                    #endregion

                    #region 修改付款方式，不需要收取预付款
                    //如果修改为不需要收取预付款，并且报价审核完成
                    if (entity.FrontMark == 0 && oldEntity.FrontMark == 1 && oldEntity.MoneyOkMark == 1)
                    {
                        //初始化生产单
                        Sale_CustomerEntity sale_CustomerEntity = db.FindEntity<Sale_CustomerEntity>(t => t.ProduceId == oldEntity.Code);//老销售单code会生成生产单id
                        if (sale_CustomerEntity == null)
                        {
                            //自动创建【生产单】主单部分*****************，重点：同步更新报价审核状态和报价审核时间
                            Sale_Customer_Main.SaveSaleMain(db, oldEntity);//如果下单不及时，可能重复创建
                        }
                        RecordHelp.AddRecord(4, keyValue, "修改为不需要收预付款");
                    }
                    #endregion

                    #region 修改付款方式，不需要收取尾款，同步到入库单AfterMark尾款状态
                    if (entity.AfterMark == 0 && oldEntity.AfterMark == 1 && oldEntity.PushMark == 1)//之前需要收尾款，现在不需要尾款,推单之后才有可能生成入库单
                    {
                        Buys_OrderEntity buys_CustomerEntity = db.FindEntity<Buys_OrderEntity>(t => t.Id == oldEntity.Code);//老销售单code会生成生产单id
                        if (buys_CustomerEntity != null)
                        {
                            buys_CustomerEntity.AfterMark = 0;
                            db.Update<Buys_OrderEntity>(buys_CustomerEntity);
                            RecordHelp.AddRecord(4, keyValue, "修改为不需要收尾款");
                        }
                    }
                    #endregion

                    entity.Modify(keyValue);
                    db.Update<DZ_OrderEntity>(entity);//要放在oldEntity后面修改才可以，否则oldEntity和entity都是一样的了
                    db.Commit();
                }
                else
                {
                    entity.Create();
                    //this.BaseRepository().Insert(entity);
                    db.Insert<DZ_OrderEntity>(entity);
                    coderuleService.UseRuleSeed(entity.CreateUserId, "", ((int)CodeRuleEnum.DZ_Order).ToString(), db);//占用单据号
                    db.Commit();

                    if (entity.OrderType != 3)//不是客诉单
                    {
                        //发微信模板消息---接单之后，给审图人提醒--刘明存oA-EC1WVqHl_gsBM3We2rgOHIMEQ
                        //订单生成通知（审图提醒）
                        TemplateWxApp.SendTemplateNew("oA-EC1WVqHl_gsBM3We2rgOHIMEQ", "您好，有新的订单需要审图!", entity.OrderTitle, entity.Code, "请进行审图。");
                    }
                    else
                    {
                        //发微信模板消息---客诉单接单之后，给审图人提醒--刘琛oA-EC1X6RWfW1_DNJ_VNiA3uhOYg
                        //订单生成通知（审图提醒）
                        TemplateWxApp.SendTemplateNew("oA-EC1X6RWfW1_DNJ_VNiA3uhOYg", "您好，有新的订单需要审图!", entity.OrderTitle, entity.Code, "请进行审图。");
                    }

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
                    if (oldEntity != null)
                    {
                        //第一次报价审核的通知，可重复操作 && oldEntity.MoneyOkMark != 1
                        if (state == 1)
                        {
                            RecordHelp.AddRecord(4, keyValue, "报价审核");
                            //发微信模板消息（报价审核提醒）
                            if (!string.IsNullOrEmpty(oldEntity.CreateUserName))
                            {
                                //发送给创建订单的人，店长代替店员创建，所以店长能看见拆单报价
                                var hsf_CardList = db.IQueryable<Hsf_CardEntity>(t => t.Name.Equals(oldEntity.CreateUserName));
                                if (hsf_CardList.Count() != 0)
                                {
                                    var hsf_CardEntity = hsf_CardList.First();
                                    //不直接给销售员报价，只有店长才能知道报价
                                    string backMsg = TemplateWxApp.SendTemplateMoneyOk(hsf_CardEntity.OpenId, "您好，您的订单报价已审核完成!", oldEntity.Code, oldEntity.OrderTitle, oldEntity.MoneyAccounts.ToString(), "");
                                    if (backMsg != "ok")
                                    {
                                        //业务员没有关注公众号，报错：微信Post请求发生错误！错误代码：43004，说明：require subscribe hint: [ziWtva03011295]
                                        LogHelper.AddLog(oldEntity.SalesmanUserName + "没有关注公众号");//记录日志
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
                                    //自动创建【生产单】主单部分*****************，重点：同步更新报价审核状态和报价审核时间
                                    oldEntity.MoneyOkMark = state;
                                    oldEntity.MoneyOkDate = DateTime.Now;
                                    Sale_Customer_Main.SaveSaleMain(db, oldEntity);//如果下单不及时，可能重复创建
                                }
                            }
                        }

                        //第一次报价驳回的通知
                        if (state == -1 && oldEntity.MoneyOkMark != -1)
                        {
                            //发微信模板消息（报价审核驳回提醒）-给报价人提醒
                            var hsf_CardList = db.IQueryable<Hsf_CardEntity>(t => t.Name.Equals(oldEntity.MoneyUserName));
                            if (hsf_CardList.Count() != 0)
                            {
                                var hsf_CardEntity = hsf_CardList.First();
                                string backMsg = TemplateWxApp.SendTemplateReject(hsf_CardEntity.OpenId, "您好，报价审核人驳回订单!", oldEntity.Code, oldEntity.OrderTitle);
                                if (backMsg != "ok")
                                {
                                    LogHelper.AddLog(oldEntity.SalesmanUserName + "没有关注公众号");//记录日志
                                }
                            }

                            //修改生产单里面的报价审核驳回
                            Sale_CustomerEntity sale_CustomerEntity = db.FindEntity<Sale_CustomerEntity>(t => t.ProduceId == oldEntity.Code);//老销售单code会生成生产单id
                            if (sale_CustomerEntity != null)
                            {
                                sale_CustomerEntity.MoneyOkMark = state;
                                sale_CustomerEntity.MoneyOkDate = DateTime.Now;
                                db.Update<Sale_CustomerEntity>(sale_CustomerEntity);
                            }

                            RecordHelp.AddRecord(4, keyValue, "报价审核驳回");
                        }


                        //销售单里面的报价审核状态都是要修改的
                        DZ_OrderEntity entity = new DZ_OrderEntity()
                        {
                            MoneyOkMark = state,
                            MoneyOkDate = DateTime.Now,
                            MoneyOkUserId = OperatorProvider.Provider.Current().UserId,
                            MoneyOkUserName = OperatorProvider.Provider.Current().UserName
                        };
                        entity.Modify(keyValue);
                        db.Update<DZ_OrderEntity>(entity);

                        db.Commit();//放在最后
                    }
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
                    RecordHelp.AddRecord(4, keyValue, "签收确认");
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
                    RecordHelp.AddRecord(4, keyValue, "结单");

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


        /// <summary>
        /// 导入酷家乐
        /// </summary>
        /// <param name="dtSource">实体对象</param>
        /// <returns></returns>
        public string BatchAddEntity(string keyValue, DataTable dtSource, string dir)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                DZ_OrderEntity oldEntity = GetEntity(keyValue);
                if (!string.IsNullOrEmpty(oldEntity.MoneyPathKuJiaLe))
                {
                    //重复导入，删掉之前导入的所有，不管是酷家乐或是1010
                    //先删掉keyValue之前导入的room和item
                    db.Delete<DZ_Money_RoomEntity>(t => t.OrderId == keyValue);
                    db.Delete<DZ_Money_ItemEntity>(t => t.OrderId == keyValue);
                    oldEntity.MoneyPathKuJiaLe = "";
                    oldEntity.MoneyPath1010 = "";
                }
                //获取销售订单表
                int p = 0;
                if (oldEntity.CompanyName == "临沂乐豪斯")
                {
                    p = 2;
                }
                else if (oldEntity.CompanyName == "青岛乐豪斯")
                {
                    p = 3;
                }
                else
                {
                    p = 1;
                }

                int rowsCount = dtSource.Rows.Count;
                string productName = "";

                for (int r = 4; r < rowsCount; r++)
                {
                    string firstName = dtSource.Rows[r][0].ToString();
                    if (firstName.Contains("房间名称："))
                    {
                        string roomName = firstName.Replace("房间名称：", "");
                        DZ_Money_RoomEntity roomEntity = AddDbRoom(roomName, keyValue, oldEntity.Code, "KuJiaLe");
                        roomEntity.RoomAmount = 0;

                        //从当前房间开始遍历，获取当前房间的详情
                        for (r = r + 3; r < rowsCount; r++)
                        {
                            firstName = dtSource.Rows[r][0].ToString();


                            if (firstName.Contains("移门清单"))
                            {
                                decimal width = 0;
                                decimal height = 0;
                                int shan = 0;
                                string[] keys = { "黑色窄框", "白色窄框", "35框", "65框" };
                                int keysIndex = 0;
                                string keyWord = "";
                                for (r = r + 3; r < rowsCount; r++)
                                {
                                    string secondName = dtSource.Rows[r][1].ToString();//D:移门顶轨（DG上-GU）
                                    if (secondName.Contains("移门顶轨"))//只取一个高
                                    {
                                        string widthStr = dtSource.Rows[r][6].ToString();//宽：2244
                                        width = Convert.ToDecimal(widthStr);
                                    }

                                    if (secondName.Contains("黑色窄框") || secondName.Contains("白色窄框") || secondName.Contains("35框") || secondName.Contains("65框"))//只取一个宽
                                    {
                                        string heightStr = dtSource.Rows[r][9].ToString();//高：2,261	
                                        height = Convert.ToDecimal(heightStr);
                                        shan++;
                                        for (int i = 0; i < keys.Length; i++)
                                        {
                                            if (secondName.Contains(keys[i]))
                                            {
                                                keysIndex = i;
                                                keyWord = keys[keysIndex];
                                            }
                                        }
                                    }

                                    if (string.IsNullOrEmpty(secondName))
                                    {
                                        if (width != 0 && height != 0)
                                        {
                                            var product = db.FindEntity<DZ_ProductEntity>(t => t.Name.Contains(keyWord));//名称相同的
                                            if (product != null)
                                            {
                                                decimal? _place = GetPlanPrice(p, product);
                                                decimal _area = width * height / 1000000;
                                                decimal? _amount = _place * _area;
                                                roomEntity.RoomAmount += _amount;
                                                AddDbItem(roomEntity.RoomId, roomName, product.Id, product.Code, product.Name, $"门洞H{height}mm*W{width}mm，分{shan}扇", 1, _area, product.Unit, _place, _amount, keyValue, oldEntity.Code, "KuJiaLe", db);

                                            }
                                        }
                                        break;//只计算一次。默认当前房间只有一个移门
                                    }
                                }
                            }
                            if (firstName.Contains("板件清单"))
                            {
                                List<DZ_Money_ItemEntity> ItemList = new List<DZ_Money_ItemEntity>();
                                for (r = r + 3; r < rowsCount; r++)
                                {
                                    string secondName = dtSource.Rows[r][1].ToString();//两层酒格
                                    if (secondName.Contains("圆弧踢脚板"))
                                    {
                                        var product = db.FindEntity<DZ_ProductEntity>(t => t.Name.Contains("圆弧踢脚板"));//名称相同的
                                        if (product != null)
                                        {
                                            decimal? _place = GetPlanPrice(p, product);
                                            decimal? _amount = _place;
                                            roomEntity.RoomAmount += _amount;
                                            var itemEntity = GetDbItem(roomEntity.RoomId, roomName, product.Id, product.Code, secondName, product.Guige, 1, 1, product.Unit, _place, _amount, keyValue, oldEntity.Code, "KuJiaLe", db);

                                            var itemFrist = ItemList.Find(t => t.ProductId == product.Id);
                                            if (itemFrist == null)
                                            {
                                                ItemList.Add(itemEntity);
                                            }
                                            else
                                            {
                                                //同一产品，同一材质，累加金额、数量、面积
                                                itemFrist.Count += itemEntity.Count;
                                                itemFrist.Area += itemEntity.Area;
                                                itemFrist.Amount = itemFrist.Area * _place;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        foreach (var item in ItemList)
                                        {
                                            db.Insert<DZ_Money_ItemEntity>(item);
                                        }
                                        break;
                                    }
                                }
                            }

                            if (firstName.Contains("组件清单"))
                            {
                                List<DZ_Money_ItemEntity> ItemList = new List<DZ_Money_ItemEntity>();
                                for (r = r + 3; r < rowsCount; r++)
                                {
                                    string secondName = dtSource.Rows[r][1].ToString();//两层酒格
                                    if (!string.IsNullOrEmpty(secondName))
                                    {
                                        if (secondName.Contains("抽盒") || secondName.Contains("格抽") || secondName.Contains("裤抽") || secondName.Contains("键盘抽")
                                        || secondName.Contains("拉板抽") || secondName.Contains("主机") || secondName.Contains("酒格") || secondName.Contains("酒杯架") || secondName.Contains("酒瓶托"))
                                        {
                                            string[] keys = { "抽盒", "格抽", "裤抽", "键盘抽", "拉板抽", "主机", "酒格", "酒杯架", "酒瓶托" };
                                            int keysIndex = 0;
                                            string keyWord = "";
                                            for (int i = 0; i < keys.Length; i++)
                                            {
                                                if (secondName.Contains(keys[i]))
                                                {
                                                    keysIndex = i;
                                                    keyWord = keys[keysIndex];
                                                }
                                            }
                                            if (!string.IsNullOrEmpty(keyWord))
                                            {
                                                var product = db.FindEntity<DZ_ProductEntity>(t => t.Name.Contains(keyWord));//名称相同的
                                                if (product != null)
                                                {
                                                    decimal? _place = GetPlanPrice(p, product);
                                                    decimal? _amount = _place;
                                                    roomEntity.RoomAmount += _amount;
                                                    var itemEntity = GetDbItem(roomEntity.RoomId, roomName, product.Id, product.Code, secondName, product.Guige, 1, 1, product.Unit, _place, _amount, keyValue, oldEntity.Code, "KuJiaLe", db);

                                                    var itemFrist = ItemList.Find(t => t.ProductId == product.Id);
                                                    if (itemFrist == null)
                                                    {
                                                        ItemList.Add(itemEntity);
                                                    }
                                                    else
                                                    {
                                                        //同一产品，同一材质，累加金额、数量、面积
                                                        itemFrist.Count += itemEntity.Count;
                                                        itemFrist.Area += itemEntity.Area;
                                                        itemFrist.Amount = itemFrist.Area * _place;
                                                    }
                                                }
                                            }
                                        }

                                        if (secondName.Contains("45罗马柱1#") || secondName.Contains("45罗马柱2#") || secondName.Contains("80罗马柱1#"))
                                        {
                                            //Y:80罗马柱1#(带柱头1#)	
                                            string[] keys = {"45罗马柱1#", "45罗马柱2#", "80罗马柱1#"};
                                            int keysIndex = 0;
                                            string keyWord = "";
                                            for (int i = 0; i < keys.Length; i++)
                                            {
                                                if (secondName.Contains(keys[i]))
                                                {
                                                    keysIndex = i;
                                                    keyWord = keys[keysIndex];
                                                }
                                            }
                                            if (!string.IsNullOrEmpty(keyWord))
                                            {
                                                var product = db.FindEntity<DZ_ProductEntity>(t => t.Name.Contains(keyWord));//名称相同的
                                                if (product != null)
                                                {
                                                    string heightStr = dtSource.Rows[r][9].ToString();//高：2,261	
                                                    decimal length = Convert.ToDecimal(heightStr) / 1000;

                                                    decimal? _place = GetPlanPrice(p, product);
                                                    decimal? _amount = _place * length;

                                                    roomEntity.RoomAmount += _amount;
                                                    var itemEntity = GetDbItem(roomEntity.RoomId, roomName, product.Id, product.Code, product.Name, product.Guige, 1, length, product.Unit, _place, _amount, keyValue, oldEntity.Code, "KuJiaLe", db);

                                                    var itemFrist = ItemList.Find(t => t.ProductId == product.Id);
                                                    if (itemFrist == null)
                                                    {
                                                        ItemList.Add(itemEntity);
                                                    }
                                                    else
                                                    {
                                                        //同一产品，同一材质，累加金额、数量、面积
                                                        itemFrist.Count += itemEntity.Count;
                                                        itemFrist.Area += itemEntity.Area;
                                                        itemFrist.Amount = itemFrist.Area * _place;
                                                    }
                                                }

                                                //带柱头的
                                                string[] keys2 = { "柱头1#", "柱头2#", };
                                                int keysIndex2 = 0;
                                                string keyWord2 = "";
                                                for (int i = 0; i < keys2.Length; i++)
                                                {
                                                    if (secondName.Contains(keys2[i]))
                                                    {
                                                        keysIndex2 = i;
                                                        keyWord2 = keys2[keysIndex2];
                                                    }
                                                }
                                                if (!string.IsNullOrEmpty(keyWord2))
                                                {
                                                    string keyWordNo = keyWord.Substring(0, 2);

                                                    var product2 = db.FindEntity<DZ_ProductEntity>(t => t.Name.Contains(keyWordNo+keyWord2));//名称相同的
                                                    if (product2 != null)
                                                    {
                                                        decimal? _place = GetPlanPrice(p, product2);
                                                        decimal? _amount = _place*2;//数量*2
                                                        roomEntity.RoomAmount += _amount;
                                                        var itemEntity = GetDbItem(roomEntity.RoomId, roomName, product2.Id, product2.Code, product2.Name, product2.Guige, 1, 2, product2.Unit, _place, _amount, keyValue, oldEntity.Code, "KuJiaLe", db);

                                                        var itemFrist = ItemList.Find(t => t.ProductId == product2.Id);
                                                        if (itemFrist == null)
                                                        {
                                                            ItemList.Add(itemEntity);
                                                        }
                                                        else
                                                        {
                                                            //同一产品，同一材质，累加金额、数量、面积
                                                            itemFrist.Count += itemEntity.Count;
                                                            itemFrist.Area += itemEntity.Area;
                                                            itemFrist.Amount = itemFrist.Area * _place;
                                                        }
                                                    }
                                                }
                                            }
                                        }



                                        if (secondName.Contains("衣通"))
                                        {
                                            //P:高档衣通座	
                                            string keyWord = secondName.Substring(2);//去掉前两位P:
                                            var product = db.FindEntity<DZ_ProductEntity>(t => t.Name.Contains(keyWord));//名称相同的
                                            if (product != null)
                                            {
                                                decimal? _place = GetPlanPrice(p, product);

                                                string widthStr = dtSource.Rows[r][6].ToString();//宽：2244
                                                decimal width = Convert.ToDecimal(widthStr);
                                                decimal length = 0;
                                                if (keyWord == "高档衣通")
                                                {
                                                    length = (width - 6)/1000;//高档衣通长度= ceil（（宽度-6）/1000）
                                                }
                                                else
                                                {
                                                    length = (width - 10) / 1000;//衣通长度=ceil（（宽度-10）/1000）
                                                }
                                                if (length<1)
                                                {
                                                    length = 1;//小于1米，按1米
                                                }

                                                decimal? _amount = _place * length;
                                                roomEntity.RoomAmount += _amount;
                                                var itemEntity = GetDbItem(roomEntity.RoomId, roomName, product.Id, product.Code, keyWord, product.Guige, 1, length, product.Unit, _place, _amount, keyValue, oldEntity.Code, "KuJiaLe", db);

                                                var itemFrist = ItemList.Find(t => t.ProductId == product.Id);
                                                if (itemFrist == null)
                                                {
                                                    ItemList.Add(itemEntity);
                                                }
                                                else
                                                {
                                                    //同一产品，同一材质，累加金额、数量、面积
                                                    itemFrist.Count += itemEntity.Count;
                                                    itemFrist.Area += itemEntity.Area;
                                                    itemFrist.Amount = itemFrist.Area * _place;
                                                }
                                            }
                                        }

                                    }
                                    else
                                    {
                                        foreach (var item in ItemList)
                                        {
                                            db.Insert<DZ_Money_ItemEntity>(item);
                                        }
                                        break;
                                    }

                                }
                            }
                            if (firstName.Contains("门板清单"))
                            {
                                List<DZ_Money_ItemEntity> ItemList = new List<DZ_Money_ItemEntity>();
                                for (r = r + 3; r < rowsCount; r++)
                                {
                                    string secondName = dtSource.Rows[r][1].ToString();//Y:km-xs-502	
                                    if (secondName.Contains("T:")) //同柜体材料的门板（T：）不计算，已导入柜体1010清单，
                                    {
                                        continue;
                                    }
                                    string caizhi = dtSource.Rows[r][5].ToString();//横纹经典红木
                                    string area = dtSource.Rows[r][12].ToString();//0.340312

                                    if (secondName.Contains("Y:") || secondName.Contains("G:"))//其余拼框门(Y：)、嵌条门(Y：)、吸塑门(Y：)按展开面积计算；//外协门板按展开面积计算，不足0.3㎡按0.3㎡（轻奢玻璃门，石英门，轻奢玻璃门(配石英门专用)，隐框镜面门，土耳其黄玻璃门）
                                    {
                                        secondName = secondName.Substring(2);//去掉前两位Y:

                                        bool isDcb = false;//默认不是多层板，默认颗粒板
                                        if (secondName.Contains("多层板"))//单价+30
                                        {
                                            secondName = secondName.Replace("多层板", "");
                                            isDcb = true;
                                        }
                                        var product = db.FindEntity<DZ_ProductEntity>(t => t.Name.ToUpper().Contains(secondName.ToUpper()));//名称相同的
                                        if (product != null)
                                        {
                                            decimal? _place = GetPlanPrice(p, product);
                                            if (_place == 0 || _place == null)
                                            {
                                                return "产品报价不存在：" + secondName;
                                            }

                                            if (secondName.Contains("km-xs"))
                                            {
                                                if (caizhi.Contains("肤感") && caizhi.Contains("高光") && caizhi.Contains("肌理"))//膜不一样
                                                {
                                                    _place += 50;//单价+50
                                                }
                                                if (caizhi.Contains("做旧"))
                                                {
                                                    _place += 80;//单价+50
                                                }
                                            }
                                            if (isDcb)//单价+30
                                            {
                                                _place += 30;//单价多层板+30
                                            }

                                            decimal? _area = Convert.ToDecimal(area);
                                            if (secondName.Contains("G:"))//外协门板,不足0.3㎡按0.3㎡
                                            {
                                                _area = _area > 0.3M ? _area : 0.3M;
                                            }
                                            decimal? _amount = _place * _area;
                                            roomEntity.RoomAmount += _amount;
                                            var itemEntity= GetDbItem(roomEntity.RoomId, roomName, product.Id, product.Code, secondName, caizhi, 1, _area, product.Unit, _place, _amount, keyValue, oldEntity.Code, "KuJiaLe", db);

                                            var itemFrist = ItemList.Find(t => t.ProductId == product.Id && t.Guige == caizhi);
                                            if(itemFrist == null)
                                            {
                                                ItemList.Add(itemEntity);
                                            }
                                            else
                                            {
                                                //同一产品，同一材质，累加金额、数量、面积
                                                itemFrist.Count += itemEntity.Count;
                                                itemFrist.Area += itemEntity.Area;
                                                itemFrist.Amount = itemFrist.Area * _place;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        foreach (var item in ItemList)
                                        {
                                            db.Insert<DZ_Money_ItemEntity>(item);
                                        }
                                        break;
                                    }
                                }
                            }
                            if (firstName.Contains("五金"))
                            {
                                for (r = r + 3; r < rowsCount; r++)
                                {
                                    string secondName = dtSource.Rows[r][1].ToString();//P:1370拉直器
                                    string count = dtSource.Rows[r][14].ToString();//6
                                    if (!secondName.IsEmpty())
                                    {
                                        secondName = secondName.Substring(2);//去掉前两位Y:
                                        var product = db.FindEntity<DZ_ProductEntity>(t => t.Name.Contains(secondName));//名称相同的
                                        if (product != null)
                                        {
                                            decimal? _place = GetPlanPrice(p, product);
                                            if (_place == 0 || _place == null)
                                            {
                                                return "产品报价不存在：" + secondName;
                                            }

                                            decimal? _count = Convert.ToDecimal(count);
                                            decimal? _amount = _place * _count;
                                            roomEntity.RoomAmount += _amount;
                                            AddDbItem(roomEntity.RoomId, roomName, product.Id, product.Code, secondName, product.Guige, 1, _count, product.Unit, _place, _amount, keyValue, oldEntity.Code, "KuJiaLe", db);
                                        }
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                            if (firstName.Contains("线条"))
                            {
                                List<DZ_Money_ItemEntity> ItemList = new List<DZ_Money_ItemEntity>();
                                for (r = r + 3; r < rowsCount; r++)
                                {
                                    string secondName = dtSource.Rows[r][1].ToString();//P:顶线-95顶线1#	
                                    if (!secondName.IsEmpty())
                                    {
                                        if (secondName.Contains("顶线"))
                                        {
                                            secondName = secondName.Substring(3);//去掉前两位顶线-
                                            var product = db.FindEntity<DZ_ProductEntity>(t => t.Name.Contains(secondName));//名称相同的
                                            if (product != null)
                                            {
                                                decimal? _place = GetPlanPrice(p, product);
                                                if (_place == 0 || _place == null)
                                                {
                                                    return "产品报价不存在：" + secondName;
                                                }

                                                string widthStr = dtSource.Rows[r][6].ToString();//宽：3,571	
                                                                                                 //95顶线1#/75顶线1#按米计算      95顶线1#/75顶线1#长度=（宽度 >2400？长度=宽度/2000+0.2，数量=ceil（宽度/2400）：长度=宽度/1000+0.2）
                                                decimal width = Convert.ToDecimal(widthStr);
                                                decimal length;

                                                if (width > 2400)
                                                {
                                                    length = width / 2000 + 0.2M * width / 2400;
                                                }
                                                else
                                                {
                                                    length = width / 1000 + 0.2M;
                                                }

                                                decimal? _amount = _place * length;
                                                roomEntity.RoomAmount += _amount;

                                                var itemEntity = GetDbItem(roomEntity.RoomId, roomName, product.Id, product.Code, secondName, product.Guige, 1, length, product.Unit, _place, _amount, keyValue, oldEntity.Code, "KuJiaLe", db);

                                                var itemFrist = ItemList.Find(t => t.ProductId == product.Id);
                                                if (itemFrist == null)
                                                {
                                                    ItemList.Add(itemEntity);
                                                }
                                                else
                                                {
                                                    //同一产品，同一材质，累加金额、数量、面积
                                                    itemFrist.Count += itemEntity.Count;
                                                    itemFrist.Area += itemEntity.Area;
                                                    itemFrist.Amount = itemFrist.Area * _place;
                                                }
                                            }
                                        }

                                        if (secondName.Contains("脚线"))
                                        {
                                            //脚线-铝合金踢脚板	
                                            string amountStr = dtSource.Rows[r][18].ToString();//0.00
                                            if (amountStr== "0")
                                            {
                                                secondName = secondName.Substring(3);//去掉前两位脚线-
                                                var product = db.FindEntity<DZ_ProductEntity>(t => t.Name.Contains(secondName));//名称相同的
                                                if (product != null)
                                                {
                                                    decimal? _place = GetPlanPrice(p, product);
                                                    if (_place == 0 || _place == null)
                                                    {
                                                        return "产品报价不存在：" + secondName;
                                                    }

                                                    string widthStr = dtSource.Rows[r][6].ToString();
                                                    decimal width = Convert.ToDecimal(widthStr);
                                                    decimal count = count = (width + 600) / 2000 *2;//数量=ceil（金额为0的踢脚板宽度+600）/2000（最终需要拆单人员核对）   固定长度2.0米/根

                                                    decimal? _amount = _place * count;
                                                    roomEntity.RoomAmount += _amount;

                                                    var itemEntity = GetDbItem(roomEntity.RoomId, roomName, product.Id, product.Code, secondName, product.Guige, 1, count, product.Unit, _place, _amount, keyValue, oldEntity.Code, "KuJiaLe", db);

                                                    var itemFrist = ItemList.Find(t => t.ProductId == product.Id);
                                                    if (itemFrist == null)
                                                    {
                                                        ItemList.Add(itemEntity);
                                                    }
                                                    else
                                                    {
                                                        //同一产品，同一材质，累加金额、数量、面积
                                                        itemFrist.Count += itemEntity.Count;
                                                        itemFrist.Area += itemEntity.Area;
                                                        itemFrist.Amount = itemFrist.Area * _place;
                                                    }
                                                }
                                            }
                                            
                                        }
                                    }
                                    else
                                    {
                                        foreach (var item in ItemList)
                                        {
                                            db.Insert<DZ_Money_ItemEntity>(item);
                                        }
                                        break;
                                    }
                                }
                            }

                            if (firstName.Contains("房间名称："))
                            {
                                var oldRoom = db.FindEntity<DZ_Money_RoomEntity>(t => t.RoomName == roomName && t.OrderId == keyValue);
                                if (oldRoom != null)
                                {
                                    oldRoom.RoomAmount += roomEntity.RoomAmount;
                                    oldRoom.Source = "KuJiaLe";
                                    db.Update<DZ_Money_RoomEntity>(oldRoom);
                                }
                                else
                                {
                                    db.Insert<DZ_Money_RoomEntity>(roomEntity);
                                    productName += roomName + "、";
                                }
                                r = r - 1;
                                break;//当前room遍历完毕，跳出循环
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(productName))
                {
                    oldEntity.ProductName = productName.Substring(0, productName.Length - 1);
                }

                oldEntity.MoneyPathKuJiaLe = dir;
                db.Update<DZ_OrderEntity>(oldEntity);
                db.Commit();
                return "导入酷家乐报价文件成功！";
            }
            catch (Exception ex)
            {
                db.Rollback();
                throw;
                //LogHelper.AddLog(ex.Message);
                //return ex.Message;
            }
        }

        /// <summary>
        /// 批量（新增）1010
        /// </summary>
        /// <param name="dtSource">实体对象</param>
        /// <returns></returns>
        public string BatchAddEntity1010(string keyValue, DataTable dtSource, string dir)
        {
            try
            {
                IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
                DZ_OrderEntity oldEntity = GetEntity(keyValue);
                if (!string.IsNullOrEmpty(oldEntity.MoneyPath1010))
                {
                    //重复导入，删掉之前导入的所有，不管是酷家乐或是1010
                    //先删掉keyValue之前导入的room和item
                    db.Delete<DZ_Money_RoomEntity>(t => t.OrderId == keyValue);
                    db.Delete<DZ_Money_ItemEntity>(t => t.OrderId == keyValue);
                    oldEntity.MoneyPathKuJiaLe = "";
                    oldEntity.MoneyPath1010 = "";
                }
                int p = 0;
                if (oldEntity.CompanyName == "临沂乐豪斯")
                {
                    p = 2;
                }
                else if (oldEntity.CompanyName == "青岛乐豪斯")
                {
                    p = 3;
                }
                else
                {
                    p = 1;
                }

                int rowsCount = dtSource.Rows.Count;
                string productName = "";

                for (int r = 4; r < rowsCount; r++)
                {
                    string c1 = dtSource.Rows[r][0].ToString();//分段多门柜3915*2020*500
                    if (!string.IsNullOrEmpty(c1))
                    {
                        if (c1 == "总计：")
                        {
                            break;
                        }
                        string roomName = Regex.Match(c1, "[\u4e00-\u9fa5]+").Value;
                        //创建房间db
                        DZ_Money_RoomEntity roomEntity = AddDbRoom(roomName, keyValue, oldEntity.Code, "1010");
                        roomEntity.RoomAmount = 0;
                        for (int i = r + 1; i < rowsCount; i++)
                        {
                            string c2 = dtSource.Rows[i][1].ToString();//18-横纹天然原橡颗粒板
                            if (!string.IsNullOrEmpty(c2))
                            {
                                string[] c22 = c2.Split('-');
                                if (c22.Length == 2)
                                {
                                    string houdu = c22[0];
                                    string caizhiNMame = c22[1];
                                    decimal? sumArea = 0;
                                    int? sumCount = 0;
                                    var product = db.FindEntity<DZ_ProductEntity>(t => t.Guige.Contains(houdu) && t.Name.Contains(caizhiNMame.Substring(caizhiNMame.Length - 3)));//颗粒板+18
                                    if (product != null)
                                    {
                                        decimal? _place = GetPlanPrice(p, product);
                                        if (_place == 0 || _place == null)
                                        {
                                            return "产品报价不存在：" + caizhiNMame + houdu;
                                        }
                                        else
                                        {
                                            for (int j = i + 1; j < rowsCount; j++)
                                            {

                                                string c11 = dtSource.Rows[i][1].ToString();//18-横纹天然原橡颗粒板
                                                if (c11 == "★")//异型另加10元/块	
                                                {
                                                    _place += 10;
                                                }
                                                string kuan = dtSource.Rows[j][3].ToString();//宽
                                                string gao = dtSource.Rows[j][4].ToString();//高
                                                if (!string.IsNullOrEmpty(kuan))
                                                {
                                                    sumArea += Math.Round(((decimal)Convert.ToDecimal(kuan) * Convert.ToDecimal(gao) / 1000000), 2);

                                                }
                                                else
                                                {
                                                    //跳空，说明当前材质已经遍历完成，计算金额
                                                    decimal? _amount = _place * sumArea;
                                                    roomEntity.RoomAmount += _amount;
                                                    //创建房间内的材质db
                                                    AddDbItem(roomEntity.RoomId, roomName, product.Id, product.Code, caizhiNMame, product.Guige, sumCount, sumArea, product.Unit, _place, _amount, keyValue, oldEntity.Code, "1010", db);
                                                    i = j - 1;//当前空行，-1回去，i会自增1
                                                    break;//当前item遍历完毕，跳出循环
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        return "产品不存在：" + caizhiNMame + houdu;
                                    }
                                }
                            }
                            else
                            {
                                var oldRoom = db.FindEntity<DZ_Money_RoomEntity>(t => t.RoomName == roomName && t.OrderId == keyValue);
                                if (oldRoom != null)
                                {
                                    oldRoom.RoomAmount += roomEntity.RoomAmount;
                                    oldRoom.Source = "1010";
                                    db.Update<DZ_Money_RoomEntity>(oldRoom);
                                }
                                else
                                {
                                    db.Insert<DZ_Money_RoomEntity>(roomEntity);
                                    productName += roomName+"、";
                                }
                                r = i - 1;
                                break;//当前room遍历完毕，跳出循环
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(productName))
                {
                    oldEntity.ProductName = productName.Substring(0, productName.Length - 1);
                }
                oldEntity.MoneyPath1010 = dir;
                db.Update<DZ_OrderEntity>(oldEntity);
                db.Commit();
                return "导入1010报价文件成功";
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(ex.Message);
                return ex.Message;
            }

        }

        /// <summary>
        /// 编辑报价保存
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="entryList">实体子对象</param>
        /// <returns></returns>
        public void SaveMoneyForm(string keyValue, DZ_OrderEntity entity, List<DZ_Money_ItemEntity> entryList)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                //生产扫码之后编辑
                DZ_OrderEntity oldEntity = GetEntity(keyValue);
                if (oldEntity != null)
                {
                    //主表
                    entity.Modify(keyValue);
                    db.Update(entity);
                    //明细
                    db.Delete<DZ_Money_ItemEntity>(t => t.OrderId.Equals(keyValue));

                    foreach (DZ_Money_ItemEntity item in entryList)
                    {
                        item.Create();
                        item.OrderId = keyValue;
                        db.Insert(entryList);
                    }

                    db.Commit();
                    RecordHelp.AddRecord(4, keyValue, "编辑报价");
                }
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }



        public decimal? GetPlanPrice(int p, DZ_ProductEntity entity)
        {
            decimal? price = null;
            switch (p)
            {
                case 1:
                    price = entity.Plan1;
                    break;
                case 2:
                    price = entity.Plan2;
                    break;
                case 3:
                    price = entity.Plan3;
                    break;
                default:
                    break;
            }
            return price;
        }

        public DZ_Money_RoomEntity AddDbRoom(string roomName, string orderId, string orderCode, string source)
        {
            DZ_Money_RoomEntity roomEntity = new DZ_Money_RoomEntity()
            {
                RoomName = roomName,
                OrderId = orderId,
                OrderCode = orderCode,
                Source = source
            };
            roomEntity.Create();
            return roomEntity;
        }
        public void AddDbItem(string roomId, string roomName, string productId, string productCode, string productName, string guige, decimal? count, decimal? area, string unit, decimal? price, decimal? amount, string orderId, string orderCode, string source, IRepository db)
        {
            DZ_Money_ItemEntity itemEntity = new DZ_Money_ItemEntity()
            {
                RoomId = roomId,
                RoomName = roomName,
                ProductId = productId,
                ProductCode = productCode,
                ProductName = productName,
                Guige = guige,
                Count = count,
                Area = area,
                Unit = unit,
                Price = price,
                Amount = amount,
                OrderId = orderId,
                OrderCode = orderCode,
                Source = source
            };
            itemEntity.Create();
            db.Insert<DZ_Money_ItemEntity>(itemEntity);//插入柜体部分item
        }

        public DZ_Money_ItemEntity GetDbItem(string roomId, string roomName, string productId, string productCode, string productName, string guige, decimal? count, decimal? area, string unit, decimal? price, decimal? amount, string orderId, string orderCode, string source, IRepository db)
        {
            DZ_Money_ItemEntity itemEntity = new DZ_Money_ItemEntity()
            {
                RoomId = roomId,
                RoomName = roomName,
                ProductId = productId,
                ProductCode = productCode,
                ProductName = productName,
                Guige = guige,
                Count = count,
                Area = area,
                Unit = unit,
                Price = price,
                Amount = amount,
                OrderId = orderId,
                OrderCode = orderCode,
                Source = source
            };
            itemEntity.Create();
            return itemEntity;
        }
        
    }
}
