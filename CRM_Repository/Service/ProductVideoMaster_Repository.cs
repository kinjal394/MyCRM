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
    public class ProductVideoMaster_Repository : IProductVideoMaster_Repository, IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;

        public ProductVideoMaster_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void SaveProductVideo(ProductVideoMaster objProductVideoMaster)
        {
            try
            {
                context.ProductVideoMasters.Add(objProductVideoMaster);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateProductVideo(ProductVideoMaster objProductVideoMaster)
        {
            try
            {
                context.Entry(objProductVideoMaster).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteProductVideo(int VideoId)
        {
            try
            {
                ProductVideoMaster objProductVideoMaster = context.ProductVideoMasters.Where(z => z.VideoId == VideoId).SingleOrDefault();
                objProductVideoMaster.IsActive = false;
                context.Entry(objProductVideoMaster).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProductVideoMaster GetByVideoId(int VideoId)
        {
            try
            {

                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@VideoId", VideoId);
                return new dalc().GetDataTable_Text("SELECT * FROM ProductVideoMaster with(nolock) WHERE VideoId = @VideoId", para).ConvertToList<ProductVideoMaster>().FirstOrDefault();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<ProductVideoMaster> GetProductVideo()
        {
            try
            {

                return new dalc().selectbyquerydt("SELECT * FROM ProductVideoMaster with(nolock) ").ConvertToList<ProductVideoMaster>().AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<ProductVideoMaster> GetVideoByProductId(int ProductId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@ProductId", ProductId);
                return new dalc().GetDataTable_Text("SELECT * FROM  ProductVideoMaster  with(nolock) Where ProductId =@ProductId AND IsActive=1 ", para).ConvertToList<ProductVideoMaster>().AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<ProductVideoMaster> GetVideoByProductSupplierId(int ProductId, int CatalogId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@ProductId", ProductId);
                para[1] = new SqlParameter().CreateParameter("@CatalogId", CatalogId);
                return new dalc().GetDataTable_Text("SELECT * FROM  ProductVideoMaster  with(nolock) Where ProductId =@ProductId AND CatalogId = @CatalogId AND IsActive=1 ", para).ConvertToList<ProductVideoMaster>().AsQueryable();
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
