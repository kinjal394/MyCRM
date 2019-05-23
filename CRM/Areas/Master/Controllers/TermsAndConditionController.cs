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
    public class TermsAndConditionController : Controller
    {
        private ITermsAndCondition_Repository _ITermsAndCondition_Repository;
        // GET: Master/TermsAndCondition
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TermsAndConditionPopup()
        {
            return View();
        }
        public TermsAndConditionController()

        {
            this._ITermsAndCondition_Repository = new TermsAndCondition_Repository(new elaunch_crmEntities());

        }
        [HttpPost]
        public JsonResult SaveTermsAndCondition(TermsAndConditionMaster objTerms)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                TermsAndConditionMaster terms = new TermsAndConditionMaster();
                terms.TermsId = objTerms.TermsId;
                terms.Description = objTerms.Description;
                terms.Title = objTerms.Title;
                terms.IsActive = true;
                if (objTerms.TermsId > 0)
                {
                    var tlist = _ITermsAndCondition_Repository.DuplicateEditTermsAndCondition(terms.TermsId, terms.Title).ToList();
                    if (tlist.Count == 0)
                    {
                        _ITermsAndCondition_Repository.UpdateTermsAndCondition(terms);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully", null);

                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "TermsAndCondition Already Exists", null);

                    }
                }
                else
                {
                    var clist = _ITermsAndCondition_Repository.DuplicateTermsAndCondition(terms.Title).ToList();
                    if (clist.Count == 0)
                    {
                        _ITermsAndCondition_Repository.AddTermsAndCondition(terms);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully", null);

                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "TermsAndCondition Already Exists", null);

                        // Already Exists
                    }
                }

            }
            catch (Exception ex)
            {
                ex.SetLog("Create/Update TermsAndCondition");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult DeleteTermsAndCondition(string TermsId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    if (TermsId != "")
                    {
                        int cid = Convert.ToInt32(TermsId);
                        TermsAndConditionMaster tmaster = new TermsAndConditionMaster();
                        tmaster = _ITermsAndCondition_Repository.GetTermsAndConditionById(cid);
                        tmaster.IsActive = false;
                        //tmaster.TermsId = cid;
                        _ITermsAndCondition_Repository.UpdateTermsAndCondition(tmaster);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Delete successfully", null);
                    }
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "User is not valid", null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Delete TermsAndCondition");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GettermsAndConditionById(int TermsId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                //if (sessionUtils.HasUserLogin())
                //{
                var objterms = _ITermsAndCondition_Repository.GetTermsAndConditionById(TermsId);
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, objterms);
                //}
                //else
                //{
                //    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "User is not valid", null);
                //}
            }
            catch (Exception ex)
            {
                ex.SetLog("Get TermsAndCondition by Id");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            _ITermsAndCondition_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}