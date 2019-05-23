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

namespace CRM.Areas.Employee.Controllers
{
    [HasLoginSessionFilter]
    public class AccountTypeController : Controller
    {
        private IAccountType_Repository _IAccountType_Repository;

        public AccountTypeController()
        {
            this._IAccountType_Repository = new AccountType_Repository(new CRM_Repository.Data.elaunch_crmEntities());
        }

        // GET: Employee/AccountType
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddAccountType()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CreateUpdate(AccountTypeMaster objAccountType)
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    bool isExist = _IAccountType_Repository.IsExist(objAccountType.AccountTypeId, objAccountType.AccountType);
                    if (!isExist)
                    {
                        if (objAccountType.AccountTypeId <= 0)
                        {
                            objAccountType.IsActive = true;
                            _IAccountType_Repository.SaveAccountType(objAccountType);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully", null);
                        }
                        else
                        {
                            AccountTypeMaster objAccountTypeMaster = _IAccountType_Repository.GetByAccountTypeId(objAccountType.AccountTypeId);
                            objAccountTypeMaster.AccountType = objAccountType.AccountType;
                            _IAccountType_Repository.UpdateAccountType(objAccountTypeMaster);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully", null);
                        }
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Exists, "AccountType Already Exists", null);
                    }
                }
                catch (Exception ex)
                {
                    ex.SetLog("Create/Update Account Type");
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
        public JsonResult Delete(int Id)
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    _IAccountType_Repository.DeleteAccountType(Id);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Delete successfully", null);
                }
                catch (Exception ex)
                {
                    ex.SetLog("Delete Account Type");
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
                    AccountTypeMaster objAccountTypeMaster = _IAccountType_Repository.GetByAccountTypeId(Id);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, objAccountTypeMaster);
                }
                catch (Exception ex)
                {
                    ex.SetLog("Get Account Type by Id");
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                }
            }
            else
            {
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "User is not valid", null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            _IAccountType_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}