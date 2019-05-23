using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM
{
    public class CRMModel
    {
    }

    public enum MessageType
    {
        Suceess = 1,
        InvalidUser = 2,
        Error = 3,
        NoDataFound = 4,
        Exists = 5
    }

    public static class MessageValue
    {
        public const string Insert = "Record Inserted Successfully";
        public const string Update = "Record Updated Successfully";
        public const string Delete = "Record Deleted Successfully";
        public const string Param = "Please Enter Mendatory Parameter";
        public const string Error = "Something went wrong";
        public const string Exists = " Alerady Exists";
        public const string InvalidUser = "Invlaid User";
    }

    public class DataResponse
    {
        public MessageType ResponseType { get; set; }  //0:error, 1: success, 2: warning, 3: information 
        public string Message { get; set; }
        public object DataList { get; set; }
    }
}