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
using static CRM.PdfFunction;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace CRM.Areas.Operation.Controllers
{
    [HasLoginSessionFilter]
    public class BillofLoadingController : Controller
    {

     
        private IBillofLoading_Repository _IBillofLoading_Repository;
        private IBuyerContactDetail_Repository _IBuyerContactDetail_Repository;

        public BillofLoadingController()
        {
            this._IBuyerContactDetail_Repository = new BuyerContactDetail_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IBillofLoading_Repository = new BillofLoading_Repository(new CRM_Repository.Data.elaunch_crmEntities());
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddBillofLoading(int id = 0, int temp = 0)
        {
            ViewBag.id = id;
            ViewBag.isdisable = temp;
            return View();
        }
        [HttpGet]
        public JsonResult GetBuyerAddressInfoById(int id)
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    BuyerAddressDetail objBuyerAddressDetail = _IBuyerContactDetail_Repository.GetBAddbyid(id);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", new
                    {
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


        [HttpGet]
        public JsonResult GetAllBillofLoadingInfoById(int id)
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    BillofLoadingMaster objDetail = _IBillofLoading_Repository.GetBillofLoadingID(id);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", new
                    {
                        objBillofLoadingMaster = objDetail
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


        [HttpPost]
        public JsonResult CreateUpdate(BillofLoadingMaster objInputSOM)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    int ResponseVal = 0;
                    objInputSOM.IsActive = true;
                    objInputSOM.Date = DateTime.Now;
                    if (objInputSOM.BLId > 0)
                    {

                        _IBillofLoading_Repository.UpdateBillofLoading(objInputSOM);
                        ResponseVal = 2;
                    }
                    else
                    {
                        _IBillofLoading_Repository.AddBillofLoading(objInputSOM);
                        ResponseVal = 1;
                    }
                    //ResponseVal 1: insert,2:update ,0: error
                    if (ResponseVal == 1)
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully", null);
                    }
                    else if (ResponseVal == 2)
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully", null);
                    }
                    else if (ResponseVal == 0)
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Opps! something wrong", null);
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
                ex.SetLog("Create/Update Billof Loading");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult DeleteById(int id)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    int ResponseVal = 0;
                    try
                    {
                        _IBillofLoading_Repository.DeleteBillofLoading(id);
                        ResponseVal = 1;
                    }
                    catch (Exception)
                    {
                        ResponseVal = 0;
                    }
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
                ex.SetLog("Delete Billof Loading");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult PrintById(int id)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    int ResponseVal = 0;
                    var str = "";
                    try
                    {
                        BillofLoadingMaster obj = _IBillofLoading_Repository.GetBillofLoadingID(id);
                        ResponseVal = 1;
                        str = GetHtmlString(System.Web.HttpContext.Current.Server.MapPath("~/ReportFormat/BillofLoadingReport.html"));

                        string[] prddesc = obj.ProductDescription.Split('|');
                        string fulldesc = "";
                        foreach (var x in prddesc)
                        {
                            fulldesc += x.Split('-')[1].ToString() + '|';
                        }

                        str = str.Replace("@@ShipperName@@", obj.ShipperName);
                        str = str.Replace("@@ShipperAddress@@", obj.ShipperAddress);
                        
                        str = str.Replace("@@ConsigneeName@@", obj.ConsigneeName);
                        str = str.Replace("@@ConsigneeAddress@@", obj.ConsigneeAddress);
                        
                        str = str.Replace("@@Freight@@", obj.Freight);
                        str = str.Replace("@@POL@@", obj.POL);
                        str = str.Replace("@@POD@@", obj.POD);

                        str = str.Replace("@@ProductDescription@@", fulldesc.Replace("|", "<br/>").ToString());
                        str = str.Replace("@@ShippingMarksNumber@@", obj.ShippingMarksNumber);
                        str = str.Replace("@@TotalNOPkgs@@", obj.TotalNOPkgs);
                        str = str.Replace("@@GrossWeight@@", Convert.ToString(obj.GrossWeight));
                        str = str.Replace("@@NetWeight@@", Convert.ToString(obj.NetWeight));

                        str = str.Replace("@@VolMeasurement@@", Convert.ToString(obj.VolMeasurement));
                        str = str.Replace("@@CompanyName@@", obj.CompanyName);
                        //var sdate = Convert.ToString(obj.Date) != "" ? obj.Date.ToString("dd/MM/yyyy") : Convert.ToString(obj.Date);
                        var strdate = Convert.ToString(obj.Date) != "" ? obj.Date?.ToString("dd/MM/yyyy") : "";
                        str = str.Replace("@@Date@@", Convert.ToString(strdate));
                    }
                    catch (Exception)
                    {
                        ResponseVal = 0;
                    }
                    //ResponseVal 1: Delete,0: error
                    if (ResponseVal == 1)
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, str, null);
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
                ex.SetLog("Delete Shipping Order");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            _IBillofLoading_Repository.Dispose();
            _IBuyerContactDetail_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}