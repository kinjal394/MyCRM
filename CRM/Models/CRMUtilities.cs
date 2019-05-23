using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace CRM.Models
{
    public static class CRMUtilities
    {
        public static DataResponse UnAuthorizedAccess(DataResponse dataResponse)
        {
            dataResponse.ResponseType = 0;
            dataResponse.Message = "UnAuthorized User";
            dataResponse.DataList = null;
            return dataResponse;
        }

        public static DataResponse GenerateApiResponse(MessageType ResponseType, string Message, object DataList)
        {
            DataResponse dataResponse = new DataResponse();
            dataResponse.ResponseType = ResponseType;
            dataResponse.Message = Message.ToString();
            dataResponse.DataList = DataList;
            return dataResponse;
        }
        public static void SetLog(this Exception ex, string msg, Boolean IsRedirect = false)
        {
            if (ex is System.Data.Entity.Validation.DbEntityValidationException)
            {
                foreach (var validationErrors in ((System.Data.Entity.Validation.DbEntityValidationException)ex).EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += ";" + string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                    }
                }
            }
            string FileName = DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year;
            if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/Logs")))
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Logs"));
            if (!File.Exists(HttpContext.Current.Server.MapPath("~/Logs/" + FileName + ".txt")))
            {
                using (StreamWriter sw = File.CreateText(HttpContext.Current.Server.MapPath("~/Logs/" + FileName + ".txt")))
                {
                    // StreamWriter sw = new StreamWriter(HttpContext.Current.Server.MapPath("~/Logs/" + FileName + ".txt"), true);
                    sw.WriteLine("Error on " + DateTime.Now + " ,Exception Message:" + ex.Message + ",Inner Message:" + ex.InnerException + ",Line:" + ex.StackTrace + ",Additional Msg:" + msg);
                    sw.Flush();
                    sw.Close();
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(HttpContext.Current.Server.MapPath("~/Logs/" + FileName + ".txt")))
                {
                    sw.WriteLine("Error on " + DateTime.Now + " ,Exception Message:" + ex.Message + ",Inner Message:" + ex.InnerException + ",Line:" + ex.StackTrace + ",Additional Msg: " + msg);
                    sw.Flush();
                    sw.Close();
                }
            }
            if (IsRedirect)
                throw ex;
        }
    }
}