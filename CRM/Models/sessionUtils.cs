using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM
{
    public class sessionUtils
    {
        public static Boolean HasUserLogin()
        {
            if (HttpContext.Current.Session["UserId"] != null)
                return true;
            else
                return false;
        }
        public static int UserId { get { return Convert.ToString(HttpContext.Current.Session["UserId"]).GetProperInt(); } }
        public static int UserRollType { get { return Convert.ToString(HttpContext.Current.Session["UserRollType"]).GetProperInt(); } }
        public static int UserDeptId { get { return Convert.ToString(HttpContext.Current.Session["UserDeptId"]).GetProperInt(); } }
    }
}