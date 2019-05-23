using CRM.App_Start;
using CRM.Models;
using CRM_Repository.Data;
using CRM_Repository.DTOModel;
using CRM_Repository.Service;
using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Areas.Employee.Controllers
{
    [HasLoginSessionFilter]
    public class WorkProfileController : Controller
    {
        private IWorkProfile_Repository _IWorkProfile_Repository;

        public WorkProfileController()
        {
            this._IWorkProfile_Repository = new WorkProfile_Repository(new elaunch_crmEntities());
        }
        // GET: Employee/WorkProfile
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddWorkProfile()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveWorkProfile(WorkProfileMaster obj)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {

                DateTime datevalue = DateTime.Parse(obj.WorkDate.ToString(), CultureInfo.InvariantCulture);
                WorkProfileMaster WPObj = new WorkProfileMaster();
                WPObj.WorkProfileId = obj.WorkProfileId;
                WPObj.DepartmentId = obj.DepartmentId;
                WPObj.Title = obj.Title;
                WPObj.Description = obj.Description;
                WPObj.WorkTime = obj.WorkTime;
                WPObj.WorkDay = datevalue.ToString("dddd");
                WPObj.WorkDate = obj.WorkDate;
                WPObj.WorkCycle = obj.WorkCycle;
                WPObj.IsActive = true;

                if (WPObj.WorkProfileId > 0)
                {
                    //var cntList = _IWorkProfile_Repository.DuplicateEditWorkProfile(agencytypeObj.WorkProfileId, agencytypeObj.WorkProfile).ToList();
                    //if (cntList.Count == 0)
                    //{
                        _IWorkProfile_Repository.UpdateWorkProfile(WPObj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully.", null);
                    //}
                    //else
                    //{
                    //    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "WorkProfile Name Already Exists.", null);
                    //}
                }
                else
                {
                    //var cntList = _IWorkProfile_Repository.DuplicateWorkProfile(agencytypeObj.WorkProfile).ToList();
                    //if (cntList.Count == 0)
                    //{
                        _IWorkProfile_Repository.AddWorkProfile(WPObj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully.", null);
                    //}
                    //else
                    //{
                    //    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "WorkProfile Name Already Exists.", null);
                    //}
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Create/Update Agency Type");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteWorkProfile(int WorkProfileId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                WorkProfileMaster mainObj = new WorkProfileMaster();
                WorkProfileModle WPObj = _IWorkProfile_Repository.GetWorkProfileById(WorkProfileId);
                mainObj.WorkProfileId = WPObj.WorkProfileId;
                mainObj.IsActive = false;
                _IWorkProfile_Repository.UpdateWorkProfile(mainObj);
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Delete successfully.", null);
            }
            catch (Exception ex)
            {
                ex.SetLog("Delete Agency Type");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetByIdWorkProfile(int WorkProfileId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var obj = _IWorkProfile_Repository.GetWorkProfileById(Convert.ToInt32(WorkProfileId));
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, obj);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get WorkProfile by Id");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllWorkProfile()
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var obj = _IWorkProfile_Repository.GetAllWorkProfile(sessionUtils.UserDeptId, sessionUtils.UserRollType);
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, obj);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get All WorkProfile");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

    }
}