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
    public class ProductPhotoMaster_Repository : IProductPhotoMaster_Repository,IDisposable
    {

        private CRM_Repository.Data.elaunch_crmEntities context;

        public ProductPhotoMaster_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void SaveProductPhoto(ProductPhotoMaster objProductPhotoMaster)
        {
            try
            {
                context.ProductPhotoMasters.Add(objProductPhotoMaster);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateProductPhoto(ProductPhotoMaster objProductPhotoMaster)
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

        public void DeleteProductPhoto(int PhotoId)
        {
            try
            {
                ProductPhotoMaster objProductPhotoMaster = context.ProductPhotoMasters.Where(z => z.PhotoId == PhotoId).SingleOrDefault();
                objProductPhotoMaster.IsActive = false;
                context.Entry(objProductPhotoMaster).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProductPhotoMaster GetByPhotoId(int PhotoId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@PhotoId", PhotoId);
                return new dalc().GetDataTable_Text("SELECT * FROM ProductPhotoMaster with(nolock) WHERE PhotoId = @PhotoId", para).ConvertToList<ProductPhotoMaster>().FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<ProductPhotoMaster> GetProductPhoto()
        {
            try
            {
                return new dalc().selectbyquerydt("SELECT * FROM ProductPhotoMaster with(nolock) ").ConvertToList<ProductPhotoMaster>().AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<ProductPhotoMaster> GetPhotoByProductId(int ProductId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@ProductId", ProductId);
                return new dalc().GetDataTable_Text("SELECT * FROM  ProductPhotoMaster  with(nolock) Where ProductId =@ProductId AND IsActive=1 ", para).ConvertToList<ProductPhotoMaster>().AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<ProductPhotoMaster> GetPhotoByProductSupplierId(int ProductId, int CatalogId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@ProductId", ProductId);
                para[1] = new SqlParameter().CreateParameter("@CatalogId", CatalogId);
                return new dalc().GetDataTable_Text("SELECT * FROM  ProductPhotoMaster  with(nolock) Where ProductId =@ProductId AND CatalogId = @CatalogId AND IsActive=1 ", para).ConvertToList<ProductPhotoMaster>().AsQueryable();
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
