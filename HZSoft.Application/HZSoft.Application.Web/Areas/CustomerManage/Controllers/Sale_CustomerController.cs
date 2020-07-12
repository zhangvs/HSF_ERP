using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.Busines.CustomerManage;
using HZSoft.Util;
using HZSoft.Util.WebControl;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web;
using System.IO;
using System;
using HZSoft.Util.Offices;
using System.Data;

namespace HZSoft.Application.Web.Areas.CustomerManage.Controllers
{
    /// <summary>
    /// �� �� 6.1
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2017-12-23 14:41
    /// �� ��������ͳ��
    /// </summary>
    public class Sale_CustomerController : MvcControllerBase
    {
        private Sale_CustomerBLL sale_customerbll = new Sale_CustomerBLL();

        #region ��ͼ����
        /// <summary>
        /// �����µ�
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OrderDownIndex()
        {
            return View();
        }
        /// <summary>
        /// �����µ�
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OrderDownForm()
        {
            return View();
        }

        /// <summary>
        /// �����Ƶ�
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OrderPushIndex()
        {
            return View();
        }
        /// <summary>
        /// ����ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OrderPushBackForm()
        {
            return View();
        }

        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OrderIndex()
        {
            return View();
        }
        /// <summary>
        /// ��ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Sale_CustomerForm()
        {
            return View();
        }
        /// <summary>
        /// �Ų��ƻ�
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PlanForm()
        {
            return View();
        }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult StepBackForm()
        {
            return View();
        }
        /// <summary>
        /// ��ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Detail()
        {
            return View();
        }

        /// <summary>
        /// �µ����
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OrderDownOverIndex()
        {
            return View();
        }

        /// <summary>
        /// �Ƶ����
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OrderPushOverIndex()
        {
            return View();
        }

        /// <summary>
        /// ������ֻ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OrderOnlyReadIndex()
        {
            return View();
        }
        /// <summary>
        /// ���������
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OrderOverIndex()
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
            var data = sale_customerbll.GetPageList(pagination, queryJson);
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
        // <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = sale_customerbll.GetList(queryJson);
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
            var data = sale_customerbll.GetEntity(keyValue);
            var childData = sale_customerbll.GetDetails(keyValue);
            var jsonData = new
            {
                entity = data,
                childEntity = childData
            };
            return ToJsonResult(jsonData);
        }
        /// <summary>
        /// ��ȡ�ӱ���ϸ��Ϣ 
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns>���ض���Json</returns>
        [HttpGet]
        public ActionResult GetDetailsJson(string keyValue)
        {
            var data = sale_customerbll.GetDetails(keyValue);
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
            sale_customerbll.RemoveForm(keyValue);
            return Success("ɾ���ɹ���");
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
        public ActionResult SaveForm(string keyValue, string strEntity,string strChildEntitys)
        {
            strEntity = strEntity.Replace("&nbsp;", "");//�������ڴ���
            var entity = strEntity.ToObject<Sale_CustomerEntity>();
            var childEntitys = strChildEntitys.ToList<Sale_Customer_ItemEntity>();
            sale_customerbll.SaveForm(keyValue, entity, childEntitys);
            return Success("�����ɹ���");
        }


        /// <summary>
        /// �Ƶ�,����
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="state">״̬1�Ƶ�-1����</param>
        /// <param name="orderId">���۵�id</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult UpdatePushState(string keyValue, int? state, string orderId)
        {
            sale_customerbll.UpdatePushState(keyValue, state, orderId);
            return Success("�����ɹ���");
        }

        /// <summary>
        /// �µ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveDownForm(string keyValue, Sale_CustomerEntity entity)
        {
            sale_customerbll.SaveDownForm(keyValue,entity);
            return Success("�����ɹ���");
        }

        /// <summary>
        /// �Ų�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SavePlanForm(string keyValue, Sale_CustomerEntity entity)
        {
            sale_customerbll.SavePlanForm(keyValue, entity);
            return Success("�����ɹ���");
        }

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="step">����</param>
        /// <param name="name">����</param>
        /// <returns></returns>
        public ActionResult SaveStepBackForm(string keyValue, int? step, string name)
        {
            sale_customerbll.SaveStepBackForm(keyValue, step, name);
            return Success("�����ɹ���");
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SavePushBackForm(string keyValue, Sale_CustomerEntity entity)
        {
            sale_customerbll.SavePushBackForm(keyValue, entity);
            return Success("�����ɹ���");
        }

        #endregion

        /// <summary>
        /// ������ϸҳ
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult LiaoDetail()
        {
            return View();
        }
        /// <summary>
        /// ���ϱ༭
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult LiaoForm()
        {
            return View();
        }
        /// <summary>
        /// �����ϵ���excel
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ImportLiao(string keyValue)
        {
            ViewBag.OrderId = keyValue;
            return View();
        }
        [HttpPost]
        public ActionResult ImportLiao(HttpPostedFileBase filebase, string keyValue)
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

                string dir = string.Format("/Resource/Liao/{0}/", DateTime.Now.ToString("yyyyMMdd"));
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
                ViewBag.error = sale_customerbll.BatchAddEntityLiao(keyValue, dtSource, fullDir1);

                System.Threading.Thread.Sleep(2000);
            }
            return View();
        }
    }
}
