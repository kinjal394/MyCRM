using CRM.Models;
using CRM_Repository.Data;
using CRM_Repository.DTOModel;
using CRM_Repository.Service;
using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace CRM.Areas.Master.Controllers
{
    public class DashboardController : Controller
    {
        private IAttendance_Repository _IAttendance_Repository;
        public DashboardController()
        {
            this._IAttendance_Repository = new Attendance_Repository(new elaunch_crmEntities());
        }

        // GET: Master/Dashboard
        public ActionResult Dashboard()
        {
            return View();
        }
        // GET: Master/DashboardPopup
        public ActionResult DashboardPopup()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AttendanceForm()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AttendanceReason()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveDailyWork(AttendanceModel objInputDailyWork)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    objInputDailyWork.UserId = sessionUtils.UserId;
                    objInputDailyWork.IPAdd = Request.UserHostAddress;
                    int resval = _IAttendance_Repository.InsertUpdateDailyWord(objInputDailyWork);
                    if (resval > 0)
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Successfully", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Opps! something wrong", null);
                    }
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "Invalid User", null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Create/Update Buyer");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllDailyWorkInfo(int id)
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    AttendanceModel objAttendanceModel = new AttendanceModel();
                    objAttendanceModel.UserId = sessionUtils.UserId;
                    objAttendanceModel.WorkTypeId = id;
                    objAttendanceModel.DailyWorkDetail = _IAttendance_Repository.GetDetailWorkById(sessionUtils.UserId, id).ToList();
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", new
                    {
                        objAttendanceModel = objAttendanceModel
                    });
                }
                catch (Exception ex)
                {
                    ex.SetLog("Get Buyer by Id");
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                }
            }
            else
            {
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "Invalid User", null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetDashbordData()
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    string condition = "", taskCondition = "", inqCondition = "";
                    if (sessionUtils.UserRollType != 1)
                    {
                        condition = " AND CreatedBy = " + sessionUtils.UserId;
                        taskCondition = " AND ToId = " + sessionUtils.UserId;
                        inqCondition = " AND AssignId = " + sessionUtils.UserId;
                    }
                    var NoofData = _IAttendance_Repository.GetDashbordData(condition).ToList();
                    var TaskStatusData = _IAttendance_Repository.GetDashbordTaskStatusData(taskCondition).ToList();
                    var InqStatusData = _IAttendance_Repository.GetDashbordInqStatusData(inqCondition).ToList();
                    var VisitorData = _IAttendance_Repository.GetDashbordCountryVisitorData().ToList();
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", new
                    {
                        NoofData = NoofData,
                        TaskStatusData = TaskStatusData,
                        InqStatusData = InqStatusData,
                        VisitorData = VisitorData
                    });
                }
                catch (Exception ex)
                {
                    ex.SetLog("Get Dashbord Data");
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                }
            }
            else
            {
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "Invalid User", null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetChartDetails(int id)
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                  
                    ChartData ds = new ChartData();
                    ds = _IAttendance_Repository.GetChartData(id, sessionUtils.UserId, sessionUtils.UserRollType);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", ds);
                }
                catch (Exception ex)
                {
                    ex.SetLog("Get Buyer by Id");
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                }
            }

            else
            {
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "Invalid User", null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
    }
}