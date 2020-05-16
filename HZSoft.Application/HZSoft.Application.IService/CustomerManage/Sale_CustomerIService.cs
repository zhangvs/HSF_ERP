using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Data.Repository;
using HZSoft.Util.WebControl;
using System.Collections.Generic;

namespace HZSoft.Application.IService.CustomerManage
{
    /// <summary>
    /// �� �� 6.1
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2017-12-23 14:41
    /// �� ��������ͳ��
    /// </summary>
    public interface Sale_CustomerIService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        IEnumerable<Sale_CustomerEntity> GetPageList(Pagination pagination, string queryJson);
        IEnumerable<Sale_CustomerEntity> GetList(string queryJson);
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        Sale_CustomerEntity GetEntity(string keyValue);
        /// <summary>
        /// ��ȡ�ӱ���ϸ��Ϣ
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        IEnumerable<Sale_Customer_ItemEntity> GetDetails(string keyValue);
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����</param>
        void RemoveForm(string keyValue);
        /// <summary>
        /// ����������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        void SaveForm(string keyValue, Sale_CustomerEntity entity,List<Sale_Customer_ItemEntity> entryList);
        /// <summary>
        /// �µ�
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        void SaveDownForm(string keyValue, Sale_CustomerEntity entity);
        /// <summary>
        /// �Ƶ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="state">״̬1�Ƶ���-1����</param>
        /// <param name="orderId">���۵�id</param>
        /// <returns></returns>
        void UpdatePushState(string keyValue, int? state, string orderId);
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        void SavePushBackForm(string keyValue, Sale_CustomerEntity entity);
        /// <summary>
        /// �Ų�
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        void SavePlanForm(string keyValue, Sale_CustomerEntity entity);
        /// <summary>
        /// ����״̬�޸�
        /// </summary>
        /// <param name="entity"></param>
        void UpdateStepState(Sale_CustomerEntity entity);
        #endregion
    }
}