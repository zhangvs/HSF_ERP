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
    /// �� �� 6.1
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2017-12-23 14:41
    /// �� ��������ͳ��
    /// </summary>
    public class Sale_CustomerService : RepositoryFactory, Sale_CustomerIService
    {
        private DZ_OrderIService dz_orderService = new DZ_OrderService();
        private Buys_OrderIService buyService = new Buys_OrderService();
        
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<Sale_CustomerEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            string strSql = "select * from Sale_Customer where DeleteMark=0 ";
            //��������
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and CreateDate >= '" + startTime + "' and CreateDate < '" + endTime + "'";
            }
            //�������
            if (!queryParam["ProduceCode"].IsEmpty())
            {
                string ProduceCode = queryParam["ProduceCode"].ToString();
                strSql += " and ProduceCode like '%" + ProduceCode + "%'";
            }
            //���۱��
            if (!queryParam["OrderCode"].IsEmpty())
            {
                string OrderCode = queryParam["OrderCode"].ToString();
                strSql += " and OrderCode like '%" + OrderCode + "%'";
            }

            //��˾��
            if (!queryParam["CompanyName"].IsEmpty())
            {
                string CompanyName = queryParam["CompanyName"].ToString();
                strSql += " and CompanyName like '%" + CompanyName + "%'";
            }
            //�ͻ���
            if (!queryParam["CustomerName"].IsEmpty())
            {
                string CustomerName = queryParam["CustomerName"].ToString();
                strSql += " and CustomerName like '%" + CustomerName + "%'";
            }
            //������
            if (!queryParam["SalesmanUserName"].IsEmpty())
            {
                string SalesmanUserName = queryParam["SalesmanUserName"].ToString();
                strSql += " and SalesmanUserName like '%" + SalesmanUserName + "%'";
            }
            //������Ա
            if (!queryParam["StepUserName"].IsEmpty())
            {
                string StepUserName = queryParam["StepUserName"].ToString();
                strSql += " and (KaiLiaoUserName like '%" + StepUserName + "%' or  FengBianUserName like '%" + StepUserName + "%' or  PaiZuanUserName like '%" + StepUserName + "%' or  ShiZhuangUserName like '%" + StepUserName + "%' or  BaoZhuangUserName like '%" + StepUserName + "%' or  XiSuUserName like '%" + StepUserName + "%')";
            }
            //�������
            if (!queryParam["MoneyOkMark"].IsEmpty())
            {
                int MoneyOkMark = queryParam["MoneyOkMark"].ToInt();
                strSql += " and MoneyOkMark  = " + MoneyOkMark;
            }
            //�µ�
            if (!queryParam["DownMark"].IsEmpty())
            {
                int DownMark = queryParam["DownMark"].ToInt();
                strSql += " and DownMark  = " + DownMark;
            }
            //�Ƶ�
            if (!queryParam["PushMark"].IsEmpty())
            {
                int PushMark = queryParam["PushMark"].ToInt();
                strSql += " and PushMark  = " + PushMark;
            }
            //��װ
            if (!queryParam["BaoZhuangMark"].IsEmpty())
            {
                int BaoZhuangMark = queryParam["BaoZhuangMark"].ToInt();
                strSql += " and BaoZhuangMark  = " + BaoZhuangMark;
            }
            //���
            if (!queryParam["EnterMark"].IsEmpty())
            {
                int EnterMark = queryParam["EnterMark"].ToInt();
                strSql += " and EnterMark  = " + EnterMark;
            }
            //����
            if (!queryParam["SendMark"].IsEmpty())
            {
                int SendMark = queryParam["SendMark"].ToInt();
                strSql += " and SendMark  = " + SendMark;
            }
            //����
            if (!queryParam["OverMark"].IsEmpty())
            {
                int OverMark = queryParam["OverMark"].ToInt();
                strSql += " and OverMark  = " + OverMark;
            }

            return this.BaseRepository().FindList<Sale_CustomerEntity>(strSql.ToString(), pagination);
        }


        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<Sale_CustomerEntity> GetList(string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            string strSql = "select * from Sale_Customer  where DeleteMark=0 ";
            //��������
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and CreateDate >= '" + startTime + "' and CreateDate < '" + endTime + "'";
            }
            //�������
            if (!queryParam["ProduceCode"].IsEmpty())
            {
                string ProduceCode = queryParam["ProduceCode"].ToString();
                strSql += " and ProduceCode like '%" + ProduceCode + "%'";
            }
            //���۱��
            if (!queryParam["OrderCode"].IsEmpty())
            {
                string OrderCode = queryParam["OrderCode"].ToString();
                strSql += " and OrderCode like '%" + OrderCode + "%'";
            }

            //��˾��
            if (!queryParam["CompanyName"].IsEmpty())
            {
                string CompanyName = queryParam["CompanyName"].ToString();
                strSql += " and CompanyName like '%" + CompanyName + "%'";
            }
            //�ͻ���
            if (!queryParam["CustomerName"].IsEmpty())
            {
                string CustomerName = queryParam["CustomerName"].ToString();
                strSql += " and CustomerName like '%" + CustomerName + "%'";
            }

            //������
            if (!queryParam["SalesmanUserName"].IsEmpty())
            {
                string SalesmanUserName = queryParam["SalesmanUserName"].ToString();
                strSql += " and SalesmanUserName like '%" + SalesmanUserName + "%'";
            }
            //������Ա
            if (!queryParam["StepUserName"].IsEmpty())
            {
                string StepUserName = queryParam["StepUserName"].ToString();
                strSql += " and (KaiLiaoUserName like '%" + StepUserName + "%' or  FengBianUserName like '%" + StepUserName + "%' or  PaiZuanUserName like '%" + StepUserName + "%' or  ShiZhuangUserName like '%" + StepUserName + "%' or  BaoZhuangUserName like '%" + StepUserName + "%' or  XiSuUserName like '%" + StepUserName + "%')";
            }

            //�������
            if (!queryParam["MoneyOkMark"].IsEmpty())
            {
                int MoneyOkMark = queryParam["MoneyOkMark"].ToInt();
                strSql += " and MoneyOkMark  = " + MoneyOkMark;
            }
            //�µ�
            if (!queryParam["DownMark"].IsEmpty())
            {
                int DownMark = queryParam["DownMark"].ToInt();
                strSql += " and DownMark  = " + DownMark;
            }
            //��װ
            if (!queryParam["BaoZhuangMark"].IsEmpty())
            {
                int BaoZhuangMark = queryParam["BaoZhuangMark"].ToInt();
                strSql += " and BaoZhuangMark  = " + BaoZhuangMark;
            }
            //���
            if (!queryParam["EnterMark"].IsEmpty())
            {
                int EnterMark = queryParam["EnterMark"].ToInt();
                strSql += " and EnterMark  = " + EnterMark;
            }
            //����
            if (!queryParam["SendMark"].IsEmpty())
            {
                int SendMark = queryParam["SendMark"].ToInt();
                strSql += " and SendMark  = " + SendMark;
            }
            //����
            if (!queryParam["OverMark"].IsEmpty())
            {
                int OverMark = queryParam["OverMark"].ToInt();
                strSql += " and OverMark  = " + OverMark;
            }
            return this.BaseRepository().FindList<Sale_CustomerEntity>(strSql.ToString());
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public Sale_CustomerEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<Sale_CustomerEntity>(keyValue);
        }
        /// <summary>
        /// ��ȡ�ӱ���ϸ��Ϣ
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public IEnumerable<Sale_Customer_ItemEntity> GetDetails(string keyValue)
        {
            //return this.BaseRepository().FindList<Sale_Customer_ItemEntity>("select * from Sale_Customer_Item where MainId='"+keyValue+ "'");
            return this.BaseRepository().FindList<Sale_Customer_ItemEntity>(t => t.MainId.Equals(keyValue)).OrderBy(t => t.Sort).ToList();
        }
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����</param>
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
        /// �������޸ı༭
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <param name="entryList">ʵ���Ӷ���</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, Sale_CustomerEntity entity, List<Sale_Customer_ItemEntity> entryList)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                //����ɨ��֮��༭
                Sale_CustomerEntity oldEntity = GetEntity(keyValue);
                if (oldEntity!=null)
                {
                    //�������ɨ��֮�����޸ı༭������Ĺ�ѡ״̬Ĭ��==1������ɨ��֮���״̬Ϊ2��������Ϊ�༭�޸�Ϊ2
                    entity.KaiLiaoMark = oldEntity.KaiLiaoMark ==2  ? 2 : entity.KaiLiaoMark;
                    entity.FengBianMark = oldEntity.FengBianMark == 2 ? 2 : entity.FengBianMark;
                    entity.PaiZuanMark = oldEntity.PaiZuanMark == 2 ? 2 : entity.PaiZuanMark;
                    entity.ShiZhuangMark = oldEntity.ShiZhuangMark == 2 ? 2 : entity.ShiZhuangMark;
                    entity.BaoZhuangMark = oldEntity.BaoZhuangMark == 2 ? 2 : entity.BaoZhuangMark;
                    LogHelper.AddLog("����ɨ��֮���ֱ༭��"+oldEntity.ProduceCode);
                }

                //��װɨ��֮��-���ֹ�ѡ���ϴ���--�ֱ༭
                Buys_OrderEntity oldBuysEntity = buyService.GetEntity(keyValue);
                if (oldBuysEntity!=null)
                {
                    //����Ѿ�����ˣ���ѡ���ϴ��󣬷����ٹ�ѡ�ˣ���Ҫ�޸ģ�Ҫͬ������ⵥ
                    if (entity.GuiTiMark==1 && oldBuysEntity.GuiEnterMark==null)
                    {
                        oldBuysEntity.GuiEnterMark = 0;//�޸Ĺ���״̬Ϊ��Ҫ���
                    }
                    if (entity.WuJinMark == 1 && oldBuysEntity.WuEnterMark == null)
                    {
                        oldBuysEntity.WuEnterMark = 0;//�޸����״̬Ϊ��Ҫ���
                    }
                    if (entity.MenBanMark == 1 && oldBuysEntity.MenEnterMark == null)
                    {
                        oldBuysEntity.MenEnterMark = 0;//�޸��Ű�״̬Ϊ��Ҫ���
                    }
                    if (entity.WaiXieMark == 1 && oldBuysEntity.WaiEnterMark == null)
                    {
                        oldBuysEntity.WaiEnterMark = 0;//�޸���Э״̬Ϊ��Ҫ���
                    }
                    LogHelper.AddLog("��װɨ��֮��-���ֹ�ѡ���ϴ���--�ֱ༭��" + oldEntity.ProduceCode);
                }

                //����
                entity.Modify(keyValue);
                db.Update(entity);
                //��ϸ
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
        /// �µ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        public void SaveDownForm(string keyValue, Sale_CustomerEntity entity)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    //Sale_CustomerEntity oldEntity = GetEntity(keyValue);
                    //ԭ������û���µ��ļ�����һ���ϴ��µ��ļ������޸��µ�״̬
                    if (!string.IsNullOrEmpty(entity.DownPath))// && string.IsNullOrEmpty(oldEntity.DownPath)//����֮ǰ��û���ϴ����޸��µ�״̬
                    {
                        //�޸��������µ�״̬
                        entity.DownDate = DateTime.Now;
                        entity.DownUserId = OperatorProvider.Provider.Current().UserId;
                        entity.DownUserName = OperatorProvider.Provider.Current().UserName;

                        //�޸����۵��µ�״̬
                        DZ_OrderEntity dZ_OrderEntity = new DZ_OrderEntity
                        {
                            DownMark = entity.DownMark,
                            DownDate = DateTime.Now,
                            DownUserId = OperatorProvider.Provider.Current().UserId,
                            DownUserName = OperatorProvider.Provider.Current().UserName,
                            DownPath = entity.DownPath
                        };
                        dZ_OrderEntity.Modify(entity.OrderId);//ԭ������ʵ��Ŷ�
                        db.Update<DZ_OrderEntity>(dZ_OrderEntity);


                        //��΢��ģ����Ϣ---�µ�֮�󣬸��̶��ʷ���Ϣ����oA-EC1W1BQZ46Wc8HPCZZUUFbE9M
                        //��������֪ͨ��8�µ����ѣ�
                        TemplateWxApp.SendTemplateNew("oA-EC1W1BQZ46Wc8HPCZZUUFbE9M",
                            "���ã����µĶ�����Ҫ�Ƶ�!", entity.OrderTitle, entity.OrderCode, "���������Ƶ���");
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
        /// ����
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
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
                        //�޸�����������״̬
                        entity.PushMark = -1;
                        entity.PushDate = DateTime.Now;
                        entity.PushUserId = OperatorProvider.Provider.Current().UserId;
                        entity.PushUserName = OperatorProvider.Provider.Current().UserName;

                        //�޸����۵�����״̬
                        DZ_OrderEntity dZ_OrderEntity = new DZ_OrderEntity
                        {
                            PushMark = -1,
                            PushDate = DateTime.Now,
                            PushBackReason = entity.PushBackReason,
                            PushBackPath = entity.PushBackPath
                        };
                        dZ_OrderEntity.Modify(entity.OrderId);//ԭ������ʵ��Ŷ�
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
        /// �Ƶ�,����
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="state">״̬1�Ƶ�-1����</param>
        /// <param name="orderId">���۵�id</param>
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
                        //�޸����۵��Ƶ�״̬
                        DZ_OrderEntity dZ_OrderEntity = new DZ_OrderEntity
                        {
                            PushMark = state,
                            PushDate = DateTime.Now
                        };
                        dZ_OrderEntity.Modify(orderId);//ԭ������ʵ��Ŷ�
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
        /// �Ų���������
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
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
        /// �޸Ĺ���״̬
        /// </summary>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        public void UpdateStepState(Sale_CustomerEntity entity)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                db.Update < Sale_CustomerEntity >(entity);
                //ͬ�����ӵ���-����״̬
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
        /// ��ϸ������ÿ��������Ҫ�Ĳ��ϡ����ټ�����������ã�
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
        /// ���
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
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
