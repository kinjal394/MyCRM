using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
  public interface IInvoiceType_Repository : IDisposable
    {
        void AddInvoiceType(InvoiceTypeMaster ITM);
        IQueryable<InvoiceTypeMaster> DuplicateTInvoiceType(InvoiceTypeMaster ITM);
        void UpdateInvoiceType(InvoiceTypeMaster obj);
        void DeleteInvoiceType(int InvoiceTypeId);
        IQueryable<InvoiceTypeMaster> GetInvoiceTypeById(int InvoiceTypeId);
    }
}
