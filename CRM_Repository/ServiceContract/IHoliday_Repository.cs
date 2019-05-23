using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
    public interface IHoliday_Repository : IDisposable
    {

        void AddHoliday(HolidayMaster holiday);
        void UpdateHoliday(HolidayMaster holiday);
        HolidayMaster GetHolidayById(int id);
        IQueryable<HolidayMaster> GetAllHoliday();
        bool CheckHoliday(HolidayMaster obj);
        IQueryable<HolidayNameMaster> GetAllHolidayName();
    }
}
