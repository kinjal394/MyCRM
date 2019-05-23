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
    public class ITRController : Controller
    {
        private IITR_Repository _IITR_Repository;

        public ITRController()
        {
            this._IITR_Repository = new ITR_Repository(new elaunch_crmEntities());
        }
        // GET: Master/ITR
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddITR()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveITR(ITRMaster objitr)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                ITRMaster itrobj = new ITRMaster();
                itrobj.ITRId = objitr.ITRId;
                itrobj.ITRName = objitr.ITRName.Trim();
                itrobj.IsActive = true;
                if (itrobj.ITRId > 0)
                {
                    var cntList = _IITR_Repository.DuplicateEditITRName(itrobj.ITRId, itrobj.ITRName).ToList();
                    if (cntList.Count == 0)
                    {
                        _IITR_Repository.UpdateITR(itrobj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully.", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "ITR Already Exists", null);
                    }
                }
                else
                {
                    var cntList = _IITR_Repository.DuplicateITR(itrobj.ITRName).ToList();
                    if (cntList.Count == 0)
                    {
                        _IITR_Repository.AddITR(itrobj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully.", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "ITR Already Exists", null);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Create/Update ITR");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteITR(int ITRId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                
                ITRMaster itrobj = new ITRMaster();
                itrobj = _IITR_Repository.GetITRByID(ITRId);
                itrobj.IsActive = false;
                _IITR_Repository.UpdateITR(itrobj);
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Delete successfully.", null);
            }
            catch (Exception ex)
            {
                ex.SetLog("Delete ITR");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetByIdITR(int ITRId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var obj = _IITR_Repository.GetITRByID(Convert.ToInt32(ITRId));
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, obj);

            }
            catch (Exception ex)
            {
                ex.SetLog("Get ITR by Id");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            _IITR_Repository.Dispose();
            base.Dispose(disposing);
        }

    }
}