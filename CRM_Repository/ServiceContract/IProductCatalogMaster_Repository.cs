using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
    public interface IProductCatalogMaster_Repository : IDisposable
    {
        ProductCatalogMaster SaveProductCatalog(ProductCatalogMaster objProductCatalogMaster);
        void UpdateProductCatalog(ProductCatalogMaster objProductCatalogMaster);
        ProductPrice SaveProductPrice(ProductPrice objProductPrice);
        void UpdateProductPrice(ProductPrice objProductPrice);
        void DeleteProductCatalog(int PhotoId);
        ProductCatalogMaster GetByCatalogId(int PhotoId);
        IQueryable<ProductCatalogMaster> GetProductCatalog();
        IQueryable<ProductCatalogMaster> GetCatalogByProductId(int ProductId);
        IQueryable<ProductCatalogMaster> GetCatalogByProductSupplierId(int ProductId, int CatalogId);
        IQueryable<ProductPackingDetail> GetPackingByProductSupplierId(int ProductId, int CatalogId);
        bool CheckProductModenolDuplication(string Obj);
        IQueryable<ProductSuppDocumentDetail> GetSuppDocumentbyId(int ProductId);
        IQueryable<ProductSuppDocumentDetail> GetSuppDocumentByProductSuppId(int ProductId, int CatalogId);
        bool CheckSupplierModelNolDuplication(string Obj);
    }
}
