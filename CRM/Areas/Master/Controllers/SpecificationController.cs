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
    public class SpecificationController : Controller
    {
        private ISpecification_Repository _ISpecification_Repository;

        public SpecificationController()
        {
            this._ISpecification_Repository = new Specification_Repository(new elaunch_crmEntities());
        }

        // GET: Master/Specification
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddSpecification()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveSpecification(TechnicalSpecMaster d)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                TechnicalSpecMaster specificationObj = new TechnicalSpecMaster();
                specificationObj.SpecificationId = d.SpecificationId;
                specificationObj.TechHeadId = d.TechHeadId;
                specificationObj.TechSpec = d.TechSpec.Trim();
                specificationObj.IsActive = true;
                if (specificationObj.SpecificationId > 0)
                {
                    var cntList = _ISpecification_Repository.DuplicateEditSpecification(specificationObj.SpecificationId, specificationObj.TechSpec).ToList();
                    if (cntList.Count == 0)
                    {
                        _ISpecification_Repository.UpdateSpecification(specificationObj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully.", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Specification Already Exists", null);
                    }
                }
                else
                {
                    var cntList = _ISpecification_Repository.DuplicateSpecification(specificationObj.TechSpec).ToList();
                    if (cntList.Count == 0)
                    {
                        _ISpecification_Repository.AddSpecification(specificationObj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully.", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Specification Already Exists", null);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Create/Update Specification");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteSpecification(int SpecificationId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                //_ISpecificationType_Repository.DeleteSpecification(Convert.ToInt32(SpecificationId));
                TechnicalSpecMaster specificationObj = new TechnicalSpecMaster();
                specificationObj = _ISpecification_Repository.GetSpecificationByID(SpecificationId);
                specificationObj.IsActive = false;
                _ISpecification_Repository.UpdateSpecification(specificationObj);
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Delete successfully.", null);
            }
            catch (Exception ex)
            {
                ex.SetLog("Delete Specification");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetByIdSpecification(int SpecificationId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var obj = _ISpecification_Repository.GetSpecificationByID(Convert.ToInt32(SpecificationId));
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, obj);

            }
            catch (Exception ex)
            {
                ex.SetLog("Get Specification by Id");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSpecification()
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var TSM = _ISpecification_Repository.GetAllSpecification().ToList();
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, TSM);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get All Specification");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            _ISpecification_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}