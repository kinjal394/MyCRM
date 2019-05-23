using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM_Repository.Service;
using CRM_Repository.ServiceContract;
using CRM_Repository.Data;
using CRM.Models;
using CRM_Repository.DTOModel;
using CRM.App_Start;
using CRM_Repository.ExtendedModel;

namespace CRM.Areas.Transaction.Controllers
{
    [HasLoginSessionFilter]
    public class InquiryController : Controller
    {
        private IInquiry_Repository _IInquiry_Repository;
        private InquiryItemDetail_Repository _IInquiryItemDetail_Repository;
        private IBuyer_Repository _IBuyer_Repository;
        private IBuyerContactDetail_Repository _IBuyerContactDetail_Repository;
        private IUser_Repository _IUser_Repository;
        private ISMSSpeech_Repository _ISMSSpeech_Repository;
        private IEmailSpeech_Repository _IEmailSpeech_Repository;
        public InquiryController()
        {
            this._IUser_Repository = new User_Repository(new elaunch_crmEntities());
            this._IInquiry_Repository = new Inquiry_Repository(new elaunch_crmEntities());
            this._IInquiryItemDetail_Repository = new InquiryItemDetail_Repository(new elaunch_crmEntities());
            this._IBuyer_Repository = new Buyer_Repository(new elaunch_crmEntities());
            this._IBuyerContactDetail_Repository = new BuyerContactDetail_Repository(new elaunch_crmEntities());
            this._ISMSSpeech_Repository = new SMSSpeech_Repository(new elaunch_crmEntities());
            this._IEmailSpeech_Repository = new EmailSpeech_Repository(new elaunch_crmEntities());
        }

        // GET: Transaction/Inquiry
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult InquiryPopup()
        {
            return View();
        }

        public ActionResult GridIndex()
        {
            return View();
        }


        public ActionResult InquiryFollowup()
        {
            return View();
        }

        public ActionResult InquiryFollowupList()
        {
            return View();
        }

        public ActionResult AddInquiry(int id = 0, int temp = 0)
        {
            ViewBag.id = id;
            ViewBag.isdisable = temp;
            return View();
        }

        public JsonResult GetInquiryFollowUp(int id)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var data = _IInquiry_Repository.GetInquiryFollowUpById(id);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get Inquiry FollowUp");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetInquiryFollowUpByID(int id)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var data = _IInquiry_Repository.FetchInquiryFollowUpById(id);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get Inquiry FollowUp");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllInquiryInfo()
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var data = _IInquiry_Repository.GetAllInquiryFollowup(sessionUtils.UserId.ToString(),sessionUtils.UserRollType).ToList();
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get Inquiry FollowUp");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SaveInquiryFollowUp(InquiryFollowupMaster objInquiry)
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    var response = new InquiryFollowupMaster();


                    if (objInquiry.NextFollowDate == null)
                    {
                        objInquiry.NextFollowDate = DateTime.Now.Date;
                    }
                    if (objInquiry.NextFollowTime == null)
                    {
                        objInquiry.NextFollowTime = DateTime.Now.TimeOfDay;
                    }
                    objInquiry.IsActive = true;
                    if (objInquiry.FollowupId > 0)
                    {
                        var data = _IInquiry_Repository.GetInquiryFollowById(objInquiry.FollowupId);
                        //if (data.AssignId == objInquiry.AssignId)
                        //{
                        objInquiry.CreatedDate = data.CreatedDate;
                        objInquiry.CreatedBy = data.CreatedBy;
                        objInquiry.Modifydate = DateTime.Now;
                        objInquiry.ModifyBy = sessionUtils.UserId;
                        _IInquiry_Repository.UpdateInquiryFollowUp(objInquiry);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Data Updated Successfully", null);
                        //}
                        //else
                        //{
                        //    objInquiry.CreatedDate = DateTime.Now;
                        //    objInquiry.CreatedBy = sessionUtils.UserId;
                        //    response = _IInquiry_Repository.InsertInquiryFollowUp(objInquiry);
                        //    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Data Insert Successfully", null);
                        //}
                    }
                    else
                    {
                        objInquiry.CreatedDate = DateTime.Now;
                        objInquiry.CreatedBy = sessionUtils.UserId;
                        response = _IInquiry_Repository.InsertInquiryFollowUp(objInquiry);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Data Insert Successfully", null);
                    }
                }
                catch (Exception ex)
                {
                    ex.SetLog("Create/Update Inquiry");
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                }
            }
            else
            {
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, MessageValue.InvalidUser, null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetInquiryReportingUser(int inqId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                InquiryMaster inq = _IInquiry_Repository.GetInquiryId(inqId);
                if (sessionUtils.UserRollType > 1)
                {
                    var data = _IUser_Repository.GetInquiryReportingUser(sessionUtils.UserId).ToList();
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
                }
                else
                {
                    var data = _IUser_Repository.GetInquiryReportingUser(inq.CreatedBy).ToList();
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
                }
                //var data = _IUser_Repository.GetReportingUser(sessionUtils.UserId).ToList();
                //dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get Inquiry by UserId");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                throw ex;
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult CreateUpdate(InquiryModel objInputInqiryModel)
        {
            Common cm = new Common();
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    objInputInqiryModel.CreatedBy = sessionUtils.UserId;
                    objInputInqiryModel.ModifyBy = sessionUtils.UserId;
                    //var inqdata = _IInquiry_Repository.GetAllInquiry().FirstOrDefault();
                    //if (inqdata.InqNo == objInputInqiryModel.InqNo && objInputInqiryModel.InqId == 0)
                    //{
                    if (objInputInqiryModel.InqId == 0)
                    {
                        var data = cm.GetAutoNumber("Inquiry");
                        objInputInqiryModel.InqNo = data.Rows[0][0].ToString();
                    }
                   // }
                    int ResponseVal = _IInquiry_Repository.CreateUpdate(objInputInqiryModel);
                    //ResponseVal 1: insert,2:update ,0: error
                    if (ResponseVal == 1)
                    {
                        SMSSpeechMaster Speech = _ISMSSpeech_Repository.DuplicateSMSSpeech("Inquiry Speech").FirstOrDefault(); // To GET SMS Speech used SMS Tile.
                        EmailSpeechMaster SpeechEmail = _IEmailSpeech_Repository.CheckEmailSpeech("Inquiry Speech"); // To GET EMAIL Speech used Email Tile.
                        string[] mobilearray = objInputInqiryModel.MobileNo.Split(',');
                        foreach (string mobile in mobilearray)
                        {
                            string mob = mobile.ToString();
                            cm.sendsms(mob, Speech.SMS);
                        }
                        cm.sendmail(objInputInqiryModel.Email, SpeechEmail.Description, "Introduction from Gurjari Ltd.", SpeechEmail.Email, SpeechEmail.Password);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully and Your Inquiry Number is " + objInputInqiryModel.InqNo, null);
                    }
                    else if (ResponseVal == 2)
                    {
                        SMSSpeechMaster Speech = _ISMSSpeech_Repository.DuplicateSMSSpeech("Inquiry Speech").FirstOrDefault(); // To GET SMS Speech used SMS Tile.
                        EmailSpeechMaster SpeechEmail = _IEmailSpeech_Repository.CheckEmailSpeech("Inquiry Speech"); // To GET EMAIL Speech used Email Tile.
                        string[] mobilearray = objInputInqiryModel.MobileNo.Split(',');
                        foreach (string mobile in mobilearray)
                        {
                            string mob = mobile.ToString();
                            cm.sendsms(mob, Speech.SMS);
                        }
                        cm.sendmail(objInputInqiryModel.Email, SpeechEmail.Description, "Introduction from Gurjari Ltd.", SpeechEmail.Email, SpeechEmail.Password);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully and Your Inquiry Number is " + objInputInqiryModel.InqNo, null);
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
                ex.SetLog("Create/Update Inquiry");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult CreateUpdateInquiry(InquiryMaster objinquiry)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (ModelState.IsValid)
                {
                    if (objinquiry.InqId > 0)
                    {
                        var inqlist = _IInquiry_Repository.DuplicateEditInquiry(objinquiry.InqId, objinquiry.BuyerName, objinquiry.Email, objinquiry.MobileNo, objinquiry.Requirement).ToList();
                        if (inqlist.Count == 0)
                        {
                            InquiryMaster objinq = _IInquiry_Repository.GetInquiryId(objinquiry.InqId);
                            if (objinquiry.CityId == null)
                            {
                                int areaId = _IInquiry_Repository.GetAreaByCityId(objinquiry.CityId.Value);
                                objinq.CityId = areaId;
                            }
                            else
                            {
                                objinq.CityId = objinquiry.CityId;
                            }
                            objinq.InqId = objinquiry.InqId;
                            objinq.BuyerName = objinquiry.BuyerName;

                            objinq.ModifyBy = sessionUtils.UserId;
                            objinq.ModifyDate = DateTime.Now;
                            objinq.Email = objinquiry.Email;
                            objinq.InqDate = objinquiry.InqDate;
                            objinq.Subject = objinquiry.Subject;
                            objinq.IsActive = true;
                            //objinq.MobileCode1 = objinquiry.MobileCode1;
                            //objinq.MobileCode2 = objinquiry.MobileCode2;
                            objinq.MobileNo = objinquiry.MobileNo;
                            // objinq.MobileNo2 = objinquiry.MobileNo2;
                            objinq.SourceId = objinquiry.SourceId;
                            objinq.Requirement = objinquiry.Requirement;
                            objinq.Address = objinquiry.Address;
                            objinq.CityId = objinquiry.CityId;
                            _IInquiry_Repository.UpdateInquiry(objinq);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully", null);

                        }
                        else
                        {
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Already Exists", null);

                        }
                    }
                    else
                    {
                        var inqlist = _IInquiry_Repository.DuplicateEditInquiry(objinquiry.InqId, objinquiry.BuyerName, objinquiry.Email, objinquiry.MobileNo, objinquiry.Requirement).ToList();
                        if (inqlist.Count == 0)
                        {

                            InquiryMaster objinq = new InquiryMaster();
                            if (objinquiry.CityId == null)
                            {
                                int areaId = _IInquiry_Repository.GetAreaByCityId(objinquiry.CityId.Value);
                                objinq.CityId = areaId;
                            }
                            else
                            {
                                objinq.CityId = objinquiry.CityId;

                            }
                            //objinq.InqId = objinquiry.InqId;
                            objinq.BuyerName = objinquiry.BuyerName;
                            //objinq.AreaId = objinquiry.AreaId;
                            objinq.CreatedBy = sessionUtils.UserId;
                            objinq.CreatedDate = DateTime.Now;
                            objinq.Email = objinquiry.Email;
                            objinq.InqDate = objinquiry.InqDate;
                            objinq.IsActive = true;
                            //objinq.MobileCode1 = objinquiry.MobileCode1;
                            //objinq.MobileCode2 = objinquiry.MobileCode2;
                            objinq.MobileNo = objinquiry.MobileNo;
                            objinq.Subject = objinquiry.Subject;
                            //  objinq.MobileNo2 = objinquiry.MobileNo2;
                            objinq.SourceId = objinquiry.SourceId;
                            objinq.Requirement = objinquiry.Requirement;
                            objinq.Address = objinquiry.Address;
                            objinq.InqNo = _IInquiry_Repository.DuplicateInquiryNo();
                            objinq.Status = 1;

                            _IInquiry_Repository.InsertInquiry(objinq);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Inserted successfully... Your Inquiry Number Is : " + objinq.InqNo + "", null);

                        }
                        else
                        {
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Already Exists", null);
                        }
                    }
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, MessageValue.Param, null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Create/Update Inquiry");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteInquiry(int InqId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    InquiryModel objInputInquiryModel = new InquiryModel();
                    objInputInquiryModel.InqId = InqId;
                    objInputInquiryModel.DeletedBy = sessionUtils.UserId;

                    int ResponseVal = _IInquiry_Repository.Delete(objInputInquiryModel);
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
                ex.SetLog("Delete Inquiry");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteInquiryOLD(string InqId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    if (InqId != "")
                    {
                        int iid = Convert.ToInt32(InqId);
                        InquiryMaster objinq = new InquiryMaster();
                        objinq = _IInquiry_Repository.GetInquiryId(iid);
                        objinq.IsActive = false;
                        objinq.DeletedBy = sessionUtils.UserId;
                        objinq.DeletedDate = DateTime.Now;
                        _IInquiry_Repository.UpdateInquiry(objinq);
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
                ex.SetLog("Delete Inquiry by Id");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult InquiryNoAuto()
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var data = _IInquiry_Repository.DuplicateInquiryNo();
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get Inquiry No");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                throw ex;
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllInquiryInfoById(int id)
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    InquiryModel objInquiryMaster = _IInquiry_Repository.GetInquiryById(id);

                    //List<InquiryItemModel> objvmInquiryItem = new List<InquiryItemModel>();
                    //List<InquiryItemModel> objInquiryItem = _IInquiryItemDetail_Repository.GetInquiryItemDetailById(id).ToList();
                    //if (objInquiryItem != null)
                    //{
                    //    foreach (var item in objInquiryItem)
                    //    {
                    //        InquiryItemModel vmIItem = new InquiryItemModel();
                    //        vmIItem.InqDetailId = item.InqDetailId;
                    //        vmIItem.InqId = item.InqId;
                    //        vmIItem.CategoryId = item.CategoryId;
                    //        vmIItem.Category = item.Category;
                    //        vmIItem.SubCategoryId = item.SubCategoryId;
                    //        vmIItem.SubCategory = item.SubCategory;
                    //        vmIItem.MainProductId = item.MainProductId;
                    //        vmIItem.MainProductName = item.MainProductName;
                    //        vmIItem.ProductId = item.ProductId;
                    //        vmIItem.ProductName = item.ProductName;
                    //        vmIItem.ProductDescription = item.ProductDescription;
                    //        vmIItem.QtyCodeData = item.QtyCodeData;
                    //        vmIItem.QtyCode = item.QtyCode;
                    //        vmIItem.Qty = item.Qty;
                    //        vmIItem.Status = 2;
                    //        objvmInquiryItem.Add(vmIItem);
                    //    }
                    //}
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", new
                    {
                        objInquiryMaster = objInquiryMaster//,
                        //objInquiryItemDetail = objvmInquiryItem
                    });
                }
                catch (Exception ex)
                {
                    ex.SetLog("Get Inquiry by Id");
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
        public JsonResult GetBuyerContactDetailById(int id)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                BuyerMaster buyerdata = _IBuyer_Repository.GetById(id);
                List<BuyerContactDetail> buyercontact = _IBuyerContactDetail_Repository.GetByBuyerId(id).ToList();
                BuyerAddressDetail buyeraddress = _IBuyerContactDetail_Repository.GetAddressByBuyerId(id).FirstOrDefault();
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", new
                {
                    buyerdata = buyerdata,
                    buyercontact = buyercontact,
                    buyeraddress = buyeraddress
                });
            }
            catch (Exception ex)
            {
                ex.SetLog("Get BuyerAddressDetail in Inquiry");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetInvoiceInfo()
        {
            DataResponse dataResponse = new DataResponse();
            Common cm = new Common();
            try
            {
                var getData = cm.GetAutoNumber("Inquiry");

                //var ID = 0;
                //String date = DateTime.Now.ToString("dd");
                //String Month = DateTime.Now.ToString("MM");
                //String Year = DateTime.Now.Year.ToString();
                //string no = Year + Month + date;
                //var newdata = getData.FirstOrDefault();
                //var tempId = Convert.ToInt32(newdata.InqNo.Substring(3));
                //if (newdata != null) { ID = tempId; }
                //string InvCode = GenerateCode("INQ", 6, (ID + 1));
                //string InvCode = GenerateCode("INQ"+ no + "", 4, (ID + 1));
                var data = new
                {
                    InvCode = getData.Rows[0][0].ToString()
                };
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get Invoice info");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public FileResult GetVCCard(int id)
        {
            var obj = _IInquiry_Repository.GetInquiryId(id);
            Common cm = new Common();
            string str = "";
            VCardData objvcard = new VCardData();
            if (obj != null)
            {
                objvcard.Name = obj.BuyerName;
                objvcard.Email = obj.Email;
                string[] mobilearray = obj.MobileNo.Split(',');
                foreach (string mobile in mobilearray)
                {
                    string mob = mobile.Split(' ')[1].ToString();
                    objvcard.Mobileno = mob;
                    str += cm.VCardFile(objvcard);

                }             
            }

            return File(System.Text.Encoding.ASCII.GetBytes(str), "text/x-vcard");
        }

        public string GenerateCode(string Prefix, int? Total, int? Id)
        {
            string STR = "";
            if (Id != 0)
            {

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                if (Total == 0)
                    Total = 8;
                var L = Total - Id.Value.ToString().Length;
                if (L != 0)
                {
                    for (int i = 0; i < L; i++)
                    {
                        if (i == 0)
                        {
                            int X = 0;
                            sb.Append(Prefix + X);
                        }
                        else
                            sb.Append(0);
                    }
                }
                else
                {
                    sb.Append(Prefix);
                }
                STR = sb.ToString() + Id;
            }
            else
                STR = Prefix + "500001";
            return STR;

        }

        public JsonResult GetGridInquiryData()
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    List<InquiryMaster> objInquiryMaster = _IInquiry_Repository.GetInqGridInquiry().ToList();
                    List<InquiryFollowupModel> objInquiryFollowMaster = _IInquiry_Repository.GetInqGridFollowup(objInquiryMaster[0].AllInqId).ToList();
                    foreach (InquiryMaster item in objInquiryMaster)
                    {
                        item.lstInquiryFollowupModel = objInquiryFollowMaster.Where(Z => Z.InqId == item.InqId).ToList();
                        //item.lstInquiryFollowupModel= _IInquiry_Repository.GetInqGridFollowup(item.InqId.ToString()).ToList();

                    }
                    //
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", new
                    {
                        //objInquiryMaster = objInquiryMaster,
                        //objInquiryFollowMaster = objInquiryFollowMaster
                        objInquiryMaster = objInquiryMaster
                    });
                }
                catch (Exception ex)
                {
                    ex.SetLog("Get Inquiry by Id");
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