using CRM.Models;
using CRM_Repository.Data;
using CRM_Repository.Service;
using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.App_Start;

namespace CRM.Areas.Master.Controllers
{
  [HasLoginSessionFilter]
    public class WorkReminderController : Controller
    {
        private IWorkReminder_Repository _IWorkReminder_Repository;
        public WorkReminderController()
        {
            this._IWorkReminder_Repository = new WorkRemind_Repository(new elaunch_crmEntities());
        }
        // GET: Master/WorkReminder
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddWorkReminder()
        {
            return View();
        }
        [HttpPost]
        public JsonResult SaveWorkReminder(WorkReminderMaster d)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                WorkReminderMaster WorkReminderobj = new WorkReminderMaster();
                WorkReminderobj.WorkRemindId = d.WorkRemindId;
                WorkReminderobj.DepartmentId = d.DepartmentId;
                WorkReminderobj.Title = d.Title;
                WorkReminderobj.Description = d.Description;
                WorkReminderobj.RemindDate = d.RemindDate;
                WorkReminderobj.RemindTime = d.RemindTime;
                WorkReminderobj.RemindMode = d.RemindMode;
                WorkReminderobj.IsActive = true;

                //WorkReminderobj.DeletedBy = sessionUtils.UserId;
                if(d.WorkRemindId>0)
                {
                    var data = _IWorkReminder_Repository.GetWorkReminderByID(d.WorkRemindId);
                    data.Title = WorkReminderobj.Title;
                    data.RemindTime = WorkReminderobj.RemindTime;
                    data.RemindMode = WorkReminderobj.RemindMode;
                    data.RemindDate = WorkReminderobj.RemindDate;
                    data.Description = WorkReminderobj.Description;
                    data.DepartmentId = WorkReminderobj.DepartmentId;
                    data.IsActive = true;
                    //data.CreatedBy = WorkReminderobj.CreatedBy;
                    //data.CreatedDate = WorkReminderobj.CreatedDate;
                    data.ModifyBy = sessionUtils.UserId;
                    data.ModifyDate = DateTime.Now;
                    _IWorkReminder_Repository.UpdateWorkReminder(data);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully.", null);
                }
                else
                {
                   
                    WorkReminderobj.CreatedBy = sessionUtils.UserId;
                    WorkReminderobj.CreatedDate = DateTime.Now;
                    _IWorkReminder_Repository.AddWorkReminder(WorkReminderobj);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully.", null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Create/Update  Work Reminder");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteWorkReminder(int WorkRemindId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                WorkReminderMaster WorkReminderObj = new WorkReminderMaster();
                WorkReminderObj = _IWorkReminder_Repository.GetWorkReminderByID(WorkRemindId);
                WorkReminderObj.IsActive = false;
                WorkReminderObj.DeletedBy = sessionUtils.UserId;
                WorkReminderObj.DeletedDate = DateTime.Now;
                _IWorkReminder_Repository.UpdateWorkReminder(WorkReminderObj);
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Delete successfully.", null);
            }
            catch (Exception ex)
            {
                ex.SetLog("Delete Work Reminder");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetByIdWorkReminder(int WorkRemindId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var obj = _IWorkReminder_Repository.GetWorkReminderByID(Convert.ToInt32(WorkRemindId));
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, obj);

            }
            catch (Exception ex)
            {
                ex.SetLog("Get WorkRemind by Id");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            _IWorkReminder_Repository.Dispose();
            base.Dispose(disposing);
        }

    }
}