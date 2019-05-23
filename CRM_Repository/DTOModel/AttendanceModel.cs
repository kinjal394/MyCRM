using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.Data
{
    public class AttendanceModel
    {
        public int UserId { get; set; }
        public int WorkTypeId { get; set; }
        public string IPAdd { get; set; }
        public List<DailyWorkReport> DailyWorkDetail { get; set; }
    }

    public enum WorkType
    {
        Error = 0,
        WorkStart = 1,
        WorkEnd = 2,
        LunchStart = 3,
        LunchEnd = 4
    }
    public class DataAttendance
    {
        public WorkType WorkTypeId { get; set; }  //1: Work Start, 2: Work End, 3: Lunch Start, 4: Lunch End
        public bool IsCheck { get; set; }
        public string Message { get; set; }
    }
    public partial class DailyWorkReport
    {
        public string TaskStatus { get; set; }
        public string Name { get; set; }
        public string TaskInqNo { get; set; }
        public string TaskTypeName { get; set; }
    }
    public class DashbordData
    {
        public string tblName { get; set; }
        public int noofRecord { get; set; }
    }

    public class DashbordStatusData
    {
        public int Status { get; set; }
        public string TaskStatus { get; set; }
        public int TotalTask { get; set; }
        public int Percentage { get; set; }
    }

    public class DashbordVisitorData
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int TotalVisitor { get; set; }
        public int Percentage { get; set; }
    }
   
}
