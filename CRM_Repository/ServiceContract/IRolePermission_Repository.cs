using CRM_Repository.Data;
using CRM_Repository.DTOModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
    public interface IRolePermission_Repository
    {
        IList<RolePermissionModel> GetAllRole();
        IList<PageOperationMaster> GetRolePermission(int GroupId);
        void SavePermission(List<RolePermissionModel> model, int groupId);
        DataTable GetPermissionRecord();
    }
}
