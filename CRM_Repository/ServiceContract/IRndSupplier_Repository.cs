using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
    public interface IRndSupplier_Repository:IDisposable
    {
        RNDSupplierMaster GetRndSupplierById(int id);
        List<RNDSupplierMaster> GetRndSupplierByProductId(int id);
        SupplierDetailByIdModel GetCompanyDetailById(int id);
    }
}
