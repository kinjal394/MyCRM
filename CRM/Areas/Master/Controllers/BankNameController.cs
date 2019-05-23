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
    public class BankNameController : Controller
    {
        private IBankName_Repository _IBankName_Repository;
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BankNamePopup()
        {
            return View();
        }
        public BankNameController()
        {
            this._IBankName_Repository = new BankName_Repository(new elaunch_crmEntities());
        }
        [HttpPost]
        public JsonResult SaveBankName(BankNameMaster objdeBankName)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    BankNameMaster ObjBank = new BankNameMaster();
                    ObjBank.BankId = objdeBankName.BankId;
                    ObjBank.BankName = objdeBankName.BankName;
                    ObjBank.IsActive = true;
                    if (objdeBankName.BankId > 0)
                    {
                        var plist = _IBankName_Repository.DuplicateEditBankName(ObjBank.BankId, ObjBank.BankName).ToList();
                        if (plist.Count == 0)
                        {
                            _IBankName_Repository.UpdateBankName(ObjBank);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully", null);

                        }
                        else
                        {
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "BankName Already Exists", null);

                        }
                        //_ISource_Repository.UpdateSource(source);
                        //msg = "Success";
                    }
                    else
                    {
                        var clist = _IBankName_Repository.DuplicateBankName(ObjBank.BankName).ToList();
                        if (clist.Count == 0)
                        {
                            _IBankName_Repository.AddBankName(ObjBank);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully", null);

                        }
                        else
                        {
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "BankName Already Exists", null);

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
                ex.SetLog("Create/Update BankName");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult DeleteBankName(string BankId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    if (BankId != "")
                    {
                        int cid = Convert.ToInt32(BankId);
                        BankNameMaster dmaster = new BankNameMaster();
                        dmaster = _IBankName_Repository.GetBankNameById(cid);
                        dmaster.IsActive = false;
                        //smaster.SourceId = cid;
                        _IBankName_Repository.UpdateBankName(dmaster);
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
                ex.SetLog("Delete BankName");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetBankNameById(int BankId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    var objdepart = _IBankName_Repository.GetbankNameById(BankId);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, objdepart);
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "User is not valid", null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Get BankName by Id");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult BankNameBind()
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var data = _IBankName_Repository.getAllBankName().ToList();
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
            }
            catch (Exception ex)
            {
                ex.SetLog("Bind BankName");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            _IBankName_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}