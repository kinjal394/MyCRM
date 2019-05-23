using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using CRM_Repository.DataServices;
using CRM_Repository.ServiceContract;
using CRM_Repository.Data;
using System.Transactions;

namespace CRM_Repository.Service
{
  public  class LegerHead_Repository : ILegerHead_Repository,IDisposable
    {

        private CRM_Repository.Data.elaunch_crmEntities context;

        public LegerHead_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void AddLegerHead(LegerHeadMaster Obj)
        {

            try
            {
                context.LegerHeadMasters.Add(Obj);
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void UpdateLegerHead(LegerHeadMaster Obj)
        {
            try
            {
                context.Entry(Obj).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

       
        public IQueryable<LegerHeadMaster> GetAllLegerHead()
        {
            try
            {
              return new dalc().selectbyquerydt("SELECT * FROM LegerHeadMaster with(nolock) WHERE IsActive = 1").ConvertToList<LegerHeadMaster>().AsQueryable();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public LegerHeadMaster GetLegerHeadById(int LegerHeadId)
        {
            try
            {
               
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@LegerHeadId", LegerHeadId);
                return new dalc().GetDataTable_Text("SELECT * FROM LegerHeadMaster with(nolock) WHERE LegerHeadId=@LegerHeadId ", para).ConvertToList<LegerHeadMaster>().FirstOrDefault();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public IQueryable<LegerHeadMaster> DuplicateEditLegerHead(int LegerHeadId, string LegerHeadName)
        {
            try
            {
               
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@LegerHeadId", LegerHeadId);
                para[1] = new SqlParameter().CreateParameter("@LegerHeadName", LegerHeadName);
                return new dalc().GetDataTable_Text("SELECT * FROM LegerHeadMaster with(nolock) WHERE RTRIM(LTRIM(LegerHeadName)) =RTRIM(LTRIM(@LegerHeadName))  AND LegerHeadId<>@LegerHeadId AND IsActive = 1", para).ConvertToList<LegerHeadMaster>().AsQueryable();

            }
            catch (Exception)
            {

                throw;
            }

        }

        public IQueryable<LegerHeadMaster> DuplicateLegerHead(string LegerHeadName)
        {
            try
            {
               
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@LegerHeadName", LegerHeadName);
                return new dalc().GetDataTable_Text("SELECT * FROM LegerHeadMaster with(nolock) WHERE RTRIM(LTRIM(LegerHeadName)) =RTRIM(LTRIM(@LegerHeadName))  AND IsActive = 1", para).ConvertToList<LegerHeadMaster>().AsQueryable();

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
