using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.IService.CustomerManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZSoft.Application.Service.CustomerManage
{
    /// <summary>
    /// 添加操作记录所有的
    /// </summary>
    public class RecordHelp
    {
        private static ITrailRecordService recordService = new TrailRecordService();

        /// <summary>
        /// 操作记录添加
        /// </summary>
        /// <param name="sort"></param>
        /// <param name="objectId"></param>
        /// <param name="content"></param>
        public static void AddRecord(int sort,string objectId,string content)
        {
            TrailRecordEntity recordEntity = new TrailRecordEntity()
            {
                ObjectSort = sort,
                ObjectId = objectId,
                TrackContent = content
            };
            recordService.SaveForm(null, recordEntity);//全是新增
        }
    }
}
