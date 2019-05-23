using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;
using System.Transactions;
using System.Data.SqlClient;
using CRM_Repository.DataServices;
namespace CRM_Repository.Service
{
    public class Currency_Repository : ICurrency_Repository, IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;
        public Currency_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void AddCurrency(CurrencyMaster Currency)
        {
            try
            {
                context.CurrencyMasters.Add(Currency);
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool CheckCurrencyType(CurrencyMaster obj, bool isUpdate)
        {
            try
            {
                if (isUpdate)
                {
                    //var data = context.CurrencyMasters.Where(e => e.CurrencyName.Trim() == obj.CurrencyName.Trim() && e.CurrencyId != obj.CurrencyId && e.IsActive == true).ToList();
                    //return data.Count > 0 ? true : false;
                    SqlParameter[] para = new SqlParameter[2];
                    para[0] = new SqlParameter().CreateParameter("@CurrencyName", obj.CurrencyName);
                    para[1] = new SqlParameter().CreateParameter("@CurrencyId", obj.CurrencyId);
                    return new dalc().GetDataTable_Text("SELECT * FROM CurrencyMaster with(nolock) WHERE RTRIM(LTRIM(CurrencyName))=RTRIM(LTRIM(@CurrencyName)) AND CurrencyId<>@CurrencyId AND IsActive=1", para).Rows.Count > 0 ? true : false;

                }
                else
                {
                    //var data = context.CurrencyMasters.Where(e => e.CurrencyName.Trim() == obj.CurrencyName.Trim() && e.IsActive == true).ToList();
                    //return data.Count > 0 ? true : false;
                    SqlParameter[] para = new SqlParameter[1];
                    para[0] = new SqlParameter().CreateParameter("@CurrencyName", obj.CurrencyName);
                    return new dalc().GetDataTable_Text("SELECT * FROM CurrencyMaster with(nolock) WHERE RTRIM(LTRIM(CurrencyName))=RTRIM(LTRIM(@CurrencyName)) AND  IsActive=1", para).Rows.Count > 0 ? true : false;

                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IQueryable<CurrencyMaster> GetAllCurrency()
        {
            try
            {
               
                return new dalc().selectbyquerydt("SELECT * FROM CurrencyMaster with(nolock) ").ConvertToList<CurrencyMaster>().AsQueryable();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public CurrencyMaster GetCurrencyById(int CurrencyId)
        {
            try
            {
               
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@CurrencyId", CurrencyId);
                return new dalc().GetDataTable_Text("SELECT * FROM CurrencyMaster with(nolock) WHERE CurrencyId=@CurrencyId ", para).ConvertToList<CurrencyMaster>().FirstOrDefault();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateCurrency(CurrencyMaster Currency)
        {
            try
            {
                context.Entry(Currency).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
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
