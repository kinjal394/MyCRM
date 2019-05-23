using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;
using CRM_Repository.DTOModel;

namespace CRM_Repository.ServiceContract
{
    public interface ISalesTechnicalDetail_Repository : IDisposable
    {
        void AddSalesTechnicalDetail(SalesTechnicalDetailMaster obj);
        void UpdateSalesTechnicalDetail(SalesTechnicalDetailMaster obj);
        void DeleteSalesTechnicalDetail(int id);
        IQueryable<vmSalesTechnicalDetail> GetSalesTechnicalDetailById(int id);
        IQueryable<SalesTechnicalDetailMaster> GetAllSalesTechnicalDetail();
        IQueryable<SalesTechnicalDetailMaster> GetBySalesItemId(int id);
        SalesTechnicalDetailMaster GetSalesTechnicalSpecialById(int itemId, int techId);
    }
}
