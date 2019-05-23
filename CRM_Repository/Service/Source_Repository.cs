using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;
using CRM_Repository.ServiceContract;
using System.Transactions;
using System.Data.SqlClient;
using CRM_Repository.DataServices;
namespace CRM_Repository.Service
{
    public class Source_Repository : ISource_Repository, IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;
        public Source_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }
        public void AddSource(SourceMaster obj)
        {
            try
            {
                context.SourceMasters.Add(obj);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

        }
        public SourceMaster GetSourceById(int SourceId)
        {
            try
            {
                //return context.SourceMasters.Find(id);
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@SourceId", SourceId);
                return new dalc().GetDataTable_Text("SELECT * FROM SourceMaster with(nolock) WHERE SourceId=@SourceId AND IsActive = 1", para).ConvertToList<SourceMaster>().FirstOrDefault();

            }
            catch (Exception)
            {

                throw;
            }

        }
        public IQueryable<SourceMaster> DuplicateEditSource(int SourceId, string SourceName)
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var source = context.SourceMasters.Where(x => x.SourceId != SourceId && x.SourceName == SourceName && x.IsActive == true);
                //    scope.Complete();
                //    return source.AsQueryable();
                //}
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@SourceId", SourceId);
                para[1] = new SqlParameter().CreateParameter("@SourceName", SourceName);
                return new dalc().GetDataTable_Text("SELECT * FROM SourceMaster with(nolock) WHERE SourceId <> @SourceId AND RTRIM(LTRIM(SourceName)) = RTRIM(LTRIM(@SourceName)) AND IsActive = 1", para).ConvertToList<SourceMaster>().AsQueryable();

            }
            catch (Exception)
            {

                throw;
            }

        }
        public IQueryable<SourceMaster> DuplicateSource(string SourceName)
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var source = context.SourceMasters.Where(x => x.SourceName == SourceName && x.IsActive == true);
                //    scope.Complete();
                //    return source.AsQueryable();
                //}

                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@SourceName", SourceName);
                return new dalc().GetDataTable_Text("SELECT * FROM SourceMaster with(nolock) WHERE RTRIM(LTRIM(SourceName)) =RTRIM(LTRIM(@SourceName)) AND IsActive = 1", para).ConvertToList<SourceMaster>().AsQueryable();
            }
            catch (Exception)
            {

                throw;
            }

        }
        public void UpdateSource(SourceMaster obj)
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
        public IQueryable<SourceMaster> GetAllSource()
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var source = context.SourceMasters.Where(x => x.IsActive == true);
                //    scope.Complete();
                //    return source.AsQueryable();
                //}
                return new dalc().selectbyquerydt("SELECT * FROM SourceMaster with(nolock) WHERE IsActive = 1").ConvertToList<SourceMaster>().AsQueryable();

            }
            catch
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
