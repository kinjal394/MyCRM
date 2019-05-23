using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.DTOModel
{
    public class RolePermissionModel
    {
        public int MenuId { get; set; }
        public string Menu { get; set; }
        public string SubMenuUrl { get; set; }
        public string MenuCode { get; set; }
        public string SubMenuCode { get; set; }
        public bool IsView { get; set; }
        public bool IsEdit { get; set; }
        public bool IsAdd { get; set; }
        public bool IsDelete { get; set; }
    }
}
