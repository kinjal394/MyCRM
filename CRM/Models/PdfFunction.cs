using SelectPdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CRM
{
    public static class PdfFunction
    {
        public static void GeneratePdf(string htmlCode, string FileName)
        {

            HtmlToPdf converter = new HtmlToPdf();
            converter.Options.DisplayHeader = true;
            converter.Header.DisplayOnFirstPage = true;
            converter.Header.DisplayOnOddPages = true;
            converter.Header.DisplayOnEvenPages = true;
            converter.Header.Height = 75;

            // add some html content to the header
            // PdfHtmlSection headerHtml = new PdfHtmlSection("http://demo19.enextlayer.com/HeaderPage.html");
            // headerHtml.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;
            // converter.Header.Add(headerHtml);
            PdfDocument doc = converter.ConvertHtmlString(htmlCode);
            doc.Save(HttpContext.Current.Response, false, FileName + ".pdf");
            doc.Close();
        }

        public static string GetHtmlString(string Url)
        {
            string htmlCode = "";
            using (WebClient client = new WebClient())
            {
                htmlCode = client.DownloadString(Url);
            }
            return htmlCode;
        }
        public class SMSModel
        {
            public string mobile { get; set; }
            public string SMS { get; set; }
        }
        public class EmailModel
        {
            public string SenderType { get; set; }
            public string CompanyIds { get; set; }
            public string Subject { get; set; }
            public string Emailspeech { get; set; }//data
            public string description { get; set; }
            public string EmailSignature { get; set; }//data2
            public string Signature { get; set; }
            public string EmailId { get; set; }
            public string SenderEmail { get; set; }
            public string BOdescription { get; set; }
            public string OTSignature { get; set; }
        }
        public class Offerletter
        {
            public string ComLogo { get; set; }
            public string companyName { get; set; }
            public string RegOffAdd { get; set; }
            public string CorpOffAdd { get; set; }
            public string MobileNo { get; set; }
            public string Email { get; set; }
            public string Web { get; set; }
            public string Designation { get; set; }

            public DateTime joiningDate { get; set; }
            public DateTime offerDate { get; set; }

            public string Candidatename { get; set; }
            public string RefNo { get; set; }
            public string Address { get; set; }
            public string Mail { get; set; }
            public decimal Salary { get; set; }

        }

        public class Promotiontter
        {
            public string ComLogo { get; set; }
            public string companyName { get; set; }
            public string RegOffAdd { get; set; }
            public string CorpOffAdd { get; set; }
            public string MobileNo { get; set; }
            public string Email { get; set; }
            public string Web { get; set; }
            public string NextDesignation { get; set; }

            public DateTime joiningDate { get; set; }
            public DateTime offerDate { get; set; }
            public string Username { get; set; }
            public string CurrentDesignation { get; set; }
            public string DepartmentName { get; set; }
            public decimal Salary { get; set; }

        }

        public class letter
        {
           
            //OfferLetter
            public string OfferCompanyHeadname { get; set; }
            public string OfferCompanyname { get; set; }
            public string OfferName { get; set; }
            public string OfferDesignation { get; set; }
            public string OfferAddress { get; set; }
            public string OfferPincode { get; set; }
            public string OfferSalary { get; set; }
            public string LatterType { get; set; }
            public DateTime OfferDate { get; set; }

            //Appointment Letter
            public string AppointmentName { get; set; }
            public string AppointmentDesignation { get; set; }
            public string AppointmentAddress { get; set; }
            public string Appointmentpincode { get; set; }
            public string AppointmentSalary { get; set; }
            public DateTime AppointmentDate { get; set; }
            public string AppointmentCompanyName { get; set; }
            public string AppointmentCompanyHeadName { get; set; }


            //Termination Letter
            public string TerminationName { get; set; }
            public string TerminationAddress { get; set; }
            public string Terminationpincode { get; set; }
            public DateTime TerminationDate { get; set; }
            public string TerminationCompanyName { get; set; }
            public string Post { get; set; }
            public string SupervisorName { get; set; }

            //Promotion Letter
            public string PromotionName { get; set; }
            public string PromotionDesignation { get; set; }
            public string PromotionAddress { get; set; }
            public string Promotionpincode { get; set; }
            public DateTime PromotionDate { get; set; }
            public string PromotionSalary { get; set; }
            public string PromotionCompanyName { get; set; }
            public string companyheadName { get; set; }
            public string MobileNo { get; set; }

            //IncrementLetter Letter
            public string IncrementName { get; set; }
            public string IncrementAddress { get; set; }
            public string IncrementSalary { get; set; }
            public DateTime IncrementDate { get; set; }
            public string ComHeadName { get; set; }
            public string InccompanyName { get; set; }


            //Appraisal Letter
            public string AppraisalName { get; set; }
            public string AppraisalAddress { get; set; }
            public DateTime AppraisalDate { get; set; }
            public string AppraisalCompanyName { get; set; }
            public string CHeadName { get; set; }

            //Releasing Letter
            public string ReleasingName { get; set; }
            public string ReleasingcomAddress { get; set; }
            public DateTime ReleasingDate { get; set; }
            public string ReleasingCompanyName { get; set; }
            public string ReleasingCompanyHeadName { get; set; }
        }
    }
}