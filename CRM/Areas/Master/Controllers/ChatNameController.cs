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

namespace CRM.Areas.Master.Controllers
{
    [HasLoginSessionFilter]
    public class ChatNameController : Controller
    {
        private IChatName_Repository _IChatName_Repository;
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ChatNamePopup()
        {
            return View();
        }
        public ChatNameController()
        {
            this._IChatName_Repository = new ChatName_Repository(new elaunch_crmEntities());
        }

        [HttpPost]
        public JsonResult SaveChatName(ChatNameMaster objdeChatname)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    ChatNameMaster ObjCha = new ChatNameMaster();
                    ObjCha.ChatId = objdeChatname.ChatId;
                    ObjCha.ChatName = objdeChatname.ChatName;
                    ObjCha.IsActive = true;
                    if (objdeChatname.ChatId > 0)
                    {
                        var plist = _IChatName_Repository.DuplicateEditChatName(ObjCha.ChatId, ObjCha.ChatName).ToList();
                        if (plist.Count == 0)
                        {
                            _IChatName_Repository.UpdateChatName(ObjCha);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully", null);

                        }
                        else
                        {
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "ChatName Already Exists", null);

                        }
                        //_ISource_Repository.UpdateSource(source);
                        //msg = "Success";
                    }
                    else
                    {
                        var clist = _IChatName_Repository.DuplicateChatName(ObjCha.ChatName).ToList();
                        if (clist.Count == 0)
                        {
                            _IChatName_Repository.AddChatName(ObjCha);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully", null);

                        }
                        else
                        {
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "ChatName Already Exists", null);

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
                ex.SetLog("Create/Update ChatName");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult DeleteChatName(string ChatId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    if (ChatId != "")
                    {
                        int cid = Convert.ToInt32(ChatId);
                        ChatNameMaster dmaster = new ChatNameMaster();
                        dmaster = _IChatName_Repository.GetChatNameById(cid);
                        dmaster.IsActive = false;
                        //smaster.SourceId = cid;
                        _IChatName_Repository.UpdateChatName(dmaster);
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
                ex.SetLog("Delete ChatName");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetChartNameById(int ChatId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    var objdepart = _IChatName_Repository.GetchatNameById(ChatId);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, objdepart);
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "User is not valid", null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Get ChatName by Id");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult ChatNameBind()
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var data = _IChatName_Repository.getAllChatName().ToList();
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get All ChatName");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                throw ex;
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            _IChatName_Repository.Dispose();
            base.Dispose(disposing);
        }

    }
}