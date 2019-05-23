using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
    public interface IVendorBankDetail_Repository : IDisposable
    {
        void AddVendorBankDetail(VendorBankMaster obj);
        void UpdateVendorBankDetail(VendorBankMaster obj);
        void DeleteVendorBankDetail(int id);
        VendorBankMaster GetVendorBankDetailById(int id);
        IQueryable<VendorBankMaster> GetAllVendorBankDetail();
        IQueryable<VendorBankMaster> GetByVendorId(int id);
    }
}
