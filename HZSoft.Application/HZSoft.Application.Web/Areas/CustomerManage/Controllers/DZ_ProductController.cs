using HZSoft.Util;
using HZSoft.Util.WebControl;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;
using HZSoft.Application.Busines.CustomerManage;
using HZSoft.Application.Entity.CustomerManage;
using System.Web;
using HZSoft.Application.Code;
using System.IO;
using System;
using System.Data;
using HZSoft.Util.Offices;

namespace HZSoft.Application.Web.Areas.CustomerManage.Controllers
{
    /// <summary>
    /// �� �� 6.1
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2017-12-15 15:02
    /// �� ������Ʒ��
    /// </summary>
    public class DZ_ProductController : MvcControllerBase
    {
        private DZ_ProductBLL productbll = new DZ_ProductBLL();

        #region ��ͼ����
        /// <summary>
        /// ����+����
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DZ_ProductIndex()
        {
            return View();
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public ActionResult DZ_ProductTypeIndex()
        {
            return View();
        }
        /// <summary>
        /// ��ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DZ_ProductTypeForm()
        {
            return View();
        }
        /// <summary>
        /// ��ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DZ_ProductForm()
        {
            return View();
        }
        /// <summary>
        /// ��ϸҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DZ_ProductDetail()
        {
            return View();
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DZ_ProductImport()
        {
            return View();
        }
        #endregion

        #region ��ȡ����
        /// <summary>
        /// �����б� 
        /// </summary>
        /// <param name="keyword">�ؼ��ֲ�ѯ</param>
        /// <returns>��������Json</returns>
        [HttpGet]
        public ActionResult GetTreeJson(string keyword)
        {
            var data = productbll.GetList("{\"IsTree\":\"1\"}").OrderBy(t => t.SortCode).ToList();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.TreeWhere(t => t.Name.Contains(keyword), "");
            }
            var treeList = new List<TreeEntity>();
            foreach (DZ_ProductEntity item in data)
            {
                TreeEntity tree = new TreeEntity();
                bool hasChildren = data.Count(t => t.ParentId == item.Id) == 0 ? false : true;
                tree.id = item.Id;
                tree.text = item.Name;
                tree.value = item.Code;
                tree.parentId = item.ParentId;
                tree.isexpand = true;
                tree.complete = true;
                tree.Attribute = "isTree";
                tree.AttributeValue = item.IsTree.ToString();
                tree.hasChildren = hasChildren;
                treeList.Add(tree);
            }
            return Content(treeList.TreeToJson());
        }
        /// <summary>
        /// ��ϸ�б�
        /// </summary>
        /// <param name="keyword">�ؼ��ֲ�ѯ</param>
        /// <returns>���������б�Json</returns>
        [HttpGet]
        public ActionResult GetTreeListJson(string keyword)
        {
            var data = productbll.GetList("{\"IsTree\":\"1\"}").OrderBy(t => t.SortCode).ToList();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.TreeWhere(t => t.Name.Contains(keyword), "Id");
            }
            var TreeList = new List<TreeGridEntity>();
            foreach (DZ_ProductEntity item in data)
            {
                TreeGridEntity tree = new TreeGridEntity();
                bool hasChildren = data.Count(t => t.ParentId == item.Id) == 0 ? false : true;
                tree.id = item.Id;
                tree.parentId = item.ParentId;
                tree.expanded = true;
                tree.hasChildren = hasChildren;
                tree.entityJson = item.ToJson();
                TreeList.Add(tree);
            }
            return Content(TreeList.TreeJson());
        }

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
            var data = productbll.GetPageList(pagination, queryJson);
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
            var data = productbll.GetList(queryJson);
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
            var data = productbll.GetEntity(keyValue);
            return ToJsonResult(data);
        }
        #endregion

        #region �ύ����
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
            productbll.RemoveForm(keyValue);
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
        public ActionResult SaveForm(string keyValue, DZ_ProductEntity entity)
        {
            productbll.SaveForm(keyValue, entity);
            return Success("�����ɹ���");
        }
        #endregion

        #region ��֤����
        /// <summary>
        /// ��Ŀֵ�����ظ�
        /// </summary>
        /// <param name="Code">��Ŀֵ</param>
        /// <param name="keyValue">����</param>
        /// <param name="Id">����Id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ExistCode(string Code, string keyValue, string Id)
        {
            bool IsOk = productbll.ExistCode(Code, keyValue, Id);
            return Content(IsOk.ToString());
        }
        /// <summary>
        /// ��Ŀ�������ظ�
        /// </summary>
        /// <param name="Name">��Ŀ��</param>
        /// <param name="keyValue">����</param>
        /// <param name="Id">����Id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ExistName(string Name, string keyValue, string Id)
        {
            bool IsOk = productbll.ExistName(Name, keyValue, Id);
            return Content(IsOk.ToString());
        }
        #endregion

        /// <summary>
        /// �ϴ���Ʒ
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
                        string dir = string.Format("/Resource/Product/{0}/", uploadDate);
                        if (Directory.Exists(Server.MapPath(dir)) == false)//��������ھʹ���file�ļ���
                        {
                            Directory.CreateDirectory(Server.MapPath(dir));
                        }
                        //string newfileName = DateTime.Now.ToString("yyyyMMddHHmmss");
                        //ԭͼ
                        //string fullDir1 = dir + newfileName + fileExt;
                        string fullDir1 = dir + file.FileName;
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


        #region ��������
        public FileResult GetFile()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "Resource/ExcelTemplate/";
            string fileName = "���ϵ���ģ��.xls";
            return File(path + fileName, "application/ms-excel", fileName);
        }

        [HttpPost]
        public ActionResult DZ_ProductImport(HttpPostedFileBase filebase)
        {
            HttpPostedFileBase file = Request.Files["files"];
            string FileName;
            string savePath;
            if (file == null || file.ContentLength <= 0)
            {
                ViewBag.error = "�ļ�����Ϊ��";
                return View();
            }
            else
            {
                string filename = Path.GetFileName(file.FileName);
                int filesize = file.ContentLength;//��ȡ�ϴ��ļ��Ĵ�С��λΪ�ֽ�byte
                string fileEx = System.IO.Path.GetExtension(filename);//��ȡ�ϴ��ļ�����չ��
                string NoFileName = System.IO.Path.GetFileNameWithoutExtension(filename);//��ȡ����չ�����ļ���
                int Maxsize = 4000 * 1024;//�����ϴ��ļ������ռ��СΪ4M
                string FileType = ".xls,.xlsx";//�����ϴ��ļ��������ַ���

                FileName = NoFileName + DateTime.Now.ToString("yyyyMMddhhmmss") + fileEx;
                if (!FileType.Contains(fileEx))
                {
                    ViewBag.error = "�ļ����Ͳ��ԣ�ֻ�ܵ���xls��xlsx��ʽ���ļ�";
                    return View();
                }
                if (filesize >= Maxsize)
                {
                    ViewBag.error = "�ϴ��ļ�����4M�������ϴ�";
                    return View();
                }
                string path = AppDomain.CurrentDomain.BaseDirectory + "Resource/ExcelData/";
                savePath = Path.Combine(path, FileName);
                file.SaveAs(savePath);
            }

            //excelתDataTable
            DataTable dtSource = ExcelHelper.ExcelImport(savePath);

            //��������TelphoneWash��
            //SqlBulkCopyByDatatable("TelphoneWash", dtSource);

            //һ���в���
            //����������ƣ�����ʱ������ع�
            ViewBag.error = productbll.BatchAddEntity(dtSource);

            System.Threading.Thread.Sleep(2000);
            return View();
        }
        #endregion
    }
}
