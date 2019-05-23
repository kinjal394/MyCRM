using CRM.App_Start;
using CRM.Models;
using CRM_Repository.Data;
using CRM_Repository.DTOModel;
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
    public class TOController : Controller
    {
        private ITO_Repository _ITO_Repository;
        private ITOItem_Repository _ITOItem_Repository;
        private IProductParameterMaster_Repository _IProductParameterMaster_Repository;
        public TOController()
        {
            this._ITO_Repository = new TO_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._ITOItem_Repository = new TOItem_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IProductParameterMaster_Repository = new ProductParameterMaster_Repository(new CRM_Repository.Data.elaunch_crmEntities());

        }
        // GET: Master/TO
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddTO(int id = 0,int temp = 0)
        {
            ViewBag.id = id;
            ViewBag.isdisable = temp;
            return View();
        }
        public ActionResult AddTOItem()
        {
            return View();
        }
        public ActionResult Specification()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetAllTOInfoById(int TOId)
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    TOModel objTOMaster = _ITO_Repository.GetTOById(TOId);
                    List<TOItemModel> objTOItemDetail = _ITOItem_Repository.GetTOItembyTOId(TOId).ToList();
                    List<TechnicleDetail> objTechnicaldetail = _ITO_Repository.GetTechnicalDetailbyToid(TOId).ToList();
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", new
                    {
                        objTOMaster = objTOMaster,
                        objTOItemDetail = objTOItemDetail,
                        objTechnicaldetail= objTechnicaldetail
                    });
                }
                catch (Exception ex)
                {
                    ex.SetLog("Get TO Detail");

                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                }
            }
            else
            {
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "Invalid User", null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult CreateUpdate(TOModel objInputTOMaster)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    objInputTOMaster.CreatedBy = sessionUtils.UserId;
                    objInputTOMaster.ModifyBy = sessionUtils.UserId;
                    objInputTOMaster.DeletedBy = sessionUtils.UserId;
                    int ResponseVal = _ITO_Repository.CreateUpdate(objInputTOMaster);
                    //ResponseVal 1: insert,2:update ,0: error
                    if (ResponseVal == 1)
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully", null);
                    }
                    else if (ResponseVal == 2)
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Opps! something wrong", null);
                    }
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "Invalid User", null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Create/Update TO Info");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteById(int TOId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    TOModel objInputTO = new TOModel();
                    objInputTO.TOId = TOId;
                    objInputTO.DeletedBy = sessionUtils.UserId;

                    int ResponseVal = _ITO_Repository.Delete(objInputTO);
                    //ResponseVal 1: Delete,0: error
                    if (ResponseVal == 1)
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Delete successfully", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Opps! something wrong", null);
                    }
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "Invalid User", null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Delete TO Info");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetTecgnicalspec(string pid,string Techparaid)
       {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    var objTOItemDetail = _IProductParameterMaster_Repository.GetByTechparaid(Convert.ToInt32(pid), Convert.ToInt32(Techparaid));
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", new
                    {
                        objTOMaster = objTOItemDetail
                    });
                }
                catch (Exception ex)
                {
                    ex.SetLog("Get TO Detail");
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                }
            }
            else
            {
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "Invalid User", null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
    }
}