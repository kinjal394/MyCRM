using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;

namespace CRM_Repository.ServiceContract
{
    public interface IPaymentTerms_Repository : IDisposable
    {
        void AddPaymentTerms(PaymentTermsMaster obj);
        void UpdatePaymentTerms(PaymentTermsMaster obj);
        PaymentTermsMaster GetPaymentTermsByID(int id);
        IQueryable<PaymentTermsMaster> DuplicatePaymentTerms(string TermName);
        IQueryable<PaymentTermsMaster> DuplicateEditPaymentTerms(int PaymentTermId, string TermName);
        IQueryable<PaymentTermsMaster> GetAllPaymentTerms();
    }
}
