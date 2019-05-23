using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using CRM_Repository.Data;
using CRM_Repository.ServiceContract;
using CRM_Repository.DataServices;
using System.Data.SqlClient;

namespace CRM_Repository.Service
{
    public class ITR_Repository : IITR_Repository, IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;

        public ITR_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }
        public void AddITR(ITRMaster objitr)
        {
            try
            {
                context.ITRMasters.Add(objitr);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public void DeleteITR(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@ITRId", id);
                ITRMaster ITR = new dalc().GetDataTable_Text("SELECT * FROM ITRMaster with(nolock) WHERE ITRId=@ITRId", para).ConvertToList<ITRMaster>().FirstOrDefault();
                if (ITR != null)
                {
                    context.ITRMasters.Remove(ITR);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public IQueryable<ITRMaster> DuplicateEditITRName(int ITRId, string ITRName)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[3];
                para[0] = new SqlParameter().CreateParameter("@ITRId", ITRId);
                para[1] = new SqlParameter().CreateParameter("@ITRName", ITRName);
                para[2] = new SqlParameter().CreateParameter("@IsActive", "true");
                var ITR = new dalc().GetDataTable_Text("SELECT * FROM ITRMaster with(nolock) WHERE ITRId!=@ITRId and ITRName=@ITRName and IsActive=@IsActive", para).ConvertToList<ITRMaster>().AsQueryable();

                return ITR.AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public IQueryable<ITRMaster> DuplicateITR(string ITRName)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@ITRName", ITRName);
                return new dalc().GetDataTable_Text("SELECT * FROM ITRMaster with(nolock) WHERE RTRIM(LTRIM(ITRName)) = RTRIM(LTRIM(@ITRName)) AND IsActive=1 ", para).ConvertToList<ITRMaster>().AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public IQueryable<ITRMaster> GetAllITR()
        {
            try
            {
                return new dalc().selectbyquerydt("SELECT * FROM ITRMaster with(nolock) WHERE IsActive=1").ConvertToList<ITRMaster>().AsQueryable();

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public ITRMaster GetITRByID(int ITRId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@ITRId", ITRId);
                return new dalc().GetDataTable_Text("SELECT * FROM ITRMaster with(nolock) WHERE ITRId=@ITRId", para).ConvertToList<ITRMaster>().FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public void UpdateITR(ITRMaster objitr)
        {
            try
            {
                context.Entry(objitr).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
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
