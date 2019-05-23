using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.DTOModel
{
    public class PurchaseOrderDetailModel
    {
        public int PoDetailId { get; set; }
        public int PoId { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public int SubCategoryId { get; set; }
        public string SubCategory { get; set; }
        public int MainProductId { get; set; }
        public string MainProduct { get; set; }
        public int ProductId { get; set; }
        public string Product { get; set; }
        public string Description { get; set; }
        public int QtyCode { get; set; }
        public string QtyCodeData { get; set; }
        public decimal Qty { get; set; }
        public int PriceCode { get; set; }
        public string PriceCodeData { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Amount { get; set; }
        public string ModelNo { get; set; }
        public string ProductPhotoes { get; set; }
        public string MachinaryPhotoes { get; set; }
        public int Status { get; set; }
        public int PoDetailIndex { get; set; }
    }
    public class PurchaseOrderTechnicalDetailModel
    {
        public int POSpecId { get; set; }
        public int SpecId { get; set; }
        public string SpecVal { get; set; }
        public int PoDetailId { get; set; }
        public string SpecName { get; set; }
        public int Status { get; set; }
        public int PoDetailIndex { get; set; }
        public int SpecHeadId { get; set; }
        public string SpecHead { get; set; }
    }
    public class PurchaseOrderModel
    {
        public int PoId { get; set; }
        public string PoNo { get; set; }
        public string PoRefNo { get; set; }
        public DateTime PoDate { get; set; }
        public string Remark { get; set; }
        public Nullable<int> TermsConditionId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalTax { get; set; }
        public decimal PayableAmount { get; set; }
        public string ModeOfShipment { get; set; }
        public Nullable<int> PriceCode { get; set; }
        public int LandingPort { get; set; }
        public string DischargePortName { get; set; }
        public int SupplierId { get; set; }
        public string Supplier { get; set; }
        public string TermsCondition { get; set; }
        public Nullable<int> ModeOfShipmentId { get; set; }
        public string PriceCodeName { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public int DeleteBy { get; set; }
        public int AddressId { get; set; }
        public string Address { get; set; }
        public int ContactId { get; set; }
        public string ContactName { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Attn { get; set; }
        public int DischargePort { get; set; }
        public int DeliveryTermId { get; set; }
        public string LandingPortName { get; set; }
        public int ComId { get; set; }
        public string CompanyName { get; set; }
        public int BuyerId { get; set; }
        public string BuyerComName { get; set; }
        public string SupplierComName { get; set; }
        public string BuyerAddress { get; set; }
        public string BuyerTel { get; set; }
        public string BuyerWebsite { get; set; }
        public string BuyerEmail { get; set; }
        public string BuyerTax { get; set; }
        public string SupplierTax { get; set; }
        public int BuyerContactId { get; set; }
        public int SupplierContactId { get; set; }
        public string BuyerContactperson { get; set; }
        public string SupplierContactperson { get; set; }

        public List<PurchaseOrderDetailModel> PurchaseOrderDetails { get; set; }
        public List<PurchaseOrderTechnicalDetailModel> TechSpecParameterMasters { get; set; }

    }
}
