using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.DTOModel
{
    public class vmSalesTechnicalDetail
    {
        public int TechDetailId { get; set; }
        public int ItemId { get; set; }
        public int TechParaId { get; set; }
        public string Value { get; set; }
        public int Status { get; set; }
    }
    public class vmSalesItemDetail
    {
        public int SOId { get; set; }
        public int ItemId { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public int SubCategoryId { get; set; }
        public string SubCategory { get; set; }
        public int MainProductId { get; set; }
        public string MainProductName { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ModelNo { get; set; }
        public int OriginId { get; set; }
        public string CountryOfOrigin { get; set; }
        public int QtyCode { get; set; }
        public string QtyCodeData { get; set; }
        public decimal Qty { get; set; }
        public int UnitPriceCode { get; set; }
        public string UnitPriceCodeData { get; set; }
        public decimal UnitPrice { get; set; }
        public int Status { get; set; }
        public List<vmSalesTechnicalDetail> SalesTechnicalDetails { get; set; }
    }
    public class SalesOrderModel
    {
        public int SOId { get; set; }
        public DateTime SoDate { get; set; }
        public string SoRefNo { get; set; }
        public string SoNo { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string TelNo1 { get; set; }
        public string Address { get; set; }
        public string VAT { get; set; }
        public int BuyerId { get; set; }
        public int BuyerContactId { get; set; }
        public string ContactPerson { get; set; }
        public string BEmail { get; set; }
        public int MobileCode { get; set; }
        public string MobileNo { get; set; }
        public string Remark { get; set; }
        public decimal TotalAmount { get; set; }
        public int TotalAmountCode { get; set; }
        public int DeliveryTermId { get; set; }
        public string DeliveryName { get; set; }
        public int PaymentTermId { get; set; }
        public string TermName { get; set; }
        public int CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public string RegOffAdd { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public int DeleteBy { get; set; }
        public List<vmSalesItemDetail> SalesItemDetails { get; set; }
    }
}
