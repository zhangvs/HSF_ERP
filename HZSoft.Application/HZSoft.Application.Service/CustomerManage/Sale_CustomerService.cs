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
                strSql += " and MoneyOkMark  = " + MoneyOkMark;
            }
            //下单
            if (!queryParam["DownMark"].IsEmpty())
            {
                int DownMark = queryParam["DownMark"].ToInt();
                strSql += " and DownMark  = " + DownMark;
            }
            //推单
            if (!queryParam["PushMark"].IsEmpty())
            {
                int PushMark = queryParam["PushMark"].ToInt();
                strSql += " and PushMark  = " + PushMark;
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
                strSql += " and MoneyOkMark  = " + MoneyOkMark;
            }
            //下单
            if (!queryParam["DownMark"].IsEmpty())
            {
                int DownMark = queryParam["DownMark"].ToInt();
                strSql += " and DownMark  = " + DownMark;
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
                    //如果生产扫码之后又修改编辑，工序的勾选状态默认==1，但是扫码之后的状态为2，不能因为编辑修改为2
                    entity.KaiLiaoMark = oldEntity.KaiLiaoMark ==2  ? 2 : entity.KaiLiaoMark;
                    entity.FengBianMark = oldEntity.FengBianMark == 2 ? 2 : entity.FengBianMark;
                    entity.PaiZuanMark = oldEntity.PaiZuanMark == 2 ? 2 : entity.PaiZuanMark;
                    entity.ShiZhuangMark = oldEntity.ShiZhuangMark == 2 ? 2 : entity.ShiZhuangMark;
                    entity.BaoZhuangMark = oldEntity.BaoZhuangMark == 2 ? 2 : entity.BaoZhuangMark;
                    LogHelper.AddLog("生产扫码之后又编辑，"+oldEntity.ProduceCode);
                }

                //包装扫码之后-发现勾选材料错误--又编辑
                Buys_OrderEntity oldBuysEntity = buyService.GetEntity(keyValue);
                if (oldBuysEntity!=null)
                {
                    //如果已经入库了，勾选材料错误，发现少勾选了，需要修改，要同步到入库单
                    if (entity.GuiTiMark==1 && oldBuysEntity.GuiEnterMark==null)
                    {
                        oldBuysEntity.GuiEnterMark = 0;//修改柜体状态为需要入库
                    }
                    if (entity.WuJinMark == 1 && oldBuysEntity.WuEnterMark == null)
                    {
                        oldBuysEntity.WuEnterMark = 0;//修改五金状态为需要入库
                    }
                    if (entity.MenBanMark == 1 && oldBuysEntity.MenEnterMark == null)
                    {
                        oldBuysEntity.MenEnterMark = 0;//修改门板状态为需要入库
                    }
                    if (entity.WaiXieMark == 1 && oldBuysEntity.WaiEnterMark == null)
                    {
                        oldBuysEntity.WaiEnterMark = 0;//修改外协状态为需要入库
                    }
                    LogHelper.AddLog("包装扫码之后-发现勾选材料错误--又编辑，" + oldEntity.ProduceCode);
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
                    //Sale_CustomerEntity oldEntity = GetEntity(keyValue);
                    //原生产单没有下单文件，第一次上传下单文件，则修改下单状态
                    if (!string.IsNullOrEmpty(entity.DownPath))// && string.IsNullOrEmpty(oldEntity.DownPath)//不管之前有没有上传都修改下单状态
                    {
                        //修改生产单下单状态
                        entity.DownDate = DateTime.Now;
                        entity.DownUserId = OperatorProvider.Provider.Current().UserId;
                        entity.DownUserName = OperatorProvider.Provider.Current().UserName;

                        //修改销售单下单状态
                        DZ_OrderEntity dZ_OrderEntity = new DZ_OrderEntity
                        {
                            DownMark = entity.DownMark,
                            DownDate = DateTime.Now,
                            DownUserId = OperatorProvider.Provider.Current().UserId,
                            DownUserName = OperatorProvider.Provider.Current().UserName,
                            DownPath = entity.DownPath
                        };
                        dZ_OrderEntity.Modify(entity.OrderId);//原生产单实体才对
                        db.Update<DZ_OrderEntity>(dZ_OrderEntity);


                        //发微信模板消息---下单之后，给程东彩发消息提醒oA-EC1W1BQZ46Wc8HPCZZUUFbE9M
                        //订单生成通知（8下单提醒）
                        TemplateWxApp.SendTemplateNew("oA-EC1W1BQZ46Wc8HPCZZUUFbE9M",
                            "您好，有新的订单需要推单!", entity.OrderTitle, entity.OrderCode, "请进行审核推单。");
                    }
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
                }
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }


        /// <summary>
        /// 推单,撤单
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


    }
}
