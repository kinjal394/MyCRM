using CRM_Repository.Data;
using CRM_Repository.DataServices;
using CRM_Repository.DTOModel;
using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CRM_Repository.Service
{
    public class Attendance_Repository : IAttendance_Repository, IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;
        private IUser_Repository _IUser_Repository;
        public Attendance_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
            this._IUser_Repository = new User_Repository(_context);
        }

        public Attendance_Repository()
        {

        }

        public int AddDailyWork(DailyWorkReport obj)
        {
            try
            {
                context.DailyWorkReports.Add(obj);
                context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public int UpdateDailyWork(DailyWorkReport obj)
        {
            try
            {
                context.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return 2;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public int DeleteDailyWork(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@DailyWorkId", id);
                DailyWorkReport DailyWork = new dalc().GetDataTable_Text("SELECT * FROM DailyWorkReport with(nolock) WHERE DailyWorkId=@DailyWorkId", para).ConvertToList<DailyWorkReport>().FirstOrDefault();
                if (DailyWork != null)
                {
                    context.DailyWorkReports.Remove(DailyWork);
                    context.SaveChanges();
                    return 3;
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public DailyWorkReport GetDailyWorkByID(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@DailyWorkId", id);
                return new dalc().GetDataTable_Text("SELECT * FROM DailyWorkReport with(nolock) WHERE DailyWorkId=@DailyWorkId", para).ConvertToList<DailyWorkReport>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public IQueryable<DailyWorkReport> GetAllDailyWork()
        {
            try
            {
                return new dalc().selectbyquerydt(@"SELECT * FROM DailyWorkReport with(nolock)").ConvertToList<DailyWorkReport>().AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public DataAttendance CreateUpdate(AttendanceModel objWorkDetail)
        {
            DataAttendance dataResponse = new DataAttendance();
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    #region "Attendancd Insert"
                    //WorkTypeId : 1-Work Start,2-Work End,3-Lunch Start,4-Lunch End
                    int time = objWorkDetail.WorkTypeId;
                    AttendanceMaster atnd = _IUser_Repository.GetAttendancebyUserid(objWorkDetail.UserId, DateTime.Now);
                    string IPAddress = objWorkDetail.IPAdd;
                    AttendanceMaster objAttendance;
                    string mode = "", msg = "";
                    if (atnd == null)
                    {
                        objAttendance = new AttendanceMaster();
                        objAttendance.UserId = objWorkDetail.UserId;
                        objAttendance.OnDate = DateTime.Now;
                        objAttendance.IsActive = true;
                        mode = "ADD";
                        if (objAttendance.WorkStartTime == null && time == 1)
                        {
                            msg = "Work is not started for today.";
                            dataResponse = CommonFunctions.GenerateAttResponse(WorkType.WorkStart, true, msg);
                            //return Json(new { ok = true, message = msg, WorkType = time, isPopup = isPopup }, JsonRequestBehavior.AllowGet);
                        }
                        else if (objAttendance.LunchStartTime == null && time == 4)
                        {
                            msg = "Lunch is not started for today.";
                            dataResponse = CommonFunctions.GenerateAttResponse(WorkType.LunchEnd, true, msg);
                            //return Json(new { ok = true, message = msg, WorkType = time, isPopup = isPopup }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        objAttendance = atnd;
                        mode = "EDIT";
                        if (objAttendance.WorkStartTime == null && time == 2)
                        {
                            msg = "Work is not started for today.";
                            dataResponse = CommonFunctions.GenerateAttResponse(WorkType.WorkEnd, true, msg);
                            //return Json(new { ok = true, message = msg, WorkType = time, isPopup = isPopup }, JsonRequestBehavior.AllowGet);
                        }
                        else if (objAttendance.LunchStartTime == null && time == 4)
                        {
                            msg = "Lunch is not started for today.";
                            dataResponse = CommonFunctions.GenerateAttResponse(WorkType.LunchEnd, true, msg);
                            //return Json(new { ok = true, message = msg, WorkType = time, isPopup = isPopup }, JsonRequestBehavior.AllowGet);
                        }
                    }

                    if (time == 1)
                    {
                        if (objAttendance.WorkStartTime == null)
                        {
                            objAttendance.WorkStartTime = DateTime.Now.TimeOfDay;
                            objAttendance.WorkStartIP = IPAddress;
                            msg = "Work is Started.";
                        }
                        else
                            msg = "Work is already Started for today";
                    }
                    else if (time == 2)
                    {
                        if (objAttendance.WorkEndTime == null)
                        {
                            objAttendance.WorkEndTime = DateTime.Now.TimeOfDay;
                            objAttendance.WorkEndIP = IPAddress;
                            msg = "Work is Ended";
                        }
                        else
                            msg = "Work is already ended for today.";
                    }
                    else if (time == 3)
                    {
                        if (objAttendance.LunchStartTime == null)
                        {
                            objAttendance.LunchStartTime = DateTime.Now.TimeOfDay;
                            objAttendance.LunchStartIP = IPAddress;
                            msg = "Lunch is started.";
                        }
                        else
                            msg = "Lunch is already started for today.";
                    }
                    else if (time == 4)
                    {
                        if (objAttendance.LunchEndTime == null)
                        {
                            objAttendance.LunchEndTime = DateTime.Now.TimeOfDay;
                            objAttendance.LunchEndIP = IPAddress;
                            msg = "Lunch is ended.";
                        }
                        else
                            msg = "Lunch is already ended for today.";
                    }
                    else
                    {
                        dataResponse = CommonFunctions.GenerateAttResponse(WorkType.Error, false, "Error.");
                        //return Json(new { ok = false, message = "Error.", WorkType = time, isPopup = isPopup }, JsonRequestBehavior.AllowGet);
                    }

                    if (mode == "ADD")
                    {
                        _IUser_Repository.AddAttendance(objAttendance);
                        InsertUpdateDailyWord(objWorkDetail, time);
                        dataResponse = CommonFunctions.GenerateAttResponse(WorkType.WorkStart, true, msg);
                        //return Json(new { ok = true, message = msg, WorkType = time, isPopup = isPopup }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        _IUser_Repository.UpdateAttendance(objAttendance);
                        InsertUpdateDailyWord(objWorkDetail, time);
                        if (time == 2)
                            dataResponse = CommonFunctions.GenerateAttResponse(WorkType.WorkEnd, true, msg);
                        else if (time == 3)
                            dataResponse = CommonFunctions.GenerateAttResponse(WorkType.LunchStart, true, msg);
                        else if (time == 4)
                            dataResponse = CommonFunctions.GenerateAttResponse(WorkType.WorkEnd, true, msg);
                        //return Json(new { ok = true, message = msg, WorkType = time, isPopup = isPopup }, JsonRequestBehavior.AllowGet);
                    }
                    #endregion
                    context.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                    dataResponse = CommonFunctions.GenerateAttResponse(WorkType.Error, false, "Error.");
                }
                return dataResponse;
            }
        }

        public int CreateUpdateDailyWork(DailyWorkReport objWorkDetail)
        {
            int resVal;
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    //DailyWorkId,UserId,Date,TaskInqId,TaskType,Remark,Persontage,StartTime,EndTime,Title,Description,StatusId
                    DailyWorkReport ObjDailyWorkDetail = new DailyWorkReport();

                    if (objWorkDetail.DailyWorkId <= 0)
                    {
                        #region "INSERT"
                        ObjDailyWorkDetail.DailyWorkId = objWorkDetail.DailyWorkId;
                        ObjDailyWorkDetail.UserId = objWorkDetail.UserId;
                        ObjDailyWorkDetail.Date = objWorkDetail.Date;
                        ObjDailyWorkDetail.TaskInqId = objWorkDetail.TaskInqId;
                        ObjDailyWorkDetail.TaskType = objWorkDetail.TaskType;
                        ObjDailyWorkDetail.Remark = objWorkDetail.Remark;
                        ObjDailyWorkDetail.Persontage = objWorkDetail.Persontage;
                        ObjDailyWorkDetail.StartTime = objWorkDetail.StartTime;
                        ObjDailyWorkDetail.EndTime = objWorkDetail.EndTime;
                        ObjDailyWorkDetail.Title = objWorkDetail.Title;
                        ObjDailyWorkDetail.Description = objWorkDetail.Description;
                        ObjDailyWorkDetail.StatusId = objWorkDetail.StatusId;
                        context.DailyWorkReports.Add(ObjDailyWorkDetail);
                        #endregion
                        resVal = 1;
                    }
                    else
                    {
                        #region "Update"
                        ObjDailyWorkDetail = context.DailyWorkReports.Find(objWorkDetail.DailyWorkId);
                        ObjDailyWorkDetail.UserId = objWorkDetail.UserId;
                        ObjDailyWorkDetail.Date = objWorkDetail.Date;
                        ObjDailyWorkDetail.TaskInqId = objWorkDetail.TaskInqId;
                        ObjDailyWorkDetail.TaskType = objWorkDetail.TaskType;
                        ObjDailyWorkDetail.Remark = objWorkDetail.Remark;
                        ObjDailyWorkDetail.Persontage = objWorkDetail.Persontage;
                        ObjDailyWorkDetail.StartTime = objWorkDetail.StartTime;
                        ObjDailyWorkDetail.EndTime = objWorkDetail.EndTime;
                        ObjDailyWorkDetail.Title = objWorkDetail.Title;
                        ObjDailyWorkDetail.Description = objWorkDetail.Description;
                        ObjDailyWorkDetail.StatusId = objWorkDetail.StatusId;
                        context.Entry(ObjDailyWorkDetail).State = System.Data.Entity.EntityState.Modified;
                        #endregion
                        resVal = 2;
                    }
                    // Update Main Tables
                    if (objWorkDetail.TaskType == 1)
                    {
                        TaskMaster objtask = context.TaskMasters.Find(objWorkDetail.TaskInqId);
                        objtask.Status = Convert.ToInt32(objWorkDetail.StatusId);
                        context.Entry(objtask).State = System.Data.Entity.EntityState.Modified;
                    }
                    else if (objWorkDetail.TaskType == 2)
                    {
                        InquiryMaster objinq = context.InquiryMasters.Find(objWorkDetail.TaskInqId);
                        objinq.Status = Convert.ToInt32(objWorkDetail.StatusId);
                        context.Entry(objinq).State = System.Data.Entity.EntityState.Modified;
                    }

                    context.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                    resVal = 0;
                }
                return resVal;
            }
        }

        public int InsertUpdateDailyWord(AttendanceModel objWorkDetail, int WorkType)
        {
            try
            {
                //if (WorkType == 1 || WorkType == 3)
                //{
                #region "INSERT"
                if (objWorkDetail.DailyWorkDetail != null)
                {
                    foreach (var item in objWorkDetail.DailyWorkDetail)
                    {
                        TaskMaster objtask = context.TaskMasters.Find(item.TaskInqId);
                        //objtask.Status = item.TaskStatus;
                        context.Entry(objtask).State = System.Data.Entity.EntityState.Modified;

                        DailyWorkReport ObjDailyWorkDetail = new DailyWorkReport();
                        ObjDailyWorkDetail.DailyWorkId = item.DailyWorkId;
                        ObjDailyWorkDetail.UserId = objWorkDetail.UserId;
                        ObjDailyWorkDetail.Date = DateTime.Now;
                        ObjDailyWorkDetail.TaskInqId = item.TaskInqId;
                        ObjDailyWorkDetail.TaskType = 1;
                        //  ObjDailyWorkDetail.AttandanceType = 1;
                        ObjDailyWorkDetail.Remark = item.Remark;
                        ObjDailyWorkDetail.Persontage = item.Persontage;
                        context.DailyWorkReports.Add(ObjDailyWorkDetail);
                    }
                    context.SaveChanges();
                }
                #endregion
                //}
                //else if (WorkType == 2 || WorkType == 4)
                //{
                //    #region "UPDATE"
                //    if (objWorkDetail.DailyWorkDetail != null)
                //    {
                //        foreach (var item in objWorkDetail.DailyWorkDetail)
                //        {
                //            DailyWorkReport ObjDailyWorkDetail = context.DailyWorkReports.Find(item.DailyWorkId);
                //            ObjDailyWorkDetail.DailyWorkId = item.DailyWorkId;
                //            ObjDailyWorkDetail.UserId = objWorkDetail.UserId;
                //            ObjDailyWorkDetail.Date = DateTime.Now;
                //            ObjDailyWorkDetail.TaskInqId = item.TaskInqId;
                //            ObjDailyWorkDetail.TaskType = item.TaskType;
                //            ObjDailyWorkDetail.AttandanceType = item.AttandanceType;
                //            ObjDailyWorkDetail.Remark = item.Remark;
                //            ObjDailyWorkDetail.Persontage = item.Persontage;
                //            context.Entry(ObjDailyWorkDetail).State = System.Data.Entity.EntityState.Modified;
                //        }
                //    }
                //    #endregion
                //}
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IQueryable<DailyWorkReport> GetDetailWorkById(int UserId, int WorkId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@UserId", UserId);
                //para[1] = new SqlParameter().CreateParameter("@WorkId", WorkId);
                return new dalc().GetDataTable_Text(@"Select * from DailyWorkReport  Where  UserId= @UserId 
                                                    AND [Date] = Convert(Date,getdate())", para).ConvertToList<DailyWorkReport>().AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public IQueryable<DashbordData> GetDashbordData(string condition)
        {
            try
            {
                string str = @"SELECT 'InquiryMaster' AS tblName, COUNT(*) As noofRecord FROM InquiryMaster Where IsActive = 1 " + condition +
                            " UNION SELECT 'TaskMaster' AS tblName, COUNT(*) As noofRecord FROM TaskMaster Where IsActive = 1 " + condition +
                            " UNION SELECT 'BuyerMaster' AS tblName, COUNT(*) As noofRecord FROM BuyerMaster Where IsActive = 1 " + condition +
                            " UNION SELECT 'ProductMaster' AS tblName, COUNT(*) As noofRecord FROM ProductMaster Where IsActive = 1 " + condition;

                return new dalc().selectbyquerydt(str).ConvertToList<DashbordData>().AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public IQueryable<DashbordStatusData> GetDashbordTaskStatusData(string condition)
        {
            try
            {
                return new dalc().selectbyquerydt(@" SELECT TM.TaskStatus As [Status],TS.TaskStatus
                                                , COUNT(T.TaskId) as TotalTask,
                                                ((COUNT(T.TaskId) * 100) / (SELECT Count(1)
                                                FROM TaskMaster T
                                                left join TaskDetailMaster TM on TM.TaskId = T.TaskId 
                                                left join TaskStatusMaster TS on TS.StatusId = TM.TaskStatus
                                                Where TS.TaskStatus is not null  And T.IsActive = 1 " + condition +
                                                @" AND TM.TaskDetailId = 
                                                (Select  Max(TMM.TaskDetailId) As TaskDetailId from TaskMaster TT
                                                Left join TaskDetailMaster TMM On TT.TaskId = TMM.TaskId
                                                Where TMM.TaskId = T.TaskId And TMM.IsActive = 1  " + condition +
                                                @" )))  As Percentage
                                                FROM TaskMaster T
                                                left join TaskDetailMaster TM on TM.TaskId = T.TaskId 
                                                left join TaskStatusMaster TS on TS.StatusId = TM.TaskStatus
                                                Where TS.TaskStatus is not null  And T.IsActive = 1 " + condition +
                                                @" AND TM.TaskDetailId = 
                                                (Select  Max(TMM.TaskDetailId) As TaskDetailId from TaskMaster TT
                                                Left join TaskDetailMaster TMM On TT.TaskId = TMM.TaskId
                                                Where TMM.TaskId = T.TaskId And TMM.IsActive = 1 " + condition +
                                                @" )
                                                GROUP BY TM.TaskStatus,TS.TaskStatus 
                                                Order by [Status] ").ConvertToList<DashbordStatusData>().AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public IQueryable<DashbordStatusData> GetDashbordInqStatusData(string condition)
        {
            try
            {
                return new dalc().selectbyquerydt(@" SELECT TM.[Status],TS.TaskStatus
                                                    , COUNT(T.InqId) as TotalTask,
                                                    ((COUNT(T.InqId) * 100) / (SELECT Count(1)
                                                    FROM InquiryMaster T
                                                    left join InquiryFollowupMaster TM on TM.InqId = T.InqId 
                                                    left join TaskStatusMaster TS on TS.StatusId = TM.[Status]
                                                    Where TS.TaskStatus is not null  And T.IsActive = 1 " + condition +
                                                    @" AND TM.FollowupId = 
                                                    (Select  Max(TMM.FollowupId) As FollowupId from InquiryMaster TT
                                                    Left join InquiryFollowupMaster TMM On TT.InqId = TMM.InqId
                                                    Where TMM.InqId = T.InqId And TMM.IsActive = 1 " + condition +
                                                    @" )))  As Percentage
                                                    FROM InquiryMaster T
                                                    left join InquiryFollowupMaster TM on TM.InqId = T.InqId 
                                                    left join TaskStatusMaster TS on TS.StatusId = TM.[Status]
                                                    Where TS.TaskStatus is not null  And T.IsActive = 1 " + condition +
                                                    @" AND TM.FollowupId = 
                                                    (Select  Max(TMM.FollowupId) As TaskDetailId from InquiryMaster TT
                                                    Left join InquiryFollowupMaster TMM On TT.InqId = TMM.InqId
                                                    Where TMM.InqId = T.InqId And TMM.IsActive = 1 " + condition +
                                                    @" )
                                                    GROUP BY TM.[Status],TS.TaskStatus 
                                                    Order by [Status] ").ConvertToList<DashbordStatusData>().AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public IQueryable<DashbordVisitorData> GetDashbordCountryVisitorData()
        {
            try
            {
                return new dalc().selectbyquerydt(@" Select Top 5
                                                C.CountryId,C.CountryName , Count(1) As TotalVisitor,
                                                ((Count(1) * 100) / (Select Count(1) from BuyerAddressDetail)) As Percentage
                                                from BuyerAddressDetail As BC  with(nolock)
                                                Left join CityMaster As D  with(nolock) on BC.CityId = D.CityId
                                                Left join StateMaster As S  with(nolock) on S.StateId = D.StateId
                                                Left join CountryMaster As C  with(nolock) on C.CountryId = S.CountryId
                                                Group By C.CountryId,C.CountryName
                                                Order by TotalVisitor Desc ").ConvertToList<DashbordVisitorData>().AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public DataSet GetDailyReportData(ReportPara obj)
        {
            dalc odal = new dalc();
            DataSet ds = new DataSet();
            int ID = 0;
            try
            {
                ID = Convert.ToInt32(obj.ID);
            }
            catch
            {
                ID = 0;
            }
            SqlParameter[] para = new SqlParameter[9];
            para[0] = new SqlParameter().CreateParameter("@ID", ID);
            para[1] = new SqlParameter().CreateParameter("@UserId", obj.UserId);
            para[2] = new SqlParameter().CreateParameter("@UserType", obj.UserType);
            para[3] = new SqlParameter().CreateParameter("@A", obj.A);
            para[4] = new SqlParameter().CreateParameter("@B", obj.B);
            para[5] = new SqlParameter().CreateParameter("@C", obj.C);
            para[6] = new SqlParameter().CreateParameter("@D", obj.D);
            para[7] = new SqlParameter().CreateParameter("@E", obj.E);
            para[8] = new SqlParameter().CreateParameter("@F", obj.F);
            ds = odal.GetDataset("Daily_Report", para);
            ds.Tables[0].TableName = "DailyRepotingSystemMain";
            return ds;

        }
        public DataSet GetDailyReport(ReportPara obj)
        {
            dalc odal = new dalc();
            DataSet ds = new DataSet();
            int ID = 0;
            try
            {
                ID = Convert.ToInt32(obj.ID);
            }
            catch
            {
                ID = 0;
            }
            SqlParameter[] para = new SqlParameter[9];
            para[0] = new SqlParameter().CreateParameter("@ID", ID);
            para[1] = new SqlParameter().CreateParameter("@UserId", obj.UserId);
            para[2] = new SqlParameter().CreateParameter("@UserType", obj.UserType);
            para[3] = new SqlParameter().CreateParameter("@A", obj.A);
            para[4] = new SqlParameter().CreateParameter("@B", obj.B);
            para[5] = new SqlParameter().CreateParameter("@C", obj.C);
            para[6] = new SqlParameter().CreateParameter("@D", obj.D);
            para[7] = new SqlParameter().CreateParameter("@E", obj.E);
            para[8] = new SqlParameter().CreateParameter("@F", obj.F);
            ds = odal.GetDataset("Daily_Report_System", para);
            ds.Tables[0].TableName = "DailyReportSystem";
            return ds;

        }
        public DailyWorkReport GetDailyWorkreportByID(string taskinqno, int id, int LoginUser)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[2];
                if (id == 1)
                {
                    para[0] = new SqlParameter().CreateParameter("@TaskNo", taskinqno);
                    para[1] = new SqlParameter().CreateParameter("@UserId", LoginUser);
                    return new dalc().GetDataTable_Text(@"select TS.TaskId[TaskInqId],TS.Task[Title],MS.Note[Description] 
                                                          from taskmaster as TS 
                                                          inner join taskdetailmaster as MS on MS.TaskId=TS.TaskId
                                                          where RTRIM(LTRIM(TS.TaskNo))=RTRIM(LTRIM(@TaskNo)) AND TS.CreatedBy=@UserId", para).ConvertToList<DailyWorkReport>().FirstOrDefault();
                }
                else if (id == 2)
                {
                    para[0] = new SqlParameter().CreateParameter("@InqNo", taskinqno);
                    para[1] = new SqlParameter().CreateParameter("@UserId", LoginUser);
                    return new dalc().GetDataTable_Text("select InqId[TaskInqId],BuyerName[Title],Requirement[Description] from inquirymaster As IM with(nolock) WHERE InqNo=@InqNo AND IM.CreatedBy=@UserId", para).ConvertToList<DailyWorkReport>().FirstOrDefault();
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public DailyWorkReport GetDailyWorkReportingByID(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@DailyWorkId", id);
                return new dalc().GetDataTable_Text(@"Select DW.DailyWorkId,DW.UserId,U.Name,DW.[Date],DW.TaskInqId,DW.Title,DW.Description,DW.StatusId,TS.TaskStatus,
                                                    DW.Remark, DW.Persontage, DW.StartTime, DW.EndTime,DW.TaskType,
                                                    Case When DW.TaskType = 1 Then T.TaskNo else I.InqNo end As TaskInqNo,
                                                    Case When DW.TaskType > 1 Then
                                                    Case When DW.TaskType = 2 Then 'Inquiry' Else 'Other' end
                                                    else 'Task' end As  TaskTypeName
                                                    from DailyWorkReport As DW WITH(nolock)
                                                    Left Join UserMaster As U WITH(nolock) On DW.UserId = U.UserId
                                                    Left Join TaskStatusMaster As TS WITH(nolock) On TS.StatusId = DW.StatusId
                                                    Left Join TaskMaster As T WITH(nolock) On T.TaskId = DW.TaskInqId
                                                    Left Join InquiryMaster As I WITH(nolock) On I.InqId = DW.TaskInqId
                                                    Where DW.DailyWorkId = @DailyWorkId", para).ConvertToList<DailyWorkReport>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public ChartData GetChartData(int Type,int UserId, int UserRollType)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[3];
                para[0] = new SqlParameter().CreateParameter("@UserId", UserId);
                para[1] = new SqlParameter().CreateParameter("@ChartType", Type);
                para[2] = new SqlParameter().CreateParameter("@UserRollType", UserRollType);
                DataTable dt = new dalc().GetDataTable("GetDashboardChartData", para);
                
                ChartData objChart = new ChartData();
                List<string> chartCategories = new List<string>();
                List<decimal> inqData = new List<decimal>();
                List<decimal> quoData = new List<decimal>();
                List<decimal> poData = new List<decimal>();
                List<decimal> soData = new List<decimal>();
                foreach (DataRow dr in dt.Rows)
                {
                    inqData.Add(Convert.ToDecimal(dr["Inquiry"]));
                    quoData.Add(Convert.ToDecimal(dr["Quotation"]));
                    poData.Add(Convert.ToDecimal(dr["PO"]));
                    soData.Add(Convert.ToDecimal(dr["SO"]));
                    chartCategories.Add(Convert.ToString(dr["CalanderDate"]));
                }
                objChart.categories = chartCategories;
                objChart.chartDataList = new List<ChartList>();
                objChart.chartDataList.Add(new ChartList() { name="Inquiry",data=inqData });
                objChart.chartDataList.Add(new ChartList() { name = "Quotation", data = quoData });
                objChart.chartDataList.Add(new ChartList() { name = "Purchase Order", data = poData });
                objChart.chartDataList.Add(new ChartList() { name = "Sales Order", data = soData });
                return objChart;

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }



        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    context.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }

   
}
