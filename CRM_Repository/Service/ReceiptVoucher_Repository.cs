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
    public class ReceiptVoucher_Repository : IReceiptVoucher_Repository, IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;
        public ReceiptVoucher_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }
        public void AddReceiptVoucher(ReceiptVoucherMaster obj)
        {
            try
            {
                context.ReceiptVoucherMasters.Add(obj);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

        }
        public ReceiptVoucherMaster GetReceiptVoucherById(int id)
        {
            try
            {
                // return context.ReceiptVoucherMasters.Find(id);

                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@VoucherId", id);
                return new dalc().GetDataTable_Text("SELECT * FROM ReceiptVoucherMaster with(nolock) WHERE VoucherId = @VoucherId", para).ConvertToList<ReceiptVoucherMaster>().FirstOrDefault();

            }
            catch (Exception)
            {

                throw;
            }

        }

        public IQueryable<ReceiptVoucherMaster> DuplicateEditReceiptVoucher(int VoucherId, string Type)
        {
            //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
            //{
            //    var Voucher = context.ReceiptVoucherMasters.Where(x => x.VoucherId != VoucherId && x.Type == Type && x.IsActive == true);
            //    scope.Complete();
            //    return Voucher.AsQueryable();
            //}
            try
            {
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@VoucherId", VoucherId);
                para[1] = new SqlParameter().CreateParameter("@Type", Type);
                return new dalc().GetDataTable_Text("SELECT * FROM ReceiptVoucherMaster with(nolock) WHERE VoucherId <> @VoucherId AND RTRIM(LTRIM(Type)) = RTRIM(LTRIM(@Type)) AND IsActive = 1", para).ConvertToList<ReceiptVoucherMaster>().AsQueryable();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IQueryable<ReceiptVoucherMaster> DuplicateReceiptVoucher(string Type)
        {
            //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
            //{
            //    var Voucher = context.ReceiptVoucherMasters.Where(x => x.Type == Type && x.IsActive == true);
            //    scope.Complete();
            //    return Voucher.AsQueryable();
            //}
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@Type", Type);
                return new dalc().GetDataTable_Text("SELECT * FROM ReceiptVoucherMaster with(nolock) WHERE RTRIM(LTRIM(Type)) = RTRIM(LTRIM(@Type)) AND IsActive = 1", para).ConvertToList<ReceiptVoucherMaster>().AsQueryable();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void UpdateReceiptVoucher(ReceiptVoucherMaster obj)
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

        public IQueryable<ReceiptVoucherMaster> GetAllReceiptVoucher()
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var Voucher = context.ReceiptVoucherMasters.Where(x => x.IsActive == true);
                //    scope.Complete();
                //    return Voucher.AsQueryable();
                //}
                return new dalc().selectbyquerydt("SELECT * FROM ReceiptVoucherMaster with(nolock) Where IsActive=1 ").ConvertToList<ReceiptVoucherMaster>().AsQueryable();
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
