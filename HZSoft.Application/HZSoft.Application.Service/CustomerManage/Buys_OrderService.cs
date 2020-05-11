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
    /// �� �� 6.1
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2018-06-01 16:52
    /// �� ����������
    /// </summary>
    public class Buys_OrderService : RepositoryFactory, Buys_OrderIService
    {
        private ICodeRuleService coderuleService = new CodeRuleService();
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<Buys_OrderEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var queryParam = queryJson.ToJObject();

            string strSql = "select * from Buys_Order where DeleteMark=0 ";
            //��������
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and CreateDate BETWEEN '" + startTime + "' and '" + endTime + "'";
            }
            //��ⵥ
            if (!queryParam["Code"].IsEmpty())
            {
                string Code = queryParam["Code"].ToString();
                strSql += " and Code like '%" + Code + "%'";
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
            //֧��״̬
            if (!queryParam["PaymentState"].IsEmpty())
            {
                int PaymentState = queryParam["PaymentState"].ToInt();
                strSql += " and PaymentState  = " + PaymentState;
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
            return this.BaseRepository().FindList<Buys_OrderEntity>(strSql.ToString(), pagination);
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public Buys_OrderEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<Buys_OrderEntity>(keyValue);
        }
        /// <summary>
        /// �������۵��Ų�ѯʵ��
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public Buys_OrderEntity GetEntityByOrderId(string orderId)
        {
            return this.BaseRepository().FindEntity<Buys_OrderEntity>(t => t.OrderId.Equals(orderId));
        }
        /// <summary>
        /// ��ȡ�ӱ���ϸ��Ϣ
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public IEnumerable<Buys_OrderItemEntity> GetDetails(string keyValue)
        {
            //return this.BaseRepository().FindList<Buys_OrderItemEntity>("select * from Buys_OrderItem where OrderId='"+keyValue+ "'");
            return this.BaseRepository().IQueryable<Buys_OrderItemEntity>(t => t.OrderId.Equals(keyValue)).OrderBy(t => t.CreateItemDate).ToList();
        }
        /// <summary>
        /// ��ȡ�ӱ���ϸ��Ϣ
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="sortCode">˳��</param>
        /// <returns></returns>
        public Buys_OrderItemEntity GetDetail(string keyValue,int? sortCode)
        {
            //return this.BaseRepository().FindList<Buys_OrderItemEntity>("select * from Buys_OrderItem where OrderId='"+keyValue+ "'");
            return this.BaseRepository().FindEntity<Buys_OrderItemEntity>(t => t.OrderId.Equals(keyValue) && t.SortCode== sortCode);
        }
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����</param>
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
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <param name="entryList">��ϸ����</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, Buys_OrderEntity entity,List<Buys_OrderItemEntity> entryList)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    //����
                    entity.Modify(keyValue);
                    db.Update(entity);
                    //��ϸ
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
                    //������ⵥ(�Զ����֮��ȡ��ʹ��**************)

                    //����
                    entity.Create();
                    db.Insert(entity);
                    coderuleService.UseRuleSeed(entity.CreateUserId, "", ((int)CodeRuleEnum.Buy_Order).ToString(), db);//ռ�õ��ݺ�
                    //��ϸ
                    foreach (Buys_OrderItemEntity item in entryList)
                    {
                        item.Create();
                        item.OrderId = entity.Id;
                        db.Insert(item);
                    }
                    

                    //ͬ�����ӵ���-���״̬
                    DZ_OrderEntity dZ_OrderEntity = new DZ_OrderEntity
                    {
                        EnterMark = 1,
                        EnterDate = DateTime.Now
                    };
                    dZ_OrderEntity.Modify(entity.OrderId);
                    db.Update<DZ_OrderEntity>(dZ_OrderEntity);
                    //ͬ����������-���״̬
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
        /// ����ɨ���װ֮��g������������Ϣ��������ⵥ����
        /// </summary>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        public void SaveBuyMain(Sale_CustomerEntity entity)
        {
            try
            {
                IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
                //��ȡ���۵��տ�״̬
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
                        PaymentState = orderEntity.PaymentState,//ȷ���Ƿ�ȫ���տ�
                        SendMark = 0,
                        DeleteMark = 0,
                        EnabledMark = 1,
                        TotalQty = 0
                    };

                    //�����Ƿ�ѡ���ж���Ҫ���
                    if (entity.GuiTiMark == 1)
                    {
                        buys_OrderEntity.GuiEnterMark = 0;//�����װ������
                    }
                    if (entity.MenBanMark == 1)
                    {
                        buys_OrderEntity.MenEnterMark = 0;//�Ű�
                    }
                    if (entity.WuJinMark == 1)
                    {
                        buys_OrderEntity.WuEnterMark = 0;//���
                    }
                    if (entity.WaiXieMark == 1)
                    {
                        buys_OrderEntity.WaiEnterMark = 0;//��Э
                    }

                    //������ⵥ����
                    this.BaseRepository().Insert(buys_OrderEntity);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// �ֹ��壬�Ű壬���������֮���ж��Ƿ���ȫ���
        /// </summary>
        /// <param name="itemEntity">ʵ�����</param>
        /// <returns></returns>
        public void SaveInForm(Buys_OrderItemEntity itemEntity)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                Buys_OrderItemEntity oldItemEntity = GetDetail(itemEntity.OrderId, itemEntity.SortCode);
                Buys_OrderEntity buyEntity = GetEntity(itemEntity.OrderId);
                //��ɾ���ٴ���
                if (oldItemEntity != null)
                {
                    db.Delete<Buys_OrderItemEntity>(t => t.OrderId.Equals(itemEntity.OrderId) && t.SortCode == itemEntity.SortCode);
                    buyEntity.TotalQty -= oldItemEntity.Qty;//��ȥ�Ͽ��
                }

                //������ⵥ�ӱ�
                itemEntity.Create();
                db.Insert<Buys_OrderItemEntity>(itemEntity);

                buyEntity.TotalQty += itemEntity.Qty;//�����¿��
                //�޸����״̬���ֹ��壬�Ű壬�����Э
                switch (itemEntity.SortCode)
                {
                    case 1: buyEntity.GuiEnterMark = 1;break;
                    case 2: buyEntity.MenEnterMark = 1; break;
                    case 3: buyEntity.WuEnterMark = 1; break;
                    case 4: buyEntity.WaiEnterMark = 1; break;
                    default:
                        break;
                }

                //�ж��Ƿ���ȫ���
                if (buyEntity.GuiEnterMark == 0 || buyEntity.MenEnterMark == 0 || buyEntity.WuEnterMark == 0 || buyEntity.WaiEnterMark == 0)
                {
                    //��û����ȫ���
                    buyEntity.AllEnterMark = 0;
                }
                else
                {
                    //��ȫ����޸�״̬
                    buyEntity.AllEnterMark = 1;
                    buyEntity.AllEnterDate = DateTime.Now;

                    //ͬ�����ӵ���-���״̬
                    DZ_OrderEntity dZ_OrderEntity = new DZ_OrderEntity
                    {
                        EnterMark = 1,
                        EnterDate = DateTime.Now
                    };
                    dZ_OrderEntity.Modify(buyEntity.OrderId);
                    db.Update<DZ_OrderEntity>(dZ_OrderEntity);

                    //ͬ����������-���״̬
                    Sale_CustomerEntity produceEntity = new Sale_CustomerEntity
                    {
                        EnterMark = 1,
                        EnterDate = DateTime.Now
                    };
                    produceEntity.Modify(buyEntity.ProduceId);
                    db.Update<Sale_CustomerEntity>(produceEntity);



                    string wk = "";
                    //��΢��ģ����Ϣ--������������(��ȫ�������)
                    if (buyEntity.PaymentState==3)
                    {
                        //��΢��ģ����Ϣ---���+����β��֮�󣬸���³³����Ϣ����????
                        //��������֪ͨ��9��ȫ������ѣ�
                        TemplateWxApp.SendTemplateAllIn("oA-EC1Ucth5a3bkvcJSdiTCizz_g", 
                            "���ã����µĶ����Ѿ����!", buyEntity.OrderTitle, "��" + buyEntity.TotalQty + "��������з���֪ͨ");
                    }
                    else
                    {
                        wk = "��ȷ��β�";
                    }

                    //��΢��ģ����Ϣ--������������(��ȫ�������)
                    if (!string.IsNullOrEmpty(buyEntity.SalesmanUserName))
                    {
                        var hsf_CardList = db.IQueryable<Hsf_CardEntity>(t => t.Name.Equals(buyEntity.SalesmanUserName));
                        if (hsf_CardList.Count() != 0)
                        {
                            var hsf_CardEntity = hsf_CardList.First();
                            //��������֪ͨ��ֻ�й�ע���ںŵ�ҵ��Ա�����յ���Ϣ(8��ȫ�������)
                            string backMsg = TemplateWxApp.SendTemplateAllIn(hsf_CardEntity.OpenId,
                                "���ã����Ķ����Ѿ�ȫ�����!", buyEntity.OrderTitle, "��" + buyEntity.TotalQty + "����"+ wk);
                            if (backMsg != "ok")
                            {
                                //ҵ��Աû�й�ע���ںţ�����΢��Post���������󣡴�����룺43004��˵����require subscribe hint: [ziWtva03011295]
                                LogHelper.AddLog(buyEntity.SalesmanUserName + "û�й�ע���ں�");//��¼��־
                            }
                        }
                    }
                }
                
                //����
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
        /// ����֪ͨ
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
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


                    //��΢��ģ����Ϣ---����֪֮ͨ�󣬸���ά�ŷ���Ϣ����?????
                    //��������֪ͨ��10����֪ͨ���ѣ�
                    TemplateWxApp.SendTemplateSend("oA-EC1Ucth5a3bkvcJSdiTCizz_g", 
                        "���ã����µķ���֪ͨ!", entity.Code, entity.OrderTitle + "������" + entity.SendPlanDate + "֮ǰ���ŷ�����");
                }
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }

        /// <summary>
        /// ʵ�ʷ���
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
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

                    //ͬ�����ӵ���-���״̬
                    DZ_OrderEntity dZ_OrderEntity = new DZ_OrderEntity
                    {
                        SendMark = 1,
                        SendDate = DateTime.Now
                    };
                    dZ_OrderEntity.Modify(entity.OrderId);
                    db.Update<DZ_OrderEntity>(dZ_OrderEntity);

                    //ͬ����������-���״̬
                    Sale_CustomerEntity produceEntity = new Sale_CustomerEntity
                    {
                        SendMark = 1,
                        SendDate = DateTime.Now
                    };
                    produceEntity.Modify(entity.ProduceId);
                    db.Update<Sale_CustomerEntity>(produceEntity);
                    db.Commit();


                    //��΢��ģ����Ϣ--������������(10ʵ�ʷ�������)
                    if (!string.IsNullOrEmpty(entity.SalesmanUserName))
                    {
                        var hsf_CardList = db.IQueryable<Hsf_CardEntity>(t => t.Name.Equals(entity.SalesmanUserName));
                        if (hsf_CardList.Count() != 0)
                        {
                            var hsf_CardEntity = hsf_CardList.First();
                            //��������֪ͨ��ֻ�й�ע���ںŵ�ҵ��Ա�����յ���Ϣ(11ʵ�ʷ�������)
                            string backMsg = TemplateWxApp.SendTemplateSend(hsf_CardEntity.OpenId,
                                "���ã����Ķ����Ѿ�����!", entity.Code, entity.OrderTitle+"����" + entity.TotalQty + "����");
                            if (backMsg != "ok")
                            {
                                //ҵ��Աû�й�ע���ںţ�����΢��Post���������󣡴�����룺43004��˵����require subscribe hint: [ziWtva03011295]
                                LogHelper.AddLog(entity.SalesmanUserName + "û�й�ע���ں�");//��¼��־
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
