using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
    public interface IQuotation_Repositroy : IDisposable
    {
        QuotationMaster AddQuotation(QuotationMaster Quotation);
        void UpdateQuotation(QuotationMaster Quotation);
        QuotationMaster GetQuotationById(int id);
        IQueryable<QuotationMaster> FatchQuotationById(int id);
        IQueryable<QuotationMaster> GetAllQuotation();
        int CreateUpdateQuotation(QuotationMaster objQuotationMaster, int userId);
        ProductPrice GetDelarePriceById(int ProductId, int SupplierId);
        // bool CheckQuotationType(QuotationMaster obj, bool isUpdate);
    }
}
