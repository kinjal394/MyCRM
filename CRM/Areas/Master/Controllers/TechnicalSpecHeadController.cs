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
    public class TechnicalSpecHeadController : Controller
    {
        private ITechnicalSpecHead_Repository _ITechnicalSpecHead_Repository;

        public TechnicalSpecHeadController()
        {
            this._ITechnicalSpecHead_Repository = new TechnicalSpecHead_Repository(new elaunch_crmEntities());
        }
        // GET: Master/AddressType
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddTechnicalSpecHead()
        {
            return View();
        }


        //[HttpPost]
        public JsonResult SaveTechnicalSpecHead(TechnicalSpecHeadMaster objTechnicalSpecHead)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                objTechnicalSpecHead.IsActive = true;
                if (objTechnicalSpecHead.TechHeadId > 0)
                {
                    var cntList = _ITechnicalSpecHead_Repository.DuplicateEdiTechnicalSpecHead(objTechnicalSpecHead.TechHeadId, objTechnicalSpecHead.TechHead).ToList();
                    if (cntList.Count == 0)
                    {
                        _ITechnicalSpecHead_Repository.UpdateTechnicalSpecHead(objTechnicalSpecHead);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully.", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Technical Specification Head Already Exists", null);
                    }
                }
                else
                {
                    var cntList = _ITechnicalSpecHead_Repository.DuplicateTechnicalSpecHead(objTechnicalSpecHead.TechHead).ToList();
                    if (cntList.Count == 0)
                    {
                        _ITechnicalSpecHead_Repository.AddTechnicalSpecHead(objTechnicalSpecHead);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully.", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Technical Specification Head Already Exists", null);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Create/Update Technical Specification Head");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult DeleteTechnicalSpecHead(int TechHeadId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                TechnicalSpecHeadMaster obj = new TechnicalSpecHeadMaster();
                obj = _ITechnicalSpecHead_Repository.GetByTechHeadId(TechHeadId);
                obj.IsActive = false;
                _ITechnicalSpecHead_Repository.UpdateTechnicalSpecHead(obj);
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Delete successfully.", null);
            }
            catch (Exception ex)
            {
                ex.SetLog("Delete Technical Specification Head");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }



        public JsonResult GetByIdTechnicalSpecHead(int TechHeadId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var obj = _ITechnicalSpecHead_Repository.GetByTechHeadId(Convert.ToInt32(TechHeadId));
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, obj);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get TechHead by Id");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }


        protected override void Dispose(bool disposing)
        {
            _ITechnicalSpecHead_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}