using CRM_Repository.Data;
using CRM_Repository.DataServices;
using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CRM_Repository.Service
{
   public class AdvertisementSource_Repository : IAdvertisementSource_Repository, IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;
        public AdvertisementSource_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }
        public void AddAdvertSource(AdvertisementSourceMaster obj)
        {
            try
            {
                context.AdvertisementSourceMasters.Add(obj);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public AdvertisementSourceMaster GetAdvertSourceById(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@SiteId", id);
                return new dalc().GetDataTable_Text("SELECT * FROM AdvertisementSourceMaster with(nolock) WHERE SiteId=@SiteId", para).ConvertToList<AdvertisementSourceMaster>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<AdvertisementSourceMaster> DuplicateEditAdvertSource(int SiteId, string SiteName)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@SiteId", SiteId);
                para[1] = new SqlParameter().CreateParameter("@SiteName", SiteName);
                return new dalc().GetDataTable_Text("SELECT * FROM AdvertisementSourceMaster with(nolock) WHERE SiteId != @SiteId AND SiteName = @SiteName AND IsActive = 1", para).ConvertToList<AdvertisementSourceMaster>().AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IQueryable<AdvertisementSourceMaster> DuplicateAdvertSource(string SiteName)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@SiteName", SiteName);
                return new dalc().GetDataTable_Text("SELECT * FROM AdvertisementSourceMaster with(nolock) WHERE SiteName = @SiteName AND IsActive = 1", para).ConvertToList<AdvertisementSourceMaster>().AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateAdvertSource(AdvertisementSourceMaster obj)
        {
            try
            {
                context.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IQueryable<AdvertisementSourceMaster> GetAllAdvertSource()
        {
            try
            {
                return new dalc().selectbyquerydt("SELECT * FROM AdvertisementSourceMaster with(nolock) WHERE IsActive = 1").ConvertToList<AdvertisementSourceMaster>().AsQueryable();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    context.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
