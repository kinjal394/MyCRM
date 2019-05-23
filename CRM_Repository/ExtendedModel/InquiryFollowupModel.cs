using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ExtendedModel
{
    public class InquiryFollowupModel
    {
        public int FollowupId { get; set; }
        public int InqId { get; set; }
        public string CurrentUpdate { get; set; }
        public System.TimeSpan NextFollowTime { get; set; }
        public System.DateTime NextFollowDate { get; set; }
        public string StatusName { get; set; }
        public int Status { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> Modifydate { get; set; }
        public Nullable<int> ModifyBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public bool IsActive { get; set; }
        public int AssignId { get; set; }
        public int Id { get; set; }
        public string title { get; set; }
        public string typeOfEventTitle { get; set; }
        public System.DateTime Date { get; set; }
        public System.TimeSpan Time { get; set; }

    }
}
