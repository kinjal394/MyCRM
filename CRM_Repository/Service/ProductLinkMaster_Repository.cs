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
    public class ProductLinkMaster_Repository : IProductLinkMaster_Repository, IDisposable
    {
        dalc odal = new dalc();
        private CRM_Repository.Data.elaunch_crmEntities context;

        public ProductLinkMaster_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void SaveProductLink(ProductLinkMaster objProductLinkMaster)
        {
            try
            {
                context.ProductLinkMasters.Add(objProductLinkMaster);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateProductLink(ProductLinkMaster objProductPhotoMaster)
        {
            try
            {
                context.Entry(objProductPhotoMaster).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteProductLink(int AdId)
        {
            try
            {
                ProductLinkMaster objProductLinkMaster = context.ProductLinkMasters.Where(z => z.AdId == AdId).SingleOrDefault();
                objProductLinkMaster.IsActive = false;
                context.Entry(objProductLinkMaster).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProductLinkMaster GetByLinkId(int AdId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@AdId", AdId);
                return new dalc().GetDataTable_Text("SELECT * FROM ProductLinkMaster with(nolock) WHERE AdId = @AdId", para).ConvertToList<ProductLinkMaster>().FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<ProductLinkMaster> GetProductLink()
        {
            try
            {
                return new dalc().selectbyquerydt("SELECT * FROM ProductLinkMaster with(nolock) ").ConvertToList<ProductLinkMaster>().AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<ProductLinkMaster> GetLinkByProductId(int ProductId)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                {
                    SqlParameter[] para = new SqlParameter[1];
                    para[0] = new SqlParameter().CreateParameter("@ProductId", ProductId);
                    return odal.GetDataTable_Text(" SELECT pl.*,ad.SiteName [SourceName] FROM gurjari_crmuser.ProductLinkMaster pl INNER JOIN gurjari_crmuser.AdvertisementSourceMaster ad ON ad.SiteId = pl.AdSourceId where pl.ProductId =@ProductId AND pl.IsActive=1",para).ConvertToList<ProductLinkMaster>().AsQueryable();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<ProductLinkMaster> GetLinkByProductSupplierId(int ProductId, int CatalogId)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                {
                    SqlParameter[] para = new SqlParameter[2];
                    para[0] = new SqlParameter().CreateParameter("@ProductId", ProductId);
                    para[1] = new SqlParameter().CreateParameter("@CatalogId", CatalogId);
                    return odal.GetDataTable_Text(" SELECT pl.*,ad.SiteName [SourceName] FROM gurjari_crmuser.ProductLinkMaster pl INNER JOIN gurjari_crmuser.AdvertisementSourceMaster ad ON ad.SiteId = pl.AdSourceId where pl.ProductId =@ProductId And pl.CatalogId=@CatalogId AND pl.IsActive=1", para).ConvertToList<ProductLinkMaster>().AsQueryable();
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
