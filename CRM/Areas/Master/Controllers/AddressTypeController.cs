using CRM.Models;
using CRM_Repository.Data;
using CRM_Repository.Service;
using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Areas.Master.Controllers
{
    public class AddressTypeController : Controller
    {
        private IAddressType_Repository _IAddressType_Repository;

        public AddressTypeController()
        {
            this._IAddressType_Repository = new AddressType_Repository(new elaunch_crmEntities());
        }
        // GET: Master/AddressType
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddAddressType()
        {
            return View();
        }


        //[HttpPost]
        public JsonResult SaveAddressType(AddressTypeMaster objAddressType)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                AddressTypeMaster addresstypeobj = new AddressTypeMaster();
                addresstypeobj.AddressTypeId = objAddressType.AddressTypeId;
                addresstypeobj.AddressTypeName = objAddressType.AddressTypeName.Trim();
                addresstypeobj.IsActive = true;
                if (addresstypeobj.AddressTypeId > 0)
                {
                    var cntList = _IAddressType_Repository.DuplicateEditAddressTypeName(addresstypeobj.AddressTypeId, addresstypeobj.AddressTypeName).ToList();
                    if (cntList.Count == 0)
                    {
                        _IAddressType_Repository.UpdateAddressType(addresstypeobj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully.", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "AddressType Already Exists", null);
                    }
                }
                else
                {
                    var cntList = _IAddressType_Repository.DuplicateAddressType(addresstypeobj.AddressTypeName).ToList();
                    if (cntList.Count == 0)
                    {
                        _IAddressType_Repository.AddAddressType(addresstypeobj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully.", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "AddressType Already Exists", null);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Create/Update AddressType");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult DeleteAddressType(int AddressTypeId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                AddressTypeMaster addresstypeobj = new AddressTypeMaster();
                addresstypeobj = _IAddressType_Repository.GetAddressTypeByID(AddressTypeId);
                addresstypeobj.IsActive = false;
                _IAddressType_Repository.UpdateAddressType(addresstypeobj);
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Delete successfully.", null);
            }
            catch (Exception ex)
            {
                ex.SetLog("Delete AddressType");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }



        public JsonResult GetByIdAddressType(int AddressTypeId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var obj = _IAddressType_Repository.GetAddressTypeByID(Convert.ToInt32(AddressTypeId));
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, obj);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get AddressType by Id");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }


        protected override void Dispose(bool disposing)
        {
            _IAddressType_Repository.Dispose();
            base.Dispose(disposing);
        }

    }
}