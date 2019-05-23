using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
    public interface IAddressType_Repository : IDisposable
    {
        void AddAddressType(AddressTypeMaster objAddressType);
        void UpdateAddressType(AddressTypeMaster objAddressType);
        void DeleteAddressType(int id);
        AddressTypeMaster GetAddressTypeByID(int AddressTypeId);
        IQueryable<AddressTypeMaster> GetAllAddressType();
        IQueryable<AddressTypeMaster> DuplicateAddressType(string AddressTypeName);
        IQueryable<AddressTypeMaster> DuplicateEditAddressTypeName(int AddressTypeId, string AddressTypeName);
    }
}
