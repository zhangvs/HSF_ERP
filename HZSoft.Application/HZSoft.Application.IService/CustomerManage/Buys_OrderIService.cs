using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Util.WebControl;
using System.Collections.Generic;

namespace HZSoft.Application.IService.CustomerManage
{
    /// <summary>
    /// �� �� 6.1
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2018-06-01 16:52
    /// �� ����������
    /// </summary>
    public interface Buys_OrderIService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        IEnumerable<Buys_OrderEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        Buys_OrderEntity GetEntity(string keyValue);
        Buys_OrderEntity GetEntityByOrderId(string orderId);
        /// <summary>
        /// ��ȡ�ӱ���ϸ��Ϣ
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        IEnumerable<Buys_OrderItemEntity> GetDetails(string keyValue);
        Buys_OrderItemEntity GetDetail(string keyValue, int? sortCode);
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����</param>
        void RemoveForm(string keyValue);
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        void SaveForm(string keyValue, Buys_OrderEntity entity,List<Buys_OrderItemEntity> entryList);
        void SaveSend(string keyValue, Buys_OrderEntity entity);
        void UpdateSendState(string keyValue);
        void SaveBuyMain(Sale_CustomerEntity entity);
        void SaveInForm(Buys_OrderItemEntity entity);
        void SaveLogisticsForm(string keyValue, Buys_OrderEntity entity);
        void SaveInstallForm(string keyValue, Buys_OrderEntity entity);
        #endregion
    }
}
