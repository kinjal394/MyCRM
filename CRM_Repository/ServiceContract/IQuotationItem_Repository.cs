using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
   public interface IQuotationItem_Repository : IDisposable
    {
        void AddQuotationitem(QuotationItemMaster Quotationitem);
        void UpdateQuotationitem(QuotationItemMaster Quotationitem);
        QuotationItemMaster GetQuotationitemById(int id);
        IQueryable<QuotationItemMaster> GetAllQuotationitem();
        IQueryable<QuotationItemMaster> GetQuotationIteByQuotationId(int id);
    }
}
