using HZSoft.Application.Code;
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
                    Accounts = 0,
                    FrontMark = 0,
                    AfterMark = 0,
                    MonthMark = 0,
                    PaymentDate = orderEntity.PaymentDate,
                    ShippingType = orderEntity.ShippingType,
                    Carrier = orderEntity.Carrier,
                    CreatePath = orderEntity.CreatePath,
                    Description = orderEntity.Description,

                    Id= orderEntity.Id,
                    CreateDate = DateTime.Now,
                    CreateUserId = OperatorProvider.Provider.Current().UserId,
                    CreateUserName = OperatorProvider.Provider.Current().UserName,
                    CheckTuMark = 0,//审图
                    ChaiMark = 0,//拆单
                    CheckMark = 0,//审核
                    MoneyMark = 0,//报价
                    MoneyOkMark = 0,//报价审核
                    PaymentState = 1,//未收款
                    MoneyAccounts = 0,//报价金额
                    ReceivedAmount = 0,//收款金额
                    DownMark = 0,//下单
                    PushMark = 0,//推单
                    EnterMark = 0,//入库
                    SendMark = 0,//发货通知
                    SendOutMark = 0,//实际发货
                    SignedMark = 0,//签收
                    OverMark = 0,//结束完成
                    DeleteMark = 0,
                    EnabledMark = 1
            };
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
