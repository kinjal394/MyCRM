using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
   public interface IProductLinkMaster_Repository
    {

        void SaveProductLink(ProductLinkMaster objProductLinkMaster);
        void UpdateProductLink(ProductLinkMaster objProductLinkMaster);
        void DeleteProductLink(int AdId);
        ProductLinkMaster GetByLinkId(int AdId);
        IQueryable<ProductLinkMaster> GetProductLink();
        IQueryable<ProductLinkMaster> GetLinkByProductId(int AdId);
        IQueryable<ProductLinkMaster> GetLinkByProductSupplierId(int ProductId, int CatalogId);
    }
}
