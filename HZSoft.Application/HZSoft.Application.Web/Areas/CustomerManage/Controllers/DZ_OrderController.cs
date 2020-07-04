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
    /// �� �� 6.1
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2019-11-17 08:11
    /// �� ����DZ_Order
    /// </summary>
    public class DZ_OrderController : MvcControllerBase
    {
        private CodeRuleBLL codeRuleBLL = new CodeRuleBLL();
        private DZ_OrderBLL dz_orderbll = new DZ_OrderBLL();
        private FileInfoBLL fileInfoBLL = new FileInfoBLL();
        private DZ_Money_RoomBLL dz_money_roombll = new DZ_Money_RoomBLL();

        #region ��ͼ����
        /// <summary>
        /// �б�ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DZ_OrderIndex()
        {
            return View();
        }
        /// <summary>
        /// ��ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DZ_OrderForm()
        {
            return View();
        }

        /// <summary>
        /// ��ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DZ_OrderDetail()
        {
            return View();
        }
        /// <summary>
        /// �ӵ�
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DZ_OrderIndex1()
        {
            return View();
        }
        /// <summary>
        /// ���۶Ա�
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DZ_OrderIndex12()
        {
            return View();
        }
        /// <summary>
        /// �ӵ�
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
        /// ��ͼ
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
        /// ��ͼ
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DZ_OrderForm2()
        {
            return View();
        }

        /// <summary>
        /// ��
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
        /// ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DZ_OrderForm3()
        {
            return View();
        }


        /// <summary>
        /// ���
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
        /// ���
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DZ_OrderForm4()
        {
            return View();
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DZ_OrderIndex5()
        {
            return View();
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DZ_OrderIndex5Money()
        {
            return View();
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DZ_OrderForm5()
        {
            return View();
        }
        /// <summary>
        /// ������ϸҳ
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult MoneyDetail()
        {
            return View();
        }
        /// <summary>
        /// ���۱༭
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult MoneyForm()
        {
            return View();
        }

        /// <summary>
        /// ������ˣ��������ģ�
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult MoneyOkIndex()
        {
            return View();
        }
        /// <summary>
        /// ���������ɼ�¼���������ģ�
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult MoneyOkOverIndex()
        {
            return View();
        }
        

        /// <summary>
        /// ������ˣ��������ģ�
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult MoneyOkForm()
        {
            return View();
        }
        /// <summary>
        /// �������2���������ģ�
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult MoneyOkCheckForm()
        {
            return View();
        }

        /// <summary>
        /// ǩ��ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SignedForm()
        {
            return View();
        }

        /// <summary>
        /// ����ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult BackForm()
        {
            return View();
        }


        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OverIndex()
        {
            return View();
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DeleteIndex()
        {
            return View();
        }
        #endregion

        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ����</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�Json</returns>
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
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = dz_orderbll.GetList(queryJson);
            return ToJsonResult(data);
        }
        /// <summary>
        /// ��ȡʵ�� 
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns>���ض���Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = dz_orderbll.GetEntity(keyValue);
            return ToJsonResult(data);
        }

        /// <summary>
        /// ��ȡʵ�� ����ϵͳ
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns>���ض���Json</returns>
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

        #region �ύ����
        /// <summary>
        /// һ������
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveBackForm(string keyValue, DZ_OrderEntity entity)
        {
            dz_orderbll.BackForm(keyValue, entity);
            return Success("�����ɹ���");
        }
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult RemoveForm(string keyValue)
        {
            dz_orderbll.RemoveForm(keyValue);
            return Success("ɾ���ɹ���");
        }
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, DZ_OrderEntity entity)
        {
            dz_orderbll.SaveForm(keyValue, entity);
            return Success("�����ɹ���");
        }

        /// <summary>
        /// �������༭
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="strEntity">ʵ�����</param>
        /// <param name="strChildEntitys">ʵ�����</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveMoneyForm(string keyValue, string strEntity, string strChildEntitys)
        {
            var entity = strEntity.ToObject<DZ_OrderEntity>();
            var childEntitys = strChildEntitys.ToList<DZ_Money_ItemEntity>();
            dz_orderbll.SaveMoneyForm(keyValue, entity, childEntitys);
            return Success("�����ɹ���");
        }

        /// <summary>
        /// ǩ��ȷ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveSigned(string keyValue, DZ_OrderEntity entity)
        {
            dz_orderbll.SaveSigned(keyValue, entity);
            return Success("�����ɹ���");
        }


        /// <summary>
        /// �������
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public ActionResult UpdateMoneyOkState(string keyValue,int? state)
        {
            dz_orderbll.UpdateMoneyOkState(keyValue, state);
            return Success("�����ɹ���");
        }
        /// <summary>
        /// ���
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public ActionResult UpdateOverState(string keyValue, int? state)
        {
            dz_orderbll.UpdateOverState(keyValue, state);
            return Success("�����ɹ���");
        }
        #endregion



        /// <summary>
        /// �ļ��ϴ�
        /// </summary>
        /// <returns></returns>
        public ActionResult UploadFile(string folderId)
        {

            try
            {
                var file = Request.Files[0]; //��ȡѡ���ļ�  
                var filecombin = file.FileName.Split('.');
                if (file == null || string.IsNullOrEmpty(file.FileName) || file.ContentLength == 0 || filecombin.Length < 2)
                {
                    return Json(new
                    {
                        fileid = 0,
                        src = "",
                        name = "",
                        msg = "�ϴ����� �����ļ��� �� �ļ�����"
                    });
                }


                //��ȡ�ļ������ļ���(��������·��)
                //�ļ����·����ʽ��/Resource/ResourceFile/{userId}{data}/{guid}.{��׺��}
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
                //�ж��Ƿ������ͬ�ļ������ļ� ��ͬ�ۼ�1�����ж�
                while (System.IO.File.Exists(fullFileName))
                {
                    addFileName = fileName + ++tmpIndex;
                    virtualPath = string.Format("~/Resource/DocumentFile/{0}/{1}/{2}{3}", account, uploadDate, addFileName, FileEextension);
                    fullFileName = this.Server.MapPath(virtualPath);
                }

                //�����ļ���
                string path = Path.GetDirectoryName(fullFileName);
                Directory.CreateDirectory(path);
                if (!System.IO.File.Exists(fullFileName))
                {
                    //�����ļ�
                    file.SaveAs(fullFileName);
                    //�ļ���Ϣд�����ݿ�
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
                //return Success("�ϴ��ɹ���");

                ////���屾��·��λ��
                //string local = "Upload\\Contract";
                //string filePathName = string.Empty;
                //string localPath = Path.Combine(HttpRuntime.AppDomainAppPath, local);

                //var tmpName = Server.MapPath("~/Upload/Contract/");
                //var tmp = file.FileName;
                //var tmpIndex = 0;
                ////�ж��Ƿ������ͬ�ļ������ļ� ��ͬ�ۼ�1�����ж�
                //while (System.IO.File.Exists(tmpName + tmp))
                //{
                //    tmp = filecombin[0] + "_" + ++tmpIndex + "." + filecombin[1];
                //}

                ////����·���������ļ���
                //filePathName = tmp;

                //if (!System.IO.Directory.Exists(localPath))
                //    System.IO.Directory.CreateDirectory(localPath);
                //string localURL = Path.Combine(local, filePathName);
                //file.SaveAs(Path.Combine(localPath, filePathName));   //����ͼƬ���ļ��У�
                return Json(new
                {
                    src = virtualPath.Trim().Replace("~", ""),
                    name = addFileName + FileEextension,   // ��ȡ�ļ���������׺��
                    msg = "�ϴ��ɹ�"
                });
            }
            catch { }
            return Json(new
            {
                src = "",
                name = "",   // ��ȡ�ļ���������׺��
                msg = "�ϴ�����"
            });
        }


        public ActionResult RecoveryKPJD(string folderId)
        {
            HttpContext context = System.Web.HttpContext.Current;
            context.Response.ContentType = "text/plain";
            //��������˷�Ƭ
            if (context.Request.Form.AllKeys.Any(m => m == "page"))
            {
                //ȡ��chunk��chunks
                int page = Convert.ToInt32(context.Request.Form["page"]);//��ǰ��Ƭ���ϴ���Ƭ�е�˳�򣨴�0��ʼ��
                int totalPage = Convert.ToInt32(context.Request.Form["totalPage"]);//�ܷ�Ƭ��
                string fileName = context.Request.Form["fileName"].Trim();
                fileName=fileName.Replace("#", "��").Replace(" ", "");//ȥ���ļ�������Ŀո��滻#��
                string fileExt = context.Request.Form["fileExt"];

                //����GUID�����ø�GUID��������ʱ�ļ���
                string account = OperatorProvider.Provider.Current().Account;
                string uploadDate = DateTime.Now.ToString("yyyyMMdd");

                string virtualFilePath = string.Format("~/Resource/DocumentFile/{0}/{1}/{2}.{3}", account, uploadDate, fileName, fileExt);//������ʱ�ļ�
                string filePath = Server.MapPath(virtualFilePath);

                string virtualfolderPath = string.Format("~/Resource/DocumentFile/{0}/{1}/{2}/", account, uploadDate, fileName);//������ʱ�ļ���
                string folderPath = Server.MapPath(virtualfolderPath);
                string path = folderPath + page;

                //������ʱ�����ļ���
                if (!Directory.Exists(Path.GetDirectoryName(folderPath)))
                {
                    Directory.CreateDirectory(folderPath);
                }
                //�����ļ���ɾ��
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                FileStream addFile = new FileStream(path, FileMode.Append, FileAccess.Write);
                BinaryWriter AddWriter = new BinaryWriter(addFile);
                //����ϴ��ķ�Ƭ������
                HttpPostedFile file = context.Request.Files[0];
                Stream stream = file.InputStream;

                BinaryReader TempReader = new BinaryReader(stream);
                //���ϴ��ķ�Ƭ׷�ӵ���ʱ�ļ�ĩβ
                AddWriter.Write(TempReader.ReadBytes((int)stream.Length));
                //�ر�BinaryReader�ļ��Ķ���
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
                    //�ϲ��ļ�
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
            else//û�з�Ƭֱ�ӱ���
            {
                return Json(new
                {
                    status = -1
                });
            }
        }


        /// <summary>
        /// �ϲ�
        /// </summary>
        /// <param name="folderPath">Դ�����ļ���</param>
        /// <param name="filePath">�ϲ�����ļ�</param>
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

                    //����ϴ��ķ�Ƭ������
                    Stream stream = file.Open(FileMode.Open);
                    BinaryReader TempReader = new BinaryReader(stream);
                    //���ϴ��ķ�Ƭ׷�ӵ���ʱ�ļ�ĩβ
                    AddWriter.Write(TempReader.ReadBytes((int)stream.Length));


                    //�ر�BinaryReader�ļ��Ķ���
                    TempReader.Close();
                    stream.Close();
                    AddWriter.Close();
                    addFile.Close();

                    TempReader.Dispose();
                    stream.Dispose();
                    AddWriter.Dispose();
                    addFile.Dispose();

                    //ɾ����ʱ�ļ�
                    System.IO.File.Delete(file.FullName);
                }
                //ɾ����ʱ�ļ���
                Directory.Delete(folderPath, true);
            }
        }

        /// <summary>
        /// ����·��ɾ���ļ�
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
        /// �ϴ���ƷͼƬ
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
                    string fileExt = file.FileName.Substring(file.FileName.LastIndexOf('.'));//��׺
                    try
                    {
                        string uploadDate = OperatorProvider.Provider.Current().Account;
                        string dir = string.Format("/Resource/DocumentFile/Product/{0}/", uploadDate);
                        if (Directory.Exists(Server.MapPath(dir)) == false)//��������ھʹ���file�ļ���
                        {
                            Directory.CreateDirectory(Server.MapPath(dir));
                        }
                        string newfileName = DateTime.Now.ToString("yyyyMMddHHmmss");
                        //ԭͼ
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
                Message = "��ѡ��Ҫ�ϴ����ļ���";
                return Content(new JsonMessage { Success = false, Code = "-1", Message = Message }.ToString());
            }
            Message = "��ѡ��Ҫ�ϴ����ļ���";
            return Content(new JsonMessage { Success = false, Code = "-1", Message = Message }.ToString());
        }


        /// <summary>
        /// �ϴ�ǩ�յ�
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
                    string fileExt = file.FileName.Substring(file.FileName.LastIndexOf('.'));//��׺
                    try
                    {
                        string uploadDate = OperatorProvider.Provider.Current().Account;
                        string dir = string.Format("/Resource/DocumentFile/Signed/{0}/", uploadDate);
                        if (Directory.Exists(Server.MapPath(dir)) == false)//��������ھʹ���file�ļ���
                        {
                            Directory.CreateDirectory(Server.MapPath(dir));
                        }
                        string newfileName = DateTime.Now.ToString("yyyyMMddHHmmss");
                        //ԭͼ
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
                Message = "��ѡ��Ҫ�ϴ����ļ���";
                return Content(new JsonMessage { Success = false, Code = "-1", Message = Message }.ToString());
            }
            Message = "��ѡ��Ҫ�ϴ����ļ���";
            return Content(new JsonMessage { Success = false, Code = "-1", Message = Message }.ToString());
        }


        /// <summary>
        /// �������ֱ���
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
                ViewBag.error = "������Ų���Ϊ�գ�";
                return View();
            }
            else
            {
                ViewBag.OrderId = keyValue;
            }


            if (file == null || file.ContentLength <= 0)
            {
                ViewBag.error = "�ļ�����Ϊ��";
                return View();
            }
            else
            {
                string filename = Path.GetFileName(file.FileName);
                int filesize = file.ContentLength;//��ȡ�ϴ��ļ��Ĵ�С��λΪ�ֽ�byte
                string fileExt = System.IO.Path.GetExtension(filename);//��ȡ�ϴ��ļ�����չ��
                string NoFileName = System.IO.Path.GetFileNameWithoutExtension(filename);//��ȡ����չ�����ļ���
                int Maxsize = 4000 * 1024;//�����ϴ��ļ������ռ��СΪ4M
                string FileType = ".xls,.xlsx";//�����ϴ��ļ��������ַ���

                if (!FileType.Contains(fileExt))
                {
                    ViewBag.error = "�ļ����Ͳ��ԣ�ֻ�ܵ���xls��xlsx��ʽ���ļ�";
                    return View();
                }
                if (filesize >= Maxsize)
                {
                    ViewBag.error = "�ϴ��ļ�����4M�������ϴ�";
                    return View();
                }
                
                string dir = string.Format("/Resource/KuJiaLe/{0}/", DateTime.Now.ToString("yyyyMMdd"));
                if (Directory.Exists(Server.MapPath(dir)) == false)//��������ھʹ���file�ļ���
                {
                    Directory.CreateDirectory(Server.MapPath(dir));
                }
                string newfileName = NoFileName + DateTime.Now.ToString("yyyyMMddhhmmss") + fileExt; ;
                //ԭͼ
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
        /// ����1010
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
                ViewBag.error = "������Ų���Ϊ�գ�";
                return View();
            }
            else
            {
                ViewBag.OrderId = keyValue;
            }
            if (file == null || file.ContentLength <= 0)
            {
                ViewBag.error = "�ļ�����Ϊ��";
                return View();
            }
            else
            {
                string filename = Path.GetFileName(file.FileName);
                int filesize = file.ContentLength;//��ȡ�ϴ��ļ��Ĵ�С��λΪ�ֽ�byte
                string fileExt = System.IO.Path.GetExtension(filename);//��ȡ�ϴ��ļ�����չ��
                string NoFileName = System.IO.Path.GetFileNameWithoutExtension(filename);//��ȡ����չ�����ļ���
                int Maxsize = 4000 * 1024;//�����ϴ��ļ������ռ��СΪ4M
                string FileType = ".xls,.xlsx";//�����ϴ��ļ��������ַ���

                if (!FileType.Contains(fileExt))
                {
                    ViewBag.error = "�ļ����Ͳ��ԣ�ֻ�ܵ���xls��xlsx��ʽ���ļ�";
                    return View();
                }
                if (filesize >= Maxsize)
                {
                    ViewBag.error = "�ϴ��ļ�����4M�������ϴ�";
                    return View();
                }

                string dir = string.Format("/Resource/1010/{0}/", DateTime.Now.ToString("yyyyMMdd"));
                if (Directory.Exists(Server.MapPath(dir)) == false)//��������ھʹ���file�ļ���
                {
                    Directory.CreateDirectory(Server.MapPath(dir));
                }
                string newfileName = NoFileName + DateTime.Now.ToString("yyyyMMddhhmmss") + fileExt; ;
                //ԭͼ
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
