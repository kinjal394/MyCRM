using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM_Repository.Data;
using CRM_Repository.Service;
using CRM_Repository.ServiceContract;
using CRM.Models;
using CRM.App_Start;

namespace CRM.Areas.Master.Controllers
{
    [HasLoginSessionFilter]
    public class AskcustomerDetailsController : Controller
    {
        private IAskcustomerDetails_Repository _IAskcustomerDetails_Repository;
        private ISMSSpeech_Repository _ISMSSpeech_Repository;
        private IEmailSpeech_Repository _IEmailSpeech_Repository;
        public AskcustomerDetailsController()
        {
            this._IAskcustomerDetails_Repository = new AskcustomerDetails_Repository(new elaunch_crmEntities());
            this._ISMSSpeech_Repository = new SMSSpeech_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IEmailSpeech_Repository = new EmailSpeech_Repository(new CRM_Repository.Data.elaunch_crmEntities());
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddAskcustomerDetails()
        {
            return View();
        }
        [HttpPost]
        public JsonResult SaveAskcustomerDetails(AskCustomerDetail obj)
        {
            DataResponse dataResponse = new DataResponse();
            Common cm = new Common();
            try
            {
                AskCustomerDetail ObjAskCust = new AskCustomerDetail();
                ObjAskCust.AskCustId = obj.AskCustId;
                ObjAskCust.SourceId = obj.SourceId;
                ObjAskCust.Name = obj.Name;
                ObjAskCust.Date = obj.Date;
                ObjAskCust.Requirement = obj.Requirement;
                ObjAskCust.Mobileno = obj.Mobileno;
                ObjAskCust.Email = obj.Email;
                ObjAskCust.IsActive = true;
                if (obj.AskCustId > 0)
                {
                    var data = _IAskcustomerDetails_Repository.GetAskCustomerDetailByID(Convert.ToInt32(obj.AskCustId));
                    data.SourceId = ObjAskCust.SourceId;
                    data.Name = ObjAskCust.Name;
                    data.Mobileno = ObjAskCust.Mobileno;
                    data.Email = ObjAskCust.Email;
                    data.Date = ObjAskCust.Date;
                    data.Requirement = ObjAskCust.Requirement;
                    data.IsActive = true;
                    data.ModifyBy = sessionUtils.UserId;
                    data.ModifyedDate = DateTime.Now;
                    _IAskcustomerDetails_Repository.UpdateAskCustomerDetail(data);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully.", null);
                    if (data != null)
                    {
                        if (data.IsActive == true)
                        {
                            if (data.Mobileno != null)
                            {
                                string[] mobilearray = data.Mobileno.Split(',');
                                SMSSpeechMaster Speech = _ISMSSpeech_Repository.DuplicateSMSSpeech("AskCustomer Speech").FirstOrDefault(); // To GET SMS Speech used SMS Tile.
                                foreach (string mobile in mobilearray)
                                {
                                    string mob = mobile.Split(' ')[1].ToString();
                                    cm.sendsms(mob, Speech.SMS);
                                }
                            }
                            if (data.Email != null)
                            {
                                EmailSpeechMaster SpeechEmail = _IEmailSpeech_Repository.CheckEmailSpeech("AskCustomer Speech"); // To GET EMAIL Speech used Email Tile.
                                cm.sendmail(data.Email, SpeechEmail.Description, "Introduction from Gurjari Ltd.", SpeechEmail.Email, SpeechEmail.Password);
                            }
                        }


                    }
                }
                else
                {
                    ObjAskCust.CreatedBy = sessionUtils.UserId;
                    ObjAskCust.CreatedDate = DateTime.Now;
                    _IAskcustomerDetails_Repository.AddAskCustomerDetail(ObjAskCust);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully.", null);
                    // Start : Only Insert Time SMS Send
                    if (ObjAskCust != null)
                    {
                        if (ObjAskCust.IsActive == true)
                        {
                            if (ObjAskCust.Mobileno != null)
                            {
                                string[] mobilearray = ObjAskCust.Mobileno.Split(',');
                                SMSSpeechMaster Speech = _ISMSSpeech_Repository.DuplicateSMSSpeech("AskCustomer Speech").FirstOrDefault(); // To GET SMS Speech used SMS Tile.
                                foreach (string mobile in mobilearray)
                                {
                                    string mob = mobile.Split(' ')[1].ToString();
                                    cm.sendsms(mob, Speech.SMS);
                                }
                            }
                            if (ObjAskCust.Email != null)
                            {
                                EmailSpeechMaster SpeechEmail = _IEmailSpeech_Repository.CheckEmailSpeech("AskCustomer Speech"); // To GET EMAIL Speech used Email Tile.
                                cm.sendmail(ObjAskCust.Email, SpeechEmail.Description, "Introduction from Gurjari Ltd.", SpeechEmail.Email, SpeechEmail.Password);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Create/Update  Ask Customer Details");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteAskCustomerDetails(int AskCustId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                AskCustomerDetail ObjAskCust = new AskCustomerDetail();
                ObjAskCust = _IAskcustomerDetails_Repository.GetAskCustomerDetailByID(AskCustId);
                ObjAskCust.IsActive = false;
                ObjAskCust.DeletedBy = sessionUtils.UserId;
                ObjAskCust.DeletedDate = DateTime.Now;
                _IAskcustomerDetails_Repository.UpdateAskCustomerDetail(ObjAskCust);
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Delete successfully.", null);
            }
            catch (Exception ex)
            {
                ex.SetLog("Delete Ask Customer Details");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetByIdAskCustomerDetails(int AskCustId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var obj = _IAskcustomerDetails_Repository.GetAskCustomerDetailByID(Convert.ToInt32(AskCustId));
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, obj);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get Ask customer Details by Id");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public FileResult GetVCCard(int id)
        {
            var obj = _IAskcustomerDetails_Repository.GetAskCustomerDetailByID(id);
            Common cm = new Common();
            VCardData objvcard = new VCardData();
            if (obj != null)
            {
                objvcard.Name = obj.Name;
                objvcard.Mobileno = obj.Mobileno;
                objvcard.Email = obj.Email;
            }
            string str = cm.VCardFile(objvcard);
            return File(System.Text.Encoding.ASCII.GetBytes(str), "text/x-vcard");
        }

        protected override void Dispose(bool disposing)
        {
            _IAskcustomerDetails_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}