using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data;
using CRM_Repository.DTOModel;

namespace CRM_Repository.ServiceContract
{
    public interface IAttendance_Repository : IDisposable
    {
        int AddDailyWork(DailyWorkReport obj);
        int UpdateDailyWork(DailyWorkReport obj);
        int DeleteDailyWork(int id);
        DailyWorkReport GetDailyWorkByID(int id);
        IQueryable<DailyWorkReport> GetAllDailyWork();
        DataAttendance CreateUpdate(AttendanceModel objWorkDetail);
        IQueryable<DailyWorkReport> GetDetailWorkById(int UserId, int WorkId);
        int InsertUpdateDailyWord(AttendanceModel objWorkDetail, int WorkType = 0);
        DailyWorkReport GetDailyWorkreportByID(string taskinqno, int id,int LoginUser);
        int CreateUpdateDailyWork(DailyWorkReport objWorkDetail);
        DailyWorkReport GetDailyWorkReportingByID(int id);
        DataSet GetDailyReportData(ReportPara obj);
        DataSet GetDailyReport(ReportPara obj);
        IQueryable<DashbordData> GetDashbordData(string condition);
        IQueryable<DashbordStatusData> GetDashbordTaskStatusData(string condition);
        IQueryable<DashbordStatusData> GetDashbordInqStatusData(string condition);
        IQueryable<DashbordVisitorData> GetDashbordCountryVisitorData();
        ChartData GetChartData(int Type,int UserId,int UserRollType);
    }
}
