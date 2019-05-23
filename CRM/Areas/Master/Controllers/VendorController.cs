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

namespace CRM.Areas.Master.Controllers
{
    [HasLoginSessionFilter]
    public class VendorController : Controller
    {
        private IVendor_Repository _IVendor_Repository;
        private ICountry_Repository _ICountry_Repository;
        private IState_Repository _IState_Repository;
        private ICity_Repository _ICity_Repository;
        private IDesignation_Repository _IDesignation_Repository;
        private IAgencyType_Repository _IAgencyType_Repository;
        private IVendorContactDetail_Repository _IVendorContactDetail_Repository;
        private IVendorBankDetail_Repository _IVendorBankDetail_Repository;

        public VendorController()
        {
            this._IVendor_Repository = new Vendor_Repository(new elaunch_crmEntities());
            this._ICountry_Repository = new Country_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IState_Repository = new State_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._ICity_Repository = new City_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IDesignation_Repository = new Designation_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IAgencyType_Repository = new AgencyType_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IVendorContactDetail_Repository = new VendorContactDetail_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IVendorBankDetail_Repository = new VendorBankDetail_Repository(new CRM_Repository.Data.elaunch_crmEntities());
        }

        // GET: Master/Vendor
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddVendor(int id = 0,int temp=0)
        {
            ViewBag.id = id;
            ViewBag.isdisable = temp;
            return View();
        }

        public ActionResult AddVendorAddress()
        {
            return View();
        }

        public ActionResult AddVendorContact()
        {
            return View();
        }

        public ActionResult AddVendorBank()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CreateUpdate(VendorModel objInputVendorMaster)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    objInputVendorMaster.CreatedBy = sessionUtils.UserId;
                    objInputVendorMaster.ModifyBy = sessionUtils.UserId;
                    objInputVendorMaster.DeletedBy = sessionUtils.UserId;

                    int ResponseVal = _IVendor_Repository.CreateUpdate(objInputVendorMaster);
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
            }
            catch (Exception ex)
            {
                ex.SetLog("Create/Update Vendor");
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
                    VendorModel objInputVendor = new VendorModel();
                    objInputVendor.VendorId = id;
                    objInputVendor.DeletedBy = sessionUtils.UserId;
                    int ResponseVal = _IVendor_Repository.Delete(objInputVendor);
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
                ex.SetLog("Delete Vendor");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllVendorInfoById(int id)
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    VendorModel objVendorMaster = _IVendor_Repository.GetVendorById(id);
                    List<vmVendorContactDetail> objVendorContactDetail = _IVendorContactDetail_Repository.GetByVendorId(id).ToList();
                    List<VendorBankMaster> objVendorBankDetail = _IVendorBankDetail_Repository.GetByVendorId(id).ToList();
                    List<VendorChatMaster> objVendorChatDetail = _IVendorContactDetail_Repository.GetChatByVendorId(id).ToList();
                    List<VendorAddressDetail> objVendorAddressDetail = _IVendorContactDetail_Repository.GetAddressByVendorId(id).ToList();
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", new
                    {
                        objVendorMaster = objVendorMaster,
                        objVendorContactDetail = objVendorContactDetail,
                        objVendorBankDetail = objVendorBankDetail,
                        objVendorChatDetail = objVendorChatDetail,
                        objVendorAddressDetail = objVendorAddressDetail
                    });
                }
                catch (Exception ex)
                {
                    ex.SetLog("Get Vendor");
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                }
            }
            else
            {
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "Invalid User", null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        //[HttpGet]
        //public JsonResult DeleteById(int id)
        //{
        //    DataResponse dataResponse = new DataResponse();
        //    if (sessionUtils.HasUserLogin())
        //    {
        //        try
        //        {
        //            VendorMaster objVendorMaster = _IVendor_Repository.GetVendorById(id);
        //            objVendorMaster.IsActive = false;
        //            objVendorMaster.DeletedBy = Convert.ToInt32(Session["UserId"]);
        //            objVendorMaster.DeletedDate = DateTime.Now;
        //            _IVendor_Repository.UpdateVendor(objVendorMaster);
        //            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Delete successfully", null);
        //        }
        //        catch (Exception ex)
        //        {
        //            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "", null);
        //        }
        //    }
        //    else
        //    {
        //        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "Invalid User", null);
        //    }
        //    return Json(dataResponse, JsonRequestBehavior.AllowGet);
        //}
        protected override void Dispose(bool disposing)
        {
            _IAgencyType_Repository.Dispose();
            _ICity_Repository.Dispose();
            _ICountry_Repository.Dispose();
            _IDesignation_Repository.Dispose();
            _IState_Repository.Dispose();
            _IVendorBankDetail_Repository.Dispose();
            _IVendorContactDetail_Repository.Dispose();
            _IVendor_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}