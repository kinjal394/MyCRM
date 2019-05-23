using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CRM_Repository.DTOModel
{
    public class ReportPara
    {
        public string ReportName { get { return string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["ReportName"]) ? "" : Convert.ToString(HttpContext.Current.Request.QueryString["ReportName"]); } set { value = string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["ReportName"]) ? "" : Convert.ToString(HttpContext.Current.Request.QueryString["ReportName"]); } }
        public string ID { get { return string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["ID"]) ? "" : Convert.ToString(HttpContext.Current.Request.QueryString["ID"]); } set { value = string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["ID"]) ? "" : Convert.ToString(HttpContext.Current.Request.QueryString["ID"]); } }
        public string A { get { return string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["A"]) ? "" : Convert.ToString(HttpContext.Current.Request.QueryString["A"]); } set { value = string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["A"]) ? "" : Convert.ToString(HttpContext.Current.Request.QueryString["A"]); } }
        public string B { get { return string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["B"]) ? "" : Convert.ToString(HttpContext.Current.Request.QueryString["B"]); } set { value = string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["B"]) ? "" : Convert.ToString(HttpContext.Current.Request.QueryString["B"]); } }
        public string C { get { return string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["C"]) ? "" : Convert.ToString(HttpContext.Current.Request.QueryString["C"]); } set { value = string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["C"]) ? "" : Convert.ToString(HttpContext.Current.Request.QueryString["C"]); } }
        public string D { get { return string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["D"]) ? "" : Convert.ToString(HttpContext.Current.Request.QueryString["D"]); } set { value = string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["D"]) ? "" : Convert.ToString(HttpContext.Current.Request.QueryString["D"]); } }
        public string E { get { return string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["E"]) ? "" : Convert.ToString(HttpContext.Current.Request.QueryString["E"]); } set { value = string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["E"]) ? "" : Convert.ToString(HttpContext.Current.Request.QueryString["E"]); } }
        public string F { get { return string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["F"]) ? "" : Convert.ToString(HttpContext.Current.Request.QueryString["F"]); } set { value = string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["F"]) ? "" : Convert.ToString(HttpContext.Current.Request.QueryString["F"]); } }
        public int UserId { get { return HttpContext.Current.Session["UserId"] != null ? Convert.ToInt32(HttpContext.Current.Session["UserId"]) : 0; } set { value = HttpContext.Current.Session["UserId"] != null ? Convert.ToInt32(HttpContext.Current.Session["UserId"]) : 0; } }
        public int UserType { get { return HttpContext.Current.Session["UserId"] != null ? Convert.ToInt32(HttpContext.Current.Session["UserRollType"]) : 0; } set { value = HttpContext.Current.Session["UserId"] != null ? Convert.ToInt32(HttpContext.Current.Session["UserRollType"]) : 0; } }

    }
}
