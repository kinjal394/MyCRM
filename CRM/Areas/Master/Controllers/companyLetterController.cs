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

namespace CRM.Areas.Master.Controllers
{
    [HasLoginSessionFilter]
    public class companyLetterController : Controller
    {
        private IIntervieweeCandidate_Repository _IIntervieweeCandidate_Repository;
        private ICompany_Repository _ICompany_Repository;
        private IUser_Repository _IUser_Repository;

        public companyLetterController()
        {
            this._IIntervieweeCandidate_Repository = new IntervieweeCandidate_Repository(new elaunch_crmEntities());
            this._ICompany_Repository = new Company_Repository(new elaunch_crmEntities());
            this._IUser_Repository = new User_Repository(new elaunch_crmEntities());
        }
        // GET: Master/companyLetter
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Companyletter()
        {
            return View();
        }
        [HttpPost]
        public JsonResult SetLetter(letter obj)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    if (obj.LatterType == "OfferLetter" || obj.LatterType == "AppointmentLetter" || obj.LatterType == "TerminationReport" || obj.LatterType == "PromotionLetter" || obj.LatterType == "IncrementLetter" || obj.LatterType == "AppraisalLetter" || obj.LatterType == "ReleasingLetter")
                    {
                        var str = GetHtmlString(System.Web.HttpContext.Current.Server.MapPath("~/ReportFormat/" + obj.LatterType + ".html"));
                        //OfferLetter
                        var strdate = Convert.ToString(obj.OfferDate) != "" ? obj.OfferDate.ToString("dd/MM/yyyy") : "";
                        str = str.Replace("@@OfferCompanyHeadname@@", obj.OfferCompanyHeadname);
                        str = str.Replace("@@OfferCompanyname@@", obj.OfferCompanyname);
                        str = str.Replace("@@OfferName@@", obj.OfferName);
                        str = str.Replace("@@OfferDesignation@@", obj.OfferDesignation);
                        str = str.Replace("@@OfferAddress@@", obj.OfferAddress);
                        str = str.Replace("@@OfferPincode@@", obj.OfferPincode);
                        str = str.Replace("@@OfferSalary@@", obj.OfferSalary);
                        str = str.Replace("@@OfferDate@@", strdate);

                        //Appointment Letter
                        var strAdate = Convert.ToString(obj.AppointmentDate) != "" ? obj.AppointmentDate.ToString("dd/MM/yyyy") : "";
                        str = str.Replace("@@AppointmentName@@", obj.AppointmentName);
                        str = str.Replace("@@AppointmentAddress@@", obj.AppointmentAddress);
                        str = str.Replace("@@AppointmentDesignation@@", obj.AppointmentDesignation);
                        str = str.Replace("@@AppointmentSalary@@", obj.AppointmentSalary);
                        str = str.Replace("@@AppointmentDate@@", Convert.ToString(strAdate));
                        str = str.Replace("@@AppointmentCompanyName@@", obj.AppointmentCompanyName);
                        str = str.Replace("@@AppointmentCompanyHeadName@@", obj.AppointmentCompanyHeadName);
                        str = str.Replace("@@Appointmentpincode@@", obj.Appointmentpincode);

                        //Termination Letter
                        var strTdate = Convert.ToString(obj.TerminationDate) != "" ? obj.TerminationDate.ToString("dd/MM/yyyy") : "";
                        str = str.Replace("@@TerminationName@@", obj.TerminationName);
                        str = str.Replace("@@TerminationAddress@@", obj.TerminationAddress);
                        str = str.Replace("@@TerminationDate@@", Convert.ToString(strTdate));
                        str = str.Replace("@@TerminationCompanyName@@", obj.TerminationCompanyName);
                        str = str.Replace("@@Terminationpincode@@", obj.Terminationpincode);
                        str = str.Replace("@@Post@@", obj.Post);
                        str = str.Replace("@@SupervisorName@@", obj.SupervisorName);

                        //Promotion Letter
                        var strPdate = Convert.ToString(obj.PromotionDate) != "" ? obj.PromotionDate.ToString("dd/MM/yyyy") : "";
                        str = str.Replace("@@PromotionName@@", obj.PromotionName);
                        str = str.Replace("@@PromotionAddress@@", obj.PromotionAddress);
                        str = str.Replace("@@PromotionDate@@", Convert.ToString(strPdate));
                        str = str.Replace("@@PromotionCompanyName@@", obj.PromotionCompanyName);
                        str = str.Replace("@@Promotionpincode@@", obj.Promotionpincode);
                        str = str.Replace("@@companyheadName@@", obj.companyheadName);
                        str = str.Replace("@@PromotionSalary@@", obj.PromotionSalary);
                        str = str.Replace("@@PromotionDesignation@@", obj.PromotionDesignation);
                        str = str.Replace("@@MobileNo@@", obj.MobileNo);

                        //Increment Letter
                        var strIdate = Convert.ToString(obj.IncrementDate) != "" ? obj.IncrementDate.ToString("dd/MM/yyyy") : "";
                        str = str.Replace("@@IncrementName@@", obj.IncrementName);
                        str = str.Replace("@@IncrementAddress@@", obj.IncrementAddress);
                        str = str.Replace("@@IncrementSalary@@", obj.IncrementSalary);
                        str = str.Replace("@@IncrementDate@@", Convert.ToString(strIdate));
                        str = str.Replace("@@ComHeadName@@", obj.ComHeadName);
                        str = str.Replace("@@InccompanyName@@", obj.InccompanyName);

                        //Appraisal Letter
                        var strAPPdate = Convert.ToString(obj.AppraisalDate) != "" ? obj.AppraisalDate.ToString("dd/MM/yyyy") : "";
                        str = str.Replace("@@AppraisalName@@", obj.AppraisalName);
                        str = str.Replace("@@AppraisalAddress@@", obj.AppraisalAddress);
                        str = str.Replace("@@AppraisalCompanyName@@", obj.AppraisalCompanyName);
                        str = str.Replace("@@AppraisalDate@@", Convert.ToString(strAPPdate));
                        str = str.Replace("@@CHeadName@@", obj.CHeadName);
                        //Releasing Letter
                        var strRdate = Convert.ToString(obj.ReleasingDate) != "" ? obj.ReleasingDate.ToString("dd/MM/yyyy") : "";
                        str = str.Replace("@@ReleasingName@@", obj.ReleasingName);
                        str = str.Replace("@@ReleasingCompanyName@@", obj.ReleasingCompanyName);
                        str = str.Replace("@@ReleasingDate@@", Convert.ToString(strRdate));
                        str = str.Replace("@@ReleasingcomAddress@@", obj.ReleasingcomAddress);
                        str = str.Replace("@@ReleasingCompanyHeadName@@", obj.ReleasingCompanyHeadName);
                        str = HttpUtility.HtmlEncode(str);

                        // GenerateHTMLTOPdf(str, obj.LatterType);
                        StringBuilder htmlpage = new StringBuilder();
                        htmlpage.Append(str);
                        StringReader sr = new StringReader(htmlpage.ToString());
                        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                        MemoryStream memStream = new MemoryStream();
                        PdfWriter wri = PdfWriter.GetInstance(pdfDoc, memStream);
                        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                        pdfDoc.Open();
                        htmlparser.Parse(sr);
                        //Paragraph p = new Paragraph(sr.ToString());
                        //pdfDoc.Add(p);
                        pdfDoc.Close();
                        byte[] strS = memStream.ToArray();
                        Response.ClearContent();
                        Response.ClearHeaders();
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("Content-Disposition", "attachment; filename="+ obj.LatterType + ".pdf");
                        Response.BinaryWrite(strS);
                        Response.End();
                        Response.Flush();
                        Response.Clear();

                    }
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Create/Update UploadProductData");
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);

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
                    //str = str.Replace("@@referenceno@@", Obj.referenceno);
                    var Odate=Convert.ToString(Obj.offerDate)!="" ? Obj.offerDate.ToString("dd/MM/yyyy") : "";
                    var Jdate=Convert.ToString(Obj.joiningDate)!="" ? Obj.joiningDate.ToString("dd/MM/yyyy") : "";
                    str = str.Replace("@@offerDate@@", Odate);
                    str = str.Replace("@@joiningDate@@", Jdate);
                   // str = str.Replace("@@LetterType@@", Obj.LetterType);
                    //company List
                    str = str.Replace("@@companyName@@", Obj.companyName);
                    str = str.Replace("@@companyAddress@@", Obj.RegOffAdd);
                    //str = str.Replace("@@TelePhoneNO@@", Obj.TelePhoneNO);
                    //str = str.Replace("@@Emaildata@@", Obj.Emaildata);
                    str = str.Replace("@@Web@@", Obj.Web);
                    //etter Issue by:
                   // str = str.Replace("@@UserName@@", Obj.UserName);
                    //employee List
                   // str = str.Replace("@@Employeename@@", Obj.Employeename);
                    str = str.Replace("@@MobileNo@@", Obj.MobileNo);
                    str = str.Replace("@@Mail@@", Obj.Mail);
                    //Candidate List
                    str = str.Replace("@@Candidatename@@", Obj.Candidatename);
                    //str = str.Replace("@@Name@@", Obj.Name);
                    str = str.Replace("@@Address@@", Obj.Address);
                    //str = str.Replace("@@Pincode@@", Obj.Pincode);
                    //str = str.Replace("@@State@@", Obj.State);
                   // str = str.Replace("@@Country@@", Obj.Country);
                    //str = str.Replace("@@MobNo@@", Obj.MobNo);
                    str = str.Replace("@@Email@@", Obj.Email);

                    str = str.Replace("@@Designation@@", Obj.Designation);
                   // str = str.Replace("@@SalaryHead@@", Obj.SalaryHead);
                    //str = str.Replace("@@Currency@@", Obj.Currency);
                    str = str.Replace("@@Salary@@", Convert.ToString(Obj.Salary));
                    //string[] prddesc = Obj.description.Split('|');
                    //string fulldesc = "";
                    //foreach (var x in prddesc)
                    //{
                    //    fulldesc += x.Split('-')[1].ToString() + '|';
                    //}
                    //str = str.Replace("@@description@@", fulldesc.Replace("|", "<br/>").ToString());
                    //str = str.Replace("@@Remark@@", Convert.ToString(Obj.Remark));


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
        [HttpPost]
        public JsonResult GetCompanydata(int id)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                CompanyMaster data = _ICompany_Repository.GetComapnybyid(id);
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get CompanyDetail in SalesOrder");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetUserData(int id)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                UserMaster data = _IUser_Repository.GetUserById(id);
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, new
                {
                    UserDetail = data
                });
            }
            catch (Exception ex)
            {
                ex.SetLog("Get CompanyDetail in SalesOrder");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetInterviewDetailById(string id)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                IntervieweeCandidateModel data = _IIntervieweeCandidate_Repository.GetIntervieweeCandidateByCode(id);
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);

            }
            catch (Exception ex)
            {
                ex.SetLog("Get CompanyDetail in SalesOrder");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
    }
}