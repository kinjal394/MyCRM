using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
    public interface IProductPhotoMaster_Repository : IDisposable
    {
        void SaveProductPhoto(ProductPhotoMaster objProductPhotoMaster);
        void UpdateProductPhoto(ProductPhotoMaster objProductPhotoMaster);
        void DeleteProductPhoto(int PhotoId);
        ProductPhotoMaster GetByPhotoId(int PhotoId);
        IQueryable<ProductPhotoMaster> GetProductPhoto();
        IQueryable<ProductPhotoMaster> GetPhotoByProductId(int ProductId);
        IQueryable<ProductPhotoMaster> GetPhotoByProductSupplierId(int ProductId, int CatalogId);
    }
}
