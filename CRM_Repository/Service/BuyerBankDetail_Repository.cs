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
    public class BuyerBankDetail_Repository : IBuyerBankDetail_Repository, IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;

        public BuyerBankDetail_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void AddBuyerBankDetail(BuyerBankDetail obj)
        {
            try
            {
                context.BuyerBankDetails.Add(obj);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateBuyerBankDetail(BuyerBankDetail obj)
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

        public void DeleteBuyerBankDetail(int id)
        {
            try
            {
                BuyerBankDetail BuyerBank = context.BuyerBankDetails.Find(id);
                if (BuyerBank != null)
                {
                    context.BuyerBankDetails.Remove(BuyerBank);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BuyerBankDetail GetById(int id)
        {
            try
            {

                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@BankDetailID", id);
                return new dalc().GetDataTable_Text("SELECT * FROM BuyerBankDetail with(nolock) WHERE BankDetailID=@BankDetailID ", para).ConvertToList<BuyerBankDetail>().FirstOrDefault();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<BuyerBankDetail> GetAllBuyerBankDetail()
        {
            try
            {

                return new dalc().selectbyquerydt("SELECT * FROM BuyerBankDetail with(nolock) WHERE  IsActive = 1").ConvertToList<BuyerBankDetail>().AsQueryable();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<BuyerBankDetail> GetByBuyerId(int BuyerId)
        {
            dalc odal = new dalc();
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{

                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@BuyerId", BuyerId);
                return odal.GetDataTable_Text(@"Select BB.*,BM.BankName,AC.AccountType from BuyerBankDetail BB with(nolock)
                                            Inner Join BankNameMaster BM with(nolock) on BM.BankId=BB.BankNameId
                                            Inner Join AccountTypeMaster AC with(nolock) on AC.AccountTypeId=BB.AccountTypeId
                                            where BB.BuyerId =@BuyerId and BB.IsActive='true'", para).ConvertToList<BuyerBankDetail>().AsQueryable();

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
