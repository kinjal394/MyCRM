using CRM_Repository.Data;
using CRM_Repository.DTOModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
    public interface IPurchaseOrderDetail_Repository : IDisposable
    {
        void AddPurchaseOrderDetail(PurchaseOrderDetailMaster obj);
        void UpdatePurchaseOrderDetail(PurchaseOrderDetailMaster obj);
        void DeletePurchaseOrderDetail(int id);
        PurchaseOrderDetailMaster GetById(int id);
        IQueryable<PurchaseOrderDetailMaster> GetAllPurchaseOrderDetail();
        IQueryable<PurchaseOrderDetailModel> GetByPurchaseOrderId(int id);
        IQueryable<PurchaseOrderTechnicalDetailModel> GetTechDetailByPurchaseId(int PoId);
    }
}
