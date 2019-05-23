using CRM.App_Start;
using CRM.Models;
using CRM.Models.Grid;
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
    [HasLoginSessionFilter]
    public class AreaController : Controller
    {
        // GET: Master/Area

        private IArea_Repository _IArea_Repository;

        public AreaController()
        {
            this._IArea_Repository = new Area_Repository(new elaunch_crmEntities());

        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddArea()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveArea(CRM_Repository.Data.AreaMaster Area)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (ModelState.IsValid)
                {
                    CRM_Repository.Data.AreaMaster AR = new CRM_Repository.Data.AreaMaster();
                    AR.AreaName = Area.AreaName;
                    AR.CityId = Area.CityId;
                    AR.IsActive = true;
                    AR.IsDefault = false;
                    _IArea_Repository.SaveArea(AR);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, MessageValue.Insert, null);
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Bank Name" + MessageValue.Exists, null);
                }
            }
            catch (Exception ex)
            {
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, MessageValue.Error, null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }


        public JsonResult DeleteArea(int id)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (ModelState.IsValid)
                {
                    _IArea_Repository.Delete(id);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, MessageValue.Insert, null);
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Bank Name" + MessageValue.Exists, null);
                }
            }
            catch (Exception ex)
            {
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateArea(CRM_Repository.Data.AreaMaster Area)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (ModelState.IsValid)
                {
                    _IArea_Repository.UpdateArea(Area);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, MessageValue.Insert, null);
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Bank Name" + MessageValue.Exists, null);
                }
            }
            catch (Exception ex)
            {
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            _IArea_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}