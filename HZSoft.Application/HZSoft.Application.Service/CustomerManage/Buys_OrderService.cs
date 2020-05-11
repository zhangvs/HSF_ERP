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
            //销售编号
            if (!queryParam["OrderCode"].IsEmpty())
            {
                string OrderCode = queryParam["OrderCode"].ToString();
                strSql += " and OrderCode like '%" + OrderCode + "%'";
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
            //发货
            if (!queryParam["SendMark"].IsEmpty())
            {
                int SendMark = queryParam["SendMark"].ToInt();
                strSql += " and SendMark  = " + SendMark;
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
                //获取销售单收款状态
                DZ_OrderEntity orderEntity = db.FindEntity<DZ_OrderEntity>(t => t.Id == entity.OrderId);
                if (orderEntity != null)
                {
                    Buys_OrderEntity buys_OrderEntity = new Buys_OrderEntity()
                    {
                        Code = entity.OrderCode,
                        OrderId = entity.OrderId,
                        OrderCode = entity.OrderCode,
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

                        Id = Guid.NewGuid().ToString(),
                        CreateDate = DateTime.Now,
                        PaymentState = orderEntity.PaymentState,//确认是否全部收款
                        SendMark = 0,
                        DeleteMark = 0,
                        EnabledMark = 1,
                        TotalQty = 0
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

                    //新增入库单主表
                    this.BaseRepository().Insert(buys_OrderEntity);
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
                    db.Delete<Buys_OrderItemEntity>(t => t.OrderId.Equals(itemEntity.OrderId) && t.SortCode == itemEntity.SortCode);
                    buyEntity.TotalQty -= oldItemEntity.Qty;//减去老库存
                }

                //新增入库单从表
                itemEntity.Create();
                db.Insert<Buys_OrderItemEntity>(itemEntity);

                buyEntity.TotalQty += itemEntity.Qty;//加上新库存
                //修改入库状态，分柜体，门板，五金，外协
                switch (itemEntity.SortCode)
                {
                    case 1: buyEntity.GuiEnterMark = 1;break;
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
                    if (buyEntity.PaymentState==3)
                    {
                        //发微信模板消息---入库+收齐尾款之后，给胡鲁鲁发消息提醒????
                        //订单生成通知（9完全入库提醒）
                        TemplateWxApp.SendTemplateAllIn("oA-EC1Ucth5a3bkvcJSdiTCizz_g", 
                            "您好，有新的订单已经入库!", buyEntity.OrderTitle, "共" + buyEntity.TotalQty + "包，请进行发货通知");
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
                            string backMsg = TemplateWxApp.SendTemplateAllIn(hsf_CardEntity.OpenId,
                                "您好，您的订单已经全部入库!", buyEntity.OrderTitle, "共" + buyEntity.TotalQty + "包。"+ wk);
                            if (backMsg != "ok")
                            {
                                //业务员没有关注公众号，报错：微信Post请求发生错误！错误代码：43004，说明：require subscribe hint: [ziWtva03011295]
                                LogHelper.AddLog(buyEntity.SalesmanUserName + "没有关注公众号");//记录日志
                            }
                        }
                    }
                }
                
                //主表
                buyEntity.Modify(buyEntity.Id);
                db.Update<Buys_OrderEntity>(buyEntity);


                db.Commit();
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
                    entity.Modify(keyValue);
                    entity.SendMark = 1;
                    entity.SendDate = DateTime.Now;
                    entity.SendUserId = OperatorProvider.Provider.Current().UserId;
                    entity.SendUserName = OperatorProvider.Provider.Current().UserName;
                    this.BaseRepository().Update(entity);
                    db.Update<Buys_OrderEntity>(entity);
                    db.Commit();


                    //发微信模板消息---发货通知之后，给公维才发消息提醒?????
                    //订单生成通知（10发货通知提醒）
                    TemplateWxApp.SendTemplateSend("oA-EC1Ucth5a3bkvcJSdiTCizz_g", 
                        "您好，有新的发货通知!", entity.Code, entity.OrderTitle + "，请在" + entity.SendPlanDate + "之前安排发货。");
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
        /// <returns></returns>
        public void UpdateSendState(string keyValue)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    Buys_OrderEntity entity = GetEntity(keyValue);
                    entity.Modify(keyValue);
                    entity.SendOutMark = 1;
                    entity.SendOutDate = DateTime.Now;
                    entity.SendOutUserId = OperatorProvider.Provider.Current().UserId;
                    entity.SendOutUserName = OperatorProvider.Provider.Current().UserName;
                    this.BaseRepository().Update(entity);
                    //db.Update<Buys_OrderEntity>(entity);

                    //同步到接单表-入库状态
                    DZ_OrderEntity dZ_OrderEntity = new DZ_OrderEntity
                    {
                        SendMark = 1,
                        SendDate = DateTime.Now
                    };
                    dZ_OrderEntity.Modify(entity.OrderId);
                    db.Update<DZ_OrderEntity>(dZ_OrderEntity);

                    //同步到生产表-入库状态
                    Sale_CustomerEntity produceEntity = new Sale_CustomerEntity
                    {
                        SendMark = 1,
                        SendDate = DateTime.Now
                    };
                    produceEntity.Modify(entity.ProduceId);
                    db.Update<Sale_CustomerEntity>(produceEntity);
                    db.Commit();


                    //发微信模板消息--给销售人提醒(10实际发货提醒)
                    if (!string.IsNullOrEmpty(entity.SalesmanUserName))
                    {
                        var hsf_CardList = db.IQueryable<Hsf_CardEntity>(t => t.Name.Equals(entity.SalesmanUserName));
                        if (hsf_CardList.Count() != 0)
                        {
                            var hsf_CardEntity = hsf_CardList.First();
                            //订单生成通知，只有关注公众号的业务员才能收到消息(11实际发货提醒)
                            string backMsg = TemplateWxApp.SendTemplateSend(hsf_CardEntity.OpenId,
                                "您好，您的订单已经发货!", entity.Code, entity.OrderTitle+"，共" + entity.TotalQty + "包。");
                            if (backMsg != "ok")
                            {
                                //业务员没有关注公众号，报错：微信Post请求发生错误！错误代码：43004，说明：require subscribe hint: [ziWtva03011295]
                                LogHelper.AddLog(entity.SalesmanUserName + "没有关注公众号");//记录日志
                            }
                        }
                    }
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
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
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
        #endregion

    }
}
