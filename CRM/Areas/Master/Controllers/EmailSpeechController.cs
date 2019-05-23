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
    public class EmailSpeechController : Controller
    {
        private IEmailSpeech_Repository _IEmailSpeech_Repository;

        public EmailSpeechController()
        {
            this._IEmailSpeech_Repository = new EmailSpeech_Repository(new elaunch_crmEntities());

        }
        // GET: Master/EmailSpeech
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddSignature()
        {
            return View();
        }

        public JsonResult GetSpeechById(int Id)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var data = _IEmailSpeech_Repository.GetEmailSpeechById(Id);
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get Speech by Id");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveSpeechMaster(EmailSpeechMaster obj)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {

                if (ModelState.IsValid)
                {
                    if (!_IEmailSpeech_Repository.CheckEmailSpeechExist(obj))
                    {
                        EmailSpeechMaster speech = new EmailSpeechMaster();
                        speech.Title = obj.Title;
                        speech.Description = obj.Description;
                        speech.Subject = obj.Subject;
                        speech.IsActive = true;
                        _IEmailSpeech_Repository.AddEmailSpeech(speech);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, MessageValue.Insert, null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Email Speech" + MessageValue.Exists, null);
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
                ex.SetLog("Create EmailSpeech");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public JsonResult UpdateSpeech(EmailSpeechMaster speech)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                EmailSpeechMaster obj = _IEmailSpeech_Repository.GetEmailSpeechById(speech.SpeechId);
                if (ModelState.IsValid)
                {
                    if (!_IEmailSpeech_Repository.CheckEmailSpeechExist(speech))
                    {
                        obj.Title = speech.Title;
                        obj.Description = speech.Description;
                        obj.Subject = speech.Subject;
                        _IEmailSpeech_Repository.UpdateEmailSpeech(obj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, MessageValue.Update, null);

                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Email Speech" + MessageValue.Exists, null);
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
                ex.SetLog("Update EmailSpeech");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult DeleteSpeech(int id)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                EmailSpeechMaster bankobj = _IEmailSpeech_Repository.GetEmailSpeechById(id);
                if (bankobj != null)
                {
                    //EmailSpeechMaster speechObj= new EmailSpeechMaster();
                    bankobj.IsActive = false;
                    _IEmailSpeech_Repository.UpdateEmailSpeech(bankobj);
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
                ex.SetLog("Delete EmailSpeech");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }

        }
        protected override void Dispose(bool disposing)
        {
            _IEmailSpeech_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}