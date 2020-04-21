using HZSoft.Application.Busines.BaseManage;
using HZSoft.Application.Entity.BaseManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HZSoft.Application.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 智能主机校验
    /// </summary>
    public class HostController : Controller
    {
        private Hsf_HostNameBLL hsf_hostnamebll = new Hsf_HostNameBLL();
        /// <summary>
        /// 校验主机id是否重复
        /// </summary>
        /// <param name="name">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        //[AllowAnonymous]
        //[EnableCors(origins: "http://localhost:9008/", headers: "*", methods: "GET,POST,PUT,DELETE")]
        public string Check(string name)
        {
            var data = hsf_hostnamebll.GetEntity(name);
            if (data != null)
            {
                return "no";
            }
            else
            {
                Hsf_HostNameEntity entity = new Hsf_HostNameEntity();
                entity.Serverid = name;
                hsf_hostnamebll.SaveForm(null, entity);
                return "yes";
            }
        }

    }
}
