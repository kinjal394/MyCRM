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
    public class FinancialYearController : Controller
    {
        private IFinancialYear_Repository _IFinancialYear_Repository;
        public FinancialYearController()
        {
            this._IFinancialYear_Repository = new FinancialYear_Repository(new elaunch_crmEntities());
        }
        // GET: Master/FinancialYear
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddFinancialYear()
        {
            return View();
        }
        [HttpPost]
        public JsonResult SaveFinancialYear(FinancialYearMaster Obj)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                FinancialYearMaster FinancialObj = new FinancialYearMaster();
                FinancialObj.FinancialYearId = Obj.FinancialYearId;
                FinancialObj.FinancialYear = Obj.FinancialYear;
                FinancialObj.IsActive = true;
                if(FinancialObj.FinancialYearId>0)
                {
                    var Cntlist = _IFinancialYear_Repository.DuplicateEditFinancialYear(FinancialObj.FinancialYearId, FinancialObj.FinancialYear).ToList();
                    if(Cntlist.Count==0)
                    {
                        _IFinancialYear_Repository.UpdateFinancialYear(FinancialObj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully.", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "FinancialYear Already Exists.", null);
                    }
                }
                else
                {
                    var Cntlist = _IFinancialYear_Repository.DuplicateFinancialYear(FinancialObj.FinancialYear).ToList();
                    if(Cntlist.Count==0)
                    {
                        _IFinancialYear_Repository.AddFinancialYear(FinancialObj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully.", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "FinancialYear Already Exists.", null);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Create/Update Financial Year");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteFinancialYear(int FinancialYearId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                FinancialYearMaster agencytypeObj = new FinancialYearMaster();
                agencytypeObj = _IFinancialYear_Repository.GetFinancialYearByID(FinancialYearId);
                agencytypeObj.IsActive = false;
                _IFinancialYear_Repository.UpdateFinancialYear(agencytypeObj);
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Delete successfully.", null);
            }
            catch (Exception ex)
            {
                ex.SetLog("Delete Financial Year");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null); 
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetByIdFinancialYear(int FinancialYearId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var obj = _IFinancialYear_Repository.GetFinancialYearByID(Convert.ToInt32(FinancialYearId));
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, obj);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get Financial Year by Id");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            _IFinancialYear_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}