using CRM_Repository.Data;
using CRM_Repository.DTOModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
    public interface IProduct_Repository : IDisposable
    {
        void saveProduct(ProductMaster objProductMaster);
        void UpdateProduct(ProductMaster objProductMaster);
        int UpdateProductDetail(ProductMaster objProductMaster, int UserId);
        void DeleteProduct( int ProductId, int Userid);
        ProductMaster GetProductById(int ProductId);
        IQueryable<ProductMaster> GetAllProducts();
        IQueryable<ProductMaster> GetProductBySubCategoryId(int id);
        bool IsExist(int ProductId, string ProductName, int SubCategoryId);
        DataTable ProductData(int ProductId);
        ProductFormModel FetchById(int id);
        int UpdateProductFrom(ProductFormModel objProductFormModel);
        IQueryable<ProductPrice> GetProductPriceById(int ProductId,int catelogId);
        IQueryable<ProductApplicableCharge> GetProductApplicableChargeById(int ProductId);
    }
}
