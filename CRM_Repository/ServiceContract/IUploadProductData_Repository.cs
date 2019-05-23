using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;
using CRM_Repository.DTOModel;

namespace CRM_Repository.ServiceContract
{
    public interface IUploadProductData_Repository : IDisposable
    {
        ProductCatalogMaster AddAdvLink(ProductCatalogMaster obj);
        void UpdateAdvLink(ProductCatalogMaster obj);
        void DeleteAdvLink(int id);
        List<UploadProductDataModel> FetchById(int id);
        List<UploadProductDataModel> FetchByCode(string ProductCode);
        ProductCatalogMaster GetAdvLinkById(int id);
        IQueryable<ProductCatalogMaster> GetAllAdvLink();
        int CreateUpdate(UploadProductDataModel objInputAdvLink);
        int Delete(UploadProductDataModel objInputAdvLink);
    }
}
