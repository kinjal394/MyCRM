using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;
using CRM_Repository.ServiceContract;
using System.Data.SqlClient;
using CRM_Repository.DataServices;

namespace CRM_Repository.Service
{
   public class License_Repository:ILicense_Repository, IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;
        public License_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }
        public void AddLicense(LicenseMaster obj)
        {
            try
            {
                context.LicenseMasters.Add(obj);
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }
        public void UpdateLicense(LicenseMaster obj)
        {
            try
            {
                context.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }
        public void DeleteLicense(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0]= new SqlParameter("@LicenseId", id);
                LicenseMaster License = new dalc().GetDataTable_Text("SELECT * FROM LicenseMaster with(nolock) WHERE LicenseId=@LicenseId", para).ConvertToList<LicenseMaster>().FirstOrDefault();
                if (License != null)
                {
                    context.LicenseMasters.Remove(License);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }
        public LicenseMaster GetLicenseByID(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@LicenseId", id);
                var License = new dalc().GetDataTable_Text("SELECT * FROM LicenseMaster with(nolock) WHERE LicenseId=@LicenseId", para).ConvertToList<LicenseMaster>().FirstOrDefault();
                return License;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public IQueryable<LicenseMaster> GetAllLicense()
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@IsActive", "true");
                var License = new dalc().GetDataTable_Text("SELECT * FROM LicenseMaster with(nolock) WHERE IsActive=@IsActive", para).ConvertToList<LicenseMaster>().AsQueryable();
                return License;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public IQueryable<LicenseMaster> DuplicateLicense(string LicenseName)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@LicenseName", LicenseName);
                para[1] = new SqlParameter().CreateParameter("@IsActive", "true");
                var License = new dalc().GetDataTable_Text("SELECT * FROM LicenseMaster with(nolock) WHERE LicenseName=@LicenseName and IsActive=@IsActive", para).ConvertToList<LicenseMaster>().AsQueryable();
                return License.AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public IQueryable<LicenseMaster> DuplicateEditLicense(int LicenseId, string LicenseName)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[3];
                para[0] = new SqlParameter().CreateParameter("@LicenseId", LicenseId);
                para[1] = new SqlParameter().CreateParameter("@LicenseName", LicenseName);
                para[2] = new SqlParameter().CreateParameter("@IsActive", "true");
                var License = new dalc().GetDataTable_Text("SELECT * FROM LicenseMaster with(nolock) WHERE LicenseId!=@LicenseId and LicenseName=@LicenseName and IsActive=@IsActive", para).ConvertToList<LicenseMaster>().AsQueryable();
                return License.AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
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
