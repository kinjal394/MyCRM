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
    public class RelationController : Controller
    {
        private IRelation_Repository _IRelation_Repository;

        public RelationController()
        {
            this._IRelation_Repository = new Relation_Repository(new CRM_Repository.Data.elaunch_crmEntities());
        }

        // GET: Employee/Relation
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddRelation()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CreateUpdate(RelationMaster objRelation)
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    bool isExist = _IRelation_Repository.IsExist(objRelation.RelationId, objRelation.RelationName);
                    if (!isExist)
                    {
                        if (objRelation.RelationId <= 0)
                        {
                            objRelation.IsActive = true;
                            _IRelation_Repository.SaveRelation(objRelation);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully", null);
                        }
                        else
                        {
                            RelationMaster objRelationMaster = _IRelation_Repository.GetByRelationId(objRelation.RelationId);
                            objRelationMaster.RelationName = objRelation.RelationName;
                            _IRelation_Repository.UpdateRelation(objRelationMaster);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully", null);
                        }
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Exists, "Relation Already Exists", null);
                    }
                }
                catch (Exception ex)
                {
                    ex.SetLog("Create/Update Relation");
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
                    var rel = _IRelation_Repository.GetRelationById(Id).ToList();
                    if (rel.Count == 0)
                    {
                        _IRelation_Repository.DeleteRelation(Id);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Delete successfully", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "This Relation use in User Reference Relation so, Delete User Reference Relation then Delete Relation.", null);
                    }
                }
                catch (Exception ex)
                {
                    ex.SetLog("Delete Relation");
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
                    RelationMaster objRelationMaster = _IRelation_Repository.GetByRelationId(Id);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, objRelationMaster);
                }
                catch (Exception ex)
                {
                    ex.SetLog("Get Relation by Id");
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
        public JsonResult getRelationData()
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                List<RelationMaster> lst = _IRelation_Repository.GetRelation().ToList();
                return Json(lst);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get All Relation Data");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            _IRelation_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}