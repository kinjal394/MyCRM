using CRM_Repository.Data;
using CRM_Repository.DataServices;
using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Data.SqlClient;
namespace CRM_Repository.Service
{
    public class BuyerChatDetail_Repository : IBuyerChatDetail_Repository, IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;
        dalc odal = new dalc();

        public BuyerChatDetail_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void AddBuyerChatDetail(BuyerChatDetail obj)
        {
            try
            {
                context.BuyerChatDetails.Add(obj);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public void UpdateBuyerChatDetail(BuyerChatDetail obj)
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

        public void DeleteBuyerChatDetail(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@BuyerChatId", id);
                BuyerChatDetail BuyerChat = odal.GetDataTable_Text(@"Select * from BuyerChatDetail with(nolock)
                                                 where BuyerChatId = @BuyerChatId and IsActive='true'",para).ConvertToList<BuyerChatDetail>().FirstOrDefault();
                if (BuyerChat != null)
                {
                    context.BuyerChatDetails.Remove(BuyerChat);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public BuyerChatDetail GetById(int BuyerChatId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@BuyerChatId", BuyerChatId);
                return odal.GetDataTable_Text(@"Select * from BuyerChatDetail with(nolock)
                                                 where BuyerChatId =@BuyerChatId and IsActive='true'", para).ConvertToList<BuyerChatDetail>().FirstOrDefault();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<BuyerChatDetail> GetAllBuyerChatDetail()
        {
            try
            {

                return odal.selectbyquerydt(@"Select * from BuyerChatDetail with(nolock)
                                                 where IsActive='true'").ConvertToList<BuyerChatDetail>().AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<BuyerChatDetail> GetByBuyerId(int BuyerId)
        {
            try
            {

                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@BuyerId", BuyerId);
                return odal.GetDataTable_Text(@"Select BD.*,CM.ChatName from BuyerChatDetail BD with(nolock)
                                                 Inner join ChatNameMaster CM with(nolock) on CM.ChatId=BD.ChatId
                                                 where BD.BuyerId =@BuyerId and BD.IsActive='true'", para).ConvertToList<BuyerChatDetail>().AsQueryable();

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
