﻿using HZSoft.Application.Entity.CustomerManage;
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
    public class Sale_Customer_Main
    {
        /// <summary>
        /// 保存生产单主单
        /// </summary>
        /// <param name="orderEntity">实体对象</param>
        /// <returns></returns>
        public static void SaveSaleMain(IRepository db, DZ_OrderEntity orderEntity)
        {
            try
            {
                //判断是否创建过这个生产单Id（跟生产编号一致,方便实体查询）
                Sale_CustomerEntity oldSale = db.FindEntity<Sale_CustomerEntity>(orderEntity.Code);
                //没创建过的才新增，避免下单不及时创建两个生产单初始单
                if (oldSale == null)
                {
                    //自动创建【生产单】主单部分
                    Sale_CustomerEntity sale_CustomerEntity = new Sale_CustomerEntity
                    {
                        ProduceCode = orderEntity.Code,//生产单号默认和销售单号一样
                        OrderId = orderEntity.Id,
                        OrderCode = orderEntity.Code,//销售单号
                        OrderTitle = orderEntity.OrderTitle,
                        OrderType = orderEntity.OrderType,
                        CompanyId = orderEntity.CompanyId,
                        CompanyName = orderEntity.CompanyName,
                        CustomerId = orderEntity.CustomerId,
                        CustomerName = orderEntity.CustomerName,
                        SalesmanUserId = orderEntity.SalesmanUserId,
                        SalesmanUserName = orderEntity.SalesmanUserName,
                        CustomerTelphone = orderEntity.CustomerTelphone,
                        SendPlanDate = orderEntity.SendPlanDate,
                        Address = orderEntity.Address,
                        ShippingType = orderEntity.ShippingType,
                        Carrier = orderEntity.Carrier,

                        //MoneyOkMark = orderEntity.MoneyOkMark == null ? 0 : orderEntity.MoneyOkMark,//报价审核
                        MoneyOkMark = orderEntity.MoneyOkMark,
                        MoneyOkDate = orderEntity.MoneyOkDate
                    };
                    sale_CustomerEntity.Create();//付款时间

                    //主表
                    db.Insert(sale_CustomerEntity);

                    //生成生产单id二维码
                    string url = "http://www.sikelai.cn/WeChatManage/Produce/StepSweepcode?id=" + sale_CustomerEntity.ProduceId;
                    CommonHelper.QRCode(url, sale_CustomerEntity.ProduceCode);

                    if (sale_CustomerEntity.MoneyOkMark == 1)
                    {
                        //发微信模板消息---财务已经报价审核并收款确认之后，给张宝莲发消息提醒oA-EC1bJnd0KFBuOy0joJvUOGwwk
                        //订单生成通知（7下单提醒）
                        TemplateWxApp.SendTemplateNew("oA-EC1bJnd0KFBuOy0joJvUOGwwk",
                            "您好，有新的订单需要下单!", sale_CustomerEntity.OrderTitle, sale_CustomerEntity.OrderCode, "请进行生产下单。");
                    }
                    RecordHelp.AddRecord(4, orderEntity.Id, "初始化生产单");
                }
                else
                {
                    //客诉单需要修改报价审核状态
                    oldSale.MoneyOkMark = orderEntity.MoneyOkMark;
                    oldSale.MoneyOkDate = orderEntity.MoneyOkDate;
                    oldSale.Modify(orderEntity.Code);
                    db.Update(oldSale);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
