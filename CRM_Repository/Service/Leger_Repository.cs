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
   public class Leger_Repository: ILeger_Repository,  IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;

        public Leger_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }
        public void AddLeger(LegerMaster Obj)
        {

            try
            {
                context.LegerMasters.Add(Obj);
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void UpdateLeger(LegerMaster Obj)
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

        public IQueryable<LegerMaster> GetAllLeger()
        {
            try
            {
                return new dalc().selectbyquerydt("SELECT * FROM LegerMaster with(nolock) WHERE IsActive = 1").ConvertToList<LegerMaster>().AsQueryable();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public LegerMaster GetLegerById(int id)
        {
            try
            {

                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@LegerId", id);
                return new dalc().GetDataTable_Text("SELECT * FROM LegerMaster with(nolock) WHERE LegerId=@LegerId ", para).ConvertToList<LegerMaster>().FirstOrDefault();

            }
            catch (Exception)
            {

                throw;
            }
        }
        public IQueryable<LegerMaster> DuplicateEditLeger(int LegerId, string LegerName)
        {
            try
            {

                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@LegerId", LegerId);
                para[1] = new SqlParameter().CreateParameter("@LegerName", LegerName);
                return new dalc().GetDataTable_Text("SELECT * FROM LegerMaster with(nolock) WHERE RTRIM(LTRIM(LegerName)) =RTRIM(LTRIM(@LegerName))  AND LegerId<>@LegerId AND IsActive = 1", para).ConvertToList<LegerMaster>().AsQueryable();

            }
            catch (Exception)
            {

                throw;
            }

        }
        public IQueryable<LegerMaster> DuplicateLeger(string LegerName)
        {
            try
            {

                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@LegerName", LegerName);
                return new dalc().GetDataTable_Text("SELECT * FROM LegerMaster with(nolock) WHERE RTRIM(LTRIM(LegerName)) =RTRIM(LTRIM(@LegerName))  AND IsActive = 1", para).ConvertToList<LegerMaster>().AsQueryable();

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
