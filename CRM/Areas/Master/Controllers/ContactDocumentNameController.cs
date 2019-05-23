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
    public class ContactDocumentNameController : Controller
    {
        // GET: Master/ContactDocumentName
        private ContactDocumentName_Repository _IContactDocumentName_Repository;
        public ContactDocumentNameController()
        {
            this._IContactDocumentName_Repository = new ContactDocumentName_Repository(new elaunch_crmEntities());
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ContactDocumentNamePopup()
        {
            return View();
        }
        [HttpPost]
        public JsonResult SaveContactDocumentName(ContactDocumentNameMaster Newobj)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    ContactDocumentNameMaster OLdObj = new ContactDocumentNameMaster();
                    OLdObj.ContactDocId = Newobj.ContactDocId;
                    OLdObj.ContactDocName = Newobj.ContactDocName;
                    OLdObj.IsActive = true;
                    if (Newobj.ContactDocId > 0)
                    {
                        var plist = _IContactDocumentName_Repository.DuplicateEditContactDocumentName(OLdObj.ContactDocId, OLdObj.ContactDocName).ToList();
                        if (plist.Count == 0)
                        {
                            _IContactDocumentName_Repository.UpdateContactDocumentName(OLdObj);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully", null);

                        }
                        else
                        {
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Already Exists", null);

                        }
                        //_ISource_Repository.UpdateSource(source);
                        //msg = "Success";
                    }
                    else
                    {
                        var clist = _IContactDocumentName_Repository.DuplicateContactDocumentName(OLdObj.ContactDocName).ToList();
                        if (clist.Count == 0)
                        {
                            _IContactDocumentName_Repository.AddContactDocumentName(OLdObj);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully", null);

                        }
                        else
                        {
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Already Exists", null);

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
                ex.SetLog("Creat/Update TOType");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult DeleteContactDocumentName(string ContactDocId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    if (ContactDocId != "")
                    {
                        int cid = Convert.ToInt32(ContactDocId);
                        ContactDocumentNameMaster dmaster = new ContactDocumentNameMaster();
                        dmaster = _IContactDocumentName_Repository.GetContactDocumentNameById(cid);
                        dmaster.IsActive = false;
                        //smaster.SourceId = cid;
                        _IContactDocumentName_Repository.UpdateContactDocumentName(dmaster);
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
                ex.SetLog("Delete TOType");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetContactDocumentNameById(int ContactDocId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    var objdepart = _IContactDocumentName_Repository.GetContactDocumentNameById(ContactDocId);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, objdepart);
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "User is not valid", null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Get TOType by ID");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ContactDocumentNameBind()
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var data = _IContactDocumentName_Repository.getAllContactDocumentName().ToList();
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get All ContactDocumentName");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                throw ex;
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            _IContactDocumentName_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}