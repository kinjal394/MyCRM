using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.Data
{
    public partial class SalesPurchaseDocumentMaster
    {
        public string DocName { get; set; }
        public int Status { get; set; }
    }
    public partial class SalesPurchaseEntryMaster
    {
        public string FinancialYear { get; set; }
        public List<SalesPurchaseDocumentMaster> SalesPurchaseDocMasters { get; set; }

    }
}
