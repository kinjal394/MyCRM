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
    public class SMSSpeechController : Controller
    {
        private ISMSSpeech_Repository _ISMSSpeech_Repository;
        // GET: Master/SMSSpeech
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SMSSpeechPopup()
        {
            return View();
        }
        public SMSSpeechController()

        {
            this._ISMSSpeech_Repository = new SMSSpeech_Repository(new elaunch_crmEntities());

        }
        [HttpPost]
        public JsonResult SaveSMSSpeech(SMSSpeechMaster objSMSSpeech)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    SMSSpeechMaster SMSSpeech = new SMSSpeechMaster();
                    SMSSpeech.SMSId = objSMSSpeech.SMSId;
                    SMSSpeech.SMSTitle = objSMSSpeech.SMSTitle;
                    SMSSpeech.SMS = objSMSSpeech.SMS;
                    SMSSpeech.DepartmentId = objSMSSpeech.DepartmentId;
                    SMSSpeech.IsActive = true;
                    if (objSMSSpeech.SMSId > 0)
                    {
                        var plist = _ISMSSpeech_Repository.DuplicateEditSMSSpeech(SMSSpeech.SMSId, SMSSpeech.SMSTitle).ToList();
                        if (plist.Count == 0)
                        {
                            _ISMSSpeech_Repository.UpdateSMSSpeech(SMSSpeech);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully", null);

                        }
                        else
                        {
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "SMSSpeech Already Exists", null);

                        }
                        //_ISource_Repository.UpdateSource(source);
                        //msg = "Success";
                    }
                    else
                    {
                        var clist = _ISMSSpeech_Repository.DuplicateSMSSpeech(SMSSpeech.SMSTitle).ToList();
                        if (clist.Count == 0)
                        {
                            _ISMSSpeech_Repository.AddSMSSpeech(SMSSpeech);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully", null);

                        }
                        else
                        {
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "SMSSpeech Already Exists", null);

                            // Already Exists
                        }
                    }
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "User is not valid", null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Create/Update SMSSpeech");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult DeleteSMSSpeech(string SMSId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    if (SMSId != "")
                    {
                        int cid = Convert.ToInt32(SMSId);
                        SMSSpeechMaster smaster = new SMSSpeechMaster();
                        smaster = _ISMSSpeech_Repository.GetSMSSpeechById(cid);
                        smaster.IsActive = false;
                        //smaster.SourceId = cid;
                        _ISMSSpeech_Repository.UpdateSMSSpeech(smaster);
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
                ex.SetLog("Delete SMSSpeech");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSMSSpeechById(int SMSId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    var objSMSSpeech = _ISMSSpeech_Repository.GetSMSSpeechById(SMSId);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, objSMSSpeech);
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "User is not valid", null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Get SMSSpeech by Id");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult SMSSpeechBind()
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var data = _ISMSSpeech_Repository.GetAllSMSSpeech().ToList();
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get All SMSSpeech");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                throw ex;
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            _ISMSSpeech_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}