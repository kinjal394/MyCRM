using CRM.App_Start;
using CRM.Models;
using CRM_Repository.Data;
using CRM_Repository.Service;
using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace CRM.Areas.Master.Controllers
{
    [HasLoginSessionFilter]
    public class DocumentUploadController : Controller
    {
        private IUser_Repository _IUser_Repository;
        private IDocument_Repository _IDocument_Repository;
        private IDocumentUpload_Repository _IDocumentUpload_Repository;

        // GET: Master/DocumentUpload
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DocumentUploadPopup()
        {
            return View();
        }

        public DocumentUploadController()
        {
            this._IUser_Repository = new User_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IDocument_Repository = new Document_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IDocumentUpload_Repository = new DocumentUpload_Repository(new CRM_Repository.Data.elaunch_crmEntities());
        }

        [HttpGet]
        public JsonResult bindgrid(string EmpId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                int empId = int.Parse(EmpId);
                var DocGridList = _IDocumentUpload_Repository.GetdocUploadById(empId).ToList();
                return this.Json(DocGridList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get documentUpload By Id ");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult EmployeeBind()
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var EmployeeList = _IUser_Repository.getAllUser().ToList();
                return this.Json(EmployeeList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get All User in documentUpload");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DocNameBind()
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var DocNameList = _IDocument_Repository.GetAllDoc().ToList();
                return this.Json(DocNameList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get All Document in documentUpload");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveDocumentUpload(string EmpDocId, string EmpId, string DocId, string Photo, HttpPostedFileBase file)
        {
            DataResponse dataResponse = new DataResponse();
            if (ModelState.IsValid)
            {
                string filename = "";
                int CId = int.Parse(EmpDocId);
                Common cs = new Common();

                string path = HostingEnvironment.ApplicationPhysicalPath;
                if (!System.IO.Directory.Exists(path + "\\EmployeeDocument\\"))
                {
                    System.IO.Directory.CreateDirectory(path + "\\EmployeeDocument\\");
                }

                try
                {
                    if (sessionUtils.HasUserLogin())
                    {
                        EmpDocumentMaster empdocobj = new EmpDocumentMaster();
                        //empdocobj.EmpDocId = CId;
                        empdocobj.EmpId = int.Parse(EmpId);
                        empdocobj.DocId = int.Parse(DocId);
                        empdocobj.CreatedBy = Convert.ToInt32(Session["UserId"]);
                        empdocobj.CreatedDate = DateTime.Now;
                        empdocobj.ModifyBy = Convert.ToInt32(Session["UserId"]);
                        empdocobj.ModifyDate = DateTime.Now;
                        string name = Convert.ToString(empdocobj.EmpId) + Convert.ToString(empdocobj.DocId);
                        if (file != null)
                        {
                            filename = cs.fileDocupload(file, int.Parse(name), "EmployeeDocument");
                            empdocobj.Photo = filename;
                        }
                        else
                        {
                            empdocobj.Photo = Photo;
                        }

                        empdocobj.IsActive = true;
                        var celist = _IDocumentUpload_Repository.CheckForDuplicateDoc(empdocobj.EmpId,empdocobj.DocId).ToList();
                        if (celist.Count == 0)
                        {
                            _IDocumentUpload_Repository.AddDocumentUpload(empdocobj);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully", null);

                        }
                        else
                        {
                            empdocobj.EmpDocId = celist[0].EmpDocId;
                            EmpDocumentMaster objemp = _IDocumentUpload_Repository.GetDocUploadById(empdocobj.EmpDocId);
                            objemp.Photo = empdocobj.Photo;
                            _IDocumentUpload_Repository.UpdateDocumentUpload(objemp);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Insert successfully", null);
                        }
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "User is not valid", null);
                    }
                }
                catch (Exception ex)
                {
                    ex.SetLog("Upload DocumentUpload");
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                }
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }
            else
            {
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, MessageValue.Param, null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }
        }
        protected override void Dispose(bool disposing)
        {
            _IDocumentUpload_Repository.Dispose();
            _IDocument_Repository.Dispose();
            _IUser_Repository.Dispose();
            base.Dispose(disposing);
        }
    }

}