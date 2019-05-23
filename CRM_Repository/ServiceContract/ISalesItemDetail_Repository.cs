using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;
using CRM_Repository.DTOModel;

namespace CRM_Repository.ServiceContract
{
    public interface ISalesItemDetail_Repository : IDisposable
    {
        void AddSalesItemDetail(SalesItemMaster obj);
        void UpdateSalesItemDetail(SalesItemMaster obj);
        void DeleteSalesItemDetail(int id);
        IQueryable<vmSalesItemDetail> GetSalesItemDetailById(int id);
        IQueryable<SalesItemMaster> GetAllSalesItemDetail();
        IQueryable<SalesItemMaster> GetBySalesOrderId(int id);
    }
}
