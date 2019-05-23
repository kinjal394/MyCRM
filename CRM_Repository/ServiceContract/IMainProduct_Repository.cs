using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
    public interface IMainProduct_Repository : IDisposable
    {
        void SaveMainProduct(MainProductMaster mainProduct);
        void UpdateMainProduct(MainProductMaster product);
        void DeleteMainProduct(int UserId, int mainProductId);
        bool IsProductExist(MainProductMaster mainProduct, string mode);
        MainProductMaster GetProductById(int productId);
        IQueryable<MainProductMaster> GetMainProducts();
        IQueryable<MainProductMaster> GetProductBySubcategoryId(int subcatId);
    }
}
