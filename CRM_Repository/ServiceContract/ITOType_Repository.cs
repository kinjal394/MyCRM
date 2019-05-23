using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;

namespace CRM_Repository.ServiceContract
{
    public interface ITOType_Repository : IDisposable
    {
        void AddTOType(TOTypeMaster obj);
        void UpdateTOType(TOTypeMaster obj);
        void DeleteTOType(int id);
        IQueryable<TOTypeMaster> getAllTOType();
        TOTypeMaster GetTOTypeById(int id);
        IQueryable<TOTypeMaster> GettOTypeById(int TOTypeId);
        IQueryable<TOTypeMaster> DuplicateEditToTypeName(int TOTypeId, string TOType);
        IQueryable<TOTypeMaster> DuplicateToType(string TOType);
    }
}
