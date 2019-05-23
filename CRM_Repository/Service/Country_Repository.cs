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
    public class Country_Repository : ICountry_Repository, IDisposable
    {
        private elaunch_crmEntities context;
        public Country_Repository(elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void InsertCountry(CountryMaster objcountry)
        {
            try
            {
                context.CountryMasters.Add(objcountry);
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void UpdateCountry(CountryMaster objcountry)
        {
            try
            {
                context.Entry(objcountry).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public CountryMaster GetCountryByID(int CountryID)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@CountryID", CountryID);
                return new dalc().GetDataTable_Text("SELECT * FROM CountryMaster with(nolock) WHERE CountryID=@CountryID", para).ConvertToList<CountryMaster>().FirstOrDefault();

            }
            catch (Exception)
            {

                throw;
            }

        }

        public IQueryable<CountryMaster> CheckForDuplicateCountry(int CountryID, string CountryName)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@CountryName", CountryName);
                para[1] = new SqlParameter().CreateParameter("@CountryId", CountryID);
                return new dalc().GetDataTable_Text("SELECT * FROM CountryMaster with(nolock) WHERE RTRIM(LTRIM(CountryName)) = RTRIM(LTRIM(@CountryName)) AND CountryId <> @CountryId AND IsActive = 1", para).ConvertToList<CountryMaster>().AsQueryable();

            }
            catch (Exception)
            {

                throw;
            }

        }

        public IQueryable<CountryMaster> GetAllCountry()
        {
            try
            {
                return new dalc().selectbyquerydt("SELECT * FROM CountryMaster with(nolock) WHERE  IsActive = 1").ConvertToList<CountryMaster>().AsQueryable();
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
