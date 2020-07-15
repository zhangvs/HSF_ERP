using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Util.WebControl;
using System.Collections.Generic;
using System.Data;

namespace HZSoft.Application.IService.CustomerManage
{
    /// <summary>
    /// �� �� 6.1
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2019-11-17 08:11
    /// �� ����DZ_Order
    /// </summary>
    public interface DZ_OrderIService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        IEnumerable<DZ_OrderEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        IEnumerable<DZ_OrderEntity> GetList(string queryJson);
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        DZ_OrderEntity GetEntity(string keyValue);
        #endregion

        #region �ύ����
        /// <summary>
        /// һ������
        /// </summary>
        /// <param name="keyValue">����</param>
        void BackForm(string keyValue, DZ_OrderEntity entity);
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
        void SaveForm(string keyValue, DZ_OrderEntity entity);
        /// <summary>
        /// ����ϵͳ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        void SaveMoneyForm(string keyValue, DZ_OrderEntity entity, List<DZ_Money_ItemEntity> entryList);
        void SaveSigned(string keyValue, DZ_OrderEntity entity);
        void UpdateMoneyOkState(string keyValue,int? state);
        void UpdateOverState(string keyValue, int? state);
        #endregion
        /// <summary>
        /// ������������
        /// </summary>
        /// <param name="dtSource">ʵ�����</param>
        /// <returns></returns>
        string BatchAddEntity(string keyValue, DataTable dtSource,string dir);
        string BatchAddEntity1010(string keyValue, DataTable dtSource, string dir);
        string BatchAddEntity_cw(string keyValue, DataTable dtSource, string dir);
        string BatchAddEntity1010_cw(string keyValue, DataTable dtSource, string dir);
        /// <summary>
        /// ��յ�������
        /// </summary>
        /// <param name="keyValue">����</param>
        void RemoveImportForm(string keyValue);
    }
}
