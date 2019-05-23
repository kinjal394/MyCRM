using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.Data
{
    [MetadataType(typeof(ProductApplicableChargeModel))]
    public partial class ProductApplicableCharge
    {
        public int ProductId { get; set; }
        public int SupplierId { get; set; }
        public Nullable<int> Status { get; set; }
    }
    public class ProductApplicableChargeModel
    {
        
    }
}
