using HZSoft.Util;
using HZSoft.Util.WebControl;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;
using HZSoft.Application.Busines.CustomerManage;
using HZSoft.Application.Entity.CustomerManage;

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
        /// �б�ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DZ_ProductIndex()
        {
            return View();
        }
        /// <summary>
        /// ����+����
        /// </summary>
        /// <returns></returns>
        public ActionResult DZ_ProductIndex2()
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
        #endregion

        #region ��ȡ����
        /// <summary>
        /// �����б� "{\"PId\":\"0\"}"
        /// </summary>
        /// <param name="keyword">�ؼ��ֲ�ѯ</param>
        /// <returns>��������Json</returns>
        [HttpGet]
        public ActionResult GetTreeJson(string keyword, string _parentId)
        {
            var data = productbll.GetList().OrderBy(t => t.SortCode).ToList();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.TreeWhere(t => t.Name.Contains(keyword), "");
            }
            if (!string.IsNullOrEmpty(_parentId))
            {
                data = data.TreeWhere(t => t.PId == _parentId, "Id");//����Id
            }
            var treeList = new List<TreeEntity>();
            foreach (DZ_ProductEntity item in data)
            {
                TreeEntity tree = new TreeEntity();
                bool hasChildren = data.Count(t => t.PId == item.Id) == 0 ? false : true;
                tree.id = item.Id;
                tree.text = item.Name;
                tree.value = item.Code;
                tree.parentId = item.PId;
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
        /// <param name="typeId">����Id</param>
        /// <param name="keyword">�ؼ��ֲ�ѯ</param>
        /// <returns>���������б�Json</returns>
        [HttpGet]
        public ActionResult GetTreeListJson(string typeId, string condition, string keyword)
        {
            var data = productbll.GetList("{\"PId\":\"" + typeId + "\"}").OrderBy(t => t.SortCode).ToList();
            if (!string.IsNullOrEmpty(keyword))
            {
                #region ��������ѯ
                switch (condition)
                {
                    case "Name":        //��Ʒ��
                        data = data.TreeWhere(t => t.Name.Contains(keyword), "Id");
                        break;
                    case "Code":      //��Ʒ���
                        data = data.TreeWhere(t => t.Code.Contains(keyword), "Id");
                        break;
                    default:
                        break;
                }
                #endregion
            }
            var TreeList = new List<TreeGridEntity>();
            foreach (DZ_ProductEntity item in data)
            {
                TreeGridEntity tree = new TreeGridEntity();
                bool hasChildren = data.Count(t => t.PId == item.PId) == 0 ? false : true;
                tree.id = item.Id;
                tree.parentId = item.PId;
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
        /// <param name="Value">��Ŀֵ</param>
        /// <param name="keyValue">����</param>
        /// <param name="itemId">����Id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ExistValue(string Value, string keyValue, string itemId)
        {
            bool IsOk = productbll.ExistValue(Value, keyValue, itemId);
            return Content(IsOk.ToString());
        }
        /// <summary>
        /// ��Ŀ�������ظ�
        /// </summary>
        /// <param name="Name">��Ŀ��</param>
        /// <param name="keyValue">����</param>
        /// <param name="itemId">����Id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ExistName(string Name, string keyValue, string itemId)
        {
            bool IsOk = productbll.ExistName(Name, keyValue, itemId);
            return Content(IsOk.ToString());
        }
        #endregion

    }
}
