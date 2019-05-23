using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM_Repository.Service;
using CRM_Repository.ServiceContract;
using CRM_Repository.Data;
using CRM.Models;
using CRM_Repository.DTOModel;
using CRM.App_Start;

namespace CRM.Areas.Transaction.Controllers
{
    [HasLoginSessionFilter]
    public class SalesOrderController : Controller
    {
        private ISalesOrder_Repository _ISalesOrder_Repository;
        private ISalesItemDetail_Repository _ISalesItemDetail_Repository;
        private ISalesTechnicalDetail_Repository _ISalesTechnicalDetail_Repository;
        private ICompany_Repository _ICompany_Repository;
        private IBuyer_Repository _IBuyer_Repository;
        private IBuyerContactDetail_Repository _IBuyerContactDetail_Repository;
        private IDeliveryTerms_Repository _IDeliveryTerms_Repository;
        private IPaymentTerms_Repository _IPaymentTerms_Repository;
        private ICountry_Repository _ICountry_Repository;
        private ICategory_Repository _ICategory_Repository;
        private ISubCategory_Repository _ISubCategory_Repository;
        private IProduct_Repository _IProduct_Repository;
        private ICountryOrigin_Repository _ICountryOrigin_Repository;
        private ICurrency_Repository _ICurrency_Repository;
        private IUnit_Repository _IUnit_Repository;
        private ISpecification_Repository _ISpecification_Repository;


        public SalesOrderController()
        {
            this._ISalesOrder_Repository = new SalesOrder_Repository(new elaunch_crmEntities());
            this._ISalesItemDetail_Repository = new SalesItemDetail_Repository(new elaunch_crmEntities());
            this._ISalesTechnicalDetail_Repository = new SalesTechnicalDetail_Repository(new elaunch_crmEntities());
            this._ICompany_Repository = new Company_Repository(new elaunch_crmEntities());
            this._IBuyer_Repository = new Buyer_Repository(new elaunch_crmEntities());
            this._IBuyerContactDetail_Repository = new BuyerContactDetail_Repository(new elaunch_crmEntities());
            this._IDeliveryTerms_Repository = new DeliveryTerms_Repository(new elaunch_crmEntities());
            this._IPaymentTerms_Repository = new PaymentTerms_Repository(new elaunch_crmEntities());
            this._ICountry_Repository = new Country_Repository(new elaunch_crmEntities());
            this._ICategory_Repository = new Category_Repository(new elaunch_crmEntities());
            this._ISubCategory_Repository = new SubCategory_Repository(new elaunch_crmEntities());
            this._IProduct_Repository = new Product_Repository(new elaunch_crmEntities());
            this._ICountryOrigin_Repository = new CountryOrigin_Repository(new elaunch_crmEntities());
            this._ICurrency_Repository = new Currency_Repository(new elaunch_crmEntities());
            this._IUnit_Repository = new Unit_Repository(new elaunch_crmEntities());
            this._ISpecification_Repository = new Specification_Repository(new elaunch_crmEntities());
        }

        // GET: Transaction/SalesOrder
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddSalesOrder(int id = 0,int temp=0)
        {
            ViewBag.id = id;
            ViewBag.isdisable = temp;
            return View();
        }

        public ActionResult AddSalesItemDetail()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetMasterInformation()
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var ID = _ISalesOrder_Repository.GetAllSalesOrder().FirstOrDefault().SOId;
                string InvCode = GenerateCode() ;//"SORD", 8, (ID + 1));
                List<CompanyMaster> CompanyInfo = _ICompany_Repository.GetAllCompay().ToList();
                List<BuyerMaster> BuyerInfo = _IBuyer_Repository.GetAllBuyer().ToList();
                List<BuyerContactDetail> BuyerContactInfo = _IBuyerContactDetail_Repository.GetAllBuyerContactDetail().ToList();
                List<DeliveryTermsMaster> DeliveryTermsInfo = _IDeliveryTerms_Repository.GetAllDeliveryTerms().ToList();
                List<PaymentTermsMaster> PaymentTermsInfo = _IPaymentTerms_Repository.GetAllPaymentTerms().ToList();
                List<CountryMaster> CountryInfo = _ICountry_Repository.GetAllCountry().ToList();
                List<CategoryMaster> CategoryInfo = _ICategory_Repository.GetCategory().ToList();
                List<SubCategoryMaster> SubCategoryInfo = _ISubCategory_Repository.GetSubCategory().ToList();
                List<ProductMaster> ProductInfo = _IProduct_Repository.GetAllProducts().ToList();
                List<CountryOfOriginMaster> CountryOfOriginInfo = _ICountryOrigin_Repository.GetAllCountryOfOrigin().ToList();
                List<CurrencyMaster> CurrencyInfo = _ICurrency_Repository.GetAllCurrency().ToList();
                List<UnitMaster> UnitInfo = _IUnit_Repository.GetAllUnit().ToList();
                List<TechnicalSpecMaster> SpecificationInfo = _ISpecification_Repository.GetAllSpecification().ToList();

                var data = new
                {
                    CompanyInfo = CompanyInfo,
                    BuyerInfo = BuyerInfo,
                    BuyerContactInfo = BuyerContactInfo,
                    DeliveryTermsInfo = DeliveryTermsInfo,
                    PaymentTermsInfo = PaymentTermsInfo,
                    CountryInfo = CountryInfo,
                    CategoryInfo = CategoryInfo,
                    SubCategoryInfo = SubCategoryInfo,
                    ProductInfo = ProductInfo,
                    CountryOfOriginInfo = CountryOfOriginInfo,
                    CurrencyInfo = CurrencyInfo,
                    UnitInfo = UnitInfo,
                    SpecificationInfo = SpecificationInfo,
                    InvCode = InvCode
                };
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get MasterInfo in SalesOrder");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetInvoiceInfo()
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
               // DataResponse dataResponse = new DataResponse();
                Common cm = new Common();
                //var getData = _ISalesOrder_Repository.GetAllSalesOrder();
                //var ID = 0;
                //var newdata = getData.FirstOrDefault();
                //if (newdata != null) { ID = newdata.SOId; }
                //string InvCode = GenerateCode("SORD", 8, (ID + 1));
                //var data = new
                //{
                //    InvCode = InvCode
                //};
                //dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
                var getData = cm.GetAutoNumber("SOR");

                var data = new
                {
                    InvCode = getData.Rows[0][0].ToString()
                };
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get InvoiceInfo in SalesOrder");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public string GenerateCode() {
            Common cm = new Common();
            try
            {
               
                var getData = cm.GetAutoNumber("SOR");
                return getData.Rows[0][0].ToString();
                
               
            }
            catch (Exception)
            {
                throw;
            }
           
        }

        //public string GenerateCode(string Prefix, int? Total, int? Id)
        //{
        //    string STR = "";
        //    if (Id != 0)
        //    {

        //        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //        if (Total == 0)
        //            Total = 8;
        //        var L = Total - Id.Value.ToString().Length;
        //        for (int i = 0; i < L; i++)
        //        {
        //            if (i == 0)
        //            {
        //                int X = 0;
        //                sb.Append(Prefix + X);
        //            }
        //            else
        //                sb.Append(0);
        //        }
        //        STR = sb.ToString() + Id;
        //    }
        //    else
        //        STR = Prefix + "00000001";
        //    return STR;

        //}

        [HttpGet]
        public JsonResult GetBuyerById(int id)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                List<BuyerContactDetail> data = _IBuyerContactDetail_Repository.GetByBuyerId(id).ToList();
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get BuyerInfo in SalesOrder");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetSubCategoryById(int id)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                List<ProductMaster> data = _IProduct_Repository.GetProductBySubCategoryId(id).ToList();
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get SubCategory in SalesOrder");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CreateUpdate(SalesOrderModel objInputSalesOrderMaster)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    objInputSalesOrderMaster.CreatedBy = sessionUtils.UserId;
                    objInputSalesOrderMaster.ModifyBy = sessionUtils.UserId;
                    var SOdata = _ISalesOrder_Repository.GetAllSalesOrder().FirstOrDefault();
                    if (objInputSalesOrderMaster.SoNo == objInputSalesOrderMaster.SoNo && objInputSalesOrderMaster.SOId <= 0)
                    {
                        Common cm = new Common();
                        var data = cm.GetAutoNumber("SOR");
                        objInputSalesOrderMaster.SoNo = data.Rows[0][0].ToString();
                    }
                    int ResponseVal = _ISalesOrder_Repository.CreateUpdate(objInputSalesOrderMaster);
                    //ResponseVal 1: insert,2:update ,0: error
                    if (ResponseVal == 1)
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully and Your SalesOrder Number is " + objInputSalesOrderMaster.SoNo, null);
                    }
                    else if (ResponseVal == 2)
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully and Your SalesOrder Number is " + objInputSalesOrderMaster.SoNo, null);
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
                ex.SetLog("Create/Update SalesOrder");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteById(int id)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    SalesOrderModel objInputSalesOrderMaster = new SalesOrderModel();
                    objInputSalesOrderMaster.SOId = id;
                    objInputSalesOrderMaster.DeleteBy = sessionUtils.UserId;

                    int ResponseVal = _ISalesOrder_Repository.Delete(objInputSalesOrderMaster);
                    //ResponseVal 1: Delete,0: error
                    if (ResponseVal == 1)
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Delete successfully", null);
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
                ex.SetLog("Delete SalesOrder");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DeleteByIdOLD(int id)
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    SalesOrderMaster objSalesOrderMaster = _ISalesOrder_Repository.GetSOById(id);
                    objSalesOrderMaster.IsActive = false;
                    objSalesOrderMaster.DeletedBy = Convert.ToInt32(Session["UserId"]);
                    objSalesOrderMaster.DeletedDate = DateTime.Now;
                    _ISalesOrder_Repository.UpdateSalesOrder(objSalesOrderMaster);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Delete successfully", null);
                }
                catch (Exception ex)
                {
                    ex.SetLog("Delete SalesOrder by Id");
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
        public JsonResult GetAllSalesOrderInfoById(int id)
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    SalesOrderModel objSalesOrderMaster = _ISalesOrder_Repository.GetSalesOrderById(id);
                    List<vmSalesItemDetail> objvmSalesItemDetail = new List<vmSalesItemDetail>();
                    List<vmSalesTechnicalDetail> objvmSalesTechnicalDetail = new List<vmSalesTechnicalDetail>();

                    List<vmSalesItemDetail> objSalesItemDetail = _ISalesItemDetail_Repository.GetSalesItemDetailById(id).ToList();
                    if (objSalesItemDetail != null)
                    {
                        foreach (var item in objSalesItemDetail)
                        {
                            vmSalesItemDetail vmSItem = new vmSalesItemDetail();
                            vmSItem.ItemId = item.ItemId;
                            vmSItem.SOId = item.SOId;
                            vmSItem.CategoryId = item.CategoryId;
                            vmSItem.Category = item.Category;
                            vmSItem.SubCategoryId = item.SubCategoryId;
                            vmSItem.SubCategory = item.SubCategory;
                            vmSItem.MainProductId = item.MainProductId;
                            vmSItem.MainProductName = item.MainProductName;
                            vmSItem.ProductId = item.ProductId;
                            vmSItem.ProductName = item.ProductName;
                            vmSItem.ProductDescription = item.ProductDescription;
                            vmSItem.ModelNo = item.ModelNo;
                            vmSItem.OriginId = item.OriginId;
                            vmSItem.CountryOfOrigin = item.CountryOfOrigin;
                            vmSItem.QtyCodeData = item.QtyCodeData;
                            vmSItem.QtyCode = item.QtyCode;
                            vmSItem.Qty = item.Qty;
                            vmSItem.UnitPriceCodeData = item.UnitPriceCodeData;
                            vmSItem.UnitPriceCode = item.UnitPriceCode;
                            vmSItem.UnitPrice = item.UnitPrice;
                            vmSItem.Status = 2;
                            objvmSalesItemDetail.Add(vmSItem);
                            List<SalesTechnicalDetailMaster> objSalesTechnicalDetail = _ISalesTechnicalDetail_Repository.GetBySalesItemId(item.ItemId).ToList();
                            //List<vmSalesTechnicalDetail> objSalesTechnicalDetail = _ISalesTechnicalDetail_Repository.GetSalesTechnicalDetailById(item.ItemId).ToList();
                            if (objSalesTechnicalDetail != null)
                            {
                                foreach (var subitem in objSalesTechnicalDetail)
                                {
                                    vmSalesTechnicalDetail vmSTech = new vmSalesTechnicalDetail();
                                    vmSTech.TechDetailId = subitem.TechDetailId;
                                    vmSTech.ItemId = subitem.ItemId;
                                    vmSTech.TechParaId = subitem.TechParaId;
                                    vmSTech.Value = subitem.Value;
                                    vmSTech.Status = 2;
                                    objvmSalesTechnicalDetail.Add(vmSTech);
                                }
                            }
                        }
                    }

                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", new
                    {
                        objSalesOrderMaster = objSalesOrderMaster,
                        objSalesItemDetail = objvmSalesItemDetail,
                        objSalesTechnicalDetail = objvmSalesTechnicalDetail
                    });
                }
                catch (Exception ex)
                {
                    ex.SetLog("Get All SalesOrder");
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
        public JsonResult GetSpecificationList()
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                List<TechnicalSpecMaster> SpecificationInfo = _ISpecification_Repository.GetAllSpecification().ToList();
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", SpecificationInfo);
            }
            catch (Exception ex)
            {
                ex.SetLog("GetList SalesOrder");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        public JsonResult GetBuyerDetailById(int id)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var data = _IBuyer_Repository.GetBuyerById(id).ToList();
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get BuyerDetail in SalesOrder");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetBuyerContactDetailById(int id)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                BuyerContactDetail data = _IBuyerContactDetail_Repository.GetBCbyid(id);
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get BuyerContactDetail in SalesOrder");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetCompanyDetailById(int id)
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
    }
}