using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM_Repository.Service;
using CRM_Repository.ServiceContract;
using CRM_Repository.Data;
using CRM.Models;
using CRM.App_Start;

namespace CRM.Areas.Master.Controllers
{
    [HasLoginSessionFilter]
    public class DesignationController : Controller
    {
        private IDesignation_Repository _IDesignation_Repository;

        public DesignationController()
        {
            this._IDesignation_Repository = new Designation_Repository(new elaunch_crmEntities());
        }

        // GET: Master/Designation
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddDesignation()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveDesignation(DesignationMaster d)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                DesignationMaster designationObj = new DesignationMaster();
                designationObj.DesignationId = d.DesignationId;
                designationObj.DesignationName = d.DesignationName.Trim();
                designationObj.IsActive = true;
                if (designationObj.DesignationId > 0)
                {
                    var cntList = _IDesignation_Repository.DuplicateEditDesignation(designationObj.DesignationId, designationObj.DesignationName).ToList();
                    if (cntList.Count == 0)
                    {
                        _IDesignation_Repository.UpdateDesignation(designationObj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully.", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Designation Already Exists", null);
                    }
                }
                else
                {
                    var cntList = _IDesignation_Repository.DuplicateDesignation(designationObj.DesignationName).ToList();
                    if (cntList.Count == 0)
                    {
                        _IDesignation_Repository.AddDesignation(designationObj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully.", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Designation Already Exists", null);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Create/Update Designation");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteDesignation(int DesignationId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                //_IDesignation_Repository.DeleteDesignation(Convert.ToInt32(DesignationId));
                DesignationMaster designationObj = new DesignationMaster();
                designationObj = _IDesignation_Repository.GetDesignationByID(DesignationId);
                designationObj.IsActive = false;
                _IDesignation_Repository.UpdateDesignation(designationObj);
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Delete successfully.", null);
            }
            catch (Exception ex)
            {
                ex.SetLog("Delete Designation");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetByIdDesignation(int DesignationId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var obj = _IDesignation_Repository.GetDesignationByID(Convert.ToInt32(DesignationId));
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, obj);

            }
            catch (Exception ex)
            {
                ex.SetLog("Get Designation by Id");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            _IDesignation_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}