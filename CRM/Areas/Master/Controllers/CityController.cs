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
    public class CityController : Controller
    {
        private IState_Repository _IState_Repository;
        private ICity_Repository _ICity_Repository;
        public CityController()
        {
            this._ICity_Repository = new City_Repository(new elaunch_crmEntities());
            this._IState_Repository = new State_Repository(new elaunch_crmEntities());
        }

        // GET: Master/City
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CityPopup()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveCity(CityMaster objcity)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    CityMaster objCity = new CityMaster();
                    objCity.CityId = objcity.CityId;
                    objCity.StateId = objcity.StateId;
                    objCity.CityName = objcity.CityName;
                    objCity.IsActive = true;
                    if (objcity.CityId > 0)
                    {
                        var celist = _ICity_Repository.CheckForDuplicateCity(objCity.CityId, objCity.CityName).ToList();
                        if (celist.Count == 0)
                        {
                            _ICity_Repository.UpdateCity(objCity);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully", null);

                        }
                        else
                        {
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "City Name Already Exists", null);

                        }
                    }
                    else
                    {
                        var clist = _ICity_Repository.CheckForDuplicateCity(objCity.CityId, objCity.CityName).ToList();
                        if (clist.Count == 0)
                        {
                            _ICity_Repository.InsertCity(objCity);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully", null);

                        }
                        else
                        {
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "City Name Already Exists", null);

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
                ex.SetLog("Create/Update City");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteCity(string CityId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    if (CityId != "")
                    {
                        int sid = Convert.ToInt32(CityId);
                        CityMaster objCity = new CityMaster();
                        objCity = _ICity_Repository.GetCityByID(sid);
                        objCity.IsActive = false;
                        _ICity_Repository.UpdateCity(objCity);
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
                ex.SetLog("Delete City");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetByIdCity(int CityID)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    var objcity = _ICity_Repository.GetCityByID(CityID);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, objcity);
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "User is not valid", null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Get City by Id");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CityBind(int StateId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var data = _ICity_Repository.GetCityByStateID(StateId).ToList();
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get City by StateID");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                throw ex;
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            _ICity_Repository.Dispose();
            _IState_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}