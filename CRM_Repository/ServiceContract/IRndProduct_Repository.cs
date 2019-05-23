using CRM_Repository.Data;
using CRM_Repository.ExtendedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
    public interface IRndProduct_Repository : IDisposable
    {
        RNDProductModel GetProductById(int id);
        //void SaveProduct(RNDProductModel modal,List<Data.RNDSupplierMaster> supplierList);
        int CreateUpdate(RNDProductModel objInputRNDProductMaster);
        int Delete(RNDProductModel objInputRNDProductMaster);
        RNDProductMaster AddRNDProduct(RNDProductMaster obj);
        void UpdateRNDProduct(RNDProductMaster obj);
        RNDProductModel GetAllRndProductInfoById(int Id);
        IQueryable<RNDSupplierMaster> GetAllRNDSupplierDetail(int Id);
    }
}
