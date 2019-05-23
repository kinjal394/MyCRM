using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;

namespace CRM_Repository.ServiceContract
{
    public interface ISupplierBankDetail_Repository : IDisposable
    {
        void AddSupplierBankDetail(SupplierBankMaster obj);
        void UpdateSupplierBankDetail(SupplierBankMaster obj);
        void DeleteSupplierBankDetail(int id);
        SupplierBankMaster GetById(int id);
        IQueryable<SupplierBankMaster> GetAllSupplierBankDetail();
        IQueryable<SupplierBankMaster> GetBySupplierId(int id);
    }
}
