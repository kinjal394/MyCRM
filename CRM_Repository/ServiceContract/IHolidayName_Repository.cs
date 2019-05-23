using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
    public interface IHolidayName_Repository : IDisposable
    {
        void SaveHolidayName(HolidayNameMaster objHolidayName);
        void UpdateHolidayName(HolidayNameMaster objHolidayName);
        void DeleteHolidayName(int HolidayId);
        HolidayNameMaster GetByHolidayId(int HolidayId);
        IQueryable<HolidayNameMaster> GetHolidayName();
        bool IsExist(int HolidayId, string HolidayName);
    }
}
