using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
    public interface IProductVideoMaster_Repository : IDisposable
    {
        void SaveProductVideo(ProductVideoMaster objProductVideoMaster);
        void UpdateProductVideo(ProductVideoMaster objProductVideoMaster);
        void DeleteProductVideo(int VideoId);
        ProductVideoMaster GetByVideoId(int VideoId);
        IQueryable<ProductVideoMaster> GetProductVideo();
        IQueryable<ProductVideoMaster> GetVideoByProductId(int ProductId);
        IQueryable<ProductVideoMaster> GetVideoByProductSupplierId(int ProductId, int CatalogId);
    }
}
