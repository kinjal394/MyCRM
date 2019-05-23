using CRM_Repository.Data;
using CRM_Repository.DTOModel;
using CRM_Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
    public interface IPerformaInvoice_Repository : IDisposable
    {

        PerformaInvoiceMaster AddPerformaInvoice(PerformaInvoiceMaster PIM);
        PerformaInvoiceMaster GetAllPerformaInvoiceById(int Id);
        IQueryable<PerformaProductMaster> GetByPerfomaInvId(int Id);
        IQueryable<PerformaPaymentMaster> GetByPerfomaPaymentInvId(int Id);
        PerformaInvoiceMaster GetPerformaInvoiceById(int Id);
        void UpdatePerformaInvoice(PerformaInvoiceMaster PIM);
        void DeletePerformaInvoice(int id, int UserId);
        IQueryable<PerformaInvoiceMaster> GetAllPerformaInvoice();
        PerformaInvoiceMaster CreateUpdate(PerformaInvoiceMaster objInputPerforma);
    }
}
