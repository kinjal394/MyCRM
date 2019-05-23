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

namespace CRM.Areas.Master.Controllers
{
    [HasLoginSessionFilter]
    public class OutwardCourierController : Controller
    {

        private IOutwardCourier_Repository _IOutwardCourier_Repository;
        private IBuyerContactDetail_Repository _IBuyerContactDetail_Repository;
        private ISupplierContactDetail_Repository _ISupplierContactDetail_Repository;
        private IVendorContactDetail_Repository _IVendorContactDetail_Repository;

        public OutwardCourierController()
        {
            this._IOutwardCourier_Repository = new OutwardCourier_Reposiroty(new CRM_Repository.Data.elaunch_crmEntities());
            this._IBuyerContactDetail_Repository = new BuyerContactDetail_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._ISupplierContactDetail_Repository = new SupplierContactDetail_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IVendorContactDetail_Repository = new VendorContactDetail_Repository(new CRM_Repository.Data.elaunch_crmEntities());
        }

        // GET: Master/OutwardCourier
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddCourier(int id = 0, int temp = 0)
        {
            ViewBag.id = id;
            ViewBag.isdisable = temp;
            return View();
        }

        [HttpPost]
        public JsonResult CreateUpdate(OutwardCourierModel objOutwardCourier)
        {
            string TempImgPath = System.Web.HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["TempImgPath"]);
            string PODPath = System.Web.HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["OutwardPODPath"]);
            if (!System.IO.Directory.Exists(PODPath))
            {
                System.IO.Directory.CreateDirectory(PODPath);
            }

            DataResponse dataResponse = new DataResponse();
            Common cm = new Common();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    objOutwardCourier.CreatedBy = sessionUtils.UserId;
                    objOutwardCourier.ModifyBy = sessionUtils.UserId;
                    if (objOutwardCourier.CourierId <= 0)
                    {
                        var inwardlist = _IOutwardCourier_Repository.GetOutwardCourier().LastOrDefault();
                        if (inwardlist.CourierReffNo == objOutwardCourier.CourierReffNo)
                        {
                            var getData = cm.GetAutoNumber("OutwardCourier");
                            objOutwardCourier.CourierReffNo = getData.Rows[0][0].ToString();
                        }
                    }
                    int ResponseVal = _IOutwardCourier_Repository.CreateUpdate(objOutwardCourier);

                    if (objOutwardCourier.POD != null && objOutwardCourier.POD != "")
                    {
                        if (System.IO.File.Exists(TempImgPath.ToString() + "/" + objOutwardCourier.POD.ToString()))
                        {
                            System.IO.File.Move(TempImgPath.ToString() + "/" + objOutwardCourier.POD.ToString(), PODPath.ToString() + "/" + objOutwardCourier.POD.ToString());
                        }
                    }
                    if (objOutwardCourier.ShipmentPhoto != null && objOutwardCourier.ShipmentPhoto != "")
                    {
                        if (System.IO.File.Exists(TempImgPath.ToString() + "/" + objOutwardCourier.ShipmentPhoto.ToString()))
                        {
                            System.IO.File.Move(TempImgPath.ToString() + "/" + objOutwardCourier.ShipmentPhoto.ToString(), PODPath.ToString() + "/" + objOutwardCourier.ShipmentPhoto.ToString());
                        }
                    }


                    if (ResponseVal == 1)
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully and Your Courier Number is " + objOutwardCourier.CourierReffNo, null);
                    }
                    else if (ResponseVal == 2)
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully and Your Courier Number is " + objOutwardCourier.CourierReffNo, null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Opps! something wrong", null);
                    }
                }
                catch (Exception ex)
                {
                    ex.SetLog("Create/Update OutwardCourier");
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                }
            }
            else
            {
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "User is not valid", null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(int Id)
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    _IOutwardCourier_Repository.DeleteOutwardCourier(Id, sessionUtils.UserId);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Delete successfully", null);
                }
                catch (Exception ex)
                {
                    ex.SetLog("Delete OutwardCourier");
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                }
            }
            else
            {
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "User is not valid", null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetById(int Id)
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    OutwardCourierMaster objOutwardCourier = _IOutwardCourier_Repository.GetByCourierId(Id);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, objOutwardCourier);
                }
                catch (Exception ex)
                {
                    ex.SetLog("Get OutwardCourier by Id");
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                }
            }
            else
            {
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "User is not valid", null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult FetchAllInfoById(int Id)
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    OutwardCourierModel objOutwardCourierMaster = _IOutwardCourier_Repository.FetchAllInfoById(Id);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", new
                    {
                        objOutwardCourierMaster = objOutwardCourierMaster,
                    });
                }
                catch (Exception ex)
                {
                    ex.SetLog("Get FetchInfo in OutwardCourier");
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
        public JsonResult OutwardCourierInfo()
        {
            DataResponse dataResponse = new DataResponse();
            Common cm = new Common();
            try
            {
                var getData = cm.GetAutoNumber("OutwardCourier");
                var data = new
                {
                    CourierReffNo = getData.Rows[0][0].ToString()
                };
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get OutwardInfo");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FetchAddressById(int Id, int AddressId)
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    //if (type == "B")
                    //{
                        var objAddress = _IBuyerContactDetail_Repository.GetAddressByBuyerId(Id).Where(x => x.AddressId == AddressId).FirstOrDefault();
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", objAddress);
                    //}
                    //else if (type == "V")
                    //{
                    //    var objAddress = _IVendorContactDetail_Repository.GetAddressByVendorId(Id).Where(x => x.AddressId == AddressId).FirstOrDefault();
                    //    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", objAddress);
                    //}
                    //else if (type == "B")
                    //{
                    //    var objAddress = _ISupplierContactDetail_Repository.GetAddressBySupplierId(Id).Where(x => x.AddressId == AddressId).FirstOrDefault();
                    //    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", objAddress);
                    //}
                }
                catch (Exception ex)
                {
                    ex.SetLog("Get FetchAddress in OutwardCourier");
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

        protected override void Dispose(bool disposing)
        {
            _IOutwardCourier_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}