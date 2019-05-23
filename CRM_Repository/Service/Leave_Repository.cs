using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.ServiceContract;
using System.Data.Entity;
using CRM_Repository.Data;
using System.Data;
using System.Transactions;
using System.Data.SqlClient;
using CRM_Repository.DataServices;
namespace CRM_Repository.Service
{
    public class Leave_Repository : ILeave_Repository , IDisposable
    {
        private elaunch_crmEntities context;
        public Leave_Repository(elaunch_crmEntities _context)
        {
            context = _context;
        }
        
        public void InsertLeave(LeaveMaster objleave)
        {
            try
            {
                context.LeaveMasters.Add(objleave);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public void UpdateLeave(LeaveMaster objleave)
        {
            try
            {
                context.Entry(objleave).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public IQueryable<LeaveMaster> GetAllLeave()
        {
            try
            {
              
                return new dalc().selectbyquerydt("SELECT * FROM LeaveMaster with(nolock) WHERE IsActive=1").ConvertToList<LeaveMaster>().AsQueryable();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public LeaveMaster GetLeaveID(int LeaveId)
        {
            try
            {
                //LeaveMaster lmaster = new LeaveMaster();
                //return lmaster = context.LeaveMasters.Find(LeaveId);

                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@LeaveId", LeaveId);
                return new dalc().GetDataTable_Text("SELECT * FROM LeaveMaster with(nolock) WHERE LeaveId=@LeaveId", para).ConvertToList<LeaveMaster>().FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw ex.InnerException; 
            }
        }

        public IQueryable<LeaveMaster> DuplicateEditLeave(int LeaveId)
        {
            try
            {
               

                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@LeaveId", LeaveId);
                return new dalc().GetDataTable_Text("SELECT * FROM LeaveMaster with(nolock) WHERE LeaveId<>@LeaveId and ITRName=@ITRName and IsActive=1", para).ConvertToList<LeaveMaster>().AsQueryable();
                                
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
