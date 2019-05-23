using CRM.Models;
using CRM_Repository.Data;
using CRM_Repository.Service;
using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Areas.Master.Controllers
{
    public class DailyReportingController : Controller
    {
        private IAttendance_Repository _IAttendance_Repository;
        public DailyReportingController()
        {
            this._IAttendance_Repository = new Attendance_Repository(new elaunch_crmEntities());
        }
        // GET: Master/DailyReporting
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DailyReport(int id = 0, int temp = 0)
        {
            ViewBag.id = id;
            ViewBag.isdisable = temp;
            return View();
        }

        [HttpPost]
        public JsonResult CreateUpdate(DailyWorkReport objInputDailyWork)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    objInputDailyWork.UserId = sessionUtils.UserId;
                    int resval = _IAttendance_Repository.CreateUpdateDailyWork(objInputDailyWork);
                    if (resval == 1)
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully", null);
                    }
                    else if (resval == 2)
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully", null);
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
                ex.SetLog("Create/Update DailyWork");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDailyWorkreportByID(string taskinqno, int id)
        {
            DataResponse dataResponse = new DataResponse();

            var LoginUser = sessionUtils.UserId;
            try
            {
                DailyWorkReport resval = _IAttendance_Repository.GetDailyWorkreportByID(taskinqno,id, LoginUser);
                if(resval!=null)
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", resval);

                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Task is not Allocated", null);
                    
                }
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get Daily Work Report.");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult GetDailyWorkReportingByID(int id)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                DailyWorkReport resval = _IAttendance_Repository.GetDailyWorkReportingByID(id);
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", resval);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get Daily Work Report.");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }

        }

    }
}