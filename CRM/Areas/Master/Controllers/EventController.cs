using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM_Repository.Data;
using CRM_Repository.ServiceContract;
using CRM_Repository.Service;
using CRM.Models;
using CRM.App_Start;

namespace CRM.Areas.Master.Controllers
{
    [HasLoginSessionFilter]
    public class EventController : Controller
    {
        private IEvent_Repository _EventRepostory;
        public EventController()
        {
            this._EventRepostory = new Event_Repository(new elaunch_crmEntities());
        }

        public JsonResult GetAllEvent()
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var EventType = _EventRepostory.getEventTypes().Where(x => x.IsActive == true);
                return Json(EventType, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get All Event");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult AddEvent()
        {

            return View();
        }

        // GET: Master/Event
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveEvent(EventMaster eventobj)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (ModelState.IsValid)
                {
                    if (!_EventRepostory.CheckEvent(eventobj, false))
                    {
                       
                        eventobj.IsActive = true;
                        eventobj.CreatedBy = sessionUtils.UserId;
                        eventobj.CreatedDate = DateTime.Now;
                        _EventRepostory.AddEvent(eventobj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, MessageValue.Insert, null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Event" + MessageValue.Exists, null);
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
                ex.SetLog("Create Event");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public JsonResult DeleteEvent(string id)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {

                EventMaster eventobj = _EventRepostory.getEventbyid(Convert.ToInt32(id));
                if (eventobj != null)
                {

                    eventobj.IsActive = false;
                    eventobj.DeletedBy =   sessionUtils.UserId;
                    eventobj.DeletedDate = DateTime.Now;
                    _EventRepostory.UpdateEvent(eventobj);
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
                ex.SetLog("Delete Event");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public JsonResult UpdateEvent(EventMaster Event)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (!_EventRepostory.CheckEvent(Event, true))
                {
                    EventMaster obj = _EventRepostory.getEventbyid(Event.EventId);
                    if (obj != null)
                    {
                        obj.EventTypeId = Event.EventTypeId;
                        obj.EventName = Event.EventName;
                        obj.EventDate = Event.EventDate;
                        obj.ModifyBy =   sessionUtils.UserId;
                        obj.ModifyDate = DateTime.Now;
                        _EventRepostory.UpdateEvent(obj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, MessageValue.Update, null);

                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, MessageValue.Param, null);
                    }
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Event Already Exists " + MessageValue.Exists, null);
                }
                return Json(dataResponse, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                ex.SetLog("Delete Event");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }

        }

        protected override void Dispose(bool disposing)
        {
            _EventRepostory.Dispose();
            base.Dispose(disposing);
        }
    }
}