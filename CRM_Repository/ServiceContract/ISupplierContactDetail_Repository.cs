using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
    public interface ISupplierContactDetail_Repository : IDisposable
    {
        void AddSupplierContactDetail(SupplierContactDetail obj);
        void UpdateSupplierContactDetail(SupplierContactDetail obj);
        void DeleteSupplierContactDetail(int id);
        SupplierContactDetail GetById(int id);
        IQueryable<SupplierContactDetail> GetAllSupplierContactDetail();
        IQueryable<SupplierContactDetail> GetBySupplierId(int id);
        IQueryable<SupplierContactDetail> GetByContactId(int ContactId);
        IQueryable<SupplierAddressMaster> GetAddressBySupplierId(int id);
        IQueryable<SupplierChatMaster> GetChatBySupplierId(int id);
    }
}
