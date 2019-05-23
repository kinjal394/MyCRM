using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;

namespace CRM_Repository.ServiceContract
{
    public interface ITransactionType_Repository:IDisposable
    {
        void AddTransactionType(TransactionTypeMaster obj);
        void UpdateTransactionType(TransactionTypeMaster obj);
        void DeleteTransactionType(int id);
        TransactionTypeMaster GetTransactionTypeByID(int id);
        IQueryable<TransactionTypeMaster> GetAllTransactionType();
        IQueryable<TransactionTypeMaster> DuplicateTransactionType(string TransactionType);
        IQueryable<TransactionTypeMaster> DuplicateTransactionType(int TranTypeId, string TransactionType);
    }
}
