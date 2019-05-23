using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;
using CRM_Repository.DTOModel;

namespace CRM_Repository.ServiceContract
{
    public interface IPermission_Repository : IDisposable
    {
        List<MenuDTO> GetPages();
        List<PermissionDTO> GetPermissions(int UserId);

    }
}
