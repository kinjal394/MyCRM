using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;
using CRM_Repository.DTOModel;

namespace CRM_Repository.ServiceContract
{
    public interface ISalesOrder_Repository : IDisposable
    {
        SalesOrderMaster AddSalesOrder(SalesOrderMaster obj);
        void UpdateSalesOrder(SalesOrderMaster obj);
        void DeleteSalesOrder(int id);
        SalesOrderModel GetSalesOrderById(int id);
        SalesOrderMaster GetSOById(int id);
        IQueryable<SalesOrderMaster> GetAllSalesOrder();
        int CreateUpdate(SalesOrderModel objInputSalesOrderMaster);
        int Delete(SalesOrderModel objInputSalesOrderMaster);
    }
}
