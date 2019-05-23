using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;

namespace CRM_Repository.ServiceContract
{
   public interface IPackingType_Repository:IDisposable
    {
        void AddPackingType(PackingTypeMaster obj);
        void UpdatePackingType(PackingTypeMaster obj);
        void DeletePackingType(int id);
        PackingTypeMaster GetPackingTypeByID(int id);
        IQueryable<PackingTypeMaster> GetAllPackingType();
        IQueryable<PackingTypeMaster> DuplicatePackingType(string PackingType);
        IQueryable<PackingTypeMaster> DuplicateEditPackingType(int PackingTypeId, string PackingType);
    }
}
