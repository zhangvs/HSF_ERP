using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.Busines.CustomerManage;
using HZSoft.Util;
using HZSoft.Util.WebControl;
using System.Collections.Generic;
using System.Web.Mvc;
using HZSoft.Application.Busines.SystemManage;
using HZSoft.Application.Code;
using System.Web;
using System.IO;
using System;

namespace HZSoft.Application.Web.Areas.CustomerManage.Controllers
{
    /// <summary>
    /// �� �� 6.1
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2018-06-01 16:52
    /// �� ����������
    /// </summary>
    public class Buys_OrderController : MvcControllerBase
    {
        private Buys_OrderBLL buys_orderbll = new Buys_OrderBLL();
        private CodeRuleBLL codeRuleBLL = new CodeRuleBLL();

        #region ��ͼ����
        /// <summary>
        /// �б�ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Buys_OrderIndex()
        {
            return View();
        }
        /// <summary>
        /// ��ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Buys_OrderForm()
        {
            if (Request["keyValue"] == null)
            {
                ViewBag.OrderCode = codeRuleBLL.GetBillCode(SystemInfo.CurrentUserId, "", ((int)CodeRuleEnum.Buy_Order).ToString());
            }
            return View();
        }
        /// <summary>
        /// ����֪ͨ
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult KeeperIndex()
        {
            return View();
        }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SendOutIndex()
        {
            return View();
        }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SendOutForm()
        {
            return View();
        }
        /// <summary>
        /// ��ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult KeeperForm()
        {
            return View();
        }
        /// <summary>
        /// ����ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SendForm()
        {
            return View();
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public ActionResult InGuiForm()
        {
            return View();
        }
        /// <summary>
        /// �Ű�
        /// </summary>
        /// <returns></returns>
        public ActionResult InMenForm()
        {
            return View();
        }
        /// <summary>
        /// ���
        /// </summary>
        /// <returns></returns>
        public ActionResult InWuForm()
        {
            return View();
        }
        /// <summary>
        /// ��Э
        /// </summary>
        /// <returns></returns>
        public ActionResult InWaiForm()
        {
            return View();
        }
        /// <summary>
        /// ������ϸҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Detail()
        {
            return View();
        }

        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Buys_OrderOverIndex()
        {
            return View();
        }
        /// <summary>
        /// ����֪ͨ���
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult KeeperOverIndex()
        {
            return View();
        }
        /// <summary>
        /// �ͷ����ܱ�
        /// </summary>
        /// <returns></returns>
        public ActionResult KeFuAllIndex()
        {
            return View();
        }
        /// <summary>
        /// �������
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SendOutOverIndex()
        {
            return View();
        }





        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SendLogisticsIndex()
        {
            return View();
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SendLogisticsOverIndex()
        {
            return View();
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SendLogisticsForm()
        {
            return View();
        }




        /// <summary>
        /// ������װ
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SendInstallIndex()
        {
            return View();
        }
        /// <summary>
        /// ������װ
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SendInstallOverIndex()
        {
            return View();
        }
        /// <summary>
        /// ������װ
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SendInstallForm()
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
            var data = buys_orderbll.GetPageList(pagination, queryJson);
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
        /// ��ȡʵ�� 
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns>���ض���Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = buys_orderbll.GetEntity(keyValue);
            var childData = buys_orderbll.GetDetails(keyValue);
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
            var data = buys_orderbll.GetDetails(keyValue);
            return ToJsonResult(data);
        }
        /// <summary>
        /// ��ȡ�ӱ���ϸ��Ϣ 
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns>���ض���Json</returns>
        [HttpGet]
        public ActionResult GetDetailJson(string keyValue,int? sortCode)
        {
            var data = buys_orderbll.GetDetail(keyValue,sortCode);
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
            buys_orderbll.RemoveForm(keyValue);
            return Success("ɾ���ɹ���");
        }
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <param name="orderEntryJson">��ϸ����</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, Buys_OrderEntity entity, string orderEntryJson)
        {
            var orderEntryList = orderEntryJson.ToList<Buys_OrderItemEntity>();
            buys_orderbll.SaveForm(keyValue, entity, orderEntryList);
            return Success("�����ɹ���");
        }


        /// <summary>
        /// �������
        /// </summary>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveInForm(string keyValue, Buys_OrderItemEntity entity)
        {
            buys_orderbll.SaveInForm(entity);
            return Success("�����ɹ���");
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
        public ActionResult SaveSend(string keyValue, Buys_OrderEntity entity)
        {
            buys_orderbll.SaveSend(keyValue, entity);
            return Success("�����ɹ���");
        }


        /// <summary>
        /// ʵ�ʷ���
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="SendOutImg">����ֵ</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult UpdateSendState(string keyValue, string SendOutImg)
        {
            buys_orderbll.UpdateSendState(keyValue, SendOutImg);
            return Success("�����ɹ���");
        }



        /// <summary>
        /// ������������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveLogisticsForm(string keyValue, Buys_OrderEntity entity)
        {
            buys_orderbll.SaveLogisticsForm(keyValue, entity);
            return Success("�����ɹ���");
        }

        /// <summary>
        /// ��װ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveInstallForm(string keyValue, Buys_OrderEntity entity)
        {
            buys_orderbll.SaveInstallForm(keyValue, entity);
            return Success("�����ɹ���");
        }
        #endregion


        /// <summary>
        /// �ϴ������ֳ�ͼƬ
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
                        string dir = "/Resource/DocumentFile/SendOut/";
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
        
    }
}
