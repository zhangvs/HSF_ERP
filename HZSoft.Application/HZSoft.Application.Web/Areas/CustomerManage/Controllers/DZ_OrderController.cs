using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.Busines.CustomerManage;
using HZSoft.Util;
using HZSoft.Util.WebControl;
using System.Web.Mvc;
using System.IO;
using System.Web;
using HZSoft.Application.Code;
using System;
using HZSoft.Application.Entity.PublicInfoManage;
using HZSoft.Application.Busines.PublicInfoManage;
using System.Collections.Generic;
using System.Linq;
using HZSoft.Application.Busines.SystemManage;
using System.Data;
using HZSoft.Util.Offices;

namespace HZSoft.Application.Web.Areas.CustomerManage.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// 
    /// 创 建：超级管理员
    /// 日 期：2019-11-17 08:11
    /// 描 述：DZ_Order
    /// </summary>
    public class DZ_OrderController : MvcControllerBase
    {
        private CodeRuleBLL codeRuleBLL = new CodeRuleBLL();
        private DZ_OrderBLL dz_orderbll = new DZ_OrderBLL();
        private FileInfoBLL fileInfoBLL = new FileInfoBLL();
        private DZ_Money_RoomBLL dz_money_roombll = new DZ_Money_RoomBLL();

        #region 视图功能
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DZ_OrderIndex()
        {
            return View();
        }
        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DZ_OrderForm()
        {
            return View();
        }

        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DZ_OrderDetail()
        {
            return View();
        }
        /// <summary>
        /// 接单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DZ_OrderIndex1()
        {
            return View();
        }
        /// <summary>
        /// 报价对比
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DZ_OrderIndex12()
        {
            return View();
        }
        /// <summary>
        /// 接单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DZ_OrderForm1()
        {
            if (Request["keyValue"] == null)
            {
                //ViewBag.OrderCode = codeRuleBLL.GetBillCode(SystemInfo.CurrentModuleId);
                ViewBag.OrderCode = codeRuleBLL.GetBillCode(SystemInfo.CurrentUserId, "", ((int)CodeRuleEnum.DZ_Order).ToString());
            }
            return View();
        }


        /// <summary>
        /// 审图
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DZ_OrderIndex2()
        {
            return View();
        }
        public ActionResult DZ_OrderIndex2Tu()
        {
            return View();
        }
        /// <summary>
        /// 审图
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DZ_OrderForm2()
        {
            return View();
        }

        /// <summary>
        /// 拆单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DZ_OrderIndex3()
        {
            return View();
        }
        public ActionResult DZ_OrderIndex3Chai()
        {
            return View();
        }
        /// <summary>
        /// 拆单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DZ_OrderForm3()
        {
            return View();
        }


        /// <summary>
        /// 审核
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DZ_OrderIndex4()
        {
            return View();
        }
        public ActionResult DZ_OrderIndex4Check()
        {
            return View();
        }
        /// <summary>
        /// 审核
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DZ_OrderForm4()
        {
            return View();
        }

        /// <summary>
        /// 报价
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DZ_OrderIndex5()
        {
            return View();
        }
        /// <summary>
        /// 报价
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DZ_OrderIndex5Money()
        {
            return View();
        }

        /// <summary>
        /// 报价
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DZ_OrderForm5()
        {
            return View();
        }
        /// <summary>
        /// 报价详细页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult MoneyDetail()
        {
            return View();
        }
        /// <summary>
        /// 报价编辑
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult MoneyForm()
        {
            return View();
        }

        /// <summary>
        /// 报价审核（财务中心）
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult MoneyOkIndex()
        {
            return View();
        }
        /// <summary>
        /// 报价审核完成记录（财务中心）
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult MoneyOkOverIndex()
        {
            return View();
        }
        

        /// <summary>
        /// 报价审核（财务中心）
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult MoneyOkForm()
        {
            return View();
        }
        /// <summary>
        /// 报价审核2（财务中心）
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult MoneyOkCheckForm()
        {
            return View();
        }

        /// <summary>
        /// 签收页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SignedForm()
        {
            return View();
        }

        /// <summary>
        /// 撤单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult BackForm()
        {
            return View();
        }


        /// <summary>
        /// 结束
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OverIndex()
        {
            return View();
        }

        /// <summary>
        /// 作废
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DeleteIndex()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = dz_orderbll.GetPageList(pagination, queryJson);
            var jsonData = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return ToJsonResult(jsonData);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = dz_orderbll.GetList(queryJson);
            return ToJsonResult(data);
        }
        /// <summary>
        /// 获取实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = dz_orderbll.GetEntity(keyValue);
            return ToJsonResult(data);
        }

        /// <summary>
        /// 获取实体 报价系统
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetMoneyFormJson(string keyValue)
        {
            var data = dz_orderbll.GetEntity(keyValue);
            var roomdata = dz_money_roombll.GetRoomDetails(keyValue);
            var roomItemData = dz_money_roombll.GetRoomItemDetails(keyValue);
            var jsonData = new { entity = data, roomEntity = roomdata, childEntity = roomItemData };
            return ToJsonResult(jsonData);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 一键撤销
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveBackForm(string keyValue, DZ_OrderEntity entity)
        {
            dz_orderbll.BackForm(keyValue, entity);
            return Success("撤销成功。");
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult RemoveForm(string keyValue)
        {
            dz_orderbll.RemoveForm(keyValue);
            return Success("删除成功。");
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, DZ_OrderEntity entity)
        {
            dz_orderbll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }

        /// <summary>
        /// 生产单编辑
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="strEntity">实体对象</param>
        /// <param name="strChildEntitys">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveMoneyForm(string keyValue, string strEntity, string strChildEntitys)
        {
            var entity = strEntity.ToObject<DZ_OrderEntity>();
            var childEntitys = strChildEntitys.ToList<DZ_Money_ItemEntity>();
            dz_orderbll.SaveMoneyForm(keyValue, entity, childEntitys);
            return Success("操作成功。");
        }

        /// <summary>
        /// 签收确认
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveSigned(string keyValue, DZ_OrderEntity entity)
        {
            dz_orderbll.SaveSigned(keyValue, entity);
            return Success("操作成功。");
        }


        /// <summary>
        /// 报价审核
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ActionResult UpdateMoneyOkState(string keyValue,int? state)
        {
            dz_orderbll.UpdateMoneyOkState(keyValue, state);
            return Success("操作成功。");
        }
        /// <summary>
        /// 完成
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ActionResult UpdateOverState(string keyValue, int? state)
        {
            dz_orderbll.UpdateOverState(keyValue, state);
            return Success("操作成功。");
        }
        #endregion



        /// <summary>
        /// 文件上传
        /// </summary>
        /// <returns></returns>
        public ActionResult UploadFile(string folderId)
        {

            try
            {
                var file = Request.Files[0]; //获取选中文件  
                var filecombin = file.FileName.Split('.');
                if (file == null || string.IsNullOrEmpty(file.FileName) || file.ContentLength == 0 || filecombin.Length < 2)
                {
                    return Json(new
                    {
                        fileid = 0,
                        src = "",
                        name = "",
                        msg = "上传出错 请检查文件名 或 文件内容"
                    });
                }


                //获取文件完整文件名(包含绝对路径)
                //文件存放路径格式：/Resource/ResourceFile/{userId}{data}/{guid}.{后缀名}
                string account = OperatorProvider.Provider.Current().Account;
                string fileGuid = file.FileName;//Guid.NewGuid().ToString()
                long filesize = file.ContentLength;
                string FileEextension = Path.GetExtension(file.FileName);
                string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                string uploadDate = DateTime.Now.ToString("yyyyMMdd");
                string virtualPath = string.Format("~/Resource/DocumentFile/{0}/{1}/{2}{3}", account, uploadDate, fileName, FileEextension);
                string fullFileName = this.Server.MapPath(virtualPath);

                var tmpIndex = 0;
                var addFileName = fileName;
                //判断是否存在相同文件名的文件 相同累加1继续判断
                while (System.IO.File.Exists(fullFileName))
                {
                    addFileName = fileName + ++tmpIndex;
                    virtualPath = string.Format("~/Resource/DocumentFile/{0}/{1}/{2}{3}", account, uploadDate, addFileName, FileEextension);
                    fullFileName = this.Server.MapPath(virtualPath);
                }

                //创建文件夹
                string path = Path.GetDirectoryName(fullFileName);
                Directory.CreateDirectory(path);
                if (!System.IO.File.Exists(fullFileName))
                {
                    //保存文件
                    file.SaveAs(fullFileName);
                    //文件信息写入数据库
                    FileInfoEntity fileInfoEntity = new FileInfoEntity();
                    fileInfoEntity.Create();
                    //fileInfoEntity.FileId = fileGuid;
                    if (!string.IsNullOrEmpty(folderId))
                    {
                        fileInfoEntity.FolderId = folderId;
                    }
                    else
                    {
                        fileInfoEntity.FolderId = "0";
                    }
                    fileInfoEntity.FileName = addFileName + FileEextension;
                    fileInfoEntity.FilePath = virtualPath;
                    fileInfoEntity.FileSize = filesize.ToString();
                    fileInfoEntity.FileExtensions = FileEextension;
                    fileInfoEntity.FileType = FileEextension.Replace(".", "");
                    fileInfoBLL.SaveForm("", fileInfoEntity);
                }
                //return Success("上传成功。");

                ////定义本地路径位置
                //string local = "Upload\\Contract";
                //string filePathName = string.Empty;
                //string localPath = Path.Combine(HttpRuntime.AppDomainAppPath, local);

                //var tmpName = Server.MapPath("~/Upload/Contract/");
                //var tmp = file.FileName;
                //var tmpIndex = 0;
                ////判断是否存在相同文件名的文件 相同累加1继续判断
                //while (System.IO.File.Exists(tmpName + tmp))
                //{
                //    tmp = filecombin[0] + "_" + ++tmpIndex + "." + filecombin[1];
                //}

                ////不带路径的最终文件名
                //filePathName = tmp;

                //if (!System.IO.Directory.Exists(localPath))
                //    System.IO.Directory.CreateDirectory(localPath);
                //string localURL = Path.Combine(local, filePathName);
                //file.SaveAs(Path.Combine(localPath, filePathName));   //保存图片（文件夹）
                return Json(new
                {
                    src = virtualPath.Trim().Replace("~", ""),
                    name = addFileName + FileEextension,   // 获取文件名不含后缀名
                    msg = "上传成功"
                });
            }
            catch { }
            return Json(new
            {
                src = "",
                name = "",   // 获取文件名不含后缀名
                msg = "上传出错"
            });
        }


        public ActionResult RecoveryKPJD(string folderId)
        {
            HttpContext context = System.Web.HttpContext.Current;
            context.Response.ContentType = "text/plain";
            //如果进行了分片
            if (context.Request.Form.AllKeys.Any(m => m == "page"))
            {
                //取得chunk和chunks
                int page = Convert.ToInt32(context.Request.Form["page"]);//当前分片在上传分片中的顺序（从0开始）
                int totalPage = Convert.ToInt32(context.Request.Form["totalPage"]);//总分片数
                string fileName = context.Request.Form["fileName"].Trim();
                fileName=fileName.Replace("#", "＃").Replace(" ", "");//去掉文件名里面的空格，替换#号
                string fileExt = context.Request.Form["fileExt"];

                //根据GUID创建用该GUID命名的临时文件夹
                string account = OperatorProvider.Provider.Current().Account;
                string uploadDate = DateTime.Now.ToString("yyyyMMdd");

                string virtualFilePath = string.Format("~/Resource/DocumentFile/{0}/{1}/{2}.{3}", account, uploadDate, fileName, fileExt);//虚拟临时文件
                string filePath = Server.MapPath(virtualFilePath);

                string virtualfolderPath = string.Format("~/Resource/DocumentFile/{0}/{1}/{2}/", account, uploadDate, fileName);//虚拟临时文件夹
                string folderPath = Server.MapPath(virtualfolderPath);
                string path = folderPath + page;

                //建立临时传输文件夹
                if (!Directory.Exists(Path.GetDirectoryName(folderPath)))
                {
                    Directory.CreateDirectory(folderPath);
                }
                //存在文件先删除
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                FileStream addFile = new FileStream(path, FileMode.Append, FileAccess.Write);
                BinaryWriter AddWriter = new BinaryWriter(addFile);
                //获得上传的分片数据流
                HttpPostedFile file = context.Request.Files[0];
                Stream stream = file.InputStream;

                BinaryReader TempReader = new BinaryReader(stream);
                //将上传的分片追加到临时文件末尾
                AddWriter.Write(TempReader.ReadBytes((int)stream.Length));
                //关闭BinaryReader文件阅读器
                TempReader.Close();
                stream.Close();
                AddWriter.Close();
                addFile.Close();

                TempReader.Dispose();
                stream.Dispose();
                AddWriter.Dispose();
                addFile.Dispose();
                if (page == totalPage)
                {
                    //合并文件
                    ProcessRequest(folderPath, filePath);

                    return Json(new
                    {
                        downUrl = virtualFilePath.Trim().Replace("~", ""),
                        status = 2
                    });
                }
                else
                {
                    return Json(new
                    {
                        status = 1
                    });
                }
            }
            else//没有分片直接保存
            {
                return Json(new
                {
                    status = -1
                });
            }
        }


        /// <summary>
        /// 合并
        /// </summary>
        /// <param name="folderPath">源数据文件夹</param>
        /// <param name="filePath">合并后的文件</param>
        private void ProcessRequest(string folderPath, string filePath)
        {
            HttpContext context = System.Web.HttpContext.Current;
            context.Response.ContentType = "text/plain";

            DirectoryInfo dicInfo = new DirectoryInfo(folderPath);
            if (Directory.Exists(Path.GetDirectoryName(folderPath)))
            {
                FileInfo[] files = dicInfo.GetFiles();
                foreach (FileInfo file in files.OrderBy(f => int.Parse(f.Name)))
                {
                    FileStream addFile = new FileStream(filePath, FileMode.Append);
                    BinaryWriter AddWriter = new BinaryWriter(addFile);

                    //获得上传的分片数据流
                    Stream stream = file.Open(FileMode.Open);
                    BinaryReader TempReader = new BinaryReader(stream);
                    //将上传的分片追加到临时文件末尾
                    AddWriter.Write(TempReader.ReadBytes((int)stream.Length));


                    //关闭BinaryReader文件阅读器
                    TempReader.Close();
                    stream.Close();
                    AddWriter.Close();
                    addFile.Close();

                    TempReader.Dispose();
                    stream.Dispose();
                    AddWriter.Dispose();
                    addFile.Dispose();

                    //删除临时文件
                    System.IO.File.Delete(file.FullName);
                }
                //删除临时文件夹
                Directory.Delete(folderPath, true);
            }
        }

        /// <summary>
        /// 根据路径删除文件
        /// </summary>
        /// <param name="path"></param>
        public void DeleteFile(string path)
        {
            FileAttributes attr = System.IO.File.GetAttributes(path);
            if (attr == FileAttributes.Directory)
            {
                Directory.Delete(path, true);
            }
            else
            {
                System.IO.File.Delete(path);
            }
        }



        /// <summary>
        /// 上传产品图片
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadProductPicture()
        {
            string Message = "";
            if (Request.Files.Count > 0)
            {
                HttpPostedFile file = System.Web.HttpContext.Current.Request.Files[0];
                if (file.ContentLength > 0)
                {
                    string fileExt = file.FileName.Substring(file.FileName.LastIndexOf('.'));//后缀
                    try
                    {
                        string uploadDate = OperatorProvider.Provider.Current().Account;
                        string dir = string.Format("/Resource/DocumentFile/Product/{0}/", uploadDate);
                        if (Directory.Exists(Server.MapPath(dir)) == false)//如果不存在就创建file文件夹
                        {
                            Directory.CreateDirectory(Server.MapPath(dir));
                        }
                        string newfileName = DateTime.Now.ToString("yyyyMMddHHmmss");
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
                Message = "请选择要上传的文件！";
                return Content(new JsonMessage { Success = false, Code = "-1", Message = Message }.ToString());
            }
            Message = "请选择要上传的文件！";
            return Content(new JsonMessage { Success = false, Code = "-1", Message = Message }.ToString());
        }


        /// <summary>
        /// 上传签收单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadPicture()
        {
            string Message = "";
            if (Request.Files.Count > 0)
            {
                HttpPostedFile file = System.Web.HttpContext.Current.Request.Files[0];
                if (file.ContentLength > 0)
                {
                    string fileExt = file.FileName.Substring(file.FileName.LastIndexOf('.'));//后缀
                    try
                    {
                        string uploadDate = OperatorProvider.Provider.Current().Account;
                        string dir = string.Format("/Resource/DocumentFile/Signed/{0}/", uploadDate);
                        if (Directory.Exists(Server.MapPath(dir)) == false)//如果不存在就创建file文件夹
                        {
                            Directory.CreateDirectory(Server.MapPath(dir));
                        }
                        string newfileName = DateTime.Now.ToString("yyyyMMddHHmmss");
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
                Message = "请选择要上传的文件！";
                return Content(new JsonMessage { Success = false, Code = "-1", Message = Message }.ToString());
            }
            Message = "请选择要上传的文件！";
            return Content(new JsonMessage { Success = false, Code = "-1", Message = Message }.ToString());
        }


        /// <summary>
        /// 导入酷家乐报价
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ImportKuJiaLe(string keyValue)
        {
            ViewBag.OrderId = keyValue;
            return View();
        }

        [HttpPost]
        public ActionResult ImportKuJiaLe(HttpPostedFileBase filebase, string keyValue)
        {
            HttpPostedFileBase file = Request.Files["files"];
            if (string.IsNullOrEmpty(keyValue))
            {
                ViewBag.error = "订单编号不能为空！";
                return View();
            }
            else
            {
                ViewBag.OrderId = keyValue;
            }


            if (file == null || file.ContentLength <= 0)
            {
                ViewBag.error = "文件不能为空";
                return View();
            }
            else
            {
                string filename = Path.GetFileName(file.FileName);
                int filesize = file.ContentLength;//获取上传文件的大小单位为字节byte
                string fileExt = System.IO.Path.GetExtension(filename);//获取上传文件的扩展名
                string NoFileName = System.IO.Path.GetFileNameWithoutExtension(filename);//获取无扩展名的文件名
                int Maxsize = 4000 * 1024;//定义上传文件的最大空间大小为4M
                string FileType = ".xls,.xlsx";//定义上传文件的类型字符串

                if (!FileType.Contains(fileExt))
                {
                    ViewBag.error = "文件类型不对，只能导入xls和xlsx格式的文件";
                    return View();
                }
                if (filesize >= Maxsize)
                {
                    ViewBag.error = "上传文件超过4M，不能上传";
                    return View();
                }
                
                string dir = string.Format("/Resource/KuJiaLe/{0}/", DateTime.Now.ToString("yyyyMMdd"));
                if (Directory.Exists(Server.MapPath(dir)) == false)//如果不存在就创建file文件夹
                {
                    Directory.CreateDirectory(Server.MapPath(dir));
                }
                string newfileName = NoFileName + DateTime.Now.ToString("yyyyMMddhhmmss") + fileExt; ;
                //原图
                string fullDir1 = dir + newfileName;
                string savePath = Request.MapPath(fullDir1);
                file.SaveAs(savePath);
                DataTable dtSource = ExcelHelper.ExcelImport(savePath);
                ViewBag.error = dz_orderbll.BatchAddEntity(keyValue, dtSource, fullDir1);

                System.Threading.Thread.Sleep(2000);
            }
            
            return View();
        }


        /// <summary>
        /// 导入1010
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Import1010(string keyValue)
        {
            ViewBag.OrderId = keyValue;
            return View();
        }
        [HttpPost]
        public ActionResult Import1010(HttpPostedFileBase filebase, string keyValue)
        {
            HttpPostedFileBase file = Request.Files["files"];
            if (string.IsNullOrEmpty(keyValue))
            {
                ViewBag.error = "订单编号不能为空！";
                return View();
            }
            else
            {
                ViewBag.OrderId = keyValue;
            }
            if (file == null || file.ContentLength <= 0)
            {
                ViewBag.error = "文件不能为空";
                return View();
            }
            else
            {
                string filename = Path.GetFileName(file.FileName);
                int filesize = file.ContentLength;//获取上传文件的大小单位为字节byte
                string fileExt = System.IO.Path.GetExtension(filename);//获取上传文件的扩展名
                string NoFileName = System.IO.Path.GetFileNameWithoutExtension(filename);//获取无扩展名的文件名
                int Maxsize = 4000 * 1024;//定义上传文件的最大空间大小为4M
                string FileType = ".xls,.xlsx";//定义上传文件的类型字符串

                if (!FileType.Contains(fileExt))
                {
                    ViewBag.error = "文件类型不对，只能导入xls和xlsx格式的文件";
                    return View();
                }
                if (filesize >= Maxsize)
                {
                    ViewBag.error = "上传文件超过4M，不能上传";
                    return View();
                }

                string dir = string.Format("/Resource/1010/{0}/", DateTime.Now.ToString("yyyyMMdd"));
                if (Directory.Exists(Server.MapPath(dir)) == false)//如果不存在就创建file文件夹
                {
                    Directory.CreateDirectory(Server.MapPath(dir));
                }
                string newfileName = NoFileName + DateTime.Now.ToString("yyyyMMddhhmmss") + fileExt; ;
                //原图
                string fullDir1 = dir + newfileName;
                string savePath = Request.MapPath(fullDir1);
                file.SaveAs(savePath);
                DataTable dtSource = ExcelHelper.ExcelImport(savePath);
                ViewBag.error = dz_orderbll.BatchAddEntity1010(keyValue, dtSource, fullDir1);

                System.Threading.Thread.Sleep(2000);
            }
            return View();
        }
    }
}
