using CRM.App_Start;
using CRM.Models;
using CRM_Repository.Data;
using CRM_Repository.DTOModel;
using CRM_Repository.Service;
using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Areas.Transaction.Controllers
{
    [HasLoginSessionFilter]
    public class PurchaseOrderController : Controller
    {
        private CRM_Repository.Data.elaunch_crmEntities context = new CRM_Repository.Data.elaunch_crmEntities();
        private ISupplier_Repository _ISupplier_Repository;
        private ISupplierContactDetail_Repository _ISupplierContactDetail_Repository;
        private ITermsAndCondition_Repository _ITermsAndCondition_Repository;
        private IModeShipment_Repository _IModeShipment_Repository;
        private ICurrency_Repository _ICurrency_Repository;
        private IPurchaseOrder_Repository _IPurchaseOrder_Repository;
        private IPurchaseOrderDetail_Repository _IPurchaseOrderDetail_Repository;
        private ICategory_Repository _ICategory_Repository;
        private ISubCategory_Repository _ISubCategory_Repository;
        private IProduct_Repository _IProduct_Repository;
        private IUnit_Repository _IUnit_Repository;
        private ITechnicalSpecMaster_Repository _ITechnicalSpecMaster_Repository;
        private ICompany_Repository _ICompany_Repository;

        public PurchaseOrderController()
        {


            this._ISupplier_Repository = new Supplier_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._ISupplierContactDetail_Repository = new SupplierContactDetail_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._ITermsAndCondition_Repository = new TermsAndCondition_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IModeShipment_Repository = new ModeShipment_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._ICurrency_Repository = new Currency_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IPurchaseOrder_Repository = new PurchaseOrder_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IPurchaseOrderDetail_Repository = new PurchaseOrderDetail_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._ICategory_Repository = new Category_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._ISubCategory_Repository = new SubCategory_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IProduct_Repository = new Product_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IUnit_Repository = new Unit_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._ITechnicalSpecMaster_Repository = new TechnicalSpecMaster_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._ICompany_Repository=new Company_Repository(new CRM_Repository.Data.elaunch_crmEntities());
        }

        // GET: Transaction/PurchaseOrder
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddPurchaseOrder(int id = 0, int temp = 0)
        {
            ViewBag.id = id;
            ViewBag.isdisable = temp;
            return View();
        }

        public ActionResult AddPurchaseOrderDetail()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetDropDownInformation()
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    List<SupplierMaster> SupplierInfo = _ISupplier_Repository.GetAllSupplier().ToList();
                    List<TermsAndConditionMaster> TermsAndConditionInfo = _ITermsAndCondition_Repository.GetAllTermsAndCondition().ToList();
                    List<ShipmentMaster> ShipmentInfo = _IModeShipment_Repository.GetAllShipment().ToList();
                    List<CurrencyMaster> CurrencyInfo = _ICurrency_Repository.GetAllCurrency().ToList();
                    List<CategoryMaster> CategoryInfo = _ICategory_Repository.GetCategory().ToList();
                    List<SubCategoryMaster> SubCategoryInfo = _ISubCategory_Repository.GetSubCategory().ToList();
                    List<UnitMaster> UnitInfo = _IUnit_Repository.GetAllUnit().ToList();

                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", new
                    {
                        SupplierInfo = SupplierInfo,
                        TermsAndConditionInfo = TermsAndConditionInfo,
                        ShipmentInfo = ShipmentInfo,
                        CurrencyInfo = CurrencyInfo,
                        CategoryInfo = CategoryInfo,
                        SubCategoryInfo = SubCategoryInfo,
                        UnitInfo = UnitInfo
                    });
                }
                catch (Exception ex)
                {
                    ex.SetLog("Get PurchaseOrder");
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                    throw ex;
                }
            }
            else
            {
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "Invalid User", null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetProductBySubcategoryId(int id)
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    List<ProductMaster> ProductInfo = _IProduct_Repository.GetProductBySubCategoryId(id).ToList();
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", ProductInfo);
                }
                catch (Exception ex)
                {
                    ex.SetLog("Get Productby ID in PurchaseOrder");
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
        [HttpPost]
        public JsonResult GetOrderNo(int id)
        {
            DataResponse dataResponse = new DataResponse();
            Common cm = new Common();
            try
            {
                CompanyMaster ListCompanyData = _ICompany_Repository.GetComapnybyid(id);
                var getData = cm.GetAutoNumber("PORNO", ListCompanyData.ComName);
                var data = new
                {
                    PRNo = getData.Rows[0][0].ToString(),
                    companydata= ListCompanyData

                };
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get Supplier Detail in Supplier Master");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetAllPurchaseOrderInfoById(int id)
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                List<PurchaseOrderTechnicalDetailModel> objPurchaseOrderTechnicalDetail = new List<PurchaseOrderTechnicalDetailModel>();
                try
                {
                    PurchaseOrderModel objPurchaseOrderMaster = _IPurchaseOrder_Repository.GetById(id);
                    List<PurchaseOrderDetailModel> objPurchaseOrderDetailMaster = _IPurchaseOrderDetail_Repository.GetByPurchaseOrderId(id).ToList();
                    if (objPurchaseOrderDetailMaster.Count > 0)
                    {
                        PurchaseOrderTechnicalDetailModel obj = new PurchaseOrderTechnicalDetailModel();
                        foreach (var item in objPurchaseOrderDetailMaster)
                        {
                            var objdata = _IPurchaseOrderDetail_Repository.GetTechDetailByPurchaseId(item.PoDetailId).ToList();
                            foreach (var item1 in objdata)
                            {
                                obj.POSpecId = item1.POSpecId;
                                obj.SpecId = item1.SpecId;
                                obj.SpecVal = item1.SpecVal;
                                obj.PoDetailId = item1.PoDetailId;
                                obj.SpecName = item1.SpecName;
                                obj.Status = item1.Status;
                                obj.PoDetailIndex = item1.PoDetailIndex;
                                obj.SpecHead = item1.SpecHead;
                                obj.SpecHeadId = item1.SpecHeadId;
                                objPurchaseOrderTechnicalDetail.Add(item1);
                            }
                        }
                    }
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", new
                    {
                        objPurchaseOrderMaster = objPurchaseOrderMaster,
                        objPurchaseOrderDetailMaster = objPurchaseOrderDetailMaster,
                        objPurchaseOrderTechnicalDetail = objPurchaseOrderTechnicalDetail
                    });
                }
                catch (Exception ex)
                {
                    ex.SetLog("Get PurchaseOrder by ID");
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

        [HttpPost]
        public JsonResult CreateUpdate(PurchaseOrderModel objInputPurchaseOrder)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                string TempImgPath = System.Web.HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["TempImgPath"]);
                string POProductImagePath = System.Web.HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["POProductImagePath"]);
                if (sessionUtils.HasUserLogin())
                {
                    objInputPurchaseOrder.CreatedBy = sessionUtils.UserId;
                    objInputPurchaseOrder.ModifyBy = sessionUtils.UserId;
                    PurchaseOrderMaster ResponseVal = _IPurchaseOrder_Repository.CreateUpdate(objInputPurchaseOrder);
                    if (!System.IO.Directory.Exists(POProductImagePath))
                    {
                        System.IO.Directory.CreateDirectory(POProductImagePath);
                    }
                    if (objInputPurchaseOrder.PurchaseOrderDetails != null)
                    {
                        if (objInputPurchaseOrder.PurchaseOrderDetails.Count > 0)
                        {
                            foreach (var item in objInputPurchaseOrder.PurchaseOrderDetails)
                            {
                                if (item.MachinaryPhotoes != null)
                                {
                                    if (System.IO.File.Exists(TempImgPath.ToString() + "/" + item.MachinaryPhotoes.ToString()))
                                    {
                                        if (!System.IO.File.Exists(POProductImagePath.ToString() + "/" + item.MachinaryPhotoes.ToString()))
                                        {
                                            System.IO.File.Move(TempImgPath.ToString() + "/" + item.MachinaryPhotoes.ToString(), POProductImagePath.ToString() + "/" + item.MachinaryPhotoes.ToString());
                                        }
                                    }
                                }
                                if (item.ProductPhotoes != null)
                                {
                                    if (System.IO.File.Exists(TempImgPath.ToString() + "/" + item.ProductPhotoes.ToString()))
                                    {
                                        if (!System.IO.File.Exists(POProductImagePath.ToString() + "/" + item.ProductPhotoes.ToString()))
                                        {
                                            System.IO.File.Move(TempImgPath.ToString() + "/" + item.ProductPhotoes.ToString(), POProductImagePath.ToString() + "/" + item.ProductPhotoes.ToString());
                                        }
                                    }
                                }
                            }
                        }
                    }
                    //ResponseVal 1: insert,2:update ,0: error
                    //if (ResponseVal == 1)
                    //{

                    //    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully", null);
                    //}
                    //else if (ResponseVal == 2)
                    //{
                    //    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully", null);
                    //}
                    //else
                    //{
                    //    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Opps! something wrong", null);
                    //}
                    if (ResponseVal != null)
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Successfully", new
                        {
                            objPurchaseOrderDetailMaster = _IPurchaseOrderDetail_Repository.GetByPurchaseOrderId(ResponseVal.PoId).ToList(),
                            valueData = ResponseVal.PoId
                        });
                    }
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "Invalid User", null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Create/Update PurchaseOrder");
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
                    PurchaseOrderModel objInputPurchaseOrder = new PurchaseOrderModel();
                    objInputPurchaseOrder.PoId = id;
                    objInputPurchaseOrder.DeleteBy = sessionUtils.UserId;

                    int ResponseVal = _IPurchaseOrder_Repository.Delete(objInputPurchaseOrder);
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
                ex.SetLog("Delete PurchaseOrder");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        //[HttpGet]
        //public JsonResult DeleteByIdOLD(int id)
        ////{
        ////    DataResponse dataResponse = new DataResponse();
        ////    if (sessionUtils.HasUserLogin())
        ////    {
        ////        try
        ////        {
        ////            PurchaseOrderMaster objPurchaseOrderMaster = _IPurchaseOrder_Repository.GetById(id);
        ////            objPurchaseOrderMaster.IsActive = false;
        ////            objPurchaseOrderMaster.DeletedBy = sessionUtils.UserId;
        ////            objPurchaseOrderMaster.DeletedDate = DateTime.Now;
        ////            _IPurchaseOrder_Repository.UpdatePurchaseOrder(objPurchaseOrderMaster);
        ////            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Delete successfully", null);
        ////        }
        ////        catch (Exception ex)
        ////        {
        ////            throw;
        ////        }
        ////    }
        ////    else
        ////    {
        ////        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "Invalid User", null);
        ////    }
        ////    return Json(dataResponse, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult GetInvoiceInfo()
        {
            DataResponse dataResponse = new DataResponse();
            Common cm = new Common();
            try
            {
                var getData = cm.GetAutoNumber("POR");
                List<TechnicalSpecMaster> ListTechnicalSpecMaster = _ITechnicalSpecMaster_Repository.GetTechnicalSpec().ToList();

                var data = new
                {
                    InvCode = getData.Rows[0][0].ToString(),
                    ListTechnicalSpecMaster = ListTechnicalSpecMaster
                };
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get InvoiceInfo in PurchaseOrder");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetSupplierDetail(int Id)
        {
            DataResponse dataResponse = new DataResponse();
            Common cm = new Common();
            try
            {
                SupplierModel ListSupplierData = _ISupplier_Repository.FetchById(Id);
                var getData = cm.GetAutoNumber("Supref", ListSupplierData.CompanyName, Id);
                var data = new
                {
                    PoRefNo = getData.Rows[0][0].ToString(),
                    SupplierData = ListSupplierData
                };
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get Supplier Detail in Supplier Master");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetSupplierContactDetail(int Id)
        {
            DataResponse dataResponse = new DataResponse();
            Common cm = new Common();
            try
            {
                List<SupplierContactDetail> ListSupplierContactData = _ISupplierContactDetail_Repository.GetByContactId(Id).ToList();
                var data = new
                {
                    SupplierContactData = ListSupplierContactData
                };
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get Supplie rContact Detail in Supplier Contact Detail");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
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
    }
}