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
    public class EmployeeShiftController : Controller
    {

        private IEmployeeShift_Repository _IEmployeeShift_Repository;

        public EmployeeShiftController()
        {
            this._IEmployeeShift_Repository = new EmployeeShift_Repository(new elaunch_crmEntities());
        }
        public EmployeeShiftController(IEmployeeShift_Repository _IEmployeeShift_Repository)
        {
            this._IEmployeeShift_Repository = _IEmployeeShift_Repository;
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddShift()
        {
            return View();
        }
        [HttpPost]
        public JsonResult InsertShift(EmployeeShitfMaster obj)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                double data = (TimeSpan.Parse(obj.OutTime.ToString()) - TimeSpan.Parse(obj.InTime.ToString())).TotalHours;
                string hours = data.ToString("f2");

                if (ModelState.IsValid)
                {
                    if (!_IEmployeeShift_Repository.CheckShiftExist(obj, false))
                    {
                        obj.Hours = Convert.ToDecimal(hours);
                        obj.IsActive = true;
                        _IEmployeeShift_Repository.InsertShist(obj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, MessageValue.Insert, null);
                        return Json(dataResponse, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Shift Name" + MessageValue.Exists, null);
                    }
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error,MessageValue.Error, null);
                }
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.SetLog("Create EmployeeShift");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult DeleteShift(int id)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {

                EmployeeShitfMaster empObj = _IEmployeeShift_Repository.GetShiftByID(id);
                if (empObj != null)
                {
                    empObj.IsActive = false;
                    _IEmployeeShift_Repository.UpdateShift(empObj);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess,MessageValue.Delete, null);
                    return Json(dataResponse, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Shift Not Found", null);
                    return Json(dataResponse, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Delete EmployeeShift");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);

            }

        }

        public JsonResult UpdateShift(EmployeeShitfMaster obj)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                double data = (TimeSpan.Parse(obj.OutTime.ToString()) - TimeSpan.Parse(obj.InTime.ToString())).TotalHours;
                string hours = data.ToString("f2");
                EmployeeShitfMaster emp = _IEmployeeShift_Repository.GetShiftByID(obj.ShiftId);
                if (ModelState.IsValid)
                {
                    if (!_IEmployeeShift_Repository.CheckShiftExist(obj, true))
                    {
                        emp.ShiftName = obj.ShiftName;
                        emp.Hours = Convert.ToDecimal(hours);
                        emp.InTime = obj.InTime;
                        emp.OutTime = obj.OutTime;
                        emp.LateEntryCalculate = obj.LateEntryCalculate;
                        _IEmployeeShift_Repository.UpdateShift(emp);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, MessageValue.Update, null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Shift Name" + MessageValue.Exists, null);
                    }
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, MessageValue.Param, null);
                }
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.SetLog("Update EmployeeShift Type");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }
        }

        protected override void Dispose(bool disposing)
        {
            _IEmployeeShift_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}