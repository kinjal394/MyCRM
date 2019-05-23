using CRM.App_Start;
using CRM.Models;
using CRM_Repository.Data;
using CRM_Repository.Service;
using CRM_Repository.ServiceContract;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace CRM.Areas.Transaction.Controllers
{
    [HasLoginSessionFilter]
    public class QuotationController : Controller
    {
        private ICategory_Repository _ICategory_Repository;
        private ISubCategory_Repository _ISubCategory_Repository;
        private IProduct_Repository _IProduct_Repository;
        private IUnit_Repository _IUnit_Repository;
        private IPaymentTerms_Repository _IPaymentTerms_Repository;
        private IDeliveryTerms_Repository _IDeliveryTerms_Repository;
        private ICurrency_Repository _ICurrency_Repository;
        private IUser_Repository _IUser_Repository;
        private ICompany_Repository _ICompany_Repository;
        private IBuyer_Repository _IBuyer_Repository;
        private IQuotation_Repositroy _IQuotation_Repositroy;
        private IQuotationItem_Repository _IQuotationItem_Repository;
        private ISMSSpeech_Repository _ISMSSpeech_Repository;
        private IEmailSpeech_Repository _IEmailSpeech_Repository;
        private elaunch_crmEntities context;

        public QuotationController()
        {
            this._ICategory_Repository = new Category_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._ISubCategory_Repository = new SubCategory_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IProduct_Repository = new Product_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IUnit_Repository = new Unit_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IPaymentTerms_Repository = new PaymentTerms_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IDeliveryTerms_Repository = new DeliveryTerms_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IUser_Repository = new User_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._ICurrency_Repository = new Currency_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._ICompany_Repository = new Company_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IBuyer_Repository = new Buyer_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IQuotation_Repositroy = new Quotation_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IQuotationItem_Repository = new QuotationItem_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._ISMSSpeech_Repository = new SMSSpeech_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IEmailSpeech_Repository = new EmailSpeech_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this.context = new elaunch_crmEntities();
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddQuotation(int? id, int temp = 0)
        {
            QuotationMaster dataResponse = new QuotationMaster();
            if (id.HasValue)
            {
                //dataResponse = _IQuotation_Repositroy.GetQuotationById(id.Value);
                //dataResponse.QuotationItemMasters = _IQuotationItem_Repository.GetQuotationIteByQuotationId(id.Value).Where(i => i.IsActive != false).ToList();

                //decimal total = 0m;
                //foreach (var item in dataResponse.QuotationItemMasters)
                //{
                //    SubCategoryMaster objSubCategory = _ISubCategory_Repository.GetSubCategorybyId(_IProduct_Repository.GetProductById(id.Value).MainProductId);
                //    CategoryMaster objCategory = _ICategory_Repository.GetCategoryById(objSubCategory.CategoryId);
                //    item.SubCategoryId = objSubCategory.SubCategoryId;
                //    item.CategoryId = objCategory.CategoryId;
                //    item.Status = 2;
                //    total = total + item.Total;
                //}
                //dataResponse.Total = total;
                ViewBag.id = id;
                ViewBag.isdisable = temp;
            }
            else
            {
                dataResponse = new QuotationMaster();
                ViewBag.id = 0;
                ViewBag.isdisable = temp;
                //dataResponse.QuotationNo = "QTN";
            }
            return View();
        }
        public ActionResult AddProduct()
        {
            return View();
        }
        //public JsonResult GetAllDropDownList()
        //{
        //    var headerData = _ICompany_Repository.GetAllCompay().Select(u => new { u.ComId, u.ComName, u.CorpOffAdd, u.RegOffAdd });
        //    var buyerData = _IBuyer_Repository.GetAllBuyer().Select(b => new { b.BuyerId, b.CompanyName });
        //    var categoryData = _ICategory_Repository.GetCategory().Select(c => new { c.CategoryId, c.CategoryName });
        //    //var productData = _IProduct_Repository.GetAllProducts().Select(p => new { p.ProductId, p.ProductName });
        //    var currencyData = _ICurrency_Repository.GetAllCurrency().Select(c => new { c.CurrencyId, c.CurrencyName });
        //    var unitData = _IUnit_Repository.GetAllUnit().Select(u => new { u.UnitId, u.UnitName });
        //    var deliveryTerms = _IDeliveryTerms_Repository.GetAllDeliveryTerms().Select(d => new { d.TermsId, d.DeliveryName });
        //    var paymentTerms = _IPaymentTerms_Repository.GetAllPaymentTerms().Select(p => new { p.PaymentTermId, p.TermName });
        //    var userDataList = _IUser_Repository.getAllUser().Select(u => new { u.UserId, u.Name }).ToList();
        //    var subCategoryDataList = _ISubCategory_Repository.GetSubCategory().ToList();


        //    return Json(new
        //    {
        //        headerData = headerData,
        //        buyerData = buyerData,
        //        categoryData = categoryData,
        //        //productData = productData,
        //        currencyData = currencyData,
        //        unitData = unitData,
        //        deliveryTerms = deliveryTerms,
        //        paymentTerms = paymentTerms,
        //        userDataList = userDataList,
        //        subCategoryDataList = subCategoryDataList
        //    }, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult CreateUpdate(QuotationMaster objQuotationMaster)
        {
            DataResponse dataResponse = new DataResponse();
            Common cm = new Common();
            var usreId = sessionUtils.UserId;
            try
            {
                if (objQuotationMaster.QuotationId == 0)
                {
                    var data = _IQuotation_Repositroy.GetAllQuotation().FirstOrDefault();
                    if (data.QuotationNo == objQuotationMaster.QuotationNo)
                    {
                        var getData = cm.GetAutoNumber("Quotation");
                        objQuotationMaster.QuotationNo = getData.Rows[0][0].ToString();
                    }
                }
                var status = _IQuotation_Repositroy.CreateUpdateQuotation(objQuotationMaster, usreId);
                if (status == 1)
                {
                    SMSSpeechMaster Speech = _ISMSSpeech_Repository.DuplicateSMSSpeech("Quotation Speech").FirstOrDefault(); // To GET SMS Speech used SMS Tile.
                    EmailSpeechMaster SpeechEmail = _IEmailSpeech_Repository.CheckEmailSpeech("Quotation Speech"); // To GET EMAIL Speech used Email Tile.
                                                                                                                 //SMS
                    string[] mobilearray = objQuotationMaster.IMobileNo.Split(',');
                    foreach (string mobile in mobilearray)
                    {
                        string mob = mobile.ToString();
                        cm.sendsms(mob, Speech.SMS);
                    }
                    //Email
                    string[] mailarray = objQuotationMaster.IEmail.Split(',');
                    foreach (string mail in mailarray)
                    {
                        string em = mail.ToString();
                        cm.sendmail(em, SpeechEmail.Description, SpeechEmail.Subject, SpeechEmail.Email, SpeechEmail.Password);
                    }
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Added successfully and Your Quotation Number is " + objQuotationMaster.QuotationNo, null);
                }
                else if (status == 2)
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully and Your Quotation Number is " + objQuotationMaster.QuotationNo, null);
                else if (status == 3)
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Please select atleast one product", null);
                else
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Something went wrong.Please try again", null);
            }
            catch (Exception ex)
            {
                ex.SetLog("Create/Update by Quotation");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DeleteById(int id)
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    QuotationMaster objQuotationMaster = _IQuotation_Repositroy.GetQuotationById(id);
                    objQuotationMaster.IsActive = false;
                    objQuotationMaster.DeletedBy = sessionUtils.UserId;
                    objQuotationMaster.DeletedDate = DateTime.Now;
                    _IQuotation_Repositroy.UpdateQuotation(objQuotationMaster);
                    var data = _IQuotationItem_Repository.GetQuotationIteByQuotationId(id);
                    foreach (var item in data)
                    {
                        item.IsActive = false;
                        _IQuotationItem_Repository.UpdateQuotationitem(item);
                    }
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Delete successfully", null);
                }
                catch (Exception ex)
                {
                    ex.SetLog("Delete Quotation");
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                    throw;
                }
            }
            else
            {
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "Invalid User", null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetAllQuotationInfoById(int id)
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    QuotationMaster objQuotationMaster = _IQuotation_Repositroy.FatchQuotationById(id).SingleOrDefault();
                    List<QuotationItemMaster> objQuotationItemDetail = new List<QuotationItemMaster>();

                    List<QuotationItemMaster> objQuotaItemDetail = _IQuotationItem_Repository.GetQuotationIteByQuotationId(id).ToList();
                    if (objQuotaItemDetail != null)
                    {
                        foreach (var item in objQuotaItemDetail)
                        {
                            QuotationItemMaster vmQItem = new QuotationItemMaster();
                            vmQItem.CategoryId = item.CategoryId;
                            vmQItem.ItemId = item.ItemId;
                            vmQItem.MainProductId = item.MainProductId;
                            vmQItem.OfferPrice = item.OfferPrice;
                            vmQItem.ProductPrice = item.ProductPrice;
                            vmQItem.Percentage = item.Percentage;
                            //vmQItem.OfferPriceCode = item.OfferPriceCode;
                            vmQItem.DealerPriseName = item.DealerPriseName;

                            vmQItem.ProductDescription = item.ProductDescription;
                            vmQItem.ProductId = item.ProductId;
                            vmQItem.Qty = item.Qty;
                            vmQItem.QtyCode = item.QtyCode;
                            vmQItem.QuotationId = item.QuotationId;
                            vmQItem.Status = 2;
                            vmQItem.SubCategoryId = item.SubCategoryId;
                            vmQItem.Total = item.Total;
                            vmQItem.Category = item.Category;
                            vmQItem.SubCategory = item.SubCategory;
                            vmQItem.MainProductName = item.MainProductName;
                            vmQItem.ProductName = item.ProductName;
                            vmQItem.OfferPriceName = item.OfferPriceName;
                            vmQItem.QtyCodeName = item.QtyCodeName;
                            vmQItem.SupplierId = item.SupplierId;
                            vmQItem.SupplierModelName = item.SupplierModelName;
                            vmQItem.PriceId = item.PriceId;
                            vmQItem.DealerPrice = item.DealerPrice;
                            //vmQItem.CurrSymbol = item.CurrSymbol;

                            //vmQItem.ExRate = item.ExRate;
                            vmQItem.ExRatePrice = item.ExRatePrice;

                            objQuotationItemDetail.Add(vmQItem);
                        }
                    }

                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", new
                    {
                        objQuotationMaster = objQuotationMaster,
                        objQuotationItemDetail = objQuotationItemDetail,
                    });
                }
                catch (Exception ex)
                {
                    ex.SetLog("Get Quotation by Id");
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                }
            }
            else
            {
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "Invalid User", null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetDelarePriceById(int ProductId,int SupplierId)
        {
            
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    ProductPrice DelaerPriceData  = _IQuotation_Repositroy.GetDelarePriceById(ProductId,SupplierId);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", new
                    {
                        DelaerPriceData = DelaerPriceData
                    });
                }
                catch (Exception ex)
                {
                    ex.SetLog("Get Quotation by Id");
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                }
            }
            else
            {
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "Invalid User", null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetQuotationInfo()
        {
            DataResponse dataResponse = new DataResponse();
            Common cm = new Common();
            try
            {
                var getData = cm.GetAutoNumber("Quotation");

                var data = new
                {
                    InvCode = getData.Rows[0][0].ToString()
                };
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get All Quotation");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        public virtual void printpdf()
        {

            QuotationMaster Quotation = _IQuotation_Repositroy.GetQuotationById(6);
            CompanyMaster Company = _ICompany_Repository.GetComapnybyid(Quotation.CompanyId);
            BuyerMaster Buyer = _IBuyer_Repository.GetById(Quotation.InqId);
            QuotationItemMaster QIM = _IQuotationItem_Repository.GetQuotationitemById(Quotation.QuotationId);
            ProductMaster PM = _IProduct_Repository.GetProductById(QIM.ProductId);
            var PIM1 = _IProduct_Repository.GetProductBySubCategoryId(6).ToList();

            StringBuilder SB = new StringBuilder();
            SB.AppendFormat(System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/ReportFormat/Quotation/Quotation1.html")));
            SB.Replace("@@QuotationNo@@", Quotation.QuotationNo);
            SB.Replace("@@QuotationDate@@", Quotation.QuotationDate.ToString());
            SB.Replace("@@BuyerName@@", Buyer.BuyerContactDetails.ToString());
            SB.Replace("@@CompanyName@@", Buyer.CompanyName.ToString());
            SB.Replace("@@ProductName@@", PM.ProductName.ToString());
            //  SB.Replace("@@VedioLinks@@", PM.YoutubeLink.ToString());
            //  SB.Replace("@@TechnicalSpecification@@", PM.TechnicalDetail.ToString());
            SB.Replace("@@Qty@@", QIM.Qty.ToString());
            SB.Replace("@@DeliveryTerms@@", QIM.OfferPrice.ToString());
            SB.Replace("@@Offer@@", QIM.OfferPrice.ToString());
            SB.Replace("@@Offer@@", QIM.OfferPrice.ToString());
            SB.Replace("@@  @@", QIM.OfferPrice.ToString());
            SB.Replace("@@Offer@@", QIM.OfferPrice.ToString());



            SB.Append("<table border='1' cellpadding='0' cellspacing='0'>");
            SB.Append("<tr><b>");
            SB.Append("<td>ProductName</td>");
            SB.Append("<td>HSCode</td>");
            SB.Append("<td>YoutubeLink</td>");
            SB.Append("<td>FbLink</td>");
            SB.Append("<td>GplusLink</td></b>");
            SB.Append("</tr>");
            SB.Append("</table>");
            SB.Append("<table border='1' cellpadding='0' cellspacing='0' width='100%'>");
            foreach (var XP in PIM1.ToList())
            {
                SB.Append("<tr>");
                SB.Append("<td>" + Convert.ToString(XP.ProductName + "</td>"));
                SB.Append("<td>" + Convert.ToString(XP.HSCode + "</td>"));
                //  SB.Append("<td>" + Convert.ToString(XP.YoutubeLink + "</td>"));
                SB.Append("<td>" + Convert.ToString(XP.ProductCode + "</td>"));
                SB.Append("</tr>");
            }
            SB.Append("</table>");


            Document document = new Document();
            string filePath = HostingEnvironment.MapPath("~/Content/");
            PdfWriter.GetInstance(document, new FileStream(filePath + "Demo1" + ".pdf", FileMode.Create));

            String htmlText = SB.ToString();
            document.Open();
            iTextSharp.text.html.simpleparser.HTMLWorker hw = new iTextSharp.text.html.simpleparser.HTMLWorker(document);
            hw.Parse(new StringReader(htmlText));
            document.Close();

        }

    }
}