﻿using HZSoft.Application.Code;
using HZSoft.Application.Entity.BaseManage;
using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.IService.CustomerManage;
using HZSoft.Data.Repository;
using HZSoft.Util;
using HZSoft.Util.Extension;
using HZSoft.Util.WebControl;
using HZSoft.Util.WeChat.Comm;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using ThoughtWorks.QRCode.Codec;

namespace HZSoft.Application.Service.CustomerManage
{
    /// <summary>
    /// 版 本 6.1
    /// 
    /// 创 建：张彦山
    /// 日 期：2016-04-06 17:24
    /// 描 述：应收账款
    /// </summary>
    public class ReceivableSklService : RepositoryFactory<ReceivableEntity>, IReceivableSklService
    {
        private DZ_OrderIService orderIService = new DZ_OrderService();
        private Buys_OrderIService buyIService = new Buys_OrderService();
        private Sale_CustomerService saleIService = new Sale_CustomerService();

        #region 获取数据
        /// 获取收款单列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<ReceivableEntity> GetPageList(Pagination pagination, string queryJson)
        {
            string strSql = "select * from Client_Receivable where DeleteMark=0 ";//报价的才显示 and MoneyMark = 1,,,不一定报价的显示，可以先收款，收款研发没有料单就不会到生产单
            var queryParam = queryJson.ToJObject();
            //成立日期
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and CreateDate >= '" + startTime + "' and CreateDate < '" + endTime + "'";
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
            //收款确认标识
            if (!queryParam["EnabledMark"].IsEmpty())
            {
                int EnabledMark = queryParam["EnabledMark"].ToInt();
                strSql += " and EnabledMark  = " + EnabledMark;//收款表的EnabledMark
            }

            return new RepositoryFactory().BaseRepository().FindList<ReceivableEntity>(strSql.ToString(), pagination);
        }
        //}
        ///// 获取收款单列表
        ///// </summary>
        ///// <param name="pagination">分页</param>
        ///// <param name="queryJson">查询参数</param>
        ///// <returns>返回分页列表</returns>
        //public IEnumerable<ReceivableEntity> GetPageList(Pagination pagination, string queryJson)
        //{
        //    string strSql = "select r.ReceivableId,r.PaymentTime,r.PaymentPrice,r.ReceiptPath,r.EnabledMark,r.Description,r.DeleteMark,r.CreateDate,r.CreateUserName," +
        //        "o.Code,o.OrderTitle,o.OrderType,o.CompanyName,o.CustomerName,o.SalesmanUserName,o.Accounts,o.ReceivedAmount from Client_Receivable r " +
        //        "LEFT JOIN DZ_Order o ON r.OrderId=o.Id where r.DeleteMark=0 ";//报价的才显示 and MoneyMark = 1,,,不一定报价的显示，可以先收款，收款研发没有料单就不会到生产单
        //    var queryParam = queryJson.ToJObject();
        //    //成立日期
        //    if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
        //    {
        //        DateTime startTime = queryParam["StartTime"].ToDate();
        //        DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
        //        strSql += " and r.CreateDate >= '" + startTime + "' and r.CreateDate < '" + endTime + "'";
        //    }
        //    //销售编号
        //    if (!queryParam["Code"].IsEmpty())
        //    {
        //        string Code = queryParam["Code"].ToString();
        //        strSql += " and o.Code like '%" + Code + "%'";
        //    }
        //    //标题模糊搜索
        //    if (!queryParam["OrderTitle"].IsEmpty())
        //    {
        //        string OrderTitle = queryParam["OrderTitle"].ToString();
        //        strSql += " and OrderTitle like '%" + OrderTitle + "%'";
        //    }
        //    //订单类型
        //    if (!queryParam["OrderType"].IsEmpty())
        //    {
        //        string OrderType = queryParam["OrderType"].ToString();
        //        if (OrderType == "-3")//非客诉单
        //        {
        //            strSql += " and OrderType <> 3";
        //        }
        //        else
        //        {
        //            strSql += " and OrderType = " + OrderType;
        //        }
        //    }

        //    //公司名
        //    if (!queryParam["CompanyName"].IsEmpty())
        //    {
        //        string CompanyName = queryParam["CompanyName"].ToString();
        //        strSql += " and o.CompanyName like '%" + CompanyName + "%'";
        //    }
        //    //客户名
        //    if (!queryParam["CustomerName"].IsEmpty())
        //    {
        //        string CustomerName = queryParam["CustomerName"].ToString();
        //        strSql += " and o.CustomerName like '%" + CustomerName + "%'";
        //    }
        //    //销售人
        //    if (!queryParam["SalesmanUserName"].IsEmpty())
        //    {
        //        string SalesmanUserName = queryParam["SalesmanUserName"].ToString();
        //        strSql += " and o.SalesmanUserName like '%" + SalesmanUserName + "%'";
        //    }
        //    //收款确认标识
        //    if (!queryParam["EnabledMark"].IsEmpty())
        //    {
        //        int EnabledMark = queryParam["EnabledMark"].ToInt();
        //        strSql += " and r.EnabledMark  = " + EnabledMark;//收款表的EnabledMark
        //    }

        //    return new RepositoryFactory().BaseRepository().FindList<ReceivableEntity>(strSql.ToString(), pagination);
        //}

        /// 获取收款单列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<DZ_OrderEntity> GetPaymentPageList(Pagination pagination, string queryJson)
        {
            string strSql = $"select * from DZ_Order where DeleteMark=0 ";//报价的才显示 and MoneyMark = 1,,,不一定报价的显示，可以先收款，收款研发没有料单就不会到生产单
            var expression = LinqExtensions.True<DZ_OrderEntity>();
            var queryParam = queryJson.ToJObject();
            //成立日期
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and CreateDate >= '" + startTime + "' and CreateDate < '" + endTime + "'";
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
            return new RepositoryFactory().BaseRepository().FindList<DZ_OrderEntity>(strSql.ToString(), pagination);
        }
        /// <summary>
        /// 获取收款记录列表
        /// </summary>
        /// <param name="orderId">订单主键</param>
        /// <returns></returns>
        public IEnumerable<ReceivableEntity> GetPaymentRecord(string orderId)
        {
            return this.BaseRepository().IQueryable(t => t.OrderId.Equals(orderId)).OrderByDescending(t => t.CreateDate).ToList();
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ReceivableEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存表单（新增）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(ReceivableEntity entity)
        {
            //添加【收款】
            entity.Create();
            this.BaseRepository().Insert(entity);

            //发微信模板消息---营销收款之后，给财务提醒--刘一珠改刘庆莉（收款提醒）
            TemplateWxApp.SendTemplateReceivable("oA-EC1bg4U16c63kR6yj51lA5AiM","您好，有新的收款需要确认!", entity.Code, entity.PaymentPrice.ToString(), entity.OrderTitle);
            RecordHelp.AddRecord(4, entity.OrderId, "收款"+ entity.PaymentPrice.ToString());
        }



        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void UpdateStateForm(string keyValue, ReceivableEntity entity)
        {
            ICashBalanceService icashbalanceservice = new CashBalanceService();
            DZ_OrderEntity orderEntity = orderIService.GetEntity(entity.OrderId);

            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    ReceivableEntity oldEntity = GetEntity(keyValue);

                    //第一次确认付款才真正修改金额
                    if (entity.EnabledMark == 1 && oldEntity.EnabledMark != 1)
                    {
                        //更改订单状态
                        orderEntity.ReceivedAmount = orderEntity.ReceivedAmount + entity.PaymentPrice;//收款金额
                        orderEntity.PaymentDate = oldEntity.PaymentTime;//收款日期=业务员填的日期

                        //已收款金额=订单金额
                        if (orderEntity.ReceivedAmount == orderEntity.Accounts)
                        {
                            orderEntity.PaymentState = 3;//全部收款

                            //发微信模板消息
                            if (!string.IsNullOrEmpty(oldEntity.CreateUserName))
                            {
                                var hsf_CardList = db.IQueryable<Hsf_CardEntity>(t => t.Name.Equals(oldEntity.CreateUserName));//发送给创建订单的人，店长代替店员创建，所以店长能看见拆单报价
                                if (hsf_CardList.Count() != 0)
                                {
                                    var hsf_CardEntity = hsf_CardList.First();
                                    //发微信模板消息---营销收款之后，给销售员提醒--（收款确认提醒：已全部收款）
                                    string backMsg = TemplateWxApp.SendTemplateReceivableOk(hsf_CardEntity.OpenId, "您好，有的收款已经确认!", "通过", "已全部收款");
                                    RecordHelp.AddRecord(4, entity.OrderId, "收款确认（全部收款）");
                                    if (backMsg != "ok")
                                    {
                                        //业务员没有关注公众号，报错：微信Post请求发生错误！错误代码：43004，说明：require subscribe hint: [ziWtva03011295]
                                        LogHelper.AddLog(entity.SalesmanUserName + "没有关注公众号");//记录日志
                                    }
                                }
                            }

                            //全部收款之后更新【入库单】中的【尾款】状态
                            Buys_OrderEntity buyEntity = buyIService.GetEntityByOrderId(orderEntity.Id);
                            if (buyEntity != null)
                            {
                                buyEntity.PaymentDate = DateTime.Now;//付款确认时间
                                buyEntity.PaymentState = 3;//全部收款
                                db.Update(buyEntity);
                            }
                        }
                        else
                        {
                            orderEntity.PaymentState = 2;//部分收款

                            //发微信模板消息
                            if (!string.IsNullOrEmpty(oldEntity.CreateUserName))
                            {
                                var hsf_CardList = db.IQueryable<Hsf_CardEntity>(t => t.Name.Equals(oldEntity.CreateUserName));//发送给创建订单的人，店长代替店员创建，所以店长能看见拆单报价
                                if (hsf_CardList.Count() != 0)
                                {
                                    var hsf_CardEntity = hsf_CardList.First();
                                    //发微信模板消息---营销收款之后，给销售员提醒--（收款确认提醒：部分收款）
                                    string backMsg = TemplateWxApp.SendTemplateReceivableOk(hsf_CardEntity.OpenId,  "您好，您的收款已经确认!", "通过", oldEntity.Code+"部分收款");
                                    RecordHelp.AddRecord(4, entity.OrderId, "收款确认（部分收款）");
                                    if (backMsg != "ok")
                                    {
                                        //业务员没有关注公众号，报错：微信Post请求发生错误！错误代码：43004，说明：require subscribe hint: [ziWtva03011295]
                                        LogHelper.AddLog(entity.SalesmanUserName + "没有关注公众号");//记录日志
                                    }
                                }
                            }
                        }
                        db.Update(orderEntity);

                        //修改【下单状态】，第一次收款的时候才创建
                        if (orderEntity.DownMark != 1)
                        {
                            //付了预付款，自动创建【生产单】主单部分*****************
                            Sale_Customer_Main.SaveSaleMain(db, orderEntity);//如果下单不及时，可能重复创建
                        }

                    }
                    if (entity.EnabledMark == -1 && oldEntity.EnabledMark != -1)
                    {
                        RecordHelp.AddRecord(4, entity.OrderId, "收款确认驳回");
                        var hsf_CardList = db.IQueryable<Hsf_CardEntity>(t => t.Name.Equals(oldEntity.CreateUserName));//发送给创建订单的人，店长代替店员创建，所以店长能看见拆单报价
                        if (hsf_CardList.Count() != 0)
                        {
                            var hsf_CardEntity = hsf_CardList.First();
                            //发微信模板消息---营销收款之后，给销售员提醒--（收款确认提醒：部分收款）
                            string backMsg = TemplateWxApp.SendTemplateReject(hsf_CardEntity.OpenId, "您好，收款确认人驳回订单!", oldEntity.Code, oldEntity.OrderTitle);
                            if (backMsg != "ok")
                            {
                                //业务员没有关注公众号，报错：微信Post请求发生错误！错误代码：43004，说明：require subscribe hint: [ziWtva03011295]
                                LogHelper.AddLog(entity.SalesmanUserName + "没有关注公众号");//记录日志
                            }
                        }
                    }

                    entity.Modify(keyValue);
                    this.BaseRepository().Update(entity);
                }

                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        #endregion
    }
}