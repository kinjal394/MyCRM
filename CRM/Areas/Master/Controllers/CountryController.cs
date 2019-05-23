using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM_Repository.Service;
using CRM_Repository.ServiceContract;
using CRM_Repository.Data;
using CRM.Models;
using System.Web.Hosting;
using CRM.App_Start;
using System.Data;

namespace CRM.Areas.Master.Controllers
{
    [HasLoginSessionFilter]
    public class CountryController : Controller
    {
        private ICountry_Repository _ICountry_Repository;

        public CountryController()
        {
            this._ICountry_Repository = new Country_Repository(new elaunch_crmEntities());
        }

        // GET: Master/Country
        public ActionResult Index()
        {

            Common cm = new Common();
            //DataTable data = cm.getpermission("TCOUNTRY", int.Parse(Session["UserRollType"].ToString()));
            //ViewBag.add = data.Rows[0]["IsAdd"].ToString();
            //ViewBag.edit = data.Rows[0]["IsEdit"].ToString();
            //ViewBag.delete = data.Rows[0]["IsDelete"].ToString();
            //ViewBag.view = data.Rows[0]["IsView"].ToString();
            return View();
        }

        public ActionResult CountryPopup()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CountryBind()
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var data = _ICountry_Repository.GetAllCountry().ToList();
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveCountry(string CountryId, string CountryCallCode, string CountryName, string CountryFlag, string CountryAlphaCode, HttpPostedFileBase file)
        {
            string filename = "";
            int CId = int.Parse(CountryId);
            Common cs = new Common();
            DataResponse dataResponse = new DataResponse();

            string path = HostingEnvironment.ApplicationPhysicalPath;
            if (!System.IO.Directory.Exists(path + "\\Content\\lib\\CountryFlags\\flags\\"))
            {
                System.IO.Directory.CreateDirectory(path + "\\Content\\lib\\CountryFlags\\flags\\");
            }

            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    CountryMaster objCountry = new CountryMaster();
                    objCountry.CountryId = CId;
                    objCountry.CountryName = CountryName;
                    objCountry.CountryAlphaCode = CountryAlphaCode;
                    objCountry.CountryCallCode = CountryCallCode;
                    if (file != null)
                    {
                        filename = cs.fileupload(file, "Content/lib/CountryFlags/flags", 32, 32);
                        objCountry.CountryFlag = filename;
                    }
                    else
                    {
                        objCountry.CountryFlag = CountryFlag;
                    }

                    objCountry.IsActive = true;
                    if (CId > 0)
                    {
                        var celist = _ICountry_Repository.CheckForDuplicateCountry(objCountry.CountryId, objCountry.CountryName).ToList();
                        if (celist.Count == 0)
                        {
                            _ICountry_Repository.UpdateCountry(objCountry);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully", null);

                        }
                        else
                        {
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Country Name Already Exists", null);

                        }
                    }
                    else
                    {
                        var clist = _ICountry_Repository.CheckForDuplicateCountry(objCountry.CountryId, objCountry.CountryName).ToList();
                        if (clist.Count == 0)
                        {
                            _ICountry_Repository.InsertCountry(objCountry);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully", null);

                        }
                        else
                        {
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Country Name Already Exists", null);

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
                ex.SetLog("Create/Update Country");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteCountry(string CountryId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    if (CountryId != "")
                    {
                        int cid = Convert.ToInt32(CountryId);
                        CountryMaster cmaster = new CountryMaster();
                        cmaster = _ICountry_Repository.GetCountryByID(cid);
                        cmaster.IsActive = false;
                        _ICountry_Repository.UpdateCountry(cmaster);
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
                ex.SetLog("Delete Country");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetByIdCountry(int CountryID)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    var objcountry = _ICountry_Repository.GetCountryByID(CountryID);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, objcountry);
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "User is not valid", null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Get Country by Id");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            _ICountry_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}