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
        /// 保存生产单主单
        /// </summary>
        /// <param name="orderEntity">实体对象</param>
        /// <returns></returns>
        public void SaveBuyMain(IRepository db,DZ_OrderEntity orderEntity)
        {
            try
            {
                //自动创建【生产单】主单部分
                Sale_CustomerEntity sale_CustomerEntity = new Sale_CustomerEntity
                {
                    OrderId = orderEntity.Id,
                    OrderCode = orderEntity.Code,
                    OrderTitle=orderEntity.OrderTitle,
                    ProduceCode = orderEntity.Code,//生产单号默认和销售单号一样
                    OrderType = orderEntity.OrderType,
                    CompanyId = orderEntity.CompanyId,
                    CompanyName = orderEntity.CompanyName,
                    CustomerId = orderEntity.CustomerId,
                    CustomerName = orderEntity.CustomerName,
                    SalesmanUserId = orderEntity.SalesmanUserId,
                    SalesmanUserName = orderEntity.SalesmanUserName,//销售单
                    CustomerTelphone = orderEntity.CustomerTelphone,
                    SendPlanDate = orderEntity.SendPlanDate,
                    Address = orderEntity.Address,
                    ShippingType = orderEntity.ShippingType,
                    Carrier = orderEntity.Carrier,

                    KaiLiaoMark = 1,//默认选择5步骤
                    FengBianMark = 1,
                    PaiZuanMark = 1,
                    ShiZhuangMark = 1,
                    BaoZhuangMark = 1,

                    MoneyOkMark = orderEntity.MoneyOkMark,
                    MoneyOkDate =orderEntity.MoneyOkDate//报价审核
                };
                sale_CustomerEntity.Create();//付款时间

                //主表
                db.Insert(sale_CustomerEntity);

                //生成生产单id二维码
                QRCode(sale_CustomerEntity.ProduceId);
                
            }
            catch (Exception)
            {
                throw;
            }
        }


        public string QRCode(string id)
        {
            string url = "http://www.sikelai.cn/WeChatManage/Produce/StepSweepcode?id=" + id;
            System.Drawing.Bitmap bt;
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;//编码方式(注意：BYTE能支持中文，ALPHA_NUMERIC扫描出来的都是数字)
            qrCodeEncoder.QRCodeScale = 3;//大小(值越大生成的二维码图片像素越高)41的倍数
            qrCodeEncoder.QRCodeVersion = 0;//版本(注意：设置为0主要是防止编码的字符串太长时发生错误)
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;//错误效验、错误更正(有4个等级)
            qrCodeEncoder.QRCodeBackgroundColor = Color.White;//背景色
            qrCodeEncoder.QRCodeForegroundColor = Color.Black;//前景色

            bt = qrCodeEncoder.Encode(url, Encoding.UTF8);
            string filename = id;// "code";
            string file_path = AppDomain.CurrentDomain.BaseDirectory + "Resource\\QRCode\\";
            string codeUrl = file_path + filename + ".jpg";

            //根据文件名称，自动建立对应目录
            if (!System.IO.Directory.Exists(file_path))
            {
                System.IO.Directory.CreateDirectory(file_path);
            }
            ////防止内容重复，导致名称重复问题，
            ////若要每次更新，可去掉本段代码 ↓↓↓↓↓
            //int i = 1;
            //while (System.IO.File.Exists(codeUrl))
            //{               
            //    string _filename = filename + "("+i+")";
            //    codeUrl = file_path + _filename + ".jpg";
            //    i++;
            //}
            ////   ↑↑↑↑↑↑↑
            bt.Save(codeUrl);//保存图片
            return codeUrl;
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
                        entity.DownMark = entity.DownMark;
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
            //明细表，每道工序都需要材料
            //int sort = 1;
            //if (entity.KaiLiaoMark == 1)
            //{
            //    AddItem(db, entryList, entity.ProduceId, 1,"开料", sort++);
            //}
            //if (entity.FengBianMark == 1)
            //{
            //    AddItem(db, entryList, entity.ProduceId, 2, "封边", sort++);
            //}
            //if (entity.PaiZuanMark == 1)
            //{
            //    AddItem(db, entryList, entity.ProduceId, 3, "排钻", sort++);
            //}
            //if (entity.ShiZhuangMark == 1)
            //{
            //    AddItem(db, entryList, entity.ProduceId, 4, "试装", sort++);
            //}
            //if (entity.BaoZhuangMark == 1)
            //{
            //    AddItem(db, entryList, entity.ProduceId, 5, "包装", sort++);
            //}
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
