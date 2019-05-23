using CRM.App_Start;
using CRM.Models;
using CRM_Repository.Data;
using CRM_Repository.Service;
using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Areas.Master.Controllers
{
    [HasLoginSessionFilter]
    public class RoleController : Controller
    {
        private IRole_Repository _IRole_Repository;

        public RoleController()
        {
            this._IRole_Repository = new Role_Repository(new elaunch_crmEntities());

        }
        // GET: Master/Role
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveRole(RoleMaster obj)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {

                if (ModelState.IsValid)
                {
                    if (!_IRole_Repository.CheckRoleType(obj, false))
                    {
                        RoleMaster roleobj = new RoleMaster();
                        roleobj.RoleName = obj.RoleName;
                        roleobj.CreatedDate = DateTime.Now;
                        roleobj.CreatedBy = sessionUtils.UserId;
                        roleobj.IsActive = true;
                        _IRole_Repository.AddRole(roleobj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, MessageValue.Insert, null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Role" + MessageValue.Exists, null);
                    }
                }
                else
                {
                    //return PartialView("AddBank", obj);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, MessageValue.Param, null);
                }
                return Json(dataResponse, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                ex.SetLog("Create/Update Role");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult DeleteRole(string id)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                RoleMaster roleobj = _IRole_Repository.GetRoleById(Convert.ToInt32(id));
                if (roleobj != null)
                {

                    roleobj.IsActive = false;
                    roleobj.DeletedDate = DateTime.Now;
                    roleobj.DeletedBy = sessionUtils.UserId;

                    _IRole_Repository.UpdateRole(roleobj);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, MessageValue.Delete, null);
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, MessageValue.Param, null);
                }
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.SetLog("Delete Role");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult UpdateRole(RoleMaster obj)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                // BankMaster obj = _IBank_Repository.GetBankById(bank.BankId);
                if (ModelState.IsValid)
                {
                    RoleMaster roleobj = _IRole_Repository.GetRoleById(Convert.ToInt32(obj.RoleId));
                    if (!_IRole_Repository.CheckRoleType(obj, true))
                    {
                        roleobj.RoleName = obj.RoleName;
                        roleobj.IsActive = true;
                        roleobj.ModifyDate = DateTime.Now;
                        roleobj.ModifyBy = sessionUtils.UserId;


                        _IRole_Repository.UpdateRole(roleobj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, MessageValue.Update, null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Role" + MessageValue.Exists, null);
                    }

                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, MessageValue.Param, null);
                }
                return Json(dataResponse, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                ex.SetLog("Update Role");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }

        }
        protected override void Dispose(bool disposing)
        {
            _IRole_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}