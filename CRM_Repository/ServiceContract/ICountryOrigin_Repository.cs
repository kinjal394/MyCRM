using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;

namespace CRM_Repository.ServiceContract
{
    public interface ICountryOrigin_Repository : IDisposable
    {
        void InsertOrigin(CountryOfOriginMaster objorigin);
        void UpdateOrigin(CountryOfOriginMaster objorigin);
        IQueryable<CountryOfOriginMaster> GetAllCountryOfOrigin();
        IQueryable<CountryOfOriginMaster> DuplicateOrigin(string Originname);
        CountryOfOriginMaster GetOriginID(int OriginID);
        IQueryable<CountryOfOriginMaster> DuplicateEditOrigin(int OriginId, string Originname);
    }
}
