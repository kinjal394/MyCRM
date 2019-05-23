using CRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static CRM.PdfFunction;
using CRM.App_Start;

namespace CRM.Areas.Master.Controllers
{
    [HasLoginSessionFilter]
    public class SMSController : Controller
    {
        // GET: Master/SMS
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult SetSMS(SMSModel Obj)
        {
            DataResponse dataResponse = new DataResponse();
            Common cm = new Common();
            if (sessionUtils.HasUserLogin())
            {
                int ResponseVal = 0;
                try
                {
                    ResponseVal = 1;
                    if (Obj.mobile != null)
                    {
                        string[] mobilearray = Obj.mobile.Split(',');
                        string Speech = Obj.SMS ==null?" ":Obj.SMS; // To GET SMS Speech used SMS Tile.
                        foreach (string mobile in mobilearray)
                        {
                            string mob = mobile.Split(' ')[1].ToString();
                            cm.sendsms(mob, Speech);
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                if (ResponseVal == 1)
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Message successfully send", null);
                }

            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
    }
}