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
    public class Tax_Repository : ITax_Repository,IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;
        public Tax_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }


        public void AddTax(TaxMaster tax)
        {
            try
            {
                context.TaxMasters.Add(tax);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool CheckTax(TaxMaster obj, bool isUpdate)
        {

            try
            {
                if (isUpdate)
                {
                    //var data = context.TaxMasters.Where(e => e.TaxName.Trim() == obj.TaxName.Trim() && e.TaxId != obj.TaxId && e.IsActive == true).ToList();
                    //return data.Count > 0 ? true : false;
                    SqlParameter[] para = new SqlParameter[2];
                    para[0] = new SqlParameter().CreateParameter("@TaxId", obj.TaxId);
                    para[1] = new SqlParameter().CreateParameter("@TaxName", obj.TaxName);
                    return new dalc().GetDataTable_Text("SELECT * FROM TaxMaster with(nolock) WHERE TaxId<>@TaxId AND RTRIM(LTRIM(TaxName)) = RTRIM(LTRIM(@TaxName)) AND IsActive = 1", para).Rows.Count > 0 ? true : false;

                }
                else
                {
                    //var data = context.TaxMasters.Where(e => e.TaxName.Trim() == obj.TaxName.Trim() && e.IsActive == true).ToList();
                    //return data.Count > 0 ? true : false;

                    SqlParameter[] para = new SqlParameter[1];
                    para[0] = new SqlParameter().CreateParameter("@TaxName", obj.TaxName);
                    return new dalc().GetDataTable_Text("SELECT * FROM TaxMaster with(nolock) WHERE RTRIM(LTRIM(TaxName)) = RTRIM(LTRIM(@TaxName)) AND IsActive = 1", para).Rows.Count > 0 ? true : false;

                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IQueryable<TaxMaster> GetAllTax()
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var tax = context.TaxMasters;
                //    scope.Complete();
                //    return tax.AsQueryable();
                //}
                return new dalc().selectbyquerydt("SELECT * FROM TaxMaster with(nolock) ").ConvertToList<TaxMaster>().AsQueryable();


            }
            catch (Exception)
            {

                throw;
            }

        }

        public TaxMaster GetTaxById(int Taxid)
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var tax = context.TaxMasters.Find(id);
                //    scope.Complete();
                //    return tax;
                //}
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@Taxid", Taxid);
                return new dalc().GetDataTable_Text("SELECT * FROM TaxMaster with(nolock) WHERE Taxid=@Taxid ", para).ConvertToList<TaxMaster>().FirstOrDefault();


            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateTax(TaxMaster tax)
        {
            try
            {
                context.Entry(tax).State = System.Data.Entity.EntityState.Modified;
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
