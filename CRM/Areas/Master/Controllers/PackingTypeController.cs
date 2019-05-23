using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM_Repository.Data;
using CRM_Repository.Service;
using CRM_Repository.ServiceContract;
using CRM.Models;
using CRM.App_Start;

namespace CRM.Areas.Master.Controllers
{
    [HasLoginSessionFilter]
    public class PackingTypeController : Controller
    {
        private IPackingType_Repository _IPackingType_Repository;

        public PackingTypeController()
        {
            this._IPackingType_Repository = new PackingType_Repository(new elaunch_crmEntities());
        }

        // GET: Master/PackingType
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddPackingType()
        {
            return View();
        }
        [HttpPost]
        public JsonResult SavePackingType(PackingTypeMaster d)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                PackingTypeMaster PackingTypeObj = new PackingTypeMaster();
                PackingTypeObj.PackingTypeId = d.PackingTypeId;
                PackingTypeObj.PackingType = d.PackingType.Trim();
                PackingTypeObj.IsActive = true;
                if (PackingTypeObj.PackingTypeId > 0)
                {
                    var cntList = _IPackingType_Repository.DuplicateEditPackingType(PackingTypeObj.PackingTypeId, PackingTypeObj.PackingType).ToList();
                    if (cntList.Count == 0)
                    {
                        _IPackingType_Repository.UpdatePackingType(PackingTypeObj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully.", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "PackingType Name Already Exists.", null);
                    }
                }
                else
                {
                    var cntList = _IPackingType_Repository.DuplicatePackingType(PackingTypeObj.PackingType).ToList();
                    if (cntList.Count == 0)
                    {
                        _IPackingType_Repository.AddPackingType(PackingTypeObj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully.", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "PackingType Name Already Exists.", null);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Create/Update PackingType");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeletePackingType(int PackingTypeId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                //_IAgencyType_Repository.DeleteAgencyType(Convert.ToInt32(AgencyTypeId));
                PackingTypeMaster PackingTypeObj = new PackingTypeMaster();
                PackingTypeObj = _IPackingType_Repository.GetPackingTypeByID(PackingTypeId);
                PackingTypeObj.IsActive = false;
                _IPackingType_Repository.UpdatePackingType(PackingTypeObj);
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Delete successfully.", null);
            }
            catch (Exception ex)
            {
                ex.SetLog("Delete Packing Type");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetByIdPackingType(int PackingTypeId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var obj = _IPackingType_Repository.GetPackingTypeByID(Convert.ToInt32(PackingTypeId));
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, obj);

            }
            catch (Exception ex)
            {
                ex.SetLog("Get PackingType by Id");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            _IPackingType_Repository.Dispose();
            base.Dispose(disposing);
        }

    }
}