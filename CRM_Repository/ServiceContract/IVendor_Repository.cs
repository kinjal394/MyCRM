using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;
using CRM_Repository.DTOModel;

namespace CRM_Repository.ServiceContract
{
    public interface IVendor_Repository : IDisposable
    {
        VendorMaster AddVendor(VendorMaster obj);
        void UpdateVendor(VendorMaster obj);
        void DeleteVendor(int id);
        VendorModel GetVendorById(int id);
        IQueryable<VendorMaster> GetAllVendor();
        int CreateUpdate(VendorModel objInputVendor);
        int Delete(VendorModel objInputVendor);
    }
}
