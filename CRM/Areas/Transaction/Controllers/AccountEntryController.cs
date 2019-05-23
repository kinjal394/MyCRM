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
using System.Web.Hosting;
using System.Configuration;

namespace CRM.Areas.Transaction.Controllers
{
    [HasLoginSessionFilter]
    public class AccountEntryController : Controller
    {
        private IAccountEntry_Repository _IAccountEntry_Repository;

        public AccountEntryController()
        {
            this._IAccountEntry_Repository = new AccountEntry_Repository(new elaunch_crmEntities());
        }
        // GET: Transaction/AccountEntry
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AccountEntryPopup()
        {
            return View();
        }



        [HttpPost]
        public JsonResult SaveAccountEntry(AssetsExpenseMaster obj, HttpPostedFileBase billpdf, HttpPostedFileBase Tslip, HttpPostedFileBase photo)
        {
            DataResponse dataResponse = new DataResponse();
            Common cs = new Common();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    if (!string.IsNullOrEmpty(obj.PartyName))
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(obj.Amount)))
                        {
                            if (!string.IsNullOrEmpty(obj.BillNo))
                            {
                                if (!string.IsNullOrEmpty(Convert.ToString(obj.BillDate)))
                                {
                                    //string filename_photo = "";
                                    //string filename_slip = "";
                                    //string filename_pdf = "";
                                    string path = HostingEnvironment.ApplicationPhysicalPath;
                                    if (!System.IO.Directory.Exists(path + "\\Content\\lib\\AccountEntry\\Photo\\"))
                                    {
                                        System.IO.Directory.CreateDirectory(path + "\\Content\\lib\\AccountEntry\\Photo\\");
                                    }
                                    string pathslip = HostingEnvironment.ApplicationPhysicalPath;
                                    if (!System.IO.Directory.Exists(pathslip + "\\Content\\lib\\AccountEntry\\TransactionSlip\\"))
                                    {
                                        System.IO.Directory.CreateDirectory(pathslip + "\\Content\\lib\\AccountEntry\\TransactionSlip\\");
                                    }
                                    string pathpdf = HostingEnvironment.ApplicationPhysicalPath;
                                    if (!System.IO.Directory.Exists(pathpdf + "\\Content\\lib\\AccountEntry\\BillPdf\\"))
                                    {
                                        System.IO.Directory.CreateDirectory(pathpdf + "\\Content\\lib\\AccountEntry\\BillPdf\\");
                                    }

                                    //Random rand = new Random();
                                    //int fno = rand.Next(111, 999);
                                    //if (!string.IsNullOrEmpty(Convert.ToString(photo)))
                                    //{
                                    //    filename_photo = cs.fileupload(photo, "Content/lib/AccountEntry/Photo", 100, 100);
                                    //}
                                    //if (!string.IsNullOrEmpty(Convert.ToString(Tslip)))
                                    //{
                                    //    filename_slip = cs.fileDocupload(Tslip, fno, "Content/lib/AccountEntry/TransactionSlip");
                                    //}
                                    //if (!string.IsNullOrEmpty(Convert.ToString(billpdf)))
                                    //{
                                    //    int pno = rand.Next(222, 888);
                                    //    filename_pdf = cs.fileDocupload(billpdf, pno, "Content/lib/AccountEntry/BillPDF");
                                    //}

                                    //obj.Photo = filename_photo;
                                    //obj.TransactionSlip = filename_slip;
                                    //obj.BillPdf = filename_pdf;
                                    obj.CreatedBy = sessionUtils.UserId;
                                    obj.CreatedDate = DateTime.Now;
                                    obj.IsActive = true;
                                    obj.TaxId = obj.TaxId <= 0 || obj.TaxId == null ? null : obj.TaxId;
                                    obj.TaxValue = obj.TaxValue;
                                    _IAccountEntry_Repository.InsertAccountEntry(obj);

                                    string TempImgPath = System.Web.HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["TempImgPath"]);
                                    string billpdfpath = System.Web.HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["BillPdf"]);
                                    string slippath = System.Web.HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["TransactionSlip"]);
                                    string photopath = System.Web.HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["Photo"]);

                                    if (System.IO.File.Exists(TempImgPath.ToString() + "/" + Convert.ToString(obj.BillPdf)))
                                    {
                                        System.IO.File.Move(TempImgPath.ToString() + "/" + Convert.ToString(obj.BillPdf), billpdfpath.ToString() + "/" + Convert.ToString(obj.BillPdf));
                                    }
                                    if (System.IO.File.Exists(TempImgPath.ToString() + "/" + Convert.ToString(obj.TransactionSlip)))
                                    {
                                        System.IO.File.Move(TempImgPath.ToString() + "/" + Convert.ToString(obj.TransactionSlip), slippath.ToString() + "/" + Convert.ToString(obj.TransactionSlip));
                                    }
                                    if (System.IO.File.Exists(TempImgPath.ToString() + "/" + Convert.ToString(obj.Photo)))
                                    {
                                        System.IO.File.Move(TempImgPath.ToString() + "/" + Convert.ToString(obj.Photo), photopath.ToString() + "/" + Convert.ToString(obj.Photo));
                                    }

                                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully", null);
                                }
                                else
                                {
                                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Bill Date" + MessageValue.Param, null);
                                }
                            }
                            else
                            {
                                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Bill No" + MessageValue.Param, null);
                            }
                        }
                        else
                        {
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Amount" + MessageValue.Param, null);
                        }
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Party Name" + MessageValue.Param, null);
                    }
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "Invalid User", null);
                }
                return Json(dataResponse, JsonRequestBehavior.AllowGet);



            }
            catch (Exception ex)
            {
                ex.SetLog("Create AccountEntry");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult UpdateAccountEntry(AssetsExpenseMaster obj, HttpPostedFileBase billpdf, HttpPostedFileBase Tslip, HttpPostedFileBase photo)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                Common cs = new Common();
                try
                {
                    if (sessionUtils.HasUserLogin())
                    {
                        if (!string.IsNullOrEmpty(obj.PartyName))
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(obj.Amount)))
                            {
                                if (!string.IsNullOrEmpty(obj.BillNo))
                                {
                                    if (!string.IsNullOrEmpty(Convert.ToString(obj.BillDate)))
                                    {
                                        //string filename_photo = "";
                                        //string filename_slip = "";
                                        //string filename_pdf = "";
                                        string path = HostingEnvironment.ApplicationPhysicalPath;
                                        if (!System.IO.Directory.Exists(path + "\\Content\\lib\\AccountEntry\\Photo\\"))
                                        {
                                            System.IO.Directory.CreateDirectory(path + "\\Content\\lib\\AccountEntry\\Photo\\");
                                        }
                                        string pathslip = HostingEnvironment.ApplicationPhysicalPath;
                                        if (!System.IO.Directory.Exists(pathslip + "\\Content\\lib\\AccountEntry\\TransactionSlip\\"))
                                        {
                                            System.IO.Directory.CreateDirectory(pathslip + "\\Content\\lib\\AccountEntry\\TransactionSlip\\");
                                        }
                                        string pathpdf = HostingEnvironment.ApplicationPhysicalPath;
                                        if (!System.IO.Directory.Exists(pathpdf + "\\Content\\lib\\AccountEntry\\BillPdf\\"))
                                        {
                                            System.IO.Directory.CreateDirectory(pathpdf + "\\Content\\lib\\AccountEntry\\BillPdf\\");
                                        }

                                        //Random rand = new Random();
                                        //int fno = rand.Next(111, 999);
                                        //if (!string.IsNullOrEmpty(Convert.ToString(photo)))
                                        //{
                                        //    filename_photo = cs.fileupload(photo, "Content/lib/AccountEntry/Photo", 100, 100);
                                        //}
                                        //if (!string.IsNullOrEmpty(Convert.ToString(Tslip)))
                                        //{
                                        //    filename_slip = cs.fileDocupload(Tslip, fno, "Content/lib/AccountEntry/TransactionSlip");
                                        //}
                                        //if (!string.IsNullOrEmpty(Convert.ToString(billpdf)))
                                        //{
                                        //    int pno = rand.Next(222, 888);
                                        //    filename_pdf = cs.fileDocupload(billpdf, pno, "Content/lib/AccountEntry/BillPDF");
                                        //}

                                        //AccountEntryMaster objupd = _IAccountEntry_Repository.GetAccountEntryByID(obj.AccountId);
                                        //if (string.IsNullOrEmpty(filename_photo))
                                        //{
                                        //    filename_photo = objupd.Photo;
                                        //}
                                        //if (string.IsNullOrEmpty(filename_slip))
                                        //{
                                        //    filename_slip = objupd.TransactionSlip;
                                        //}
                                        //if (string.IsNullOrEmpty(filename_pdf))
                                        //{
                                        //    filename_pdf = objupd.BillPdf;
                                        //}

                                        //obj.Photo = filename_photo;
                                        //obj.TransactionSlip = filename_slip;
                                        //obj.BillPdf = filename_pdf; 

                                        AssetsExpenseMaster objacc = _IAccountEntry_Repository.GetAccountEntryByID(obj.AccountId);
                                        objacc.ModifyBy = sessionUtils.UserId;
                                        objacc.ModifyDate = DateTime.Now;
                                        objacc.BillDate = obj.BillDate;
                                        objacc.BillNo = obj.BillNo;
                                        objacc.Amount = obj.Amount;
                                        objacc.BillPdf = obj.BillPdf;
                                        objacc.TransactionSlip = obj.TransactionSlip;
                                        objacc.Photo = obj.Photo;
                                        objacc.ExchangeRate = obj.ExchangeRate;
                                        objacc.Amount = obj.Amount;
                                        objacc.INRAmount = obj.INRAmount;
                                        objacc.CurrencyId = obj.CurrencyId;
                                        objacc.LegerId = obj.LegerId;
                                        objacc.PartyName = obj.PartyName;
                                        objacc.Remark = obj.Remark;
                                        objacc.AccountEntryType = obj.AccountEntryType;
                                        objacc.TaxId = objacc.TaxId <= 0 || objacc.TaxId == null ? null : obj.TaxId;

                                        objacc.TaxValue = obj.TaxValue;
                                        _IAccountEntry_Repository.UpdateAccountEntry(objacc);

                                        string TempImgPath = System.Web.HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["TempImgPath"]);
                                        string billpdfpath = System.Web.HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["BillPdf"]);
                                        string slippath = System.Web.HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["TransactionSlip"]);
                                        string photopath = System.Web.HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["Photo"]);

                                        if (System.IO.File.Exists(TempImgPath.ToString() + "/" + Convert.ToString(objacc.BillPdf)))
                                        {
                                            System.IO.File.Move(TempImgPath.ToString() + "/" + Convert.ToString(objacc.BillPdf), billpdfpath.ToString() + "/" + Convert.ToString(objacc.BillPdf));
                                        }
                                        if (System.IO.File.Exists(TempImgPath.ToString() + "/" + Convert.ToString(objacc.TransactionSlip)))
                                        {
                                            System.IO.File.Move(TempImgPath.ToString() + "/" + Convert.ToString(objacc.TransactionSlip), slippath.ToString() + "/" + Convert.ToString(objacc.TransactionSlip));
                                        }
                                        if (System.IO.File.Exists(TempImgPath.ToString() + "/" + Convert.ToString(objacc.Photo)))
                                        {
                                            System.IO.File.Move(TempImgPath.ToString() + "/" + Convert.ToString(objacc.Photo), photopath.ToString() + "/" + Convert.ToString(objacc.Photo));
                                        }

                                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully", null);
                                    }
                                    else
                                    {
                                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Bill Date" + MessageValue.Param, null);
                                    }
                                }
                                else
                                {
                                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Bill No" + MessageValue.Param, null);
                                }
                            }
                            else
                            {
                                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Amount" + MessageValue.Param, null);
                            }
                        }
                        else
                        {
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Party Name" + MessageValue.Param, null);
                        }
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "Invalid User", null);
                    }
                    return Json(dataResponse, JsonRequestBehavior.AllowGet);



                }
                catch (Exception ex)
                {
                    ex.SetLog("Update AccountEntry");
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                    return Json(dataResponse, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Update AccountEntry");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult GetByIdAccountEntry(int AccountId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    var objaccount = _IAccountEntry_Repository.GetAccountEntryByID(AccountId);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, objaccount);
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "User is not valid", null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Get AccountEntry");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteAccountEntry(string id)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    if (id != "")
                    {
                        int aid = Convert.ToInt32(id);
                        AssetsExpenseMaster amaster = new AssetsExpenseMaster();
                        amaster = _IAccountEntry_Repository.GetAccountEntryByID(aid);
                        amaster.IsActive = false;
                        _IAccountEntry_Repository.UpdateAccountEntry(amaster);
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
                ex.SetLog("Delete AccountEntry");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
    }
}