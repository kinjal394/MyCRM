using CRM_Repository.ExtendedModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.Data
{
    [MetadataType(typeof(InquiryMasterModel))]
    public partial class InquiryMaster
    {
        // public int CityId { get; set; }
        public string AllInqId { get; set; }
        public List<InquiryFollowupModel> lstInquiryFollowupModel { get; set; }

    }
    public class InquiryMasterModel 
    {
    }

}
