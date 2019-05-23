using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.DTOModel;

namespace CRM_Repository.ServiceContract
{
    public interface ISupplier_Repository : IDisposable
    {
        SupplierMaster AddSupplier(SupplierMaster obj);
        void UpdateSupplier(SupplierMaster obj);
        void DeleteSupplier(int id);
        SupplierModel FetchById(int id);
        SupplierMaster GetById(int id);
        IQueryable<SupplierMaster> GetAllSupplier();
        int CreateUpdate(SupplierModel objInputPurchaseOrder);
        int Delete(SupplierModel objInputPurchaseOrder);
    }
}
