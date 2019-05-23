using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.Data
{
    [MetadataType(typeof(ProductPricesModel))]
    public partial class ProductPrice
    {
        public string CurrencyName { get; set; }
        public Nullable<int> Status { get; set; }
    }
   public class ProductPricesModel
    {
    }
}
