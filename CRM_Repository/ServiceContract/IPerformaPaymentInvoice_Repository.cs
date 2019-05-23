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
    public interface IPerformaPaymentInvoice_Repository : IDisposable
    {
        void InsertPerformaPayment(PerformaPaymentMaster objPerPay);
        void UpdatePerformaPayment(PerformaPaymentMaster objPerPay);
        IQueryable<PerformaPaymentMaster> GetAllPerformaPayment();
        PerformaPaymentMaster GetPerformaPaymentByID(int Id);
    }
}
