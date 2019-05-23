using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;

namespace CRM_Repository.ServiceContract
{
    public interface IPaymentMode_Repository: IDisposable
    {
        void AddPaymentMode(PaymentModeMaster obj);
        void UpdatePaymentMode(PaymentModeMaster obj);
        void DeletePaymentMode(int id);
        PaymentModeMaster GetPaymentModeByID(int id);
        IQueryable<PaymentModeMaster> GetAllPaymentMode();
        IQueryable<PaymentModeMaster> DuplicatePaymentMode(string PaymentMode);
        IQueryable<PaymentModeMaster> DuplicateEditPaymentMode(int PaymentModeId, string PaymentMode);
    }
}
