using CRM.App_Start;
using CRM.Models;
using CRM_Repository.Data;
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
    public class InwardCourierController : Controller
    {
        private IInwardCourier_Repository _IInwardCourier_Repository;

        public InwardCourierController()
        {
            this._IInwardCourier_Repository = new InwardCourier_Repository(new CRM_Repository.Data.elaunch_crmEntities());
        }

        // GET: Master/InwardCourier
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
        public JsonResult CreateUpdate(InwardCourierMaster objInwardCourier)
        {
            DataResponse dataResponse = new DataResponse();
            Common cm = new Common();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    string TempImgPath = System.Web.HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["TempImgPath"]);
                    string InwardPODPath = System.Web.HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["InwardPODPath"]);
                    if (!System.IO.Directory.Exists(InwardPODPath))
                    {
                        System.IO.Directory.CreateDirectory(InwardPODPath);
                    }
                    if (objInwardCourier.CourierId <= 0)
                    {
                        var inwardlist = _IInwardCourier_Repository.GetInwardCourier().LastOrDefault();
                        if (inwardlist.CourierReffNo == objInwardCourier.CourierReffNo)
                        {
                            var getData = cm.GetAutoNumber("InwardCourier");
                            objInwardCourier.CourierReffNo = getData.Rows[0][0].ToString();
                        }
                        objInwardCourier.IsActive = true;
                        objInwardCourier.CreatedBy = sessionUtils.UserId;
                        objInwardCourier.CreatedDate = DateTime.Now;
                        _IInwardCourier_Repository.SaveInwardCourier(objInwardCourier);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully and Your Courier Number is " + objInwardCourier.CourierReffNo, null);
                    }
                    else
                    {
                        InwardCourierMaster objInwardCourierMaster = _IInwardCourier_Repository.GetByCourierId(objInwardCourier.CourierId);
                        objInwardCourierMaster.CourierDate = objInwardCourier.CourierDate;
                        objInwardCourierMaster.CourierTime = objInwardCourier.CourierTime;
                        objInwardCourierMaster.SenderId = objInwardCourier.SenderId;
                        objInwardCourierMaster.SenderType = objInwardCourier.SenderType;
                        objInwardCourierMaster.VendorId = objInwardCourier.VendorId;
                        objInwardCourierMaster.ShipmentRemark = objInwardCourier.ShipmentRemark;
                        objInwardCourierMaster.ReceivedBy = objInwardCourier.ReceivedBy;
                        objInwardCourierMaster.POD = objInwardCourier.POD;
                        objInwardCourierMaster.ShipmentPhoto = objInwardCourier.ShipmentPhoto;
                        objInwardCourierMaster.ShipmentRefNo = objInwardCourier.ShipmentRefNo;
                        objInwardCourierMaster.CourierTypeId = objInwardCourier.CourierTypeId;
                        objInwardCourierMaster.CourierReffNo = objInwardCourier.CourierReffNo;
                        objInwardCourierMaster.VendorName = objInwardCourier.VendorName;
                        objInwardCourier.ModifyBy = sessionUtils.UserId;
                        objInwardCourier.ModifyDate = DateTime.Now;
                        _IInwardCourier_Repository.UpdateInwardCourier(objInwardCourierMaster);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully and Your Courier Number is " + objInwardCourier.CourierReffNo, null);
                    }
                    if (objInwardCourier.POD != null)
                    {
                        if (System.IO.File.Exists(TempImgPath.ToString() + "/" + objInwardCourier.POD.ToString()))
                        {
                            if (!System.IO.File.Exists(InwardPODPath.ToString() + "/" + objInwardCourier.POD.ToString()))
                            {
                                System.IO.File.Move(TempImgPath.ToString() + "/" + objInwardCourier.POD.ToString(), InwardPODPath.ToString() + "/" + objInwardCourier.POD.ToString());
                            }
                        }
                    }


                    if (objInwardCourier.ShipmentPhoto != null)
                    {
                        if (System.IO.File.Exists(TempImgPath.ToString() + "/" + objInwardCourier.ShipmentPhoto.ToString()))
                        {
                            if (!System.IO.File.Exists(InwardPODPath.ToString() + "/" + objInwardCourier.ShipmentPhoto.ToString()))
                            {
                                System.IO.File.Move(TempImgPath.ToString() + "/" + objInwardCourier.ShipmentPhoto.ToString(), InwardPODPath.ToString() + "/" + objInwardCourier.ShipmentPhoto.ToString());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ex.SetLog("Create InwardCourier");
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
                    _IInwardCourier_Repository.DeleteInwardCourier(Id, sessionUtils.UserId);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Delete successfully", null);
                }
                catch (Exception ex)
                {
                    ex.SetLog("Delete InwardCourier");
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
                    InwardCourierMaster objInwardCourier = _IInwardCourier_Repository.GetByCourierId(Id);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, objInwardCourier);
                }
                catch (Exception ex)
                {
                    ex.SetLog("Get InwardCourier by Id");
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                }
            }
            else
            {
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "User is not valid", null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult InwardCourierInfo()
        {
            DataResponse dataResponse = new DataResponse();
            Common cm = new Common();
            try
            {
                var getData = cm.GetAutoNumber("InwardCourier");
                var data = new
                {
                    CourierReffNo = getData.Rows[0][0].ToString()
                };
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get InwardInfo");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
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
                    InwardCourierMaster objInwardCourierMaster = _IInwardCourier_Repository.FetchAllInfoById(Id);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", new
                    {
                        objInwardCourierMaster = objInwardCourierMaster,
                    });
                }
                catch (Exception ex)
                {
                    ex.SetLog("Get FetchInfo in InwardCourier");
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
            _IInwardCourier_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}