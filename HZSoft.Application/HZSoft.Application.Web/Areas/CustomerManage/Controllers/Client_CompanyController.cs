using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.Busines.CustomerManage;
using HZSoft.Util;
using HZSoft.Util.WebControl;
using System.Web.Mvc;

namespace HZSoft.Application.Web.Areas.CustomerManage.Controllers
{
    /// <summary>
    /// �� �� 6.1
    /// 
    /// �� ��������
    /// �� �ڣ�2020-03-15 11:09
    /// �� ����������
    /// </summary>
    public class Client_CompanyController : MvcControllerBase
    {
        private Client_CompanyBLL client_companybll = new Client_CompanyBLL();

        #region ��ͼ����
        /// <summary>
        /// �б�ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Client_CompanyIndex()
        {
            return View();
        }
        /// <summary>
        /// ��ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Client_CompanyForm()
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
            var data = client_companybll.GetPageList(pagination, queryJson);
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
            var data = client_companybll.GetList(queryJson);
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
            var data = client_companybll.GetEntity(keyValue);
            return ToJsonResult(data);
        }
        #endregion

        #region ��֤����
        /// <summary>
        /// �ͻ����Ʋ����ظ�
        /// </summary>
        /// <param name="FullName">����</param>
        /// <param name="keyValue">����</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ExistFullName(string FullName, string keyValue)
        {
            bool IsOk = client_companybll.ExistFullName(FullName, keyValue);
            return Content(IsOk.ToString());
        }
        /// <summary>
        /// �ֻ������ظ�
        /// </summary>
        /// <param name="Mobile">����</param>
        /// <param name="keyValue">����</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ExistMobile(string Mobile, string keyValue)
        {
            bool IsOk = client_companybll.ExistMobile(Mobile, keyValue);
            return Content(IsOk.ToString());
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
            client_companybll.RemoveForm(keyValue);
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
        public ActionResult SaveForm(string keyValue, Client_CompanyEntity entity)
        {
            client_companybll.SaveForm(keyValue, entity);
            return Success("�����ɹ���");
        }
        #endregion
    }
}
