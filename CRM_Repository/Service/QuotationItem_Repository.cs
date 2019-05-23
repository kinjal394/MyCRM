using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;
using System.Transactions;
using System.Data.Entity.Validation;
using CRM_Repository.DataServices;
using System.Data.SqlClient;

namespace CRM_Repository.Service
{
    public class QuotationItem_Repository : IQuotationItem_Repository, IDisposable
    {
        dalc odal = new dalc();
        private CRM_Repository.Data.elaunch_crmEntities context;
        public QuotationItem_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void AddQuotationitem(QuotationItemMaster Quotationitem)
        {
            try
            {
                context.QuotationItemMasters.Add(Quotationitem);
                context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        public IQueryable<QuotationItemMaster> GetQuotationIteByQuotationId(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@QuotationId", id);
                return odal.GetDataTable_Text(@"Select QIM.*,(ISNULL(ExRate,0) * ISNULL(QIM.Total,0)) AS ExRatePrice  ,QIM.ProductId,PM.ProductName
,SCM.SubCategoryId,SCM.SubcategoryName[SubCategory],CM.CategoryId,CM.CategoryName[Category]
,UM.UnitName[QtyCodeName],CR.CurrencyName  AS DealerPriseName,
--CR.CurrencyName[OfferPriceName],
SP.ProductModelNo As SupplierModelName
from QuotationItemMaster QIM With(NOLOCK)
Inner join QuotationMaster QM With(NOLOCK) on QIM.QuotationId=QM.QuotationId
Inner join ProductMaster PM With(NOLOCK) on PM.ProductId=QIM.ProductId
Inner join ProductCatalogMaster SP With(NOLOCK) on SP.ProductId=QIM.ProductId  AND SP.CatalogId =QIM.SupplierId
Inner join SubCategoryMaster SCM With(NOLOCK) on PM.SubCategoryId=SCM.SubCategoryId
Inner join CategoryMaster CM With(NOLOCK) on SCM.CategoryId=CM.CategoryId
--Inner join CurrencyMaster CR With(NOLOCK) On CR.CurrencyId = QIM.OfferPriceCode
Inner join UnitMaster UM With(NOLOCK) on UM.UnitId=QIM.QtyCode
left join ProductPrices PP With(NOLOCK) on PP.ProductPriceId=QM.CurrencyId
left join CurrencyMaster CR With(NOLOCK) ON  CR.CurrencyId = PP.CurrencyId
where QIM.IsActive='true' and QIM.QuotationId=@QuotationId", para).ConvertToList<QuotationItemMaster>().AsQueryable();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IQueryable<QuotationItemMaster> GetAllQuotationitem()
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                {
                    var Quotationitem = context.QuotationItemMasters;
                    scope.Complete();
                    return Quotationitem.AsQueryable();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public QuotationItemMaster GetQuotationitemById(int id)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                {
                    var Quotationitem = context.QuotationItemMasters.Find(id);
                    scope.Complete();
                    return Quotationitem;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateQuotationitem(QuotationItemMaster Quotationitem)
        {
            try
            {
                context.Entry(Quotationitem).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
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
