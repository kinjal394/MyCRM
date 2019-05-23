using CRM.App_Start;
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
    [HasLoginSessionFilter]
    public class ReceiptVoucherController : Controller
    {
        private IReceiptVoucher_Repository _IReceiptVoucher_Repository;
        // GET: Master/ReceiptVoucher
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ReceiptVoucherPopup()
        {
            return View();
        }
        public ReceiptVoucherController()

        {
            this._IReceiptVoucher_Repository = new ReceiptVoucher_Repository(new elaunch_crmEntities());

        }

        [HttpPost]
        public JsonResult SaveReceiptVoucher(ReceiptVoucherMaster objvoucher)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    ReceiptVoucherMaster rvmaster = new ReceiptVoucherMaster();
                    rvmaster = objvoucher;
                    rvmaster.CreatedBy = Convert.ToInt32(Session["UserId"]);
                    rvmaster.CreatedDate = DateTime.Now;
                    rvmaster.ModifyBy = Convert.ToInt32(Session["UserId"]);
                    rvmaster.ModifyDate = DateTime.Now;
                    rvmaster.IsActive = true;
                    if (rvmaster.VoucherId > 0)
                    {
                        var plist = _IReceiptVoucher_Repository.DuplicateEditReceiptVoucher(rvmaster.VoucherId, rvmaster.Type).ToList();
                        if (plist.Count == 0)
                        {
                            _IReceiptVoucher_Repository.UpdateReceiptVoucher(rvmaster);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully", null);
                        }
                        else
                        {
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Receipt Voucher Already Exists", null);

                        }
                    }
                    else
                    {
                        var plist = _IReceiptVoucher_Repository.DuplicateReceiptVoucher(rvmaster.Type).ToList();
                        if (plist.Count == 0)
                        {
                            _IReceiptVoucher_Repository.AddReceiptVoucher(rvmaster);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully", null);

                        }
                        else
                        {
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Receipt Voucher  Already Exists", null);

                            // Already Exists
                        }
                    }
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "User is not valid", null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Create/Update ReceiptVoucher");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteReceiptVoucher(string VoucherId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    if (VoucherId != "")
                    {
                        int vid = Convert.ToInt32(VoucherId);
                        ReceiptVoucherMaster rvmaster = new ReceiptVoucherMaster();
                        rvmaster = _IReceiptVoucher_Repository.GetReceiptVoucherById(vid);
                        rvmaster.IsActive = false;
                        _IReceiptVoucher_Repository.UpdateReceiptVoucher(rvmaster);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Delete successfully", null);
                    }
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "User is not valid", null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Delete ReceiptVoucher");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetReceiptVoucherById(int VoucherId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    var objvoucher = _IReceiptVoucher_Repository.GetReceiptVoucherById(VoucherId);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, objvoucher);
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "User is not valid", null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Get ReceiptVoucher");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            _IReceiptVoucher_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}