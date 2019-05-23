using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using CRM_Repository.Data;
using CRM_Repository.ServiceContract;
using CRM_Repository.DTOModel;
using CRM_Repository.DataServices;
using System.Data.SqlClient;

namespace CRM_Repository.Service
{
    public class SalesItemDetail_Repository : ISalesItemDetail_Repository , IDisposable
    {
        dalc odal = new dalc();
        private CRM_Repository.Data.elaunch_crmEntities context;
        public SalesItemDetail_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void AddSalesItemDetail(SalesItemMaster obj)
        {
            try
            {
                context.SalesItemMasters.Add(obj);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public void UpdateSalesItemDetail(SalesItemMaster obj)
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

        public void DeleteSalesItemDetail(int id)
        {
            try
            {
                SalesItemMaster SalesItem = context.SalesItemMasters.Find(id);
                if (SalesItem != null)
                {
                    context.SalesItemMasters.Remove(SalesItem);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public IQueryable<vmSalesItemDetail> GetSalesItemDetailById(int SOId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@SOId", SOId);
                return odal.GetDataTable_Text(@"Select SM.*,CO.OriginId,CO.CountryOfOrigin,UM.UnitId,UM.UnitName As QtyCodeData,CM.CurrencyId,CM.CurrencyName As UnitPriceCodeData,PM.ProductId,
                                            PM.ProductName,SC.SubCategoryId,SC.SubCategoryName As SubCategory,CC.CategoryId,CC.CategoryName As Category
                                            from SalesItemMaster As SM With(NOLOCK)
                                            Inner join CountryOfOriginMaster As CO With(NOLOCK) On CO.OriginId = SM.CountryOfOriginId
                                            Inner join UnitMaster As UM With(NOLOCK) On UM.UnitId = SM.QtyCode
                                            Inner join CurrencyMaster As CM With(NOLOCK) On CM.CurrencyId = SM.UnitPriceCode
                                            Inner join ProductMaster As PM With(NOLOCK) On PM.ProductId = SM.ProductId
                                            Inner join SubCategoryMaster As SC With(NOLOCK) On SC.SubCategoryId = PM.SubCategoryId
                                            Inner join CategoryMaster As CC With(NOLOCK) On CC.CategoryId = SC.CategoryId
                                            Where SM.SOId =@SOId And ISNULL(SM.IsActive,0)=1", para).ConvertToList<vmSalesItemDetail>().AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public IQueryable<SalesItemMaster> GetAllSalesItemDetail()
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var SalesItem = context.SalesItemMasters.Where(x => x.IsActive == true);
                //    scope.Complete();
                //    return SalesItem;
                //}
                return new dalc().selectbyquerydt("SELECT * FROM SalesItemMaster with(nolock) WHERE  IsActive = 1").ConvertToList<SalesItemMaster>().AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public IQueryable<SalesItemMaster> GetBySalesOrderId(int SOId)
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var SalesItem = context.SalesItemMasters.Where(z => z.SOId == id && z.IsActive == true);
                //    scope.Complete();
                //    return SalesItem;
                //}
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@SOId", SOId);
                return new dalc().GetDataTable_Text("SELECT * FROM SalesItemMaster with(nolock) WHERE SOId =@SOId AND IsActive = 1", para).ConvertToList<SalesItemMaster>().AsQueryable();

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
