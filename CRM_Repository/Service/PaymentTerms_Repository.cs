using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using CRM_Repository.Data;
using CRM_Repository.ServiceContract;

using System.Data.SqlClient;
using CRM_Repository.DataServices;

namespace CRM_Repository.Service
{
    public class PaymentTerms_Repository : IPaymentTerms_Repository, IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;

        public PaymentTerms_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void AddPaymentTerms(PaymentTermsMaster obj)
        {
            try
            {
                context.PaymentTermsMasters.Add(obj);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public void UpdatePaymentTerms(PaymentTermsMaster obj)
        {
            try
            {
                context.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public PaymentTermsMaster GetPaymentTermsByID(int PaymentTermId)
        {
            try
            {
               
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@PaymentTermId", PaymentTermId);
                return new dalc().GetDataTable_Text("SELECT * FROM PaymentTermsMaster with(nolock) WHERE PaymentTermId = @PaymentTermId ", para).ConvertToList<PaymentTermsMaster>().FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public IQueryable<PaymentTermsMaster> GetAllPaymentTerms()
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var paymentTerms = context.PaymentTermsMasters;
                //    scope.Complete();
                //    return paymentTerms.AsQueryable();
                //}
                return new dalc().selectbyquerydt("SELECT * FROM PaymentTermsMaster with(nolock) WHERE IsActive = 1").ConvertToList<PaymentTermsMaster>().AsQueryable();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public IQueryable<PaymentTermsMaster> DuplicatePaymentTerms(string TermName)
        {
            try
            {
               
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@TermName", TermName);
                return new dalc().GetDataTable_Text("SELECT * FROM PaymentTermsMaster with(nolock) WHERE  RTRIM(LTRIM(TermName)) = RTRIM(LTRIM(@TermName)) AND IsActive = 1", para).ConvertToList<PaymentTermsMaster>().AsQueryable();

            }
            catch (Exception)
            {

                throw;
            }

        }

        public IQueryable<PaymentTermsMaster> DuplicateEditPaymentTerms(int PaymentTermId, string TermName)
        {
            try
            {
               
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@PaymentTermId", PaymentTermId);
                para[1] = new SqlParameter().CreateParameter("@TermName", TermName);
                return new dalc().GetDataTable_Text("SELECT * FROM PaymentTermsMaster with(nolock) WHERE PaymentTermId <> @PaymentTermId AND RTRIM(LTRIM(TermName)) = RTRIM(LTRIM(@TermName)) AND IsActive = 1", para).ConvertToList<PaymentTermsMaster>().AsQueryable();

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
