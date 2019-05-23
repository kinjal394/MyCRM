using CRM_Repository.Data;
using CRM_Repository.DTOModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
    public interface IPurchaseOrder_Repository : IDisposable
    {
        PurchaseOrderMaster AddPurchaseOrder(PurchaseOrderMaster obj);
        void UpdatePurchaseOrder(PurchaseOrderMaster obj);
        void DeletePurchaseOrder(int id);
        PurchaseOrderModel GetById(int id);
        IQueryable<PurchaseOrderMaster> GetAllPurchaseOrder();
        PurchaseOrderMaster CreateUpdate(PurchaseOrderModel objInputPurchaseOrder);
        int Delete(PurchaseOrderModel objInputPurchaseOrder);
    }
}
