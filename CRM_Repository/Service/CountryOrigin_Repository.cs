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
    public class CountryOrigin_Repository : ICountryOrigin_Repository, IDisposable
    {
        private elaunch_crmEntities context;
        public CountryOrigin_Repository(elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void InsertOrigin(CountryOfOriginMaster objorigin)
        {
            try
            {
                context.CountryOfOriginMasters.Add(objorigin);
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
          
        }

        public void UpdateOrigin(CountryOfOriginMaster objorigin)
        {
            try
            {
                context.Entry(objorigin).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        public IQueryable<CountryOfOriginMaster> GetAllCountryOfOrigin()
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var CountryOfOrigin = context.CountryOfOriginMasters.Where(x => x.IsActive == true);
                //    scope.Complete();
                //    return CountryOfOrigin;
                //}
                return new dalc().selectbyquerydt("SELECT * FROM CountryOfOriginMaster with(nolock) WHERE  IsActive = 1").ConvertToList<CountryOfOriginMaster>().AsQueryable();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public CountryOfOriginMaster GetOriginID(int OriginId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@OriginId", OriginId);
                return new dalc().GetDataTable_Text("SELECT * FROM CountryOfOriginMaster with(nolock) WHERE OriginId=@OriginId ", para).ConvertToList<CountryOfOriginMaster>().FirstOrDefault();

            }
            catch (Exception)
            {

                throw;
            }

        }

        public IQueryable<CountryOfOriginMaster> DuplicateEditOrigin(int OriginId, string Originname)
        {
            try
            {
               
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@Originname", Originname);
                para[1] = new SqlParameter().CreateParameter("@OriginId", OriginId);
                return new dalc().GetDataTable_Text("SELECT * FROM CountryOfOriginMaster with(nolock) WHERE RTRIM(LTRIM(CountryOfOrigin)) =RTRIM(LTRIM(@Originname))  AND OriginId<>@OriginId AND IsActive = 1", para).ConvertToList<CountryOfOriginMaster>().AsQueryable();

            }
            catch (Exception)
            {

                throw;
            }

        }


        public IQueryable<CountryOfOriginMaster> DuplicateOrigin(string Originname)
        {
            try
            {
                
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@Originname", Originname);
                return new dalc().GetDataTable_Text("SELECT * FROM CountryOfOriginMaster with(nolock) WHERE RTRIM(LTRIM(CountryOfOrigin)) =RTRIM(LTRIM(@Originname))  AND IsActive = 1", para).ConvertToList<CountryOfOriginMaster>().AsQueryable();

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
