using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using CRM_Repository.Data;

namespace CRM_Repository.Data
{
    [MetadataType(typeof(QuotationItemModel))]
    public partial class QuotationItemMaster
    {
        public int Status { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int MainProductId { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string MainProductName { get; set; }
        public string ProductName { get; set; }
        public string QtyCodeName { get; set; }
        public string OfferPriceName { get; set; }
        public string SupplierModelName { get; set; }
        public decimal ExRatePrice { get; set; }
        public string DealerPriseName { get; set; }
    }
    public partial class QuotationMaster
    {
        public string CompanyName { get; set; }
        public string DeliveryName { get; set; }
        public string TermName { get; set; }
        public string UserName { get; set; }
        public string BuyerName { get; set; }
        public string Inqno { get; set; }
        public string IMobileNo { get; set; }
        public string IEmail { get; set; }
        public string CurrSymbol { get; set; }
        public string ExCurrSymbol { get; set; }
    }
    public class QuotationItemModel
    {
    }
}
