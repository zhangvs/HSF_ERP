using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.IService.CustomerManage;
using HZSoft.Data.Repository;
using HZSoft.Util.Extension;
using HZSoft.Util.WebControl;
using HZSoft.Util;
using System.Collections.Generic;
using System.Linq;
using System;
using HZSoft.Application.Service.SystemManage;
using HZSoft.Application.IService.SystemManage;
using HZSoft.Application.Code;
using HZSoft.Application.Entity.BaseManage;
using HZSoft.Util.WeChat.Comm;

namespace HZSoft.Application.Service.CustomerManage
{
    /// <summary>
    /// 版 本 6.1
    /// 
    /// 创 建：超级管理员
    /// 日 期：2018-06-01 16:52
    /// 描 述：进货单
    /// </summary>
    public class Buys_OrderService : RepositoryFactory, Buys_OrderIService
    {
        private ICodeRuleService coderuleService = new CodeRuleService();
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<Buys_OrderEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var queryParam = queryJson.ToJObject();

            string strSql = "select * from Buys_Order where DeleteMark=0 ";
            //单据日期
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and CreateDate BETWEEN '" + startTime + "' and '" + endTime + "'";
            }
            //入库单
            if (!queryParam["Code"].IsEmpty())
            {
                string Code = queryParam["Code"].ToString();
                strSql += " and Code like '%" + Code + "%'";
            }
            //生产编号
            if (!queryParam["ProduceCode"].IsEmpty())
            {
                string ProduceCode = queryParam["ProduceCode"].ToString();
                strSql += " and ProduceCode like '%" + ProduceCode + "%'";
            }
            //标题模糊搜索
            if (!queryParam["OrderTitle"].IsEmpty())
            {
                string OrderTitle = queryParam["OrderTitle"].ToString();
                strSql += " and OrderTitle like '%" + OrderTitle + "%'";
            }
            //销售编号
            if (!queryParam["OrderCode"].IsEmpty())
            {
                string OrderCode = queryParam["OrderCode"].ToString();
                strSql += " and OrderCode like '%" + OrderCode + "%'";
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
                strSql += " and PaymentState  = " + PaymentState;
            }
            //完全入库标识
            if (!queryParam["AllEnterMark"].IsEmpty())
            {
                int AllEnterMark = queryParam["AllEnterMark"].ToInt();
                strSql += " and AllEnterMark  = " + AllEnterMark;
            }
            //发货通知
            if (!queryParam["SendMark"].IsEmpty())
            {
                int SendMark = queryParam["SendMark"].ToInt();
                strSql += " and SendMark  = " + SendMark;
            }
            //实际发货
            if (!queryParam["SendOutMark"].IsEmpty())
            {
                int SendOutMark = queryParam["SendOutMark"].ToInt();
                strSql += " and SendOutMark  = " + SendOutMark;
            }
            //物流
            if (!queryParam["SendLogisticsMark"].IsEmpty())
            {
                int SendLogisticsMark = queryParam["SendLogisticsMark"].ToInt();
                if (SendLogisticsMark!=0)
                {
                    strSql += " and SendLogisticsMark  <> 0 ";
                }
                else
                {
                    strSql += " and SendLogisticsMark  = 0";
                }
            }
            //安装
            if (!queryParam["SendInstallMark"].IsEmpty())
            {
                int SendInstallMark = queryParam["SendInstallMark"].ToInt();
                if (SendInstallMark != 0)
                {
                    strSql += " and SendInstallMark  <> 0 ";
                }
                else
                {
                    strSql += " and SendInstallMark  = 0";
                }
            }
            //结束
            if (!queryParam["OverMark"].IsEmpty())
            {
                int OverMark = queryParam["OverMark"].ToInt();
                strSql += " and OverMark  = " + OverMark;
            }
            return this.BaseRepository().FindList<Buys_OrderEntity>(strSql.ToString(), pagination);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Buys_OrderEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<Buys_OrderEntity>(keyValue);
        }
        /// <summary>
        /// 根据销售单号查询实体
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public Buys_OrderEntity GetEntityByOrderId(string orderId)
        {
            return this.BaseRepository().FindEntity<Buys_OrderEntity>(t => t.OrderId.Equals(orderId));
        }
        /// <summary>
        /// 获取子表详细信息
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IEnumerable<Buys_OrderItemEntity> GetDetails(string keyValue)
        {
            //return this.BaseRepository().FindList<Buys_OrderItemEntity>("select * from Buys_OrderItem where OrderId='"+keyValue+ "'");
            return this.BaseRepository().IQueryable<Buys_OrderItemEntity>(t => t.OrderId.Equals(keyValue)).OrderBy(t => t.CreateItemDate).ToList();
        }
        /// <summary>
        /// 获取子表详细信息
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="sortCode">顺序</param>
        /// <returns></returns>
        public Buys_OrderItemEntity GetDetail(string keyValue,int? sortCode)
        {
            //return this.BaseRepository().FindList<Buys_OrderItemEntity>("select * from Buys_OrderItem where OrderId='"+keyValue+ "'");
            return this.BaseRepository().FindEntity<Buys_OrderItemEntity>(t => t.OrderId.Equals(keyValue) && t.SortCode== sortCode);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            Buys_OrderEntity entity = GetEntity(keyValue);
            entity.Modify(keyValue);
            entity.DeleteMark = 1;
            this.BaseRepository().Update(entity);
            //IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            //try
            //{
            //    db.Delete<Buys_OrderEntity>(keyValue);
            //    db.Delete<Buys_OrderItemEntity>(t => t.OrderId.Equals(keyValue));
            //    db.Commit();
            //}
            //catch (Exception)
            //{
            //    db.Rollback();
            //    throw;
            //}
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="entryList">明细对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, Buys_OrderEntity entity,List<Buys_OrderItemEntity> entryList)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    //主表
                    entity.Modify(keyValue);
                    db.Update(entity);
                    //明细
                    db.Delete<Buys_OrderItemEntity>(t => t.OrderId.Equals(keyValue));

                    foreach (Buys_OrderItemEntity item in entryList)
                    {
                        item.Create();
                        item.OrderId = entity.Id;
                        db.Insert(item);
                    }
                }
                else
                {
                    //新增入库单(自动添加之后，取消使用**************)

                    //主表
                    entity.Create();
                    db.Insert(entity);
                    coderuleService.UseRuleSeed(entity.CreateUserId, "", ((int)CodeRuleEnum.Buy_Order).ToString(), db);//占用单据号
                    //明细
                    foreach (Buys_OrderItemEntity item in entryList)
                    {
                        item.Create();
                        item.OrderId = entity.Id;
                        db.Insert(item);
                    }
                    

                    //同步到接单表-入库状态
                    DZ_OrderEntity dZ_OrderEntity = new DZ_OrderEntity
                    {
                        EnterMark = 1,
                        EnterDate = DateTime.Now
                    };
                    dZ_OrderEntity.Modify(entity.OrderId);
                    db.Update<DZ_OrderEntity>(dZ_OrderEntity);
                    //同步到生产表-入库状态
                    Sale_CustomerEntity produceEntity = new Sale_CustomerEntity
                    {
                        EnterMark = 1,
                        EnterDate = DateTime.Now
                    };
                    produceEntity.Modify(entity.ProduceId);
                    db.Update<Sale_CustomerEntity>(produceEntity);

                }
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }


        /// <summary>
        /// 柜体扫码包装之后，g根据生产单信息，创建入库单主单
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveBuyMain(Sale_CustomerEntity entity)
        {
            try
            {
                IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
                Buys_OrderEntity buysEntity = db.FindEntity<Buys_OrderEntity>(t => t.Id == entity.OrderCode);//入库单号同销售订单编号
                //入库单不存在，才初始化
                if (buysEntity==null)
                {
                    //获取销售单收款状态
                    DZ_OrderEntity orderEntity = db.FindEntity<DZ_OrderEntity>(t => t.Id == entity.OrderId);
                    if (orderEntity != null)
                    {
                        Buys_OrderEntity buys_OrderEntity = new Buys_OrderEntity()
                        {
                            Code = entity.OrderCode,
                            OrderId = entity.OrderId,
                            OrderCode = entity.OrderCode,
                            OrderType = entity.OrderType,
                            OrderTitle = entity.OrderTitle,
                            ProduceId = entity.ProduceId,
                            ProduceCode = entity.ProduceCode,
                            CompanyId = entity.CompanyId,
                            CompanyName = entity.CompanyName,
                            CustomerId = entity.CustomerId,
                            CustomerName = entity.CustomerName,
                            SalesmanUserId = entity.SalesmanUserId,
                            SalesmanUserName = entity.SalesmanUserName,
                            CustomerTelphone = entity.CustomerTelphone,
                            Address = entity.Address,
                            ShippingType = entity.ShippingType,
                            Carrier = entity.Carrier,
                            SendPlanDate = entity.SendPlanDate,

                            PaymentState = orderEntity.PaymentState,//确认是否全部收款
                            PaymentDate = orderEntity.PaymentDate,
                            AfterMark = orderEntity.AfterMark,//确认是否收取尾款
                        };

                        //材料是否选择，判断需要入库
                        if (entity.GuiTiMark == 1)
                        {
                            buys_OrderEntity.GuiEnterMark = 0;//柜体包装进行中
                        }
                        if (entity.MenBanMark == 1)
                        {
                            buys_OrderEntity.MenEnterMark = 0;//门板
                        }
                        if (entity.WuJinMark == 1)
                        {
                            buys_OrderEntity.WuEnterMark = 0;//五金
                        }
                        if (entity.WaiXieMark == 1)
                        {
                            buys_OrderEntity.WaiEnterMark = 0;//外协
                        }
                        buys_OrderEntity.Create();
                        //新增入库单主表
                        this.BaseRepository().Insert(buys_OrderEntity);
                    }
                }
                
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// 分柜体，门板，五金，添加入库之后，判断是否完全入库
        /// </summary>
        /// <param name="itemEntity">实体对象</param>
        /// <returns></returns>
        public void SaveInForm(Buys_OrderItemEntity itemEntity)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                Buys_OrderItemEntity oldItemEntity = GetDetail(itemEntity.OrderId, itemEntity.SortCode);
                Buys_OrderEntity buyEntity = GetEntity(itemEntity.OrderId);

                //先删除再创建
                if (oldItemEntity != null)
                {
                    db.Delete<Buys_OrderItemEntity>(oldItemEntity.OrderEntryId);

                    buyEntity.TotalQty -= oldItemEntity.Qty;//减去老库存
                    if (itemEntity.Qty == 0)
                    {
                        //等于0的情况，直接删掉当前入库记录,当前材料的，入库状态改成null
                        switch (itemEntity.SortCode)
                        {
                            case 1: buyEntity.GuiEnterMark = -1; break;
                            case 2: buyEntity.MenEnterMark = -1; break;
                            case 3: buyEntity.WuEnterMark = -1; break;
                            case 4: buyEntity.WaiEnterMark = -1; break;
                            default:
                                break;
                        }
                    }
                }

                if (itemEntity.Qty > 0)
                {
                    //新增入库单从表，新增要新增，初始化id，用户跳过
                    itemEntity.Create();
                    db.Insert<Buys_OrderItemEntity>(itemEntity);

                    buyEntity.TotalQty += itemEntity.Qty;//加上新库存
                                                         //修改入库状态，分柜体，门板，五金，外协
                    switch (itemEntity.SortCode)
                    {
                        case 1: buyEntity.GuiEnterMark = 1; break;
                        case 2: buyEntity.MenEnterMark = 1; break;
                        case 3: buyEntity.WuEnterMark = 1; break;
                        case 4: buyEntity.WaiEnterMark = 1; break;
                        default:
                            break;
                    }

                    //判断是否完全入库
                    if (buyEntity.GuiEnterMark == 0 || buyEntity.MenEnterMark == 0 || buyEntity.WuEnterMark == 0 || buyEntity.WaiEnterMark == 0)
                    {
                        //还没有完全入库
                        buyEntity.AllEnterMark = 0;
                    }
                    else
                    {
                        //完全入库修改状态
                        buyEntity.AllEnterMark = 1;
                        buyEntity.AllEnterDate = DateTime.Now;

                        //同步到接单表-入库状态
                        DZ_OrderEntity dZ_OrderEntity = new DZ_OrderEntity
                        {
                            EnterMark = 1,
                            EnterDate = DateTime.Now
                        };
                        dZ_OrderEntity.Modify(buyEntity.OrderId);
                        db.Update<DZ_OrderEntity>(dZ_OrderEntity);

                        //同步到生产表-入库状态
                        Sale_CustomerEntity produceEntity = new Sale_CustomerEntity
                        {
                            EnterMark = 1,
                            EnterDate = DateTime.Now
                        };
                        produceEntity.Modify(buyEntity.ProduceId);
                        db.Update<Sale_CustomerEntity>(produceEntity);

                        string wk = "";
                        //发微信模板消息--给销售人提醒(完全入库提醒)
                        if (buyEntity.PaymentState == 3 || buyEntity.AfterMark == 0)
                        {
                            //发微信模板消息---完全入库+（收齐尾款或者不需要收取尾款）之后，给胡鲁鲁发消息提醒????给程东彩发全部入库提醒
                            //订单生成通知（9完全入库提醒）
                            TemplateWxApp.SendTemplateAllIn("oA-EC1W1BQZ46Wc8HPCZZUUFbE9M", "您好，有新的订单已经入库!", buyEntity.OrderTitle, "共" + buyEntity.TotalQty + "包，请进行发货通知");
                        }
                        else
                        {
                            wk = "请确认尾款。";
                        }

                        //发微信模板消息--给销售人提醒(完全入库提醒)
                        if (!string.IsNullOrEmpty(buyEntity.SalesmanUserName))
                        {
                            var hsf_CardList = db.IQueryable<Hsf_CardEntity>(t => t.Name.Equals(buyEntity.SalesmanUserName));
                            if (hsf_CardList.Count() != 0)
                            {
                                var hsf_CardEntity = hsf_CardList.First();
                                //订单生成通知，只有关注公众号的业务员才能收到消息(8完全入库提醒)
                                string backMsg = TemplateWxApp.SendTemplateAllIn(hsf_CardEntity.OpenId, "您好，您的订单已经全部入库!", buyEntity.Code, buyEntity.OrderTitle + "：共" + buyEntity.TotalQty + "包。" + wk);
                                if (backMsg != "ok")
                                {
                                    //业务员没有关注公众号，报错：微信Post请求发生错误！错误代码：43004，说明：require subscribe hint: [ziWtva03011295]
                                    LogHelper.AddLog(buyEntity.SalesmanUserName + "没有关注公众号");//记录日志
                                }
                            }
                        }
                    }
                
                }
                
                buyEntity.Modify(buyEntity.Id);
                db.Update<Buys_OrderEntity>(buyEntity);
                db.Commit();

                RecordHelp.AddRecord(4, buyEntity.OrderId, itemEntity.ProductName+"入库"+itemEntity.Qty+"包");
                
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        

        /// <summary>
        /// 发货通知
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveSend(string keyValue, Buys_OrderEntity entity)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    Buys_OrderEntity oldEntity = GetEntity(keyValue);
                    //发货通知时间不为null，老发货通知时间为null
                    if (entity.SendPlanDate !=null && entity.SendPlanDate!=oldEntity.SendPlanDate)
                    {
                        //发微信模板消息---发货通知之后，给公维才发消息提醒?????
                        //订单生成通知（10发货通知提醒）
                        //公维才
                        TemplateWxApp.SendTemplateSend("oA-EC1Z5tDaD1-ejnQe_l_gJK1Us", "您好，有新的发货通知!", entity.Code, entity.OrderTitle + "，计划发货时间：" + entity.SendPlanDate);
                        //金志花
                        TemplateWxApp.SendTemplateSend("oA-EC1UWi8i4sSkHsWV6BK7CuopA", "您好，有新的发货通知!", entity.Code, entity.OrderTitle + "，计划发货时间：" + entity.SendPlanDate);
                        //牛霞
                        TemplateWxApp.SendTemplateSend("oA-EC1TDoDKimuejhFlBV1U6M5bI", "您好，有新的发货通知!", entity.Code, entity.OrderTitle + "，计划发货时间：" + entity.SendPlanDate);
                        //胡鲁鲁
                        TemplateWxApp.SendTemplateSend("oA-EC1aaKOSNdW2wL8lHSsr3R4Dg", "您好，有新的发货通知!", entity.Code, entity.OrderTitle + "，计划发货时间：" + entity.SendPlanDate);
                    }

                    entity.Modify(keyValue);
                    entity.SendMark = 1;
                    entity.SendDate = DateTime.Now;
                    entity.SendUserId = OperatorProvider.Provider.Current().UserId;
                    entity.SendUserName = OperatorProvider.Provider.Current().UserName;
                    //this.BaseRepository().Update(entity);
                    db.Update<Buys_OrderEntity>(entity);

                    //同步到销售单-发货通知状态
                    DZ_OrderEntity dZ_OrderEntity = new DZ_OrderEntity
                    {
                        SendMark = 1,
                        SendDate = DateTime.Now,
                        SendPlanDate = entity.SendPlanDate,
                        SendUserId = entity.SendUserId,
                        SendUserName = entity.SendUserName
                    };
                    dZ_OrderEntity.Modify(entity.OrderId);
                    db.Update<DZ_OrderEntity>(dZ_OrderEntity);

                    //同步到生产表-发货通知状态
                    Sale_CustomerEntity produceEntity = new Sale_CustomerEntity
                    {
                        SendMark = 1,
                        SendDate = DateTime.Now,
                        SendPlanDate = entity.SendPlanDate,
                        SendUserId = entity.SendUserId,
                        SendUserName = entity.SendUserName
                    };
                    produceEntity.Modify(entity.ProduceId);
                    db.Update<Sale_CustomerEntity>(produceEntity);
                    db.Commit();
                    RecordHelp.AddRecord(4, entity.OrderId, "发货通知："+ entity.SendPlanDate.ToString().Replace(" 0:00:00", ""));
                }
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }

        /// <summary>
        /// 实际发货
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="SendOutImg">主键值</param>
        /// <returns></returns>
        public void UpdateSendState(string keyValue, string SendOutImg)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    Buys_OrderEntity entity = GetEntity(keyValue);
                    entity.Modify(keyValue);
                    entity.SendOutMark = 1;
                    entity.SendOutImg = SendOutImg;
                    entity.SendOutDate = DateTime.Now;
                    entity.SendOutUserId = OperatorProvider.Provider.Current().UserId;
                    entity.SendOutUserName = OperatorProvider.Provider.Current().UserName;
                    //this.BaseRepository().Update(entity);
                    db.Update<Buys_OrderEntity>(entity);

                    //同步到销售单-发货状态
                    DZ_OrderEntity dZ_OrderEntity = new DZ_OrderEntity
                    {
                        SendOutMark = 1,
                        SendOutImg = SendOutImg,
                        SendOutDate = DateTime.Now,
                        SendOutUserId = entity.SendOutUserId,
                        SendOutUserName = entity.SendOutUserName
                    };
                    dZ_OrderEntity.Modify(entity.OrderId);
                    db.Update<DZ_OrderEntity>(dZ_OrderEntity);

                    //同步到生产表-实际发货状态
                    Sale_CustomerEntity produceEntity = new Sale_CustomerEntity
                    {
                        SendOutMark = 1,
                        SendOutDate = DateTime.Now,
                        SendOutUserId = entity.SendOutUserId,
                        SendOutUserName = entity.SendOutUserName
                    };
                    produceEntity.Modify(entity.ProduceId);
                    db.Update<Sale_CustomerEntity>(produceEntity);


                    //发微信模板消息--给销售人提醒(10实际发货提醒)
                    if (!string.IsNullOrEmpty(entity.SalesmanUserName))
                    {
                        var hsf_CardList = db.IQueryable<Hsf_CardEntity>(t => t.Name.Equals(entity.SalesmanUserName));
                        if (hsf_CardList.Count() != 0)
                        {
                            var hsf_CardEntity = hsf_CardList.First();
                            //订单生成通知，只有关注公众号的业务员才能收到消息(11实际发货提醒)
                            string backMsg = TemplateWxApp.SendTemplateSendOut(hsf_CardEntity.OpenId, "您好，您的订单已经发货!", entity.Code, entity.OrderTitle + "：共" + entity.TotalQty + "包。");
                            if (backMsg != "ok")
                            {
                                //业务员没有关注公众号，报错：微信Post请求发生错误！错误代码：43004，说明：require subscribe hint: [ziWtva03011295]
                                LogHelper.AddLog(entity.SalesmanUserName + "没有关注公众号");//记录日志
                            }
                        }
                    }
                    db.Commit();//此db需要用到查询销售人
                    RecordHelp.AddRecord(4, entity.OrderId, "发货");
                }
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }


        /// <summary>
        /// 完成
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public void UpdateOverState(string keyValue, int? state)
        {
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    Sale_CustomerEntity entity = new Sale_CustomerEntity()
                    {
                        OverMark = state
                    };
                    entity.Modify(keyValue);
                    this.BaseRepository().Update(entity);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// 发货物流，同步到销售表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveLogisticsForm(string keyValue, Buys_OrderEntity entity)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.Modify(keyValue);
                    entity.SendLogisticsMark = 1;
                    entity.SendLogisticsDate = DateTime.Now;
                    entity.SendLogisticsUserId = OperatorProvider.Provider.Current().UserId;
                    entity.SendLogisticsUserName = OperatorProvider.Provider.Current().UserName;
                    db.Update<Buys_OrderEntity>(entity);


                    //同步到销售单-发货通知状态
                    DZ_OrderEntity dZ_OrderEntity = new DZ_OrderEntity
                    {
                        SendLogisticsMark = 1,
                        SendLogisticsDate = DateTime.Now,
                        SendLogisticsUserId = OperatorProvider.Provider.Current().UserId,
                        SendLogisticsUserName = OperatorProvider.Provider.Current().UserName
                    };
                    dZ_OrderEntity.Modify(entity.OrderId);
                    db.Update<DZ_OrderEntity>(dZ_OrderEntity);

                    db.Commit();

                    RecordHelp.AddRecord(4, entity.OrderId, "运输信息："+ entity.LogisticsName+" "+entity.LogisticsNO+" "+entity.LogisticsTel + " " + entity.LogisticsCost);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 安装
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveInstallForm(string keyValue, Buys_OrderEntity entity)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.Modify(keyValue);
                    entity.SendInstallMark = 1;
                    entity.SendInstallDate = DateTime.Now;
                    entity.SendInstallUserId = OperatorProvider.Provider.Current().UserId;
                    entity.SendInstallUserName = OperatorProvider.Provider.Current().UserName;
                    db.Update<Buys_OrderEntity>(entity);

                    //同步到销售单-发货通知状态
                    DZ_OrderEntity dZ_OrderEntity = new DZ_OrderEntity
                    {
                        SendInstallMark = 1,
                        SendInstallDate = DateTime.Now,
                        SendInstallUserId = OperatorProvider.Provider.Current().UserId,
                        SendInstallUserName = OperatorProvider.Provider.Current().UserName
                    };
                    dZ_OrderEntity.Modify(entity.OrderId);
                    db.Update<DZ_OrderEntity>(dZ_OrderEntity);
                    
                    db.Commit();
                    RecordHelp.AddRecord(4, entity.OrderId, "安装信息：" + entity.InstallUserName);
                }
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
