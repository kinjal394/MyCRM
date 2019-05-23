using CRM_Repository.Service;
using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM_Repository.Data;
using CRM.Models;
using CRM_Repository.DTOModel;
using CRM.App_Start;

namespace CRM.Areas.Master.Controllers
{
    [HasLoginSessionFilter]
    public class ExhibitionController : Controller
    {

        private IExhibition_Repository _IExhibition_Repository;
        private ICountry_Repository _ICountry_Repository;
        private IState_Repository _IState_Repository;
        private ICity_Repository _ICity_Repository;

        public ExhibitionController()
        {
            this._IExhibition_Repository = new Exhibition_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IState_Repository = new State_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._ICountry_Repository = new Country_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._ICity_Repository = new City_Repository(new CRM_Repository.Data.elaunch_crmEntities());
        }
        // GET: Master/Exhibition
        public ActionResult Index()
        {
            return View();

        }

        public ActionResult AddExhibition()
        {
            return View();
        }
        [HttpPost]
        public JsonResult SaveExhibition(ExhibitionModel obj)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    //if (!_IExhibition_Repository.CheckExhibitionType(obj, false))
                    //{
                    //ExhibitionMaster exhibitionData = new ExhibitionMaster();
                    //exhibitionData.ExName = obj.ExName;
                    //exhibitionData.AreaId = obj.AreaId;
                    //exhibitionData.Venue = obj.Venue;
                    //exhibitionData.NoofYears = obj.NoofYears;
                    //exhibitionData.ExProfile = obj.ExProfile;
                    //exhibitionData.OrganizerDetail = obj.OrganizerDetail;
                    //exhibitionData.BankDetail = obj.BankDetail;
                    //exhibitionData.Address = obj.Address;
                    //exhibitionData.ExDate = obj.ExDate;
                    //exhibitionData.CreatedBy =   sessionUtils.UserId;
                    //exhibitionData.CreatedDate = DateTime.Now;
                    //exhibitionData.IsActive = true;
                    obj.CreatedBy = sessionUtils.UserId;
                    obj.ModifyBy = sessionUtils.UserId;
                    obj.DeletedBy = sessionUtils.UserId;
                    int ResponseVal = _IExhibition_Repository.CreateUpdate(obj);
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
                    //_IExhibition_Repository.AddExhibition(exhibitionData);
                    //}
                    //else
                    //{
                    //    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Exhibition Name" + MessageValue.Exist, null);
                    //}
                   
                }
                else
                {
                    //return PartialView("AddBank", obj);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "Invalid User", null);
                }
                return Json(dataResponse, JsonRequestBehavior.AllowGet);



            }
            catch (Exception ex)
            {
                ex.SetLog("Create Exhibition");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult UpdateExhibition(ExhibitionMaster obj)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                ExhibitionMaster exhibitionData = _IExhibition_Repository.GetExhibitionById(obj.ExId);
                if (ModelState.IsValid)
                {
                    //if (!_IExhibition_Repository.CheckExhibitionType(obj, true))
                    //{
                        exhibitionData.ExName = obj.ExName;
                        exhibitionData.CityId = obj.CityId;
                        exhibitionData.Venue = obj.Venue;
                       // exhibitionData.NoofYears = obj.NoofYears;
                        exhibitionData.ExProfile = obj.ExProfile;
                        exhibitionData.OrganizerDetail = obj.OrganizerDetail;
                       // exhibitionData.BankDetail = obj.BankDetail;
                        exhibitionData.Address = obj.Address;
                        exhibitionData.ExDate = obj.ExDate;
                        exhibitionData.IsActive = true;
                        exhibitionData.ModifyBy =   sessionUtils.UserId;
                        exhibitionData.ModifyDate = DateTime.Now;
                    _IExhibition_Repository.UpdateExhibition(exhibitionData);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, MessageValue.Update, null);

                    //}
                    //else
                    //{
                    //    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Exhibition Name" + MessageValue.Exist, null);
                    //}

                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, MessageValue.Param, null);
                }
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.SetLog("Update Exhibition");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult DeleteExhibition(string id)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {

                ExhibitionMaster Exobj = _IExhibition_Repository.GetExhibitionById(Convert.ToInt32(id));
                if (Exobj != null)
                {

                    Exobj.IsActive = false;
                    Exobj.DeletedBy =  sessionUtils.UserId;
                    Exobj.DeletedDate = DateTime.Now;
                    _IExhibition_Repository.UpdateExhibition(Exobj);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, MessageValue.Delete, null);

                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, MessageValue.Param, null);
                }
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.SetLog("Delete Exhibition");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult GetCountry()
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var CountryList = _ICountry_Repository.GetAllCountry().ToList();
                return this.Json(CountryList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get All Country in Exhibition");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }
          
        }


        public JsonResult GetStates(int CountryID)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var StateList = _IState_Repository.GetStateByCountryID(CountryID).ToList();

                return this.Json(StateList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get States in Exhibition");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }
         
        }

        public JsonResult GetCity(int StateId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var CityList = _ICity_Repository.GetCityByStateID(StateId).ToList();

                return this.Json(CityList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get City in Exhibition");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }
         
        }
        public JsonResult GetArea(int AreaId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var AreaList = _ICity_Repository.GetCityByID(AreaId);

                return this.Json(AreaList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get Area in Exhibition");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }
        
        }

        protected override void Dispose(bool disposing)
        {
            _ICity_Repository.Dispose();
            _ICountry_Repository.Dispose();
            _IExhibition_Repository.Dispose();
            _IState_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}