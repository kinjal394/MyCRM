using CRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM_Repository.Service;
using CRM_Repository.ServiceContract;
using CRM_Repository.Data;
using static CRM.PdfFunction;

namespace CRM.Areas.Master.Controllers
{
    public class EmailBoxController : Controller
    {
        private IEmailSpeech_Repository _IEmailSpeech_Repository;
        public EmailBoxController()
        {
            this._IEmailSpeech_Repository = new EmailSpeech_Repository(new CRM_Repository.Data.elaunch_crmEntities());
        }
        // GET: Master/EmailBox
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult mailbox()
        {
            return View();
        }
        [HttpPost]
        public JsonResult SetEmail(EmailModel Obj)
        {
            DataResponse dataResponse = new DataResponse();
            Common cm = new Common();
            if (sessionUtils.HasUserLogin())
            {
                int ResponseVal = 0;
                try
                {
                    EmailSpeechMaster SpeechEmail = _IEmailSpeech_Repository.CheckEmailSpeech("Contact Speech");
                    if (Obj.EmailId != null)
                    {
                        string[] emailchk = Obj.EmailId.Split(',');
                        foreach (string email in emailchk)
                        {
                            bool validemail = IsValidEmail(email);
                            if (validemail == false)
                            {
                                Obj.EmailId = Obj.EmailId.Replace(email + ",", "");
                            }
                        }
                        cm.sendmail(Obj.EmailId, Obj.description, Obj.Subject, SpeechEmail.Email, SpeechEmail.Password, Obj.Signature);
                        ResponseVal = 1;
                    }
                   
                }
                catch (Exception)
                {

                    throw;
                }
                if (ResponseVal == 1)
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Email successfully send", null);
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Email Not Valid. Please Select another Company.", null);
                }

            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}