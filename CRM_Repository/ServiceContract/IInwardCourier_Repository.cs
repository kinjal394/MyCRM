using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
    public interface IInwardCourier_Repository : IDisposable
    {
        void SaveInwardCourier(InwardCourierMaster objInwardCourier);
        void UpdateInwardCourier(InwardCourierMaster objInwardCourier);
        void DeleteInwardCourier(int CourierId,int UserId);
        InwardCourierMaster GetByCourierId(int CourierId);
        IQueryable<InwardCourierMaster> GetInwardCourier();
        InwardCourierMaster FetchAllInfoById(int CourierId);

    }
}
