using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;
using CRM_Repository.ServiceContract;
using CRM_Repository.DataServices;
using System.Transactions;
using System.Data.SqlClient;
namespace CRM_Repository.Service
{
    public class VendorBankDetail_Repository : IVendorBankDetail_Repository, IDisposable
    {
        dalc odal = new dalc();
        private CRM_Repository.Data.elaunch_crmEntities context;
        public VendorBankDetail_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void AddVendorBankDetail(VendorBankMaster obj)
        {
            try
            {
                context.VendorBankMasters.Add(obj);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public void UpdateVendorBankDetail(VendorBankMaster obj)
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

        public void DeleteVendorBankDetail(int id)
        {
            try
            {
                VendorBankMaster VendorBank = context.VendorBankMasters.Find(id);
                if (VendorBank != null)
                {
                    context.VendorBankMasters.Remove(VendorBank);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public VendorBankMaster GetVendorBankDetailById(int id)
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var VendorBank = context.VendorBankMasters.Find(id);
                //    scope.Complete();
                //    return VendorBank;
                //}
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@BankDetailId", id);
                return new dalc().GetDataTable_Text("SELECT * FROM VendorBankMaster with(nolock) WHERE BankDetailId=@BankDetailId", para).ConvertToList<VendorBankMaster>().FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public IQueryable<VendorBankMaster> GetAllVendorBankDetail()
        {
            try
            {

                return new dalc().selectbyquerydt("SELECT * FROM VendorBankMaster with(nolock) WHERE IsActive=1").ConvertToList<VendorBankMaster>().AsQueryable();

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public IQueryable<VendorBankMaster> GetByVendorId(int id)
        {
            try
            {
                //return odal.selectbyquerydt(@"Select * from VendorBankMaster with(nolock) Where VendorId = " + id + " And ISNULL(IsActive,0)=1").ConvertToList<VendorBankMaster>().AsQueryable();
                try
                {
                    //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                    //{
                    //    var VendorBank = context.VendorBankMasters.Where(x => x.VendorId == id && x.IsActive == true);
                    //    scope.Complete();
                    //    return VendorBank;
                    //}
                    SqlParameter[] para = new SqlParameter[1];
                    para[0] = new SqlParameter().CreateParameter("@VendorId", id);
                    return new dalc().GetDataTable_Text(@"Select BB.*,BM.BankName,AC.AccountType from VendorBankMaster BB with(nolock)
                                            Inner Join BankNameMaster BM with(nolock) on BM.BankId = BB.BankNameId
                                            Inner Join AccountTypeMaster AC with(nolock) on AC.AccountTypeId = BB.AccountTypeId
                                            where BB.VendorId = @VendorId and BB.IsActive = 1", para).ConvertToList<VendorBankMaster>().AsQueryable();
                }
                catch (Exception ex)
                {
                    throw ex.InnerException;
                }
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
