using CRM.Models;
using CRM_Repository.Data;
using CRM_Repository.Service;
using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM_Repository.DTOModel;
using CRM.App_Start;

namespace CRM.Areas.Master
{
    [HasLoginSessionFilter]
    public class SupplierController : Controller
    {

        private ICountry_Repository _ICountry_Repository;
        private ISupplier_Repository _ISupplier_Repository;
        private ISupplierContactDetail_Repository _ISupplierContactDetail_Repository;
        private ISupplierBankDetail_Repository _ISupplierBankDetail_Repository;

        public SupplierController()
        {
            this._ICountry_Repository = new Country_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._ISupplier_Repository = new Supplier_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._ISupplierContactDetail_Repository = new SupplierContactDetail_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._ISupplierBankDetail_Repository = new SupplierBankDetail_Repository(new CRM_Repository.Data.elaunch_crmEntities());
        }

        // GET: Master/Supplier
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddSupplier(int id = 0,int temp=0)
        {
            ViewBag.id = id;
            ViewBag.isdisable = temp;
            return View();
        }

        public ActionResult AddSupplierAddress()
        {
            return View();
        }

        public ActionResult AddSupplierContact()
        {
            return View();
        }

        public ActionResult AddSupplierBank()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetMasterInformation()
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    List<CountryMaster> CountryInfo = _ICountry_Repository.GetAllCountry().ToList();
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", CountryInfo);
                }
                catch (Exception ex)
                {
                    ex.SetLog("Get All Country in Supplier");
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
        public JsonResult CreateUpdate(SupplierModel objInputSupplier)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    objInputSupplier.CreatedBy = sessionUtils.UserId;
                    objInputSupplier.ModifyBy = sessionUtils.UserId;
                    objInputSupplier.DeleteBy = sessionUtils.UserId;
                    int ResponseVal = _ISupplier_Repository.CreateUpdate(objInputSupplier);
                    //ResponseVal 1: insert,2:update ,0: error
                    if (ResponseVal == 1)
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully", null);
                    }
                    else if (ResponseVal == 2)
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully", null);
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
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.SetLog("Create/Update Supplier");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult DeleteById(int id)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    SupplierModel objInputSupplier = new SupplierModel();
                    objInputSupplier.SupplierId = id;
                    objInputSupplier.DeleteBy = sessionUtils.UserId;

                    int ResponseVal = _ISupplier_Repository.Delete(objInputSupplier);
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
                ex.SetLog("Delete Supplier");
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
                    SupplierMaster objSupplierMaster = _ISupplier_Repository.GetById(id);
                    objSupplierMaster.IsActive = false;
                    objSupplierMaster.DeletedBy = sessionUtils.UserId;
                    objSupplierMaster.DeletedDate = DateTime.Now;
                    _ISupplier_Repository.UpdateSupplier(objSupplierMaster);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Delete successfully", null);
                }
                catch (Exception ex)
                {
                    ex.SetLog("Delete Supplier by Id");
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                    return Json(dataResponse, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "Invalid User", null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllSupplierInfoById(int id)
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    //--BankDetailId,SupplierId,BankName,IFSCCode,AccountName,BeneficiaryName,CreatedBy,CreatedDate,ModifyBy,ModifyDate,DeletedBy,DeletedDate,IsActive,AccountNo
                    SupplierModel objSupplierMaster = _ISupplier_Repository.FetchById(id);
                    List<SupplierContactDetail> objSupplierContactDetail = _ISupplierContactDetail_Repository.GetBySupplierId(id).ToList();
                    List<SupplierBankMaster> objSupplierBankDetail = _ISupplierBankDetail_Repository.GetBySupplierId(id).ToList();
                    List<SupplierChatMaster> objSupplierChatDetail = _ISupplierContactDetail_Repository.GetChatBySupplierId(id).ToList();
                    List<SupplierAddressMaster> objSupplierAddressDetail = _ISupplierContactDetail_Repository.GetAddressBySupplierId(id).ToList();

                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", new
                    {
                        objSupplierMaster = objSupplierMaster,
                        objSupplierContactDetail = objSupplierContactDetail,
                        objSupplierBankDetail = objSupplierBankDetail,
                        objSupplierChatDetail = objSupplierChatDetail,
                        objSupplierAddressDetail = objSupplierAddressDetail
                    });
                }
                catch (Exception ex)
                {
                    ex.SetLog("Get Supplier");
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                    return Json(dataResponse, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "Invalid User", null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            _ICountry_Repository.Dispose();
            _ISupplierBankDetail_Repository.Dispose();
            _ISupplierContactDetail_Repository.Dispose();
            _ISupplier_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}