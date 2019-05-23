using CRM.App_Start;
using CRM.Models;
using CRM_Repository.Data;
using CRM_Repository.DTOModel;
using CRM_Repository.Service;
using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static CRM.PdfFunction;

namespace CRM.Areas.Master.Controllers
{
    [HasLoginSessionFilter]
    public class PerformaInvoiceController : Controller
    {
        // GET: Master/PerformaInvoice

        private ICompany_Repository _ICompany_Repository;
        private IBuyer_Repository _IBuyer_Repository;

        private IDeliveryTerms_Repository _IDeliveryTerms_Repository;
        private IPaymentTerms_Repository _IPaymentTerms_Repository;
        private IModeShipment_Repository _IModeShipment_Repository;
        private IDesignation_Repository _IDesignation_Repository;
        //private IBuyerContactDetail_Repository _IBuyerContactDetail_Repository;
        private IPort_Repository _IPort_Repository;
        // private IUser_Repository _IUser_Repository;
        private ICountryOrigin_Repository _ICountryOrigin_Repository;
        private IBank_Repository _IBank_Repository;
        private ISupplier_Repository _ISupplier_Repository;
        private IPurchaseOrder_Repository _IPurchaseOrder_Repository;
        private ISupplierContactDetail_Repository _ISupplierContactDetail_Repository;
        private ICurrency_Repository _ICurrency_Repository;
        private IUnit_Repository _IUnit_Repository;
        private IPerformaInvoice_Repository _IPerformaInvoice_Repository;
        private IPerformaPaymentInvoice_Repository _IPerformaPaymentInvoice_Repository;

        public PerformaInvoiceController()
        {
            this._ICompany_Repository = new Company_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IBuyer_Repository = new Buyer_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IDeliveryTerms_Repository = new DeliveryTerms_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IPaymentTerms_Repository = new PaymentTerms_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IDesignation_Repository = new Designation_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IModeShipment_Repository = new ModeShipment_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IPort_Repository = new Port_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._ICountryOrigin_Repository = new CountryOrigin_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IBank_Repository = new Bank_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._ISupplier_Repository = new Supplier_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IPurchaseOrder_Repository = new PurchaseOrder_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._ISupplierContactDetail_Repository = new SupplierContactDetail_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._ICurrency_Repository = new Currency_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IUnit_Repository = new Unit_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IPerformaInvoice_Repository = new PerformaInvoice_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IPerformaPaymentInvoice_Repository = new PerformaPaymentInvoice_Repository(new CRM_Repository.Data.elaunch_crmEntities());
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PerformaPayment(int id = 0, int temp = 0)
        {
            ViewBag.id = id;
            ViewBag.isdisable = temp;
            return View();
        }

        public ActionResult AddPerformaInvoice(int id = 0, int temp = 0)
        {
            ViewBag.id = id;
            ViewBag.isdisable = temp;
            return View();
        }

        public ActionResult AddPerformaProduct()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetMasterInformation()
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                //var ID = _IPerformaInvoice_Repository.GetAllPerformaInvoice().OrderByDescending(X => X.PerformaInvId).FirstOrDefault().PerformaInvId;
                //var ID = 2;
                string Code = GenerateCode();// GenerateCode("INR", 8, ID);
                List<CompanyMaster> CompanyInfo = _ICompany_Repository.GetAllCompay().ToList();
                List<DeliveryTermsMaster> DeliveryTermInfo = _IDeliveryTerms_Repository.GetAllDeliveryTerms().ToList();
                List<PaymentTermsMaster> PaymentTermInfo = _IPaymentTerms_Repository.GetAllPaymentTerms().ToList();
                List<ShipmentMaster> ShipmentInfo = _IModeShipment_Repository.GetAllShipment().ToList();
                List<PortMaster> PortInfo = _IPort_Repository.GetAllPort().ToList();
                List<CountryOfOriginMaster> CountryOfOriginInfo = _ICountryOrigin_Repository.GetAllCountryOfOrigin().ToList();
                List<SupplierMaster> SupplierInfo = _ISupplier_Repository.GetAllSupplier().ToList();
                List<BankMaster> BankInfo = _IBank_Repository.getAllBank().ToList();
                List<PurchaseOrderMaster> PurchaseOrderInfo = _IPurchaseOrder_Repository.GetAllPurchaseOrder().ToList();
                List<CurrencyMaster> CurrencyInfo = _ICurrency_Repository.GetAllCurrency().ToList();
                List<UnitMaster> UnitInfo = _IUnit_Repository.GetAllUnit().ToList();
                var data = new
                {
                    CompanyInfo = CompanyInfo,
                    DeliveryTermInfo = DeliveryTermInfo,
                    PaymentTermInfo = PaymentTermInfo,
                    ShipmentInfo = ShipmentInfo,
                    PortInfo = PortInfo,
                    CountryOfOriginInfo = CountryOfOriginInfo,
                    SupplierInfo = SupplierInfo,
                    BankInfo = BankInfo,
                    PurchaseOrderInfo = PurchaseOrderInfo,
                    CurrencyInfo = CurrencyInfo,
                    UnitInfo = UnitInfo,
                    Code = Code

                };
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get MasterInfo in PerformaInvoice");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                throw;
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetInvoiceInfo()
        {
            DataResponse dataResponse = new DataResponse();
            Common cm = new Common();
            try
            {
                //var getData = _IPerformaInvoice_Repository.GetAllPerformaInvoice();
                //var ID = 0;
                //var newdata = getData.FirstOrDefault();
                //if (newdata != null) { ID = newdata.PerformaInvId; }
                //string InvCode = GenerateCode("INR", 8, (ID + 1));
                //var data = new
                //{
                //    InvCode = InvCode
                //};
                //dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
                var getData = cm.GetAutoNumber("INR");

                var data = new
                {
                    InvCode = getData.Rows[0][0].ToString()
                };
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);


            }
            catch (Exception ex)
            {
                ex.SetLog("Get PerformaInvoice");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public string GenerateCode()
        {
            DataResponse dataResponse = new DataResponse();
            Common cm = new Common();
            string ID = "";
            try
            {
                var getData = cm.GetAutoNumber("INR");

                var data = new
                {
                    ID = getData.Rows[0][0].ToString()
                };
                return ID;
            }
            catch (Exception ex)
            {
                ex.SetLog("Get GenerateCode");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                throw;
            }
        }

        public JsonResult GetConsigneById(int Id)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var data = _IBuyer_Repository.GetConsigneById(Id);
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get Consigne by Id");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetConsigneAddressById(int Id)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var data = _IBuyer_Repository.GetConsigneAddressById(Id);
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get Consigne by Id");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetConsigneContactById(int Id)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var data = _IBuyer_Repository.GetConsigneContactById(Id);
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get Consigne by Id");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetsupplierContactDetailByid(int id)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var data = _ISupplierContactDetail_Repository.GetBySupplierId(id).ToList();
                SupplierMaster OBJSupplier = _ISupplier_Repository.GetById(Convert.ToInt32(id));
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", new
                {
                    data = data,
                    OBJSupplier = OBJSupplier,
                });
            }
            catch (Exception ex)
            {
                ex.SetLog("Get PerformaInvoice");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CreateUpdate(PerformaInvoiceMaster objInputPerforma)
        {
            Common cm = new Common();
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    objInputPerforma.CreatedBy = sessionUtils.UserId;
                    objInputPerforma.ModifyBy = sessionUtils.UserId;
                    objInputPerforma.DeletedBy = sessionUtils.UserId;
                    PerformaInvoiceMaster ResponseVal = _IPerformaInvoice_Repository.CreateUpdate(objInputPerforma);
                    //ResponseVal 1: insert,2:update ,0: error
                    if (ResponseVal != null)
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "successfully", new
                        {
                            valueData = ResponseVal.PerformaInvId
                        });
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Opps! something wrong", null);
                    }
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "Invalid User", null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Create/Update Performa Invoice");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public JsonResult SavePerformaInvoice(PerformaInvoiceMaster PIM)
        //{
        //    DataResponse dataResponse = new DataResponse();
        //    try
        //    {
        //        PerformaInvoiceMaster PIMM = new PerformaInvoiceMaster();
        //        PIMM = PIM;
        //        PIMM.CreatedDate = DateTime.Now;
        //        PIMM.CreatedBy = sessionUtils.UserId;
        //        PIMM.IsActive = true;
        //        _IPerformaInvoice_Repository.AddPerformaInvoice(PIMM);
        //        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, MessageValue.Insert, null);
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.SetLog("Create PerformaInvoice");
        //        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
        //    }
        //    return Json(dataResponse, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public JsonResult UpdatePerformaInvoice(PerformaInvoiceMaster PIM)
        //{
        //    DataResponse dataResponse = new DataResponse();
        //    try
        //    {
        //        PerformaInvoiceMaster PIMM = _IPerformaInvoice_Repository.GetPerformaInvoiceById(PIM.PerformaInvId);
        //        //PIMM = PIM;
        //        PIMM.PerformaInvNo = PIM.PerformaInvNo;
        //        //PIMM.PoId = PIM.PoId;
        //        PIMM.PerformaInvDate = PIM.PerformaInvDate;
        //        //PIMM.CompanyId = PIM.CompanyId;
        //        PIMM.DeliveryTermId = PIM.DeliveryTermId;
        //        //PIMM.ToCompanyId = PIM.ToCompanyId;
        //        PIMM.PaymentTermId = PIM.PaymentTermId;
        //        PIMM.ModeOfShipmentId = PIM.ModeOfShipmentId;
        //        //PIMM.ContactId = PIM.ContactId;
        //        PIMM.LoadingPortId = PIM.LoadingPortId;
        //        //PIMM.Description = PIM.Description;
        //        PIMM.DischargePortId = PIM.DischargePortId;
        //        PIMM.LoadingPortId = PIM.LoadingPortId;
        //        //PIMM.CountryOfOriginId = PIM.CountryOfOriginId;
        //        //PIMM.QtyCode = PIM.QtyCode;
        //        //PIMM.Qty = PIM.Qty;
        //        //PIMM.OfferPriceCode = PIM.OfferPriceCode;
        //        //PIMM.OfferPrice = PIM.OfferPrice;
        //        //PIMM.BeneficialyId = PIM.BeneficialyId;
        //        PIMM.ModifyDate = DateTime.Now;
        //        PIMM.ModifyBy = sessionUtils.UserId;
        //        PIMM.IsActive = true;
        //        _IPerformaInvoice_Repository.UpdatePerformaInvoice(PIMM);
        //        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, MessageValue.Update, null);
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.SetLog("Update PerformaInvoice");
        //        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
        //    }
        //    return Json(dataResponse, JsonRequestBehavior.AllowGet);
        //}
        private string AmountWord(int amount)
        {
            string[] Ones = { "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Ninteen" };
            string[] Tens = { "Ten", "Twenty", "Thirty", "Fourty", "Fift", "Sixty", "Seventy", "Eighty", "Ninty" };
            int no = Convert.ToInt32(amount);
            string strWords = "";
            if (no > 9999 && no < 100000)
            {
                int i = no / 10000;
                strWords = strWords + Ones[i - 1] + " Lack ";
                no = no % 10000;
            }
            if (no > 999 && no < 10000)
            {
                int i = no / 1000;
                strWords = strWords + Ones[i - 1] + " Thousand ";
                no = no % 1000;
            }
            if (no > 99 && no < 1000)
            {
                int i = no / 100;
                strWords = strWords + Ones[i - 1] + " Hundred ";
                no = no % 100;
            }
            if (no > 19 && no < 100)
            {
                int i = no / 10;
                strWords = strWords + Tens[i - 1] + " ";
                no = no % 10;
            }
            if (no > 0 && no < 20)
            {
                strWords = strWords + Ones[no - 1];
            }
            return strWords;
        }

        [HttpPost]
        public JsonResult PrintById(int id)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    int ResponseVal = 0;
                    var str = "";
                    try
                    {
                        PerformaInvoiceMaster objPerformaInvoice = _IPerformaInvoice_Repository.GetAllPerformaInvoiceById(id);
                        List<PerformaProductMaster> objPerformaProduct = _IPerformaInvoice_Repository.GetByPerfomaInvId(id).ToList();

                        ResponseVal = 1;
                        str = GetHtmlString(System.Web.HttpContext.Current.Server.MapPath("~/ReportFormat/PerformaInvoiceReport.html"));

                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        string ContryOfOrigin = "", mainTotalInWords = "";
                        int count = 0; decimal mainTotal = 0;
                        foreach (var x in objPerformaProduct)
                        {
                            count += 1;
                            mainTotal += Convert.ToDecimal(x.OfferPrice);
                            ContryOfOrigin = x.CountryOfOrigin;
                            sb.AppendLine("<div style='width:100%'>");
                            sb.AppendLine("<div style='width:10%; float:left; font-weight:bold; text-align:center;'>" + count + "</div>");
                            sb.AppendLine("<div style='width:10%; float:left; font-weight:bold; text-align:center;'>" + x.ProductName + "</div>");
                            sb.AppendLine("<div style='width:10%; float:left; font-weight:bold; text-align:center;'>" + x.Qty + "</div>");
                            sb.AppendLine("<div style='width:10%; float:left; font-weight:bold; text-align:center;'>" + x.OfferPrice + "</div>");
                            sb.AppendLine("</div>");
                        }
                        mainTotalInWords = AmountWord(Convert.ToInt32(mainTotal));
                        str = str.Replace("@@LoadProduct@@", sb.ToString());
                        str = str.Replace("@@PerformaInvNo@@", objPerformaInvoice.PerformaInvNo);
                        var strdate = Convert.ToString(objPerformaInvoice.PerformaInvDate) != "" ? (objPerformaInvoice.PerformaInvDate.ToString("dd/MM/yyyy")) : "";
                        str = str.Replace("@@PerformaInvDate@@", Convert.ToString(strdate));
                        str = str.Replace("@@DeliveryTerm@@", objPerformaInvoice.DeliveryTerm);
                        str = str.Replace("@@PaymentTerm@@", objPerformaInvoice.PaymentTerm);
                        str = str.Replace("@@ModeOfShipment@@", objPerformaInvoice.ModeOfShipment);
                        str = str.Replace("@@LoadingPort@@", objPerformaInvoice.LoadingPort);
                        str = str.Replace("@@DischargePort@@", objPerformaInvoice.DischargePort);
                        str = str.Replace("@@ConsigneName@@", objPerformaInvoice.ConsigneName);
                        str = str.Replace("@@ConsigneAddress@@", objPerformaInvoice.ConsigneAddress);
                        str = str.Replace("@@ConsigneTel@@", objPerformaInvoice.ConsigneTel);
                        str = str.Replace("@@ContactName@@", objPerformaInvoice.ContactName);
                        str = str.Replace("@@CountryOfOrigin@@", ContryOfOrigin);
                        str = str.Replace("@@Shippingmarks@@", objPerformaInvoice.ShippingMarks);
                        str = str.Replace("@@BeneficiaryName@@", objPerformaInvoice.BeneficiaryName);
                        str = str.Replace("@@BankName@@", objPerformaInvoice.BankName);
                        str = str.Replace("@@BankAddress@@", objPerformaInvoice.BankAddress);
                        str = str.Replace("@@AccountNo@@", objPerformaInvoice.AccountNo);
                        str = str.Replace("@@IFSCCode@@", objPerformaInvoice.IFSCCode);
                        str = str.Replace("@@Total@@", Convert.ToString(mainTotal));
                        str = str.Replace("@@TotalInWords@@", mainTotalInWords);
                    }
                    catch (Exception)
                    {
                        ResponseVal = 0;
                    }
                    //ResponseVal 1: Delete,0: error
                    if (ResponseVal == 1)
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, str, null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Opps! something wrong", null);
                    }
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "Invalid User", null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Print Performa Invoice.");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeletePerformaInvoice(int id)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                int UserId = sessionUtils.UserId;
                _IPerformaInvoice_Repository.DeletePerformaInvoice(id, UserId);
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, MessageValue.Delete, null);
            }
            catch (Exception ex)
            {
                ex.SetLog("Delete PerformaInvoice");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllPerformaInvoiceById(int id)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                PerformaInvoiceMaster objPerformaInvoice = _IPerformaInvoice_Repository.GetAllPerformaInvoiceById(id);
                List<PerformaProductMaster> objPerformaProduct = _IPerformaInvoice_Repository.GetByPerfomaInvId(id).ToList();
                List<PerformaPaymentMaster> objPerformaPayment = _IPerformaInvoice_Repository.GetByPerfomaPaymentInvId(id).ToList();
                decimal TotalAmount = objPerformaProduct.Where(z => z.PerformaInvId == id).Sum(z => z.FinalPrice);
                decimal PaidAmount = objPerformaPayment.Where(z => z.PerformaInvId == id).Sum(z => z.ReceivedAmount);

                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", new
                {
                    objPerformaInvoice = objPerformaInvoice,
                    objPerformaProduct = objPerformaProduct,
                    objPerformaPayment = objPerformaPayment,
                    TotalAmount = TotalAmount,
                    PaidAmount = PaidAmount
                });
            }
            catch (Exception ex)
            {
                ex.SetLog("Get AllPerformaInvoice by Id");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                throw ex;
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CreateUpdatePerformaPayment(PerformaPaymentMaster objPerformaPaymentMaster)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    if (objPerformaPaymentMaster.PerfomaPaymentId <= 0)
                    {
                        objPerformaPaymentMaster.IsActive = true;
                        objPerformaPaymentMaster.CreatedBy = sessionUtils.UserId;
                        objPerformaPaymentMaster.CreatedDate = DateTime.Now.ToUniversalTime();
                        _IPerformaPaymentInvoice_Repository.InsertPerformaPayment(objPerformaPaymentMaster);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Successfully Insert Performa Payment", null);
                    }
                    else
                    {
                        PerformaPaymentMaster tempPerformaPaymentMaster = _IPerformaPaymentInvoice_Repository.GetPerformaPaymentByID(objPerformaPaymentMaster.PerfomaPaymentId);
                        tempPerformaPaymentMaster.PerformaInvId = objPerformaPaymentMaster.PerformaInvId;
                        tempPerformaPaymentMaster.DateofReceipt = objPerformaPaymentMaster.DateofReceipt;
                        tempPerformaPaymentMaster.ReceivedAmount = objPerformaPaymentMaster.ReceivedAmount;
                        tempPerformaPaymentMaster.PaymentMode = objPerformaPaymentMaster.PaymentMode;
                        tempPerformaPaymentMaster.TransactionTypeId = objPerformaPaymentMaster.TransactionTypeId;
                        tempPerformaPaymentMaster.ReceivedRemark = objPerformaPaymentMaster.ReceivedRemark;
                        tempPerformaPaymentMaster.ModifyBy = sessionUtils.UserId;
                        tempPerformaPaymentMaster.ModifyDate = DateTime.Now.ToUniversalTime();
                        _IPerformaPaymentInvoice_Repository.UpdatePerformaPayment(tempPerformaPaymentMaster);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Successfully Update Performa Payment", null);
                    }
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "Invalid User", null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Create/Update Performa Payment");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DeletePerformaPayment(int id)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    PerformaPaymentMaster objPerformaPaymentMaster = _IPerformaPaymentInvoice_Repository.GetPerformaPaymentByID(id);
                    if (objPerformaPaymentMaster != null)
                    {
                        objPerformaPaymentMaster.IsActive = false;
                        objPerformaPaymentMaster.ModifyDate = DateTime.Now.ToUniversalTime();
                        objPerformaPaymentMaster.ModifyBy = sessionUtils.UserId;
                        _IPerformaPaymentInvoice_Repository.UpdatePerformaPayment(objPerformaPaymentMaster);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Successfully Delete Performa Payment", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "No Record Found", null);
                    }
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "Invalid User", null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Create/Update Performa Payment");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }

            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            _IBank_Repository.Dispose();
            _ICompany_Repository.Dispose();
            _ICountryOrigin_Repository.Dispose();
            _ICurrency_Repository.Dispose();
            _IDeliveryTerms_Repository.Dispose();
            _IDesignation_Repository.Dispose();
            _IModeShipment_Repository.Dispose();
            _IPaymentTerms_Repository.Dispose();
            _IPerformaInvoice_Repository.Dispose();
            _IPort_Repository.Dispose();
            _IPurchaseOrder_Repository.Dispose();
            _ISupplierContactDetail_Repository.Dispose();
            _ISupplier_Repository.Dispose();
            _IUnit_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}