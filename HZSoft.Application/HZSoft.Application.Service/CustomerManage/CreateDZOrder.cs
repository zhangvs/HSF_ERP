using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Data.Repository;
using HZSoft.Util;
using HZSoft.Util.WeChat.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZSoft.Application.Service.CustomerManage
{
    /// <summary>
    /// 创建生产单主单
    /// </summary>
    public class CreateDZOrder
    {
        /// <summary>
        /// 保存生产单主单
        /// </summary>
        /// <param name="orderEntity">实体对象</param>
        /// <returns></returns>
        public static void SaveDZOrder(IRepository db, DZSH_OrderEntity orderEntity)
        {
            try
            {
                //自动创建【生产单】主单部分
                DZ_OrderEntity dzEntity = new DZ_OrderEntity
                {
                    Code = orderEntity.Code,//默认和客诉单号一样
                    OrderTitle = orderEntity.OrderTitle,
                    OrderType = orderEntity.OrderType,
                    CompanyId = orderEntity.CompanyId,
                    CompanyName = orderEntity.CompanyName,
                    CustomerId = orderEntity.CustomerId,
                    CustomerName = orderEntity.CustomerName,
                    CustomerTelphone = orderEntity.CustomerTelphone,
                    DesignerUserName = orderEntity.DesignerUserName,
                    DesignerTelphone = orderEntity.DesignerTelphone,
                    Address = orderEntity.Address,
                    SalesmanUserId = orderEntity.SalesmanUserId,
                    SalesmanUserName = orderEntity.SalesmanUserName,
                    Accounts = orderEntity.Accounts,
                    FrontMark = orderEntity.FrontMark,
                    AfterMark = orderEntity.AfterMark,
                    MonthMark = orderEntity.MonthMark,
                    ReceivedAmount = orderEntity.ReceivedAmount,
                    PaymentDate = orderEntity.PaymentDate,
                    PaymentState = orderEntity.PaymentState,
                    ShippingType = orderEntity.ShippingType,
                    Carrier = orderEntity.Carrier,
                    CreatePath = orderEntity.CreatePath,
                    Description = orderEntity.Description
                };
                dzEntity.Create();//付款时间

                //主表
                db.Insert(dzEntity);

                //发微信模板消息---接单之后，给审图人提醒--刘明存oA-EC1WVqHl_gsBM3We2rgOHIMEQ
                //订单生成通知（审图提醒）
                //TemplateWxApp.SendTemplateNew("oA-EC1WVqHl_gsBM3We2rgOHIMEQ", "您好，有新的订单需要审图!", dzEntity.OrderTitle, dzEntity.Code, "请进行审图。");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
