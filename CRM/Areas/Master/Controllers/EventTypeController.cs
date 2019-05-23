using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM_Repository.ServiceContract;
using CRM_Repository.Service;
using CRM_Repository.Data;
using CRM.Models;
using CRM.App_Start;

namespace CRM.Areas.Master.Controllers
{
    [HasLoginSessionFilter]
    public class EventTypeController : Controller
    {
        private IEventType_Repository _IEventType_Repository;

        public EventTypeController()
        {
            this._IEventType_Repository = new EventType_Repository(new elaunch_crmEntities());

        }
        // GET: Master/EventType
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddEventType()
        {
            return View();
        }
        [HttpPost]
        public JsonResult SaveEventType(EventTypeMaster obj)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {

                if (ModelState.IsValid)
                {
                    if (!_IEventType_Repository.CheckEventType(obj,false))
                    {
                        EventTypeMaster EventTypeobj = new EventTypeMaster();
                        EventTypeobj.EventTypeName = obj.EventTypeName;
                        EventTypeobj.IsActive = true;
                        _IEventType_Repository.AddEventType(EventTypeobj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, MessageValue.Insert, null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Event Type" + MessageValue.Exists, null);
                    }
                }
                else
                {
                    //return PartialView("AddBank", obj);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, MessageValue.Param, null);
                }
                return Json(dataResponse, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                ex.SetLog("Create EventType");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public JsonResult DeleteEventType(string id)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                EventTypeMaster EventTypeobj = _IEventType_Repository.GetEventById(Convert.ToInt32(id));
                if (EventTypeobj != null)
                {

                    EventTypeobj.IsActive = false;

                    _IEventType_Repository.UpdateEventType(EventTypeobj);
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
                ex.SetLog("Delete EventType");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }

        }



        [HttpPost]
        public JsonResult UpdateEventType(EventTypeMaster Eventtypeobj)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                // BankMaster obj = _IBank_Repository.GetBankById(bank.BankId);
                if (ModelState.IsValid)
                {
                    if (!_IEventType_Repository.CheckEventType(Eventtypeobj,true))
                    {
                        Eventtypeobj.IsActive = true;
                        _IEventType_Repository.UpdateEventType(Eventtypeobj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, MessageValue.Update, null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Event Type " + MessageValue.Exists, null);
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
                ex.SetLog("Update EventType");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }

        }
        protected override void Dispose(bool disposing)
        {
            _IEventType_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}