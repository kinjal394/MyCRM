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
    public class AccountEntry_Repository : IAccountEntry_Repository, IDisposable
    {
        private elaunch_crmEntities context;
        public AccountEntry_Repository(elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void InsertAccountEntry(AssetsExpenseMaster objaccount)
        {
            try
            {
                context.AssetsExpenseMasters.Add(objaccount);
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public void UpdateAccountEntry(AssetsExpenseMaster objaccount)
        {
            try
            {
                context.Entry(objaccount).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception)
            { 
                throw;
            }

        }

        public AssetsExpenseMaster GetAccountEntryByID(int AccountId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@AccountId", AccountId);
                return new dalc().GetDataTable_Text("SELECT * FROM AssetsExpenseMaster with(nolock) WHERE AccountId=@AccountId", para).ConvertToList<AssetsExpenseMaster>().FirstOrDefault(); 
            }
            catch (Exception)
            {

                throw;
            }

        }

        public IQueryable<AssetsExpenseMaster> GetAllAccountEntry()
        {
            try
            {
                return new dalc().selectbyquerydt("SELECT * FROM AssetsExpenseMaster with(nolock) WHERE  IsActive = 1").ConvertToList<AssetsExpenseMaster>().AsQueryable();
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
