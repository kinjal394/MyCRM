using CRM_Repository.Data;
using CRM_Repository.DTOModel;
using CRM_Repository.Service;
using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace CRM.Areas.Employee.Controllers
{
    public class RolePermissionController : Controller
    {
        private IRolePermission_Repository _IRolePermission_Repository;
        private IRole_Repository _IRole_Repository;
        public RolePermissionController()
        {
            this._IRolePermission_Repository = new RolePermission_Repository(new elaunch_crmEntities());
            this._IRole_Repository = new Role_Repository(new elaunch_crmEntities());
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ChangeRole(int groupId)
        {
            List<RolePermissionModel> data = _IRolePermission_Repository.GetAllRole().ToList();
            var pageOperation = _IRolePermission_Repository.GetRolePermission(groupId);

            foreach (var menu in data)
            {
                if (pageOperation.Count > 0)
                {
                    foreach (var perm in pageOperation)
                    {
                        if (perm.MenuId == menu.MenuId)
                        {
                            menu.IsAdd = perm.IsAdd.Value;
                            menu.IsEdit = perm.IsEdit.Value;
                            menu.IsView = perm.IsView.Value;
                            menu.IsDelete = perm.IsDelete.Value;
                            break;
                        }
                        else
                        {
                            menu.IsAdd = false;
                            menu.IsEdit = false;
                            menu.IsView = false;
                            menu.IsDelete = false;
                        }
                    }
                }
                else
                {
                    menu.IsAdd = false;
                    menu.IsEdit = false;
                    menu.IsView = false;
                    menu.IsDelete = false;
                }
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllRole()
        {
            var data = _IRole_Repository.GetAllRole();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public void SavePermission(string rolePermissionModel, int groupId)
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            var myNames = ser.Deserialize<List<RolePermissionModel>>(rolePermissionModel);
            _IRolePermission_Repository.SavePermission(myNames, groupId);

        }



    }
}