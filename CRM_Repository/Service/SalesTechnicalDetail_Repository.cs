using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using CRM_Repository.Data;
using CRM_Repository.ServiceContract;
using CRM_Repository.DataServices;
using CRM_Repository.DTOModel;
using System.Data.SqlClient;

namespace CRM_Repository.Service
{
    public class SalesTechnicalDetail_Repository : ISalesTechnicalDetail_Repository ,IDisposable
    {
        dalc odal = new dalc();
        private CRM_Repository.Data.elaunch_crmEntities context;
        public SalesTechnicalDetail_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void AddSalesTechnicalDetail(SalesTechnicalDetailMaster obj)
        {
            try
            {
                context.SalesTechnicalDetailMasters.Add(obj);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public void UpdateSalesTechnicalDetail(SalesTechnicalDetailMaster obj)
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

        public void DeleteSalesTechnicalDetail(int id)
        {
            try
            {
                SalesTechnicalDetailMaster SalesTechnical = context.SalesTechnicalDetailMasters.Find(id);
                if (SalesTechnical != null)
                {
                    context.SalesTechnicalDetailMasters.Remove(SalesTechnical);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public IQueryable<vmSalesTechnicalDetail> GetSalesTechnicalDetailById(int ItemId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@ItemId", ItemId);
                return odal.GetDataTable_Text(@"Select *,2 As Status from SalesTechnicalDetailMaster With(NOLOCK)
                                            Where ItemId =@ItemId And ISNULL(IsActive,0)=1",para).ConvertToList<vmSalesTechnicalDetail>().AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public IQueryable<SalesTechnicalDetailMaster> GetAllSalesTechnicalDetail()
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var SalesTechnical = context.SalesTechnicalDetailMasters.Where(x => x.IsActive == true);
                //    scope.Complete();
                //    return SalesTechnical;
                //}
                return new dalc().selectbyquerydt("SELECT * FROM SalesTechnicalDetailMaster with(nolock) WHERE  IsActive = 1").ConvertToList<SalesTechnicalDetailMaster>().AsQueryable();

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public IQueryable<SalesTechnicalDetailMaster> GetBySalesItemId(int ItemId)
        {
            try
            {
                
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@ItemId", ItemId);
                return new dalc().GetDataTable_Text("SELECT * FROM SalesTechnicalDetailMaster with(nolock) WHERE ItemId=@ItemId AND IsActive = 1", para).ConvertToList<SalesTechnicalDetailMaster>().AsQueryable();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public SalesTechnicalDetailMaster GetSalesTechnicalSpecialById(int itemId,int techId)
        {
            try
            {
                
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@techId", techId);
                para[1] = new SqlParameter().CreateParameter("@itemId", itemId);
                return new dalc().GetDataTable_Text("SELECT * FROM SalesTechnicalDetailMaster with(nolock) WHERE TechDetailId = @techId AND itemId = @itemId AND IsActive = 1", para).ConvertToList<SalesTechnicalDetailMaster>().FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
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
