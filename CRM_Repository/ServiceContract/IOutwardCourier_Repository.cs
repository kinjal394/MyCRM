using CRM_Repository.Data;
using CRM_Repository.DTOModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
    public interface IOutwardCourier_Repository : IDisposable
    {
        void SaveOutwardCourier(OutwardCourierMaster objOutwardCourier);
        void UpdateOutwardCourier(OutwardCourierMaster objOutwardCourier);
        void DeleteOutwardCourier(int CourierId,int UserId);
        int CreateUpdate(OutwardCourierModel objOutwardCourierModel);
        OutwardCourierModel FetchAllInfoById(int CourierId);
        OutwardCourierMaster GetByCourierId(int CourierId);
        IQueryable<OutwardCourierMaster> GetOutwardCourier();
    }
}
