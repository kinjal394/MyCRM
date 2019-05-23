using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
    public interface ICountry_Repository : IDisposable
    {
        void InsertCountry(CountryMaster objcountry);
        void UpdateCountry(CountryMaster objcountry);
        IQueryable<CountryMaster> GetAllCountry();
        CountryMaster GetCountryByID(int CountryID);
        IQueryable<CountryMaster> CheckForDuplicateCountry(int CountryID, string CountryName);

    }
}
