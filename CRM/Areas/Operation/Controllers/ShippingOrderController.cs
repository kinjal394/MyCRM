using CRM.Models;
using CRM_Repository.Data;
using CRM_Repository.Service;
using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SelectPdf;
using System.IO;
using System.Text;

using static CRM.PdfFunction;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Drawing;
using CRM.App_Start;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRM.Areas.Operation.Controllers
{
    public class ShippingOrderController : Controller
    {
        private IShippingOrder_Repository _IShippingOrder_Repository;
        private IBuyerContactDetail_Repository _IBuyerContactDetail_Repository;

        public ShippingOrderController()
        {
            this._IBuyerContactDetail_Repository = new BuyerContactDetail_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IShippingOrder_Repository = new ShippingOrder_Repository(new CRM_Repository.Data.elaunch_crmEntities());
        }

        // GET: Operation/ShippingOrder
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddShippingOrder(int id = 0, int temp = 0)
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
        public JsonResult GetAllShippingOrderInfoById(int id)
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    ShippingOrderMaster objDetail = _IShippingOrder_Repository.GetShippingOrderID(id);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", new
                    {
                        objShippingOrderMaster = objDetail
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
        public JsonResult CreateUpdate(ShippingOrderMaster objInputSOM)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    int ResponseVal = 0;
                    objInputSOM.IsActive = true;
                    objInputSOM.Date = DateTime.Now;
                    if (objInputSOM.ShippingOrdId > 0)
                    {

                        _IShippingOrder_Repository.UpdateShippingOrder(objInputSOM);
                        ResponseVal = 2;
                    }
                    else
                    {
                        _IShippingOrder_Repository.AddShippingOrder(objInputSOM);
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
                ex.SetLog("Create/Update Shipping Order");
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
                        _IShippingOrder_Repository.DeleteShippingOrder(id);
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
                ex.SetLog("Delete Shipping Order");
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
                        ShippingOrderMaster obj = _IShippingOrder_Repository.GetShippingOrderID(id);
                        ResponseVal = 1;
                        str = GetHtmlString(System.Web.HttpContext.Current.Server.MapPath("~/ReportFormat/ShippingOrderReport.html"));

                        string[] prddesc = obj.ProductDescription.Split('|');
                        string fulldesc = "";
                        foreach (var x in prddesc)
                        {
                            fulldesc += x.Split('-')[1].ToString() + '|';
                        }


                        str = str.Replace("@@TypeofShipment@@", obj.TypeofShipment);
                        str = str.Replace("@@Commodity@@", obj.Commodity);
                        str = str.Replace("@@Nooftotal@@", obj.Nooftotal);
                        str = str.Replace("@@NoofBL@@", obj.NoofBL);

                        str = str.Replace("@@CPBuyerName@@", obj.CPBuyerName);
                        str = str.Replace("@@CPBuyerAddress@@", obj.CPBuyerAddress);
                        str = str.Replace("@@CPBuyerTelephone@@", obj.CPBuyerTelephone);
                        str = str.Replace("@@CPBuyerFax@@", obj.CPBuyerFax);

                        str = str.Replace("@@CPBuyerContactPerson@@", obj.CPBuyerContactPerson);
                        str = str.Replace("@@EDBuyerName@@", obj.EDBuyerName);
                        str = str.Replace("@@EDBuyerAddress@@", obj.EDBuyerAddress);
                        str = str.Replace("@@EDBuyerTelephone@@", obj.EDBuyerTelephone);


                        str = str.Replace("@@EDBuyerContactPerson@@", obj.EDBuyerContactPerson);
                        str = str.Replace("@@Freight@@", obj.Freight);
                        str = str.Replace("@@POL@@", obj.POL);
                        str = str.Replace("@@POD@@", obj.POD);

                        str = str.Replace("@@ProductDescription@@", fulldesc.Replace("|", "<br/>").ToString());
                        str = str.Replace("@@ShippingMarksNumber@@", obj.ShippingMarksNumber);
                        str = str.Replace("@@TotalNOPkgs@@", obj.TotalNOPkgs);
                        str = str.Replace("@@TotalGross@@", Convert.ToString(obj.TotalGross));

                        str = str.Replace("@@Measurement@@", Convert.ToString(obj.Measurement));
                        str = str.Replace("@@Shipmentterms@@", obj.Shipmentterms);
                        str = str.Replace("@@CompanyName@@", obj.CompanyName);
                        // var sdate = Convert.ToString(obj.Date) != "" ?  obj.Date.ToString("dd/MM/yyyy") : Convert.ToString(obj.Date);
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
            _IShippingOrder_Repository.Dispose();
            _IBuyerContactDetail_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}