using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
    public interface IArea_Repository : IDisposable
    {

        void SaveArea(AreaMaster Area);
        void UpdateArea(AreaMaster Area);
        void Delete(int AreaId);
        AreaMaster GetAreaById(int AreaId);
        IQueryable<AreaMaster> GetArea();
        IQueryable<AreaMaster> GetAreaByCityId(int CityId);
    }
}
