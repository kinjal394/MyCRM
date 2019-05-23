using System.Web.Mvc;

namespace CRM.Areas.Employee
{
    public class EmployeeAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Employee";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Employee_default",
                "Employee/{controller}/{action}/{id}/{temp}",
                new { action = "Index", id = UrlParameter.Optional, temp = UrlParameter.Optional }
            );
        }
    }
}