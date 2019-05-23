using CRM_Repository.Data;
using CRM_Repository.DTOModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
    public interface IProductParameterMaster_Repository : IDisposable
    {
        void SaveProductParameter(ProductParameterMaster objProductParameterMaster);
        void UpdateProductParameter(ProductParameterMaster objProductParameterMaster);
        void DeleteProductParameter(int TechDetailId);
        ProductParameterMaster GetByTechDetailId(int TechDetailId);
        IQueryable<ProductParameterMaster> GetProductParameter();
        IQueryable<vmProductParameterMaster> GetTechDetailByProductId(int ProductId);
        IQueryable<vmProductParameterMaster> GetTechDetailByProductSupplierId(int ProductId, int CatalogId);

        //get by Techperaid
        ProductParameterMaster GetByTechparaid(int pid,int TechParaId);
    }
}
