using CRM.Models;
using CRM_Repository.Data;
using CRM_Repository.Service;
using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM_Repository.DTOModel;
using CRM.App_Start;
using Newtonsoft.Json;
using System.Collections;
using System.Configuration;

namespace CRM.Areas.Master.Controllers
{
    [HasLoginSessionFilter]
    public class BuyerController : Controller
    {
        private IBuyer_Repository _IBuyer_Repository;
        private ICountry_Repository _ICountry_Repository;
        private IState_Repository _IState_Repository;
        private ICity_Repository _ICity_Repository;
        private IDesignation_Repository _IDesignation_Repository;
        private IBuyerContactDetail_Repository _IBuyerContactDetail_Repository;
        private IBuyerBankDetail_Repository _IBuyerBankDetail_Repository;
        private IBuyerChatDetail_Repository _IBuyerChatDetail_Repository;
        private ISMSSpeech_Repository _ISMSSpeech_Repository;
        private IEmailSpeech_Repository _IEmailSpeech_Repository;
        public BuyerController()
        {
            this._ICountry_Repository = new Country_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IState_Repository = new State_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IDesignation_Repository = new Designation_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._ICity_Repository = new City_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IBuyer_Repository = new Buyer_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IBuyerContactDetail_Repository = new BuyerContactDetail_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IBuyerBankDetail_Repository = new BuyerBankDetail_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IBuyerChatDetail_Repository = new BuyerChatDetail_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._ISMSSpeech_Repository = new SMSSpeech_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IEmailSpeech_Repository = new EmailSpeech_Repository(new CRM_Repository.Data.elaunch_crmEntities());
        }

        public ActionResult ExtraIndex()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddBuyer(int id = 0, int temp = 0)
        {
            ViewBag.id = id;
            ViewBag.isdisable = temp;
            return View();
        }

        public ActionResult AddBuyerContact()
        {
            return View();
        }

        public ActionResult AddBuyerBank()
        {
            return View();
        }

        public ActionResult AddBuyerAddress()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetMasterInformation()
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                List<CountryMaster> CountryInfo = _ICountry_Repository.GetAllCountry().ToList();
                List<StateMaster> StateInfo = _IState_Repository.GetAllState().ToList();
                List<DesignationMaster> DesignationInfo = _IDesignation_Repository.GetAllDesignation().ToList();
                var data = new
                {
                    CountryInfo = CountryInfo,
                    StateInfo = StateInfo,
                    DesignationInfo = DesignationInfo
                };
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get MasterInfo in Buyer");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetCityById(int id)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                List<CityMaster> data = _ICity_Repository.GetCityByStateID(id).ToList();
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get City in Buyer");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetContactBuyerById(int id)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
               List<BuyerContactDetail> data = _IBuyerContactDetail_Repository.GetContactDetailbyContactId(id).ToList();
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get City in Buyer");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CreateUpdate(BuyerModel objInputBuyerMaster)
        {
            Common cm = new Common();
            DataResponse dataResponse = new DataResponse();
            try
            {
                bool check = false;
                if (sessionUtils.HasUserLogin())
                {
                    string TempImgPath = System.Web.HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["TempImgPath"]);
                    string DocumentImgPath = System.Web.HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["DocumentImgPath"]);
                    if (objInputBuyerMaster.BuyerId > 0)
                    {
                        check = true;
                    }
                    if (!_IBuyer_Repository.CheckBuyerDuplication(objInputBuyerMaster, check))
                    {
                        objInputBuyerMaster.CreatedBy = sessionUtils.UserId;
                        objInputBuyerMaster.ModifyBy = sessionUtils.UserId;
                        objInputBuyerMaster.DeleteBy = sessionUtils.UserId;
                        BuyerMaster ResponseVal = _IBuyer_Repository.CreateUpdate(objInputBuyerMaster);
                        //ResponseVal 1: insert,2:update ,0: error
                        if (ResponseVal != null)
                        {
                            if (!System.IO.Directory.Exists(DocumentImgPath))
                            {
                                System.IO.Directory.CreateDirectory(DocumentImgPath);
                            }
                            //if (!string.IsNullOrEmpty(objInputBuyerMaster.DocumentsData))
                            if ((objInputBuyerMaster.DocumentsData != null))
                            {
                                string[] DocImageDetails = objInputBuyerMaster.DocumentsData.Split('|');
                                if (DocImageDetails != null)
                                {
                                    if (DocImageDetails.Length > 0)
                                    {
                                        foreach (var item in DocImageDetails)
                                        {
                                            string fileName = item.Split('-')[2].ToString();
                                            if (System.IO.File.Exists(TempImgPath.ToString() + "/" + fileName.ToString()))
                                            {
                                                if (!System.IO.File.Exists(DocumentImgPath.ToString() + "/" + fileName.ToString()))
                                                {
                                                    System.IO.File.Move(TempImgPath.ToString() + "/" + fileName.ToString(), DocumentImgPath.ToString() + "/" + fileName.ToString());
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "successfully", new
                            {
                                valueData = ResponseVal.BuyerId
                            });
                            // Start : Only Insert Time SMS Send
                            SMSSpeechMaster Speech = _ISMSSpeech_Repository.DuplicateSMSSpeech("Contact Speech").FirstOrDefault(); // To GET SMS Speech used SMS Tile.
                            if ((objInputBuyerMaster.BuyerContactDetails != null))
                            {
                                foreach (var item in objInputBuyerMaster.BuyerContactDetails)
                                {
                                    if (item.Status == 1)
                                    {
                                        string[] mobilearray = item.MobileNo.Split(',');
                                        foreach (string mobile in mobilearray)
                                        {
                                            string mob = mobile.Split(' ')[1].ToString();
                                            cm.sendsms(mob, Speech.SMS);
                                        }
                                    }
                                }
                            }
                            // End 
                            // Start : Only Insert Time Email Send
                            EmailSpeechMaster SpeechEmail = _IEmailSpeech_Repository.CheckEmailSpeech("Contact Speech"); // To GET EMAIL Speech used Email Tile.
                            if ((objInputBuyerMaster.BuyerContactDetails != null))
                            {
                                foreach (var item in objInputBuyerMaster.BuyerContactDetails)
                                {
                                    if (item.Status == 1)
                                    {
                                        cm.sendmail(item.Email, SpeechEmail.Description, "Introduction from Gurjari Ltd.", SpeechEmail.Email, SpeechEmail.Password);
                                    }
                                }
                            }
                            // End 
                        }
                        else
                        {
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Opps! something wrong", null);
                        }
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Buyer Company Name" + MessageValue.Exists, null);
                    }
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "Invalid User", null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Create/Update Buyer");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteById(int id)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    BuyerModel objInputBuyer = new BuyerModel();
                    objInputBuyer.BuyerId = id;
                    objInputBuyer.DeleteBy = sessionUtils.UserId;

                    int ResponseVal = _IBuyer_Repository.Delete(objInputBuyer);
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
                ex.SetLog("Delete Buyer");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult CheckBuyerDuplicate(BuyerModel objInputBuyerMaster)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    bool check = false;
                    if (objInputBuyerMaster.BuyerId > 0)
                    {
                        check = true;
                    }
                    if (_IBuyer_Repository.CheckBuyerDuplication(objInputBuyerMaster, check))
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Buyer Company Name" + MessageValue.Exists, null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", null);
                    }
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "Invalid User", null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Delete Buyer");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        //[HttpGet]
        //public JsonResult DeleteById(int id)
        //{
        //    DataResponse dataResponse = new DataResponse();
        //    if (sessionUtils.HasUserLogin())
        //    {
        //        try
        //        {
        //            BuyerMaster objBuyerMaster = _IBuyer_Repository.GetById(id);
        //            objBuyerMaster.IsActive = false;
        //            objBuyerMaster.DeletedBy = sessionUtils.UserId;
        //            objBuyerMaster.DeletedDate = DateTime.Now;
        //            _IBuyer_Repository.UpdateBuyer(objBuyerMaster);
        //            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Delete successfully", null);
        //        }
        //        catch (Exception ex)
        //        {
        //            throw;
        //        }
        //    }
        //    else
        //    {
        //        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "Invalid User", null);
        //    }
        //    return Json(dataResponse, JsonRequestBehavior.AllowGet);
        //}

        [HttpGet]
        public JsonResult GetAllBuyerInfoById(int id)
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    BuyerModel objBuyerMaster = _IBuyer_Repository.FetchById(id);
                    List<vmBuyerContactDetail> objBuyerContactDetail = _IBuyerContactDetail_Repository.GetById(id).ToList();
                    List<BuyerBankDetail> objBuyerBankDetail = _IBuyerBankDetail_Repository.GetByBuyerId(id).ToList();
                    List<BuyerLicenseDetail> objBuyerLicenseDetail = _IBuyerContactDetail_Repository.GetLicenseByBuyerId(id).ToList();
                    List<BuyerAddressDetail> objBuyerAddressDetail = _IBuyerContactDetail_Repository.GetAddressByBuyerId(id).ToList();
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", new
                    {
                        objBuyerMaster = objBuyerMaster,
                        objBuyerContactDetail = objBuyerContactDetail,
                        objBuyerBankDetail = objBuyerBankDetail,
                        objBuyerLicenseDetail = objBuyerLicenseDetail,
                        objBuyerAddressDetail = objBuyerAddressDetail
                    });
                }
                catch (Exception ex)
                {
                    ex.SetLog("Get Buyer by Id");
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                }
            }
            else
            {
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "Invalid User", null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getallbuyercom()
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                //List<BuyerMaster> data = _IBuyer_Repository.GetAllBuyerCompany().ToList();
                int Userid = sessionUtils.UserId;
                var data1 = _IBuyer_Repository.GetAllBuyerCompany(Userid);
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data1);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get all Buyer");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        public ActionResult getbuyerEmail(int id)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var data = _IBuyer_Repository.GetBuyerById(id);
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);

            }
            catch (Exception ex)
            {
                ex.SetLog("Get all Buyer Email");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public FileResult GetVCCard(int id)
        {
            var obj = _IBuyer_Repository.FetchById(id);
            var contactobj = _IBuyerContactDetail_Repository.GetContactDetailbybuyerId(id).ToList();
            var addresss = _IBuyerContactDetail_Repository.GetAddressByBuyerId(id).ToList();
            Common cm = new Common();
            string str = "";
            VCardData objvcard = new VCardData();
            if (obj != null)
            {
                objvcard.Company = obj.CompanyName;
                objvcard.Website = obj.WebAddress;
            }
            //if (addresss.Count != 0)
            //{
            //    //for (var i = 0; i < addresss.Count; i++)
            //    //{
            //    objvcard.Address = addresss[0].Address;
            //    objvcard.city = addresss[0].CityName;
            //    objvcard.state = addresss[0].StateName;
            //    objvcard.country = addresss[0].CountryName;
            //    objvcard.Telephone = addresss[0].Telephone;
            //    objvcard.Pincode = addresss[0].PinCode;
            //    //}
            //}
            if (contactobj != null)
            {
                for (var i = 0; i < contactobj.Count; i++)
                {
                    objvcard.Name = contactobj[i].ContactPerson;
                    objvcard.Mobileno = contactobj[i].MobileNo;
                    objvcard.Email = contactobj[i].Email;
                    str += cm.VCardFile(objvcard);
                }
            }
           
            return File(System.Text.Encoding.ASCII.GetBytes(str), "text/x-vcard");
        }

        protected override void Dispose(bool disposing)
        {
            _IBuyerBankDetail_Repository.Dispose();
            _IBuyerContactDetail_Repository.Dispose();
            _IBuyer_Repository.Dispose();
            _ICity_Repository.Dispose();
            _ICountry_Repository.Dispose();
            _IDesignation_Repository.Dispose();
            _IState_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}
