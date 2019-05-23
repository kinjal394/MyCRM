using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.Data
{
    public partial class PerformaInvoiceMaster
    {
        public string RptCompany { get; set; }
        public string DeliveryTerm { get; set; }
        public string PaymentTerm { get; set; }
        public string ModeOfShipment { get; set; }
        public string Contact { get; set; }
        public string LoadingPort { get; set; }
        public string DischargePort { get; set; }
        public string BankName { get; set; }
        public string AccountType { get; set; }
        public int Status { get; set; }
    }
    public partial class PerformaProductMaster
    {
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public int SubCategoryId { get; set; }
        public string SubCategory { get; set; }
        public string CountryOfOrigin { get; set; }
        public string QtyCodeValue { get; set; }
        public string CurrencyCodeValue { get; set; }
        public int Status { get; set; }
        public decimal FinalPrice { get; set; }
    }

    public partial class PerformaPaymentMaster
    {
        public string PaymentMode { get; set; }
        public string TranType { get; set; }
    }

}
