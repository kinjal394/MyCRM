using CRM_Repository.Data;
using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

using System.Data.SqlClient;
using CRM_Repository.DataServices;

namespace CRM_Repository.Service
{
   public class InvoiceType_Repository : IInvoiceType_Repository, IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;

        public InvoiceType_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void AddInvoiceType (InvoiceTypeMaster ITM)
        {
            try {
                context.InvoiceTypeMasters.Add(ITM);
                context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public void UpdateInvoiceType(InvoiceTypeMaster obj)
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

        public void DeleteInvoiceType(int InvoiceTypeId)
        {
            try
            {
                InvoiceTypeMaster TOS = context.InvoiceTypeMasters.Single(X => X.InvoiceTypeId == InvoiceTypeId);
                TOS.IsActive = false;
                context.Entry(TOS).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IQueryable<InvoiceTypeMaster> GetInvoiceTypeById(int id)
        {
            try
            {
               
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@InvoiceTypeId", id);
                return new dalc().GetDataTable_Text("SELECT * FROM InvoiceTypeMaster with(nolock) WHERE  InvoiceTypeId = @InvoiceTypeId AND IsActive = 1", para).ConvertToList<InvoiceTypeMaster>().AsQueryable();

            }
            catch (Exception)
            {

                throw;
            }

        }
        public IQueryable<InvoiceTypeMaster> DuplicateTInvoiceType(InvoiceTypeMaster ITM)
        {
            try
            {
               
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@InvoiceTypeName", ITM.InvoiceTypeName);
                para[1] = new SqlParameter().CreateParameter("@InvoiceTypeId", ITM.InvoiceTypeId);
                return new dalc().GetDataTable_Text("SELECT * FROM InvoiceTypeMaster with(nolock) WHERE  InvoiceTypeId <>@InvoiceTypeId AND RTRIM(LTRIM(InvoiceTypeName)) = RTRIM(LTRIM(@InvoiceTypeName))  AND IsActive = 1", para).ConvertToList<InvoiceTypeMaster>().AsQueryable();


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
