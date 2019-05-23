using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.App_Start;
using CRM_Repository.Data;
using CRM_Repository.Service;
using CRM_Repository.ServiceContract;
using CRM.Models;

namespace CRM.Areas.Master.Controllers
{
    public class ContactInvitationController : Controller
    {
        private IContactInvitation_Repository _IContactInvitation_Repository;
        public ContactInvitationController()
        {
            this._IContactInvitation_Repository = new ContactInvitation_Repository(new elaunch_crmEntities());
        }
        // GET: Master/ContactInvitation
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddconInv()
        {
            return View();
        }
        [HttpPost]
        public JsonResult SaveConInv(ContactInvitationMaster d)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                ContactInvitationMaster ConInvObj = new ContactInvitationMaster();
                ConInvObj.ContactInvitationId = d.ContactInvitationId;
                ConInvObj.ContactInvitation = d.ContactInvitation.Trim();
                ConInvObj.IsActive = true;
                if (ConInvObj.ContactInvitationId > 0)
                {
                    var cntList = _IContactInvitation_Repository.DuplicateEditConInv(ConInvObj.ContactInvitationId, ConInvObj.ContactInvitation).ToList();
                    if (cntList.Count == 0)
                    {
                        _IContactInvitation_Repository.UpdateConInv(ConInvObj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully.", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Contact Invitation Already Exists.", null);
                    }
                }
                else
                {
                    var cntList = _IContactInvitation_Repository.DuplicateConInv(ConInvObj.ContactInvitation).ToList();
                    if (cntList.Count == 0)
                    {
                        _IContactInvitation_Repository.AddConInv(ConInvObj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully.", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Contact Invitation Already Exists.", null);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Create/Update Contact Invitation");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteConInv(int ContactInvitationId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                //_IAgencyType_Repository.DeleteAgencyType(Convert.ToInt32(AgencyTypeId));
                ContactInvitationMaster ConInvobj = new ContactInvitationMaster();
                ConInvobj = _IContactInvitation_Repository.GetConInvByID(ContactInvitationId);
                ConInvobj.IsActive = false;
                _IContactInvitation_Repository.UpdateConInv(ConInvobj);
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Delete successfully.", null);
            }
            catch (Exception ex)
            {
                ex.SetLog("Delete Contact Invitation");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetByIdConInv(int ContactInvitationId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var obj = _IContactInvitation_Repository.GetConInvByID(Convert.ToInt32(ContactInvitationId));
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, obj);

            }
            catch (Exception ex)
            {
                ex.SetLog("Get Contact Invitation by Id");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllConInv()
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var obj = _IContactInvitation_Repository.GetAllConInv();
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, obj);

            }
            catch (Exception ex)
            {
                ex.SetLog("Get All Contact Invitation");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        

        protected override void Dispose(bool disposing)
        {
            _IContactInvitation_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}