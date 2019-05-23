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
    public class PhaseController : Controller
    {
        private IPhase_Repository _IPhase_Repository;

        public PhaseController()
        {
            this._IPhase_Repository = new Phase_Repository(new elaunch_crmEntities());
        }
        // GET: Master/Phase
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddPhase()
        {
            return View();
        }
        [HttpPost]
        public JsonResult SavePhase(PhaseMaster d)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                PhaseMaster PhaseObj = new PhaseMaster();
                PhaseObj.PhaseId = d.PhaseId;
                PhaseObj.Phase = d.Phase.Trim();
                PhaseObj.IsActive = true;
                if (PhaseObj.PhaseId > 0)
                {
                    var cntList = _IPhase_Repository.DuplicateEditPhase(PhaseObj.PhaseId, PhaseObj.Phase).ToList();
                    if (cntList.Count == 0)
                    {
                        _IPhase_Repository.UpdatePhase(PhaseObj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully.", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Phase Name Already Exists.", null);
                    }
                }
                else
                {
                    var cntList = _IPhase_Repository.DuplicatePhase(PhaseObj.Phase).ToList();
                    if (cntList.Count == 0)
                    {
                        _IPhase_Repository.AddPhase(PhaseObj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully.", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Phase Name Already Exists.", null);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Create/Update Phase");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeletePhase(int PhaseId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                //_IAgencyType_Repository.DeleteAgencyType(Convert.ToInt32(AgencyTypeId));
                PhaseMaster PhaseObj = new PhaseMaster();
                PhaseObj = _IPhase_Repository.GetPhaseByID(PhaseId);
                PhaseObj.IsActive = false;
                _IPhase_Repository.UpdatePhase(PhaseObj);
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Delete successfully.", null);
            }
            catch (Exception ex)
            {
                ex.SetLog("Delete Phase");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetByIdPhase(int PhaseId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var obj = _IPhase_Repository.GetPhaseByID(Convert.ToInt32(PhaseId));
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, obj);

            }
            catch (Exception ex)
            {
                ex.SetLog("Get Phase by Id");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            _IPhase_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}