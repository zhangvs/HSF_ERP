﻿using HZSoft.Application.Busines.BaseManage;
using HZSoft.Application.Entity.BaseManage;
using HZSoft.Application.Code;
using HZSoft.Cache.Factory;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using HZSoft.Application.Entity.WebApp;

namespace HZSoft.Application.Cache
{
    /// <summary>
    /// 版 本 6.1
    /// 
    /// 创建人：张彦山
    /// 日 期：2016.3.4 9:56
    /// 描 述：用户信息缓存
    /// </summary>
    public class UserCache
    {
        private UserBLL busines = new UserBLL();

        /// <summary>
        /// 用户列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserEntity> GetList()
        {
            IEnumerable<UserEntity> data = new List<UserEntity>();
            var cacheList = CacheFactory.Cache().GetCache<IEnumerable<UserEntity>>(busines.cacheKey);
            if (cacheList == null)
            {
                data = busines.GetList();
                CacheFactory.Cache().WriteCache(data, busines.cacheKey);
            }
            else
            {
                data = cacheList;
            }
            return data;
        }
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="departmentId">部门Id</param>
        /// <returns></returns>
        public IEnumerable<UserEntity> GetList(string departmentId)
        {
            var data = this.GetList();
            if (!string.IsNullOrEmpty(departmentId))
            {
                data = data.Where(t => t.DepartmentId == departmentId);
            }
            return data;
        }
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="departmentId">部门Id</param>
        /// <returns></returns>
        public IEnumerable<UserEntity> GetListByRole(string roleId)
        {
            var data = this.GetList();
            if (!string.IsNullOrEmpty(roleId))
            {
                data = data.Where(t => t.RoleId == roleId);
            }
            return data;
        }
        public Dictionary<string,appUserInfoModel> GetListToApp()
        {
            Dictionary<string, appUserInfoModel> data = new Dictionary<string,appUserInfoModel>();
            var datalist = this.GetList();
            foreach (var item in datalist)
            {
                appUserInfoModel one = new appUserInfoModel {
                    UserId = item.UserId,
                    Account = item.Account,
                    RealName = item.RealName,
                    OrganizeId = item.OrganizeId,
                    DepartmentId = item.DepartmentId
                };
                data.Add(item.UserId, one);
            }

            return data;
        }
    }
}
