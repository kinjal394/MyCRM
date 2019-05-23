using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.DTOModel
{
    public class WorkProfileModle
    {
        public int WorkProfileId { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string WorkDay { get; set; }
        public Nullable<System.TimeSpan> WorkTime { get; set; }
        public DateTime WorkDate { get; set; }
        public string WorkCycle { get; set; }
        public bool IsActive { get; set; }
    }
}
