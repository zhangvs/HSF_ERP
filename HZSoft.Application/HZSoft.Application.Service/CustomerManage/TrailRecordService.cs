﻿using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.IService.CustomerManage;
using HZSoft.Data.Repository;
using HZSoft.Util.Extension;
using HZSoft.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HZSoft.Application.Service.CustomerManage
{
    /// <summary>
    /// 版 本 6.1
    /// 
    /// 创 建：佘赐雄
    /// 日 期：2016-03-21 16:10
    /// 描 述：跟进记录
    /// </summary>
    public class TrailRecordService : RepositoryFactory<TrailRecordEntity>, ITrailRecordService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="objectId">对象Id</param>
        /// <returns>返回列表</returns>
        public IEnumerable<TrailRecordEntity> GetList(string objectId)
        {
            return this.BaseRepository().IQueryable(t => t.ObjectId.Equals(objectId)).OrderByDescending(t => t.CreateDate).ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public TrailRecordEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, TrailRecordEntity entity)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                switch (entity.ObjectSort)
                {
                    case 1:         //商机
                        ChanceEntity chanceEntity = new ChanceEntity();
                        chanceEntity.Modify(entity.ObjectId);
                        db.Update<ChanceEntity>(chanceEntity);
                        break;
                    case 2:         //客户
                        CustomerEntity customerEntity = new CustomerEntity();
                        customerEntity.Modify(entity.ObjectId);
                        db.Update<CustomerEntity>(customerEntity);
                        break;
                    case 3:         //经销商
                        Client_CompanyEntity companyEntity = new Client_CompanyEntity();
                        companyEntity.Modify(entity.ObjectId);
                        db.Update<Client_CompanyEntity>(companyEntity);
                        break;
                    case 4:         //订单
                        DZ_OrderEntity orderEntity = new DZ_OrderEntity();
                        orderEntity.Modify(entity.ObjectId);
                        db.Update<DZ_OrderEntity>(orderEntity);
                        break;
                    case 5:         //定制订单
                        DZ_OrderEntity dzEntity = new DZ_OrderEntity();
                        dzEntity.Modify(entity.ObjectId);
                        db.Update<DZ_OrderEntity>(dzEntity);
                        break;
                    default:
                        break;
                }
                entity.Create();
                db.Insert(entity);

                db.Commit();
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