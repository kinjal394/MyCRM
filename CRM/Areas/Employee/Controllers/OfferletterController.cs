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
    [HasLoginSessionFilter]
    public class OfferletterController : Controller
    {
        private IIntervieweeCandidate_Repository _IIntervieweeCandidate_Repository;
        private ICompany_Repository _ICompany_Repository;
        private IUser_Repository _IUser_Repository;

        public OfferletterController()
        {
            this._IIntervieweeCandidate_Repository = new IntervieweeCandidate_Repository(new elaunch_crmEntities());
            this._ICompany_Repository = new Company_Repository(new elaunch_crmEntities());
            this._IUser_Repository = new User_Repository(new elaunch_crmEntities());
        }
        // GET: Employee/Offerletter
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult SetOfferLetter(Offerletter Obj)
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                int ResponseVal = 0;
                var str = "";
                try
                {
                    ResponseVal = 1;
                    str = GetHtmlString(System.Web.HttpContext.Current.Server.MapPath("~/ReportFormat/OfferLetter.html"));
                    string TempImg = System.Configuration.ConfigurationManager.AppSettings["MainHost"];
                    str = str.Replace("@@referenceno@@", Obj.RefNo);
                    var Odate = Convert.ToString(Obj.offerDate) != "" ? Obj.offerDate.ToString("dd/MM/yyyy") : "";
                    var Jdate = Convert.ToString(Obj.joiningDate) != "" ? Obj.joiningDate.ToString("dd/MM/yyyy") : "";
                    str = str.Replace("@@offerDate@@", Odate);
                    str = str.Replace("@@joiningDate@@", Jdate);
                    //company List
                    str = str.Replace("@@companyName@@", Obj.companyName);
                    str = str.Replace("@@CorpOffAdd@@", Obj.CorpOffAdd);
                    str = str.Replace("@@RegOffAdd@@", Obj.RegOffAdd);
                    str = str.Replace("@@ComLogo@@", "<img id='header' src='" + TempImg + Obj.ComLogo + "' alt='' style='width: 50px; height: 50px; ' />");
                    str = str.Replace("@@MobileNo@@", Obj.MobileNo);
                    str = str.Replace("@@Email@@", Obj.Email);
                    str = str.Replace("@@Web@@", Obj.Web);
                    
                    //Candidate List
                    str = str.Replace("@@Candidatename@@", Obj.Candidatename);
                    str = str.Replace("@@Address@@", Obj.Address);
                    str = str.Replace("@@Mail@@", Obj.Mail);
                    str = str.Replace("@@Designation@@", Obj.Designation);
                    str = str.Replace("@@Salary@@", Convert.ToString(Obj.Salary));
                    //string[] prddesc = Obj.description.Split('|');
                    //string fulldesc = "";
                    //foreach (var x in prddesc)
                    //{
                    //    fulldesc += x.Split('-')[1].ToString() + '|';
                    //}
                    //str = str.Replace("@@description@@", fulldesc.Replace("|", "<br/>").ToString());


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