using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM_Repository.Service;
using CRM_Repository.ServiceContract;
using CRM_Repository.Data;
using CRM.Models;
using CRM.App_Start;
namespace CRM.Areas.Master.Controllers
{
    [HasLoginSessionFilter]
    public class QualificationController : Controller
    {
        private IQualifications_Repository _IQualifications_Repository;

        public QualificationController()
        {
            this._IQualifications_Repository = new Qualification_Repository(new elaunch_crmEntities());
        }
        // GET: Master/Qualification
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddQaul()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveQual(QualificationsMaster ObjQual)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                QualificationsMaster objquli = new QualificationsMaster();
                objquli.QualificationId = ObjQual.QualificationId;
                objquli.QualificationName = ObjQual.QualificationName.Trim();
                objquli.IsActive = true;
                if (objquli.QualificationId > 0)
                {
                    var cntList = _IQualifications_Repository.DuplicateEditQualification(objquli.QualificationId, objquli.QualificationName).ToList();
                    if (cntList.Count == 0)
                    {
                        _IQualifications_Repository.UpdateQualification(objquli);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully.", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Qualification Name Already Exists", null);
                    }
                }
                else
                {
                    var cntList = _IQualifications_Repository.DuplicateQualification(objquli.QualificationName).ToList();
                    if (cntList.Count == 0)
                    {
                        _IQualifications_Repository.AddQualification(objquli);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully.", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Qualification Name Already Exists", null);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Create/Update Qualification");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteQual(int QualId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {

                QualificationsMaster objqual = new QualificationsMaster();
                objqual = _IQualifications_Repository.GetQauliByID(QualId);
                objqual.IsActive = false;
                _IQualifications_Repository.UpdateQualification(objqual);
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Delete successfully.", null);
            }
            catch (Exception ex)
            {
                ex.SetLog("Delete Qualification");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetByIdQual(int QualId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var obj = _IQualifications_Repository.GetQauliByID(Convert.ToInt32(QualId));
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, obj);

            }
            catch (Exception ex)
            {
                ex.SetLog("Get Qualification by Id");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            _IQualifications_Repository.Dispose();
            base.Dispose(disposing);
        }

    }
}