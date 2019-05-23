using CRM_Repository.Data;
using CRM_Repository.DataServices;
using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.Service
{
    public class PerformaPaymentInvoice_Repository : IPerformaPaymentInvoice_Repository , IDisposable
    {
        private elaunch_crmEntities context;
        public PerformaPaymentInvoice_Repository(elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void InsertPerformaPayment(PerformaPaymentMaster objPerPay)
        {
            try
            {
                context.PerformaPaymentMasters.Add(objPerPay);
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public void UpdatePerformaPayment(PerformaPaymentMaster objPerPay)
        {
            try
            {
                context.Entry(objPerPay).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public PerformaPaymentMaster GetPerformaPaymentByID(int Id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@Id", Id);
                return new dalc().GetDataTable_Text("SELECT * FROM PerformaPaymentMaster with(nolock) WHERE PerfomaPaymentId=@Id", para).ConvertToList<PerformaPaymentMaster>().FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<PerformaPaymentMaster> GetAllPerformaPayment()
        {
            try
            {
                return new dalc().selectbyquerydt("SELECT * FROM PerformaPaymentMaster with(nolock) WHERE  IsActive = 1").ConvertToList<PerformaPaymentMaster>().AsQueryable();
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
