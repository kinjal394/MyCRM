using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CRM_Repository.ServiceContract
{
    public interface IRole_Repository : IDisposable
    {
        void AddRole(RoleMaster Role);
        void UpdateRole(RoleMaster Role);
        RoleMaster GetRoleById(int id);
        IQueryable<RoleMaster> GetAllRole();
        bool CheckRoleType(RoleMaster obj, bool isUpdate);
    }
}
