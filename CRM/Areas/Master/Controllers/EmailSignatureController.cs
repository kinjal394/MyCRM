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
    public class EmailSignatureController : Controller
    {
        private IEmailSignature_Repository _IEmailSignature_Repository;
        private IUser_Repository _IUser_Repository;
        public EmailSignatureController()
        {
            this._IEmailSignature_Repository = new EmailSignature_Repository(new elaunch_crmEntities());
            this._IUser_Repository = new User_Repository(new elaunch_crmEntities());
        }
        // GET: Master/EmailSignature
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetAllUsers()
        {
            return Json(_IUser_Repository.getAllUser(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddSignature()
        {
            return View();
        }

        public JsonResult GetSignatureById(int Id)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var data = _IEmailSignature_Repository.GetEmailSignatureById(Id);
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get Speech by Id");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveSignature(SignatureMaster obj)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {

                if (ModelState.IsValid)
                {
                    if (!_IEmailSignature_Repository.CheckEmailSignature(obj))
                    {
                        SignatureMaster signature = new SignatureMaster();
                        signature.Title = obj.Title;
                        signature.UserId = obj.UserId;
                        signature.Signature = obj.Signature;
                        signature.CreatedBy = sessionUtils.UserId;// Convert.ToInt32(Session["UserId"]);
                        signature.CreatedDate = DateTime.Now;
                        signature.IsActive = true;
                        _IEmailSignature_Repository.AddEmailSignature(signature);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, MessageValue.Insert, null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Email Signature" + MessageValue.Exists, null);
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
                ex.SetLog("Create EmailSignature");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public JsonResult UpdateSignature(SignatureMaster obj1)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                SignatureMaster obj = _IEmailSignature_Repository.GetEmailSignatureById(obj1.SignatureId);
                if (ModelState.IsValid)
                {
                    if (!_IEmailSignature_Repository.CheckEmailSignature(obj1))
                    {
                        obj.Signature = obj1.Signature;
                        obj.Title = obj1.Title;
                        obj.UserId = obj1.UserId;
                        obj.ModifyBy = sessionUtils.UserId;// Convert.ToInt32(Session["UserId"]);
                        obj.ModifyDate = DateTime.Now;
                        _IEmailSignature_Repository.UpdateEmailSignature(obj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, MessageValue.Update, null);

                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Email Signature" + MessageValue.Exists, null);
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
                ex.SetLog("Update EmailSignature");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult DeleteSignature(int id)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                SignatureMaster bankobj = _IEmailSignature_Repository.GetEmailSignatureById(id);
                if (bankobj != null)
                {
                    bankobj.IsActive = false;
                    bankobj.DeletedBy = sessionUtils.UserId;
                    bankobj.DeletedDate = DateTime.Now;
                    _IEmailSignature_Repository.UpdateEmailSignature(bankobj);
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
                ex.SetLog("Delete EmailSignature");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }

        }
        protected override void Dispose(bool disposing)
        {
            _IEmailSignature_Repository.Dispose();
            _IUser_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}