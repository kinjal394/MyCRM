using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CRM_Repository.Data;

namespace CRM_Repository.ServiceContract
{
    public interface ICity_Repository : IDisposable
    {
        void InsertCity(CityMaster objcity);
        void UpdateCity(CityMaster objcity); 
        CityMaster GetCityByID(int CityID);
        IQueryable<CityMaster> GetCityByStateID(int StateID);
        IQueryable<CityMaster> CheckForDuplicateCity(int CityId, string Cityname);
        IQueryable<CityMaster> GetAllCities();
     }
}
