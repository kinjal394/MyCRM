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
    public class AgencyTypeController : Controller
    {
        private IAgencyType_Repository _IAgencyType_Repository;

        public AgencyTypeController()
        {
            this._IAgencyType_Repository = new AgencyType_Repository(new elaunch_crmEntities());
        }

        // GET: Master/AgencyType
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddAgencyType()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveAgencyType(AgencyTypeMaster d)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                AgencyTypeMaster agencytypeObj = new AgencyTypeMaster();
                agencytypeObj.AgencyTypeId = d.AgencyTypeId;
                agencytypeObj.AgencyType = d.AgencyType.Trim();
                agencytypeObj.IsActive = true;
                if (agencytypeObj.AgencyTypeId > 0)
                {
                    var cntList = _IAgencyType_Repository.DuplicateEditAgencyType(agencytypeObj.AgencyTypeId, agencytypeObj.AgencyType).ToList();
                    if (cntList.Count == 0)
                    {
                        _IAgencyType_Repository.UpdateAgencyType(agencytypeObj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully.", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "AgencyType Name Already Exists.", null);
                    }
                }
                else
                {
                    var cntList = _IAgencyType_Repository.DuplicateAgencyType(agencytypeObj.AgencyType).ToList();
                    if (cntList.Count == 0)
                    {
                        _IAgencyType_Repository.AddAgencyType(agencytypeObj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully.", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "AgencyType Name Already Exists.", null);
                    }
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
        public JsonResult DeleteAgencyType(int AgencyTypeId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                //_IAgencyType_Repository.DeleteAgencyType(Convert.ToInt32(AgencyTypeId));
                AgencyTypeMaster agencytypeObj = new AgencyTypeMaster();
                agencytypeObj = _IAgencyType_Repository.GetAgencyTypeByID(AgencyTypeId);
                agencytypeObj.IsActive = false;
                _IAgencyType_Repository.UpdateAgencyType(agencytypeObj);
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Delete successfully.", null);
            }
            catch (Exception ex)
            {
                ex.SetLog("Delete Agency Type");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetByIdAgencyType(int AgencyTypeId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var obj = _IAgencyType_Repository.GetAgencyTypeByID(Convert.ToInt32(AgencyTypeId));
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, obj);

            }
            catch (Exception ex)
            {
                ex.SetLog("Get AgencyType by Id");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            _IAgencyType_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}