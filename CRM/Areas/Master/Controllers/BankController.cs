using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM_Repository.Data;
using CRM_Repository.ServiceContract;
using CRM_Repository.Service;
using CRM.Models;
using CRM.App_Start;

namespace CRM.Areas.Master.Controllers
{
    [HasLoginSessionFilter]
    public class BankController : Controller
    {

        private IBank_Repository _IBank_Repository;

        public BankController()
        {
            this._IBank_Repository = new Bank_Repository(new elaunch_crmEntities());
        }
        // GET: Master/Bank
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddBank()
        {
            return View();
        }
        [HttpPost]
        public JsonResult SaveBank(BankMaster obj)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    if (!_IBank_Repository.CheckBankExist(obj, false))
                    {
                        BankMaster bank = new BankMaster();
                        bank.BeneficiaryName = obj.BeneficiaryName;
                        bank.BankNameId = obj.BankNameId;
                        bank.AccountNo = obj.AccountNo;
                        bank.BranchName = obj.BranchName;
                        bank.IFSCCode = obj.IFSCCode;
                        bank.NickName = obj.NickName;
                        bank.SwiftCode = obj.SwiftCode;
                        bank.CreatedBy = sessionUtils.UserId;// Convert.ToInt32(Session["UserId"]);
                        bank.CreatedDate = DateTime.Now;
                        bank.IsActive = true;
                        bank.MICRCode = obj.MICRCode;
                        bank.CRNNo = obj.CRNNo;
                        bank.AccountTypeId = obj.AccountTypeId;
                        bank.RegisterEmail = obj.RegisterEmail;
                        bank.RegisterMobile = obj.RegisterMobile;
                        bank.StatementPassword = obj.StatementPassword;
                        bank.BankCustomerCareNo = obj.BankCustomerCareNo;
                        bank.BankCustomerCareEmail = obj.BankCustomerCareEmail;
                        bank.Note = obj.Note;

                        _IBank_Repository.AddBank(bank);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, MessageValue.Insert, null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Bank Name Already Exists. " + MessageValue.Exists, null);
                    }
                }
                else
                {
                    //return PartialView("AddBank", obj);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "User is not valid", null);
                }
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                e.SetLog("Create Bank");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, e.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public JsonResult UpdateBank(BankMaster bank)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                BankMaster obj = _IBank_Repository.GetBankById(bank.BankId);
                if (sessionUtils.HasUserLogin())
                {
                    if (!_IBank_Repository.CheckBankExist(obj, true))
                    {
                        obj.BeneficiaryName = bank.BeneficiaryName;
                        obj.BankNameId = bank.BankNameId;
                        obj.BranchName = bank.BranchName;
                        obj.AccountNo = bank.AccountNo;
                        obj.IFSCCode = bank.IFSCCode;
                        obj.NickName = bank.NickName;
                        obj.SwiftCode = bank.SwiftCode;
                        obj.ModifyBy = sessionUtils.UserId;// Convert.ToInt32(Session["UserId"]);
                        obj.ModifyDate = DateTime.Now;
                        obj.MICRCode = bank.MICRCode;
                        obj.CRNNo = bank.CRNNo;
                        obj.AccountTypeId = bank.AccountTypeId;
                        obj.RegisterEmail = bank.RegisterEmail;
                        obj.RegisterMobile = bank.RegisterMobile;
                        obj.StatementPassword = bank.StatementPassword;
                        obj.BankCustomerCareNo = bank.BankCustomerCareNo;
                        obj.BankCustomerCareEmail = bank.BankCustomerCareEmail;
                        obj.Note = bank.Note;

                        _IBank_Repository.UpdateBank(obj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, MessageValue.Update, null);

                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Bank Name Already Exists." + MessageValue.Exists, null);
                    }
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "User is not valid", null);
                }
                return Json(dataResponse, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                e.SetLog("Update Bank");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, e.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult DeleteBank(string id)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                BankMaster bankobj = _IBank_Repository.GetBankById(Convert.ToInt32(id));
                if (bankobj != null)
                {
                    BankMaster updbankobj = new BankMaster();
                    bankobj.IsActive = false;
                    bankobj.DeletedBy = sessionUtils.UserId;
                    bankobj.DeletedDate = DateTime.Now;
                    _IBank_Repository.UpdateBank(bankobj);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, MessageValue.Delete, null);
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, MessageValue.Param, null);
                }
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                e.SetLog("Delete Bank");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, e.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult EditBank(string id)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                BankMaster bankobj = _IBank_Repository.GetBank(Convert.ToInt32(id));
                if (bankobj != null)
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, bankobj);
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "User is not valid", null);
                }
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                e.SetLog("Get Bank");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, e.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }

        }

        protected override void Dispose(bool disposing)
        {
            _IBank_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}