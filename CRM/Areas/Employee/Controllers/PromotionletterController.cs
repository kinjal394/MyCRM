using CRM.Models;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using static CRM.PdfFunction;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Drawing;
using CRM.App_Start;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM_Repository.Data;
using CRM_Repository.ServiceContract;
using CRM_Repository.Service;
using CRM_Repository.DTOModel;

namespace CRM.Areas.Employee.Controllers
{
    public class PromotionletterController : Controller
    {
        // GET: Employee/Promotionletter
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult PrintPromotionletter(Promotiontter Obj)
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                int ResponseVal = 0;
                var str = "";
                try
                {
                    ResponseVal = 1;
                    str = GetHtmlString(System.Web.HttpContext.Current.Server.MapPath("~/ReportFormat/PromotionLetter.html"));
                    string TempImg = System.Configuration.ConfigurationManager.AppSettings["MainHost"];
                    var Odate = Convert.ToString(Obj.offerDate) != "" ? Obj.offerDate.ToString("dd/MM/yyyy") : "";
                    var Jdate = Convert.ToString(Obj.joiningDate) != "" ? Obj.joiningDate.ToString("dd/MM/yyyy") : "";
                    str = str.Replace("@@Date@@", Odate);
                    str = str.Replace("@@proDate@@", Jdate);
                    //company List
                    str = str.Replace("@@companyName@@", Obj.companyName);
                    str = str.Replace("@@CorpOffAdd@@", Obj.CorpOffAdd);
                    str = str.Replace("@@RegOffAdd@@", Obj.RegOffAdd);
                    str = str.Replace("@@ComLogo@@", "<img src='" + TempImg + Obj.ComLogo + "' alt = 'Mountain View' style='width:304px;height:228px;' />");
                    str = str.Replace("@@MobileNo@@", Obj.MobileNo);
                    str = str.Replace("@@Email@@", Obj.Email);
                    str = str.Replace("@@Web@@", Obj.Web);

                    //User List
                    str = str.Replace("@@Username@@", Obj.Username);
                    str = str.Replace("@@CurrentDesignation@@", Obj.CurrentDesignation);
                    str = str.Replace("@@NextDesignation@@", Obj.NextDesignation);
                    str = str.Replace("@@DepartmentName@@", Obj.DepartmentName);
                    str = str.Replace("@@Salary@@", Convert.ToString(Obj.Salary));
                }
                catch (Exception)
                {

                    throw;
                }
                if (ResponseVal == 1)
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, str, null);
                }

            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
    }
}