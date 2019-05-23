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

namespace CRM.Areas.Master.Controllers
{
    [HasLoginSessionFilter]
    public class BloodGroupController : Controller
    {
        private IBloodGroup_Repository _IBloodGroup_Repository;
        public BloodGroupController()
        {
            this._IBloodGroup_Repository = new BloodGroup_Repository(new elaunch_crmEntities());

        }
        // GET: Master/BloodGroup
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddBloodGroup()
        {
            return View();
        }
        [HttpPost]
        public JsonResult SaveBloodGroup(BloodGroupMaster obj)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {

                if (ModelState.IsValid)
                {
                    if (!_IBloodGroup_Repository.CheckBloodGroup(obj, false))
                    {
                        BloodGroupMaster bloodgroupObj = new BloodGroupMaster();
                        bloodgroupObj.BloodGroup = obj.BloodGroup;
                        bloodgroupObj.IsActive = true;
                        _IBloodGroup_Repository.AddBloodGroup(bloodgroupObj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, MessageValue.Insert, null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Blood Group" + MessageValue.Exists, null);
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
                ex.SetLog("Create Blood Group");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult DeleteBloodGroup(string id)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                BloodGroupMaster bloodgroupObj = _IBloodGroup_Repository.GetBloodGroupById(Convert.ToInt32(id));
                if (bloodgroupObj != null)
                { 
                    bloodgroupObj.IsActive = false;
                    _IBloodGroup_Repository.UpdateBloodGroup(bloodgroupObj);
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
                ex.SetLog("Delete Blood Group");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            } 
        }

        [HttpPost]
        public JsonResult UpdateBloodGroup(BloodGroupMaster bloodgroupObj)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                // BankMaster obj = _IBank_Repository.GetBankById(bank.BankId);
                if (ModelState.IsValid)
                {
                    if (!_IBloodGroup_Repository.CheckBloodGroup(bloodgroupObj, true))
                    {
                        bloodgroupObj.IsActive = true;
                        _IBloodGroup_Repository.UpdateBloodGroup(bloodgroupObj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, MessageValue.Update, null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Blood Group" + MessageValue.Exists, null);
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
                ex.SetLog("Update Blood Group");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }

        }

        protected override void Dispose(bool disposing)
        {
            _IBloodGroup_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}