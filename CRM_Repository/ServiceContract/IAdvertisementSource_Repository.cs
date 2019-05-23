using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
    public interface IAdvertisementSource_Repository : IDisposable
    {
        void AddAdvertSource(AdvertisementSourceMaster obj);
        void UpdateAdvertSource(AdvertisementSourceMaster obj);
        AdvertisementSourceMaster GetAdvertSourceById(int id);
        IQueryable<AdvertisementSourceMaster> GetAllAdvertSource();
        IQueryable<AdvertisementSourceMaster> DuplicateAdvertSource(string SiteName);
        IQueryable<AdvertisementSourceMaster> DuplicateEditAdvertSource(int SiteId, string SiteName);
    }
}
