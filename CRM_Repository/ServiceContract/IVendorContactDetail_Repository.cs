using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;
using CRM_Repository.DTOModel;

namespace CRM_Repository.ServiceContract
{
    public interface IVendorContactDetail_Repository : IDisposable
    {
        void AddVendorContactDetail(VendorContactDetail obj);
        void UpdateVendorContactDetail(VendorContactDetail obj);
        void DeleteVendorContactDetail(int id);
        VendorContactDetail GetVendorContactDetailById(int id);
        IQueryable<VendorContactDetail> GetAllVendorContactDetail();
        IQueryable<vmVendorContactDetail> GetByVendorId(int id);
        IQueryable<VendorAddressDetail> GetAddressByVendorId(int id);
        IQueryable<VendorChatMaster> GetChatByVendorId(int id);
    }
}
