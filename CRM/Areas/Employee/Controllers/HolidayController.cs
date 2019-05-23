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
    public class HolidayController : Controller
    {
        private IHoliday_Repository _IHoliday_Repository;

        public HolidayController()
        {
            this._IHoliday_Repository = new Holiday_Repository(new elaunch_crmEntities());
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddHoliday()
        {
            return View();
        }
        [HttpPost]
        public JsonResult SaveHoliday(HolidayMaster obj)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    //if (!_IHoliday_Repository.CheckHoliday(obj))
                    //{
                        HolidayMaster holidayObj = new HolidayMaster();
                        holidayObj.HolidayNameId = obj.HolidayNameId;
                        holidayObj.OnDate = obj.OnDate;
                        holidayObj.CreatedBy = sessionUtils.UserId;
                        holidayObj.CreatedDate = DateTime.Now;
                        holidayObj.IsActive = true;
                        holidayObj.CountryId = obj.CountryId;
                        holidayObj.StateIds = obj.StateIds;
                        holidayObj.ReligionIds = obj.ReligionIds;
                        _IHoliday_Repository.AddHoliday(holidayObj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert Successfully", null);
                    //}
                    //else
                    //{
                    //    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Holiday Name" + MessageValue.Exist, null);
                    //}
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "User is not valid", null);
                }
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.SetLog("Create Holiday");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public JsonResult DeleteHoliday(int id)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                HolidayMaster holidayObj = _IHoliday_Repository.GetHolidayById(id);
                if (holidayObj != null)
                {

                    holidayObj.IsActive = false;
                    _IHoliday_Repository.UpdateHoliday(holidayObj);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, MessageValue.Delete, null);
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, MessageValue.Param, null);
                }
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.SetLog("Delete Holiday");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult GetAllHolidayName()
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var data = _IHoliday_Repository.GetAllHolidayName();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get All Holiday");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult UpdateHoliday(HolidayMaster holidayObj)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                HolidayMaster obj = _IHoliday_Repository.GetHolidayById(holidayObj.HolidayId);
                if (sessionUtils.HasUserLogin())
                {
                    //if (!_IHoliday_Repository.CheckHoliday(holidayObj))
                    //{
                        obj.HolidayId = holidayObj.HolidayId;
                        obj.HolidayNameId = holidayObj.HolidayNameId;
                        obj.OnDate = holidayObj.OnDate;
                        obj.CountryId = holidayObj.CountryId;
                        obj.IsActive = true;
                        obj.ModifyBy = sessionUtils.UserId;
                        obj.ModifyDate = DateTime.Now;
                        obj.StateIds = holidayObj.StateIds;
                        obj.ReligionIds = holidayObj.ReligionIds;
                        _IHoliday_Repository.UpdateHoliday(obj);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update Successfully", null);
                    //}
                    //else
                    //{
                    //    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Holiday Name" + MessageValue.Exist, null);
                    //}

                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "User is not valid", null);
                }
                return Json(dataResponse, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                ex.SetLog("Update Holiday");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }

        }

        protected override void Dispose(bool disposing)
        {
            _IHoliday_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}