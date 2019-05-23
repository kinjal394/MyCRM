using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
    public interface ISalesPurchaseEntry_Repository: IDisposable
    {
        void InsertSalePurchaseEntry(SalesPurchaseEntryMaster objsalespurchase);
        void UpdateSalePurchaseEntry(SalesPurchaseEntryMaster objsalespurchase);
        IQueryable<SalesPurchaseEntryMaster> GetAllSalePurchaseEntry();
        SalesPurchaseEntryMaster GetSalePurchaseEntryByID(int SalesPurchaseId);
        int CreateUpdateSalePurchaseEntry(SalesPurchaseEntryMaster objsalespurchase);
        IQueryable<SalesPurchaseDocumentMaster> GetSalePurchaseDocBySaleID(int SalesPurchaseId);
        int DeleteSalePurchaseEntry(SalesPurchaseEntryMaster objsalespurchase);
    }
}
