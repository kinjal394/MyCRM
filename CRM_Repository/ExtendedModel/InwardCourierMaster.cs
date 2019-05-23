using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.Data
{
    [MetadataType(typeof(InwardCourierModel))]
    public partial class InwardCourierMaster
    {
        public int AreaId { get; set; }
        public string AreaName { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string Receiver { get; set; }
        public string Vendor { get; set; }
        public string Sender { get; set; }
        public string CourierType { get; set; }
    }
    public class InwardCourierModel
    {
    }
}
