using CRM_Repository.Data;
using CRM_Repository.DataServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using CRM_Repository.ServiceContract;
using System.Data.SqlClient;
namespace CRM_Repository.Service
{
    public class SupplierBankDetail_Repository : ISupplierBankDetail_Repository, IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;
        dalc odal = new dalc();

        public SupplierBankDetail_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void AddSupplierBankDetail(SupplierBankMaster obj)
        {
            try
            {
                context.SupplierBankMasters.Add(obj);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateSupplierBankDetail(SupplierBankMaster obj)
        {
            try
            {
                context.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void DeleteSupplierBankDetail(int id)
        {
            try
            {
                SupplierBankMaster SupplierBank = context.SupplierBankMasters.Find(id);
                if (SupplierBank != null)
                {
                    context.SupplierBankMasters.Remove(SupplierBank);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SupplierBankMaster GetById(int BankDetailId)
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var SupplierBank = context.SupplierBankMasters.Find(BankDetailId);
                //    scope.Complete();
                //    return SupplierBank;
                //}


                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@BankDetailId", BankDetailId);
                return new dalc().GetDataTable_Text("SELECT * FROM SupplierBankMaster with(nolock) WHERE BankDetailId=@BankDetailId ", para).ConvertToList<SupplierBankMaster>().FirstOrDefault();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<SupplierBankMaster> GetAllSupplierBankDetail()
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var SupplierBank = context.SupplierBankMasters;
                //    scope.Complete();
                //    return SupplierBank;
                //}
                return new dalc().selectbyquerydt("SELECT * FROM SupplierBankMaster with(nolock)").ConvertToList<SupplierBankMaster>().AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public IQueryable<SupplierBankMaster> GetBySupplierId(int id)
        //{
        //    try
        //    {
        //        using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
        //        {
        //            var SupplierBankBySupplier = context.SupplierBankMasters.Where(z => z.SupplierId == id && z.IsActive == true);
        //            //var SupplierBankBySupplier = odal.selectbyquerydt(@"Select * from SupplierBankMaster with(nolock)
        //            //                             where SupplierId = '" + id + "' and IsActive='true'").ConvertToList<SupplierBankMaster>().AsQueryable();
        //            scope.Complete();
        //            return SupplierBankBySupplier;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}


        public IQueryable<SupplierBankMaster> GetBySupplierId(int SupplierId)
        {
            dalc odal = new dalc();
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                    SqlParameter[] para = new SqlParameter[1];
                    para[0] = new SqlParameter().CreateParameter("@SupplierId", SupplierId);
                    return odal.GetDataTable_Text(@"Select BB.*,BM.BankName,AC.AccountType from SupplierBankMaster BB with(nolock)
                                            Inner Join BankNameMaster BM with(nolock) on BM.BankId=BB.BankNameId
                                            Inner Join AccountTypeMaster AC with(nolock) on AC.AccountTypeId=BB.AccountTypeId
                                            where BB.SupplierId =@SupplierId and BB.IsActive='true'",para).ConvertToList<SupplierBankMaster>().AsQueryable();
                //    scope.Complete();
                //    return SupplierBankMaster;
                //}
            }
            catch (Exception e)
            {
                throw e;
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
