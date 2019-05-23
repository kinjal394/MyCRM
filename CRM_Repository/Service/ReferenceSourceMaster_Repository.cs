using CRM_Repository.Data;
using CRM_Repository.DataServices;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.Service
{
    public class ReferenceSourceMaster_Repository : IReferenceSourceMaster_Repository, IDisposable
    {

        private CRM_Repository.Data.elaunch_crmEntities context;
        public ReferenceSourceMaster_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }
        public void Add(ReferenceSourceMaster obj)
        {
            try
            {
                context.ReferenceSourceMasters.Add(obj);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Update(ReferenceSourceMaster obj)
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
        public IQueryable<ReferenceSourceMaster> GetAll()
        {
            try
            {
                return new dalc().selectbyquerydt("SELECT * FROM ReferenceSourceMaster with(nolock) WHERE IsActive = 1").ConvertToList<ReferenceSourceMaster>().AsQueryable();
            }
            catch
            {
                throw;
            }
        }
        public ReferenceSourceMaster GetByID(int id)
        {
            try
            {
                //return context.SourceMasters.Find(id);
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@SourceId", id);
                return new dalc().GetDataTable_Text("SELECT * FROM ReferenceSourceMaster with(nolock) WHERE SourceId=@SourceId ", para).ConvertToList<ReferenceSourceMaster>().FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IQueryable<ReferenceSourceMaster> CheckForDuplicate(int SourceId, string SourceName)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@SourceId", SourceId);
                para[1] = new SqlParameter().CreateParameter("@SourceName", SourceName);
                return new dalc().GetDataTable_Text("SELECT * FROM ReferenceSourceMaster with(nolock) WHERE SourceId <> @SourceId AND RTRIM(LTRIM(SourceName)) = RTRIM(LTRIM(@SourceName)) AND IsActive = 1", para).ConvertToList<ReferenceSourceMaster>().AsQueryable();
            }
            catch (Exception)
            {

                throw;
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
