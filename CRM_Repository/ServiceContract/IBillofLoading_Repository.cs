using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;

namespace CRM_Repository.ServiceContract
{
    public interface IBillofLoading_Repository:IDisposable
    {
         void AddBillofLoading(BillofLoadingMaster obj);
        void UpdateBillofLoading(BillofLoadingMaster obj);
        void DeleteBillofLoading(int id);
        BillofLoadingMaster GetBillofLoadingID(int id);
        IQueryable<BillofLoadingMaster> GetAllBillofLoading();
        IQueryable<BillofLoadingMaster> DuplicateBillofLoading(string ShipperName);
        IQueryable<BillofLoadingMaster> DuplicateEditBillofLoading(int BLId, string ShipperName);
    }
}
