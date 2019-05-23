using CRM.App_Start;
using CRM.Models;
using CRM_Repository.Data;
using CRM_Repository.Service;
using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Areas.Employee.Controllers
{
    [HasLoginSessionFilter]
    public class HolidayNameController : Controller
    {
        private IHolidayName_Repository _IHolidayName_Repository;

        public HolidayNameController()
        {
            this._IHolidayName_Repository = new HolidayName_Repository(new CRM_Repository.Data.elaunch_crmEntities());
        }

        // GET: Employee/HolidayName
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddHolidayName()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CreateUpdate(HolidayNameMaster objHolidayName)
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    bool isExist = _IHolidayName_Repository.IsExist(objHolidayName.HolidayId, objHolidayName.HolidayName);
                    if (!isExist)
                    {
                        if (objHolidayName.HolidayId <= 0)
                        {
                            objHolidayName.IsActive = true;
                            _IHolidayName_Repository.SaveHolidayName(objHolidayName);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully", null);
                        }
                        else
                        {
                            HolidayNameMaster objHolidayNameMaster = _IHolidayName_Repository.GetByHolidayId(objHolidayName.HolidayId);
                            objHolidayNameMaster.HolidayName = objHolidayName.HolidayName;
                            _IHolidayName_Repository.UpdateHolidayName(objHolidayNameMaster);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully", null);
                        }
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Exists, "HolidayName Already Exists", null);
                    }
                }
                catch (Exception ex)
                {
                    ex.SetLog("Create/Update Holiday Name");
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                }
            }
            else
            {
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "User is not valid", null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(int Id)
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    _IHolidayName_Repository.DeleteHolidayName(Id);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Delete successfully", null);
                }
                catch (Exception ex)
                {
                    ex.SetLog("Delete Holiday Name");
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                }
            }
            else
            {
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "User is not valid", null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetById(int Id)
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    HolidayNameMaster objHolidayNameMaster = _IHolidayName_Repository.GetByHolidayId(Id);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, objHolidayNameMaster);
                }
                catch (Exception ex)
                {
                    ex.SetLog("Get HolidayName by Id");
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                }
            }
            else
            {
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "User is not valid", null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            _IHolidayName_Repository.Dispose();
            base.Dispose(disposing);
        }

    }
}