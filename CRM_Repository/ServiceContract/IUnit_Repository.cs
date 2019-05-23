using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
    public interface IUnit_Repository : IDisposable
    {
        void AddUnit(UnitMaster unit);
        void UpdateUnit(UnitMaster unit);
        UnitMaster GetUnitById(int id);
        IQueryable<UnitMaster> GetAllUnit();
        bool CheckUnit(UnitMaster obj, bool isUpdate);
    }
}
