using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.DTOModel
{
    public class PermissionDTO
    {
        public string SubMenuCode { get; set; }
        public string OpCode { get; set; }
    }

    public class MenuDTO
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuCode { get; set; }
        public string URL { get; set; }
        public int ParentMenuId { get; set; }
    }
}
