using CRM_Repository.Data;
using CRM_Repository.DataServices;
using CRM_Repository.DTOModel;
using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace CRM_Repository.Service
{
    public class RolePermission_Repository : IRolePermission_Repository
    {
        private CRM_Repository.Data.elaunch_crmEntities context;
        public RolePermission_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public IList<RolePermissionModel> GetAllRole()
        {
            try
            {
                //SqlParameter[] para = new SqlParameter[1];
                //para[0] = new SqlParameter().CreateParameter("@GroupId", groupId);
                return new dalc().selectbyquerydt("SELECT menu.MenuId,submenu.SubMenuId, (menu.MenuTitle +' -> '+SubMenuTitle) AS Menu,SubMenuUrl,menu.MenuCode,SubMenuCode FROM[gurjari_crm].[gurjari_crmuser].[MenuMaster] menu INNER JOIN[SubMenuMaster] submenu ON menu.MenuId = submenu.MenuId").ConvertToList<RolePermissionModel>().ToList();

            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable GetPermissionRecord()
        {
            try
            {
                //SqlParameter[] para = new SqlParameter[1];
                //para[0] = new SqlParameter().CreateParameter("@GroupId", groupId);
                return new dalc().selectbyquerydt(@"Select po.*,sb.SubMenuCode,sb.SubMenuTitle,sb.SubMenuUrl,mm.MenuCode,mm.MenuTitle 
                                                    from PageOperationMaster po
                                                    inner join SubMenuMaster sb on sb.submenuid=po.submenuid
                                                    inner join MenuMaster mm on mm.menuid=sb.menuid");

            }
            catch (Exception)
            {
                throw;
            }
        }
        public IList<PageOperationMaster> GetRolePermission(int GroupId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@GroupId", GroupId);
                return new dalc().GetDataTable_Text("SELECT * FROM gurjari_crmuser.PageOperationMaster WHERE GroupId=@GroupId", para).ConvertToList<PageOperationMaster>().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SavePermission(List<RolePermissionModel> model, int groupId)
        {
           
            var data = context.PageOperationMasters.Where(g => g.GroupId == groupId).ToList();
            context.PageOperationMasters.RemoveRange(data);
            foreach (var a in model)
            {
                PageOperationMaster model1 = new PageOperationMaster();
                model1.MenuId = a.MenuId;
                model1.IsView = a.IsView;
                model1.IsAdd = a.IsAdd;
                model1.IsEdit = a.IsEdit;
                model1.IsDelete = a.IsDelete;
                model1.GroupId = groupId;
                context.PageOperationMasters.Add(model1);
            }
            context.SaveChanges();
        }
    }
}
