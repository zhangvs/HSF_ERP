using HZSoft.Application.Busines.CustomerManage;
using HZSoft.Application.Code;
using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.Web.Utility;
using HZSoft.Util;
using HZSoft.Util.WebControl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HZSoft.Application.Web.Areas.WeChatManage.Controllers
{
    /// <summary>
    /// 思科莱投票系统
    /// </summary>
    [HandlerWXAuthorizeAttribute(LoginMode.Enforce)]
    public class SikelaiController : BaseWxUserController
    {
        #region 视图
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var data = signUpBLL.GetList("{}").ToList();
            ViewBag.Count1 = data.Count(t => t.GroupId.Equals(1) && t.CheckMark.Equals(1));
            ViewBag.Count2 = data.Count(t => t.GroupId.Equals(2) && t.CheckMark.Equals(1));
            ViewBag.Count3 = data.Count(t => t.GroupId.Equals(3) && t.CheckMark.Equals(1));
            return View();
        }
        /// <summary>
        /// 详情页
        /// </summary>
        /// <returns></returns>
        public ActionResult Details(string id,string keyword)
        {
            if (!string.IsNullOrEmpty(id) || !string.IsNullOrEmpty(keyword))
            {
                string queryJson = @"{Id:'" + id + "',keyword:'" + keyword + "'}";
                var list = signUpBLL.GetList(queryJson);
                if (list.Count()==0)
                {
                    return RedirectToAction("Error", new { msg = "没有任何结果！" });
                }
                else
                {
                    var entity = list.First();
                    if (entity != null)
                    {
                        return View(entity);
                    }
                    else
                    {
                        return RedirectToAction("Error", new { msg= "没有任何结果！" });
                    }
                }

            }
            else
            {
                return RedirectToAction("Error", new { msg = "没有任何结果！" });
            }
        }

        /// <summary>
        /// 没有任何结果
        /// </summary>
        /// <returns></returns>
        public ActionResult Error(string msg)
        {
            ViewBag.Msg = msg;
            return View();
        }

        /// <summary>
        /// 投票成功
        /// </summary>
        /// <returns></returns>
        public ActionResult Polled()
        {
            return View();
        }

        /// <summary>
        /// 活动说明
        /// </summary>
        /// <returns></returns>
        public ActionResult SklActivity()
        {
            return View();
        }

        /// <summary>
        /// 思科莱简介
        /// </summary>
        /// <returns></returns>
        public ActionResult SklIntroduce()
        {
            return View();
        }
        /// <summary>
        /// 思科莱智能家居简介
        /// </summary>
        /// <returns></returns>
        public ActionResult SklSmartIntroduce()
        {
            return View();
        }
        

        /// <summary>
        /// 最新参与
        /// </summary>
        /// <returns></returns>
        public ActionResult ListsNew()
        {
            return View();
        }
        /// <summary>
        /// 人气选手
        /// </summary>
        /// <returns></returns>
        public ActionResult ListsTop()
        {
            return View();
        }



        /// <summary>
        /// 报名
        /// </summary>
        /// <returns></returns>
        public ActionResult SignUp()
        {
            //已报名选手，跳转到审核，审核成功
            if (string.IsNullOrEmpty(CurrentWxUser.OpenId))
            {
                return View();
            }
            else
            {
                string queryJson = @"{OpenId:'" + CurrentWxUser.OpenId + "'}";
                var list = signUpBLL.GetList(queryJson);
                if (list.Count() > 0)
                {
                    var list0 = list.First();
                    if (list0.CheckMark == 0)
                    {
                        return View("SignUpCheck");
                    }
                    else
                    {
                        return RedirectToAction("SignUpSuccess", new { id = list0.Id });
                    }
                }
                return View();
            }

        }
        /// <summary>
        /// 审核
        /// </summary>
        /// <returns></returns>
        public ActionResult SignUpCheck()
        {
            return View();
        }
        /// <summary>
        /// 审核成功
        /// </summary>
        /// <returns></returns>
        public ActionResult SignUpSuccess(int id)
        {
            ViewBag.Id = id;
            return View();
        }



        /// <summary>
        /// 搜索
        /// </summary>
        /// <returns></returns>
        public ActionResult Search(string keyword)
        {
            return View();
        }


        /// <summary>
        /// 专业组
        /// </summary>
        /// <returns></returns>
        public ActionResult Top()
        {
            return View();
        }



        /// <summary>
        /// 活动规则
        /// </summary>
        /// <returns></returns>
        public ActionResult Rule()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="sidx"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetPageData(int page,string sidx, int? groupId)
        {
            string queryJson = "{CheckMark:1}";
            if (groupId!=0)
            {
                queryJson = @"{GroupId:" + groupId + ",CheckMark:1}";
            }

            Pagination pagination = new Pagination()
            {
                rows = 10,
                page = page,
                sidx = sidx,
                sord = "desc"
            };
            var data = signUpBLL.GetPageList(pagination, queryJson);
            return Content(data.ToJson());
        }
        #endregion

        #region 提交

        /// <summary>
        /// 添加水印
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddWater()
        {
            string Message = "";
            if (Request.Files.Count > 0)
            {
                HttpPostedFile file = System.Web.HttpContext.Current.Request.Files[0];
                if (file.ContentLength > 0)
                {
                    //if (file.ContentLength > 1048576)//上传图片大于1M，请重新上传
                    //{
                    //    return Content(new JsonMessage { Success = false, Code = "-1", Message = "上传图片大于1M，请重新上传" }.ToString());
                    //}
                    string fileExt = file.FileName.Substring(file.FileName.LastIndexOf('.'));//后缀
                    try
                    {
                        string fileGuid = Guid.NewGuid().ToString();
                        string uploadDate = DateTime.Now.ToString("yyyyMMdd");
                        string dir = string.Format("/Resource/Poll/{0}/", uploadDate);
                        if (Directory.Exists(Server.MapPath(dir)) == false)//如果不存在就创建file文件夹
                        {
                            Directory.CreateDirectory(Server.MapPath(dir));
                        }
                        string newfileName = DateTime.Now.ToString("HHmmssfff");
                        //原图
                        string fullDir1 = dir + newfileName + fileExt;
                        string imgFilePath = Request.MapPath(fullDir1);
                        file.SaveAs(imgFilePath);

                        return Content(new JsonMessage { Success = true, Code = "0", Message = fullDir1 }.ToString());
                    }
                    catch (Exception ex)
                    {
                        Message = HttpUtility.HtmlEncode(ex.Message);
                        return Content(new JsonMessage { Success = false, Code = "-1", Message = Message }.ToString());
                    }
                }
                return Content(new JsonMessage { Success = false, Code = "-1", Message = "请选择要上传的文件！" }.ToString());
            }
            return Content(new JsonMessage { Success = false, Code = "-1", Message = "请选择要上传的文件！" }.ToString());
        }


        Poll_SignUpBLL signUpBLL = new Poll_SignUpBLL();
        /// <summary>
        /// 提交报名
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveForm(Poll_SignUpEntity entity)
        {
            entity.OpenId = CurrentWxUser.OpenId;
            entity.NickName = CurrentWxUser.NickName;
            entity.HeadimgUrl = CurrentWxUser.HeadimgUrl;
            ////插入报名表
            signUpBLL.SaveForm(null, entity);

            return Content("提交成功！");
        }
        Poll_RecordBLL recordBLL = new Poll_RecordBLL();
        /// <summary>
        /// 投票记录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PollSave(int? id, string name)
        {
            string openid = CurrentWxUser.OpenId;// "oA-EC1Ucth5a3bkvcJSdiTCizz_g";//
            string queryJson = @"{OpenId:'" + openid + "'}";
            var list = recordBLL.GetList(queryJson);
            if (list.Count() > 0)
            {
                return RedirectToAction("Error", new { msg = "对不起，每人每天只能投票一次！" });
            }
            Poll_RecordEntity poll_RecordEntity = new Poll_RecordEntity()
            {
                OpenId = CurrentWxUser.OpenId,//"oA-EC1Ucth5a3bkvcJSdiTCizz_g",//
                NickName = CurrentWxUser.NickName,//"北龙克川",//
                HeadimgUrl = CurrentWxUser.HeadimgUrl,//"http://thirdwx.qlogo.cn/mmopen/vi_32/DYAIOgq83eqIaBiaKiavicQkAILGjtGVDibsmuia9hP1ziaSk0WUHibaRBRib8CITBwsbKib51D2ltNNpGeINFugdUjpeUQ/132",//
                PlayerId = id,
                PlayerName = name
            };
            
            ////插入表
            recordBLL.SaveForm(null, poll_RecordEntity);

            return View("Polled");
        }
        #endregion

        #region 分享代码
        /// <summary>
        /// 分享
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="playerName"></param>
        /// <returns></returns>
        public PartialViewResult _PartialShare(string playerId, string playerName)
        {
            //获得微信基础类
            BaseWxModel baseModel = GetWxModel();
            ShareModel fxModel = new ShareModel()
            {
                appid = baseModel.appid,
                nonce = baseModel.nonce,
                signature = baseModel.signature,
                thisUrl = baseModel.thisUrl,
                timestamp = baseModel.timestamp,
                fxTitle =  "思科莱“生活设计家”设计大赛，【"+ playerName + "】参赛作品,快来帮Ta投一票吧！",
                fxContent = "思科莱，为美好生活而来；全屋定制，让爱如约而至。",
                fxImg = Config.GetValue("Domain") + Url.Content("/Content/images/icon/logo.jpg"),
            };
            ViewBag.playerId = playerId;
            ViewBag.playerName = playerName;
            return PartialView(fxModel);
        }
        Poll_ShareBLL shareBLL = new Poll_ShareBLL();
        /// <summary>
        /// 添加分享记录
        /// </summary>
        /// <param name="shareUrl"></param>
        /// <param name="shareTitle"></param>
        /// <param name="shareContent"></param>
        /// <param name="shareTo"></param>
        /// <param name="playerId"></param>
        /// <param name="playerName"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AddShare(string shareUrl, string shareTitle, string shareContent, string shareTo, int? playerId, string playerName)
        {
            try
            {
                Poll_ShareEntity entity = new Poll_ShareEntity()
                {
                    CreateDate = DateTime.Now,
                    //ShareContent = shareContent,
                    ShareTitle = shareTitle,
                    ShareTo = shareTo,
                    ShareUrl = shareUrl,
                    OpenId = CurrentWxUser.OpenId,
                    NickName = CurrentWxUser.NickName,
                    HeadimgUrl = CurrentWxUser.HeadimgUrl,
                    PlayerId = playerId,
                    PlayerName= playerName
                };
                shareBLL.SaveForm(null, entity);
                return Json(new { iserror = false, message = "分享成功" });
            }
            catch (Exception ex)
            {
                return Json(new { iserror = true, message = ex.Message });
            }
        }
        #endregion

    }
}
