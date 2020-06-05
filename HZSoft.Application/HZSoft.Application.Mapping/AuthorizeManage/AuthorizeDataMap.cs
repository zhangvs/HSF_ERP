﻿using HZSoft.Application.Entity.AuthorizeManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.AuthorizeManage
{
    /// <summary>
    /// 版 本 6.1
    /// 
    /// 创建人：张彦山
    /// 日 期：2015.11.27
    /// 描 述：授权数据范围
    /// </summary>
    public class AuthorizeDataMap : EntityTypeConfiguration<AuthorizeDataEntity>
    {
        public AuthorizeDataMap()
        {
            #region 表、主键
            //表
            this.ToTable("Base_AuthorizeData");
            //主键
            this.HasKey(t => t.AuthorizeDataId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
