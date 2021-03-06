using HZSoft.Application.Code;
using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.IService.CustomerManage;
using HZSoft.Data.Repository;
using HZSoft.Util;
using HZSoft.Util.Extension;
using HZSoft.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using ThoughtWorks.QRCode.Codec;
using System.Drawing;
using System.Text;
using System;
using HZSoft.Util.WeChat.Comm;
using HZSoft.Application.Entity.BaseManage;
using System.Data;

namespace HZSoft.Application.Service.CustomerManage
{
    /// <summary>
    /// 版 本 6.1
    /// 
    /// 创 建：超级管理员
    /// 日 期：2017-12-23 14:41
    /// 描 述：余量统计
    /// </summary>
    public class Sale_CustomerService : RepositoryFactory, Sale_CustomerIService
    {
        private DZ_OrderIService dz_orderService = new DZ_OrderService();
        private Buys_OrderIService buyService = new Buys_OrderService();
        private ITrailRecordService recordService = new TrailRecordService();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<Sale_CustomerEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            string strSql = "select * from Sale_Customer where DeleteMark=0 ";
            //单据日期
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and CreateDate >= '" + startTime + "' and CreateDate < '" + endTime + "'";
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
            //生产人员
            if (!queryParam["StepUserName"].IsEmpty())
            {
                string StepUserName = queryParam["StepUserName"].ToString();
                strSql += " and (KaiLiaoUserName like '%" + StepUserName + "%' or  FengBianUserName like '%" + StepUserName + "%' or  PaiZuanUserName like '%" + StepUserName + "%' or  ShiZhuangUserName like '%" + StepUserName + "%' or  BaoZhuangUserName like '%" + StepUserName + "%' or  XiSuUserName like '%" + StepUserName + "%')";
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
            //包装
            if (!queryParam["BaoZhuangMark"].IsEmpty())
            {
                int BaoZhuangMark = queryParam["BaoZhuangMark"].ToInt();
                strSql += " and BaoZhuangMark  = " + BaoZhuangMark;
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
            //结束
            if (!queryParam["OverMark"].IsEmpty())
            {
                int OverMark = queryParam["OverMark"].ToInt();
                strSql += " and OverMark  = " + OverMark;
            }

            return this.BaseRepository().FindList<Sale_CustomerEntity>(strSql.ToString(), pagination);
        }


        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<Sale_CustomerEntity> GetList(string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            string strSql = "select * from Sale_Customer  where DeleteMark=0 ";
            //单据日期
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and CreateDate >= '" + startTime + "' and CreateDate < '" + endTime + "'";
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
            //生产人员
            if (!queryParam["StepUserName"].IsEmpty())
            {
                string StepUserName = queryParam["StepUserName"].ToString();
                strSql += " and (KaiLiaoUserName like '%" + StepUserName + "%' or  FengBianUserName like '%" + StepUserName + "%' or  PaiZuanUserName like '%" + StepUserName + "%' or  ShiZhuangUserName like '%" + StepUserName + "%' or  BaoZhuangUserName like '%" + StepUserName + "%' or  XiSuUserName like '%" + StepUserName + "%')";
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
            //包装
            if (!queryParam["BaoZhuangMark"].IsEmpty())
            {
                int BaoZhuangMark = queryParam["BaoZhuangMark"].ToInt();
                strSql += " and BaoZhuangMark  = " + BaoZhuangMark;
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
            //结束
            if (!queryParam["OverMark"].IsEmpty())
            {
                int OverMark = queryParam["OverMark"].ToInt();
                strSql += " and OverMark  = " + OverMark;
            }
            return this.BaseRepository().FindList<Sale_CustomerEntity>(strSql.ToString());
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Sale_CustomerEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<Sale_CustomerEntity>(keyValue);
        }
        /// <summary>
        /// 获取子表详细信息
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IEnumerable<Sale_Customer_ItemEntity> GetDetails(string keyValue)
        {
            //return this.BaseRepository().FindList<Sale_Customer_ItemEntity>("select * from Sale_Customer_Item where MainId='"+keyValue+ "'");
            return this.BaseRepository().FindList<Sale_Customer_ItemEntity>(t => t.MainId.Equals(keyValue)).OrderBy(t => t.Sort).ToList();
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            Sale_CustomerEntity entity = GetEntity(keyValue);
            entity.Modify(keyValue);
            entity.DeleteMark = 1;
            this.BaseRepository().Update(entity);


            //IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            //try
            //{
            //    db.Delete<Sale_CustomerEntity>(keyValue);
            //    db.Delete<Sale_Customer_ItemEntity>(t => t.MainId.Equals(keyValue));
            //    db.Commit();
            //}
            //catch (Exception)
            //{
            //    db.Rollback();
            //    throw;
            //}
        }

        /// <summary>
        /// 生产单修改编辑
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="entryList">实体子对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, Sale_CustomerEntity entity, List<Sale_Customer_ItemEntity> entryList)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                //生产扫码之后编辑
                Sale_CustomerEntity oldEntity = GetEntity(keyValue);
                if (oldEntity!=null)
                {
                    if (entity.BeiLiaoMark == 0 && entity.KaiLiaoMark == 0 && entity.FengBianMark == 0 && entity.PaiZuanMark == 0 && entity.ShiZhuangMark == 0 && entity.BaoZhuangMark == 0)
                    {
                        //都没有选中工序，则不需要柜体入库，自动创建一条入库单，仓库进行门板入库或五金外协入库，自db
                        buyService.SaveBuyMain(entity);
                    }

                    if (oldEntity.BeiLiaoMark == 2||oldEntity.KaiLiaoMark == 2 || oldEntity.FengBianMark == 2|| oldEntity.PaiZuanMark == 2|| oldEntity.ShiZhuangMark == 2|| oldEntity.BaoZhuangMark == 2)
                    {
                        //如果生产扫码之后又修改编辑，工序的勾选状态默认==1，但是扫码之后的状态为2，不能因为编辑修改为2
                        entity.BeiLiaoMark = oldEntity.BeiLiaoMark == 2 ? 2 : entity.BeiLiaoMark;
                        entity.KaiLiaoMark = oldEntity.KaiLiaoMark == 2 ? 2 : entity.KaiLiaoMark;
                        entity.FengBianMark = oldEntity.FengBianMark == 2 ? 2 : entity.FengBianMark;
                        entity.PaiZuanMark = oldEntity.PaiZuanMark == 2 ? 2 : entity.PaiZuanMark;
                        entity.ShiZhuangMark = oldEntity.ShiZhuangMark == 2 ? 2 : entity.ShiZhuangMark;
                        entity.BaoZhuangMark = oldEntity.BaoZhuangMark == 2 ? 2 : entity.BaoZhuangMark;
                        LogHelper.AddLog("生产扫码之后-又编辑，" + oldEntity.ProduceCode);
                    }
                    //包装扫码生成入库单之后-发现勾选材料错误--又编辑
                    Buys_OrderEntity oldBuysEntity = buyService.GetEntity(keyValue);
                    if (oldBuysEntity != null)
                    {
                        //如果已经入库了，勾选材料错误，发现少勾选了，需要修改，要同步到入库单
                        if (entity.GuiTiMark == 1 && oldBuysEntity.GuiEnterMark == null)
                        {
                            oldBuysEntity.GuiEnterMark = 0;//修改柜体状态为需要入库
                            LogHelper.AddLog("修改柜体状态为需要入库，" + oldEntity.ProduceCode);
                        }
                        if (entity.WuJinMark == 1 && oldBuysEntity.WuEnterMark == null)
                        {
                            oldBuysEntity.WuEnterMark = 0;//修改五金状态为需要入库
                            LogHelper.AddLog("修改五金状态为需要入库，" + oldEntity.ProduceCode);
                        }
                        if (entity.MenBanMark == 1 && oldBuysEntity.MenEnterMark == null)
                        {
                            oldBuysEntity.MenEnterMark = 0;//修改门板状态为需要入库
                            LogHelper.AddLog("修改门板状态为需要入库，" + oldEntity.ProduceCode);
                        }
                        if (entity.WaiXieMark == 1 && oldBuysEntity.WaiEnterMark == null)
                        {
                            oldBuysEntity.WaiEnterMark = 0;//修改外协状态为需要入库
                            LogHelper.AddLog("修改外协状态为需要入库，" + oldEntity.ProduceCode);
                        }

                        //如果已经入库了，勾选材料错误，发现多勾选了，需要去掉，要同步到入库单
                        if (entity.GuiTiMark == 0 && oldBuysEntity.GuiEnterMark == 1)
                        {
                            oldBuysEntity.GuiEnterMark = -1;//修改柜体状态为不需要入库
                            LogHelper.AddLog("修改柜体状态为不需要入库，" + oldEntity.ProduceCode);
                        }
                        if (entity.WuJinMark == 0 && oldBuysEntity.WuEnterMark == 1)
                        {
                            oldBuysEntity.WuEnterMark = -1;//修改五金状态为不需要入库
                            LogHelper.AddLog("修改五金状态为不需要入库，" + oldEntity.ProduceCode);
                        }
                        if (entity.MenBanMark == 0 && oldBuysEntity.MenEnterMark == 1)
                        {
                            oldBuysEntity.MenEnterMark = -1;//修改门板状态为不需要入库
                            LogHelper.AddLog("修改门板状态为不需要入库，" + oldEntity.ProduceCode);
                        }
                        if (entity.WaiXieMark == 0 && oldBuysEntity.WaiEnterMark == 1)
                        {
                            oldBuysEntity.WaiEnterMark = -1;//修改外协状态为不需要入库
                            LogHelper.AddLog("修改外协状态为不需要入库，" + oldEntity.ProduceCode);
                        }

                        db.Update<Buys_OrderEntity>(oldBuysEntity);
                    }

                    //主表
                    entity.Modify(keyValue);
                    db.Update(entity);
                    //明细
                    db.Delete<Sale_Customer_ItemEntity>(t => t.MainId.Equals(keyValue));

                    foreach (Sale_Customer_ItemEntity item in entryList)
                    {
                        item.Create();
                        item.MainId = entity.ProduceId;
                        db.Insert(entryList);
                    }

                    db.Commit();
                    RecordHelp.AddRecord(4, entity.OrderId, "补充生产单");
                }
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }


        /// <summary>
        /// 下单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveDownForm(string keyValue, Sale_CustomerEntity entity)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    Sale_CustomerEntity oldEntity = GetEntity(keyValue);
                    DZ_OrderEntity dZ_OrderEntity = new DZ_OrderEntity();
                    //原生产单没有下单文件，第一次上传下单文件，则修改下单状态
                    if (entity.DownMark == 1 && oldEntity.DownMark != 1)// && string.IsNullOrEmpty(oldEntity.DownPath)//不管之前有没有上传都修改下单状态
                    {
                        //发微信模板消息---下单之后，给程东彩发消息提醒oA-EC1W1BQZ46Wc8HPCZZUUFbE9M
                        //订单生成通知（8下单提醒）
                        TemplateWxApp.SendTemplateNew("oA-EC1W1BQZ46Wc8HPCZZUUFbE9M","您好，有新的订单需要推单!", entity.OrderTitle, entity.OrderCode, "请进行审核推单。");
                        RecordHelp.AddRecord(4, entity.OrderId, "生产下单");

                        if (entity.OrderType == 3)
                        {
                            dZ_OrderEntity.PushMark = 1;
                            dZ_OrderEntity.PushDate = DateTime.Now;
                            entity.PushMark = 1;
                            entity.PushDate = DateTime.Now;
                            RecordHelp.AddRecord(4, entity.OrderId, "客诉单跳过推单");
                        }
                    }

                    if (entity.DownMark == -1 && oldEntity.DownMark != -1)// && string.IsNullOrEmpty(oldEntity.DownPath)//不管之前有没有上传都修改下单状态
                    {
                        //给刘一珠改刘庆莉发驳回提醒
                        string backMsg = TemplateWxApp.SendTemplateReject("oA-EC1bg4U16c63kR6yj51lA5AiM", "您好，下单人驳回订单!", oldEntity.OrderCode, oldEntity.OrderTitle);
                        RecordHelp.AddRecord(4, entity.OrderId, "生产下单驳回");

                        entity.DownPath = null;//下单驳回，下单附件路径清空
                    }

                    //修改销售单下单状态
                    dZ_OrderEntity.DownMark = entity.DownMark;
                    dZ_OrderEntity.DownDate = DateTime.Now;
                    dZ_OrderEntity.DownUserId = OperatorProvider.Provider.Current().UserId;
                    dZ_OrderEntity.DownUserName = OperatorProvider.Provider.Current().UserName;
                    dZ_OrderEntity.DownPath = entity.DownPath;
                    dZ_OrderEntity.Modify(entity.OrderId);//原生产单实体才对
                    db.Update<DZ_OrderEntity>(dZ_OrderEntity);


                    //修改生产单下单状态
                    entity.DownDate = DateTime.Now;
                    entity.DownUserId = OperatorProvider.Provider.Current().UserId;
                    entity.DownUserName = OperatorProvider.Provider.Current().UserName;
                    entity.Modify(keyValue);
                    db.Update<Sale_CustomerEntity>(entity);
                    db.Commit();

                }
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }


        /// <summary>
        /// 撤单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SavePushBackForm(string keyValue, Sale_CustomerEntity entity)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    if (!string.IsNullOrEmpty(entity.PushBackPath))
                    {
                        //修改生产单撤单状态
                        entity.PushMark = -1;
                        entity.PushDate = DateTime.Now;
                        entity.PushUserId = OperatorProvider.Provider.Current().UserId;
                        entity.PushUserName = OperatorProvider.Provider.Current().UserName;

                        //修改销售单撤单状态
                        DZ_OrderEntity dZ_OrderEntity = new DZ_OrderEntity
                        {
                            PushMark = -1,
                            PushDate = DateTime.Now,
                            PushBackReason = entity.PushBackReason,
                            PushBackPath = entity.PushBackPath
                        };
                        dZ_OrderEntity.Modify(entity.OrderId);//原生产单实体才对
                        db.Update<DZ_OrderEntity>(dZ_OrderEntity);
                    }
                    entity.Modify(keyValue);
                    db.Update<Sale_CustomerEntity>(entity);
                    db.Commit();
                    RecordHelp.AddRecord(4, entity.OrderId, "生产撤单");
                }
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }


        /// <summary>
        /// 推单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="state">状态1推单-1撤单</param>
        /// <param name="orderId">销售单id</param>
        /// <returns></returns>
        public void UpdatePushState(string keyValue, int? state,string orderId)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    Sale_CustomerEntity entity = new Sale_CustomerEntity()
                    {
                        PushMark = state,
                        PushDate = DateTime.Now,
                        EndDate = DateTime.Now.AddDays(30).Date,//最迟交付日期
                        PushUserId = OperatorProvider.Provider.Current().UserId,
                        PushUserName = OperatorProvider.Provider.Current().UserName
                    };
                    entity.Modify(keyValue);
                    db.Update<Sale_CustomerEntity>(entity);

                    if (!string.IsNullOrEmpty(orderId))
                    {
                        //修改销售单推单状态
                        DZ_OrderEntity dZ_OrderEntity = new DZ_OrderEntity
                        {
                            PushMark = state,
                            PushDate = DateTime.Now
                        };
                        dZ_OrderEntity.Modify(orderId);//原生产单实体才对
                        db.Update<DZ_OrderEntity>(dZ_OrderEntity);
                    }
                    db.Commit();

                    RecordHelp.AddRecord(4, orderId, "生产推单");
                }
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }


        /// <summary>
        /// 排产（主单）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SavePlanForm(string keyValue, Sale_CustomerEntity entity)
        {
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    if (entity.StatePlanDate!=null ||entity.EndPlanDate!=null)
                    {
                        IRepository db = this.BaseRepository().BeginTrans();
                        entity.PlanMark = 1;
                        entity.Modify(keyValue);
                        db.Update<Sale_CustomerEntity>(entity);


                        //修改销售单排产状态
                        DZ_OrderEntity dZ_OrderEntity = new DZ_OrderEntity
                        {
                            PlanMark = 1,
                        };
                        dZ_OrderEntity.Modify(entity.OrderId);//原生产单实体才对
                        db.Update<DZ_OrderEntity>(dZ_OrderEntity);
                        db.Commit();

                        RecordHelp.AddRecord(4, entity.OrderId, "生产排产：" + entity.StatePlanDate.ToString().Replace(" 0:00:00", "") + "至" + entity.EndPlanDate.ToString().Replace(" 0:00:00", ""));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 工序撤销
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="step">工序</param>
        /// <param name="name">姓名</param>
        /// <returns></returns>
        public void SaveStepBackForm(string keyValue, int? step, string name)
        {
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    Sale_CustomerEntity oldEntity = GetEntity(keyValue);
                    if (oldEntity != null)
                    {
                        if (step!=null)
                        {
                            switch (step)
                            {
                                case 0:
                                    oldEntity.BeiLiaoUserName = oldEntity.BeiLiaoUserName.Replace(name, "");
                                    if (string.IsNullOrEmpty(oldEntity.BeiLiaoUserName))
                                    {
                                        oldEntity.BeiLiaoMark = 1;
                                    }
                                    break;
                                case 1:
                                    oldEntity.KaiLiaoUserName = oldEntity.KaiLiaoUserName.Replace(name, "");
                                    if (string.IsNullOrEmpty(oldEntity.KaiLiaoUserName))
                                    {
                                        oldEntity.KaiLiaoMark = 1;
                                    }
                                    break;
                                case 2:
                                    oldEntity.FengBianUserName = oldEntity.FengBianUserName.Replace(name, "");
                                    if (string.IsNullOrEmpty(oldEntity.FengBianUserName))
                                    {
                                        oldEntity.FengBianMark = 1;
                                    }
                                    break;
                                case 3:
                                    oldEntity.PaiZuanUserName = oldEntity.PaiZuanUserName.Replace(name, "");
                                    if (string.IsNullOrEmpty(oldEntity.PaiZuanUserName))
                                    {
                                        oldEntity.PaiZuanMark = 1;
                                    }
                                    break;
                                case 4:
                                    oldEntity.ShiZhuangUserName = oldEntity.ShiZhuangUserName.Replace(name, "");
                                    if (string.IsNullOrEmpty(oldEntity.ShiZhuangUserName))
                                    {
                                        oldEntity.ShiZhuangMark = 1;
                                    }
                                    break;
                                case 5:
                                    oldEntity.BaoZhuangUserName = oldEntity.BaoZhuangUserName.Replace(name, "");
                                    if (string.IsNullOrEmpty(oldEntity.BaoZhuangUserName))
                                    {
                                        oldEntity.BaoZhuangMark = 1;
                                    }
                                    break;
                                default:
                                    break;
                            }
                            oldEntity.Modify(keyValue);
                            this.BaseRepository().Update(oldEntity);
                            RecordHelp.AddRecord(4, oldEntity.OrderId, "工序撤销：" + name);
                        }                        
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 修改工序状态
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void UpdateStepState(Sale_CustomerEntity entity)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                db.Update < Sale_CustomerEntity >(entity);
                //同步到接单表-工序状态
                DZ_OrderEntity dZ_OrderEntity = new DZ_OrderEntity
                {
                    StepState = entity.StepState,
                    StepDate = DateTime.Now,
                    Id= entity.OrderId
                };
                db.Update<DZ_OrderEntity>(dZ_OrderEntity);
                db.Commit();

                //记录扫码操作记录
                TrailRecordEntity recordEntity = new TrailRecordEntity()
                {
                    TrailId = Guid.NewGuid().ToString(),
                    ObjectSort = 4,
                    ObjectId = entity.OrderId,
                    TrackContent = entity.StepName,
                    CreateUserName = entity.ModifyUserName,
                    CreateDate= DateTime.Now
                };
                recordService.SaveH5Form(recordEntity);//全是新增
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }

        /// <summary>
        /// 明细表，复制每道工序都需要的材料。不再计算块数（弃用）
        /// </summary>
        /// <param name="db"></param>
        /// <param name="entryList"></param>
        /// <param name="MainId"></param>
        /// <param name="StepId"></param>
        /// <param name="Step"></param>
        /// <param name="Sort"></param>
        public void AddItem(IRepository db,  List<Sale_Customer_ItemEntity> entryList, string MainId, int StepId, string Step, int Sort)
        {
            foreach (Sale_Customer_ItemEntity item1 in entryList)
            {
                Sale_Customer_ItemEntity entity1 = new Sale_Customer_ItemEntity
                {
                    Id = Guid.NewGuid().ToString(),
                    CreateDate = DateTime.Now,
                    StepId = StepId,
                    Step = Step,
                    MainId = MainId,
                    ProductId = item1.ProductId,
                    ProductCode = item1.ProductCode,
                    ProductName = item1.ProductName,
                    SumArea = item1.SumArea,
                    SumCount = item1.SumCount,
                    EndCount = item1.EndCount,
                    YuCount = item1.YuCount,
                    Description = item1.Description,
                    Sort = Sort
                };
                db.Insert(entity1);
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

        /// <summary>
        /// 批量（新增）1010
        /// </summary>
        /// <param name="dtSource">实体对象</param>
        /// <returns></returns>
        public string BatchAddEntityLiao(string keyValue, DataTable dtSource, string dir)
        {
            try
            {
                IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
                Sale_CustomerEntity oldEntity = GetEntity(keyValue);
                if (!string.IsNullOrEmpty(oldEntity.LiangPath))
                {
                    //重复导入，删掉之前导入的所有
                    db.Delete<Produce_OutEntity>(t => t.Code == keyValue);
                    oldEntity.LiangPath = "";
                }

                int rowsCount = dtSource.Rows.Count;

                for (int r = 4; r < rowsCount; r++)
                {
                    string c1 = dtSource.Rows[r][0].ToString();
                    if (!string.IsNullOrEmpty(c1))
                    {
                    }
                }
                oldEntity.LiangPath = dir;
                db.Update<Sale_CustomerEntity>(oldEntity);
                db.Commit();
                return "导入料单Excel文件成功";
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(ex.Message);
                return ex.Message;
            }

        }
    }
}
