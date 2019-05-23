using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
    public interface IReceiptVoucher_Repository : IDisposable
    {
        void AddReceiptVoucher(ReceiptVoucherMaster obj);
        void UpdateReceiptVoucher(ReceiptVoucherMaster obj);
        ReceiptVoucherMaster GetReceiptVoucherById(int id);
        IQueryable<ReceiptVoucherMaster> GetAllReceiptVoucher();
        IQueryable<ReceiptVoucherMaster> DuplicateReceiptVoucher(string Type);
        IQueryable<ReceiptVoucherMaster> DuplicateEditReceiptVoucher(int VoucherId, string Type);
    }
}
