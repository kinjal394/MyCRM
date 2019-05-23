using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.DTOModel
{
    public class InquiryItemModel
    {
        public int InqDetailId { get; set; }
        public int InqId { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public int SubCategoryId { get; set; }
        public string SubCategory { get; set; }
        public int MainProductId { get; set; }
        public string MainProductName { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int QtyCode { get; set; }
        public string QtyCodeData { get; set; }
        public decimal Qty { get; set; }
        public int Status { get; set; }
    }
    public class InquiryModel
    {
        public int InqId { get; set; }
        public int BuyerType { get; set; }
        public int SubjectType { get; set; }
        public string CompanyName { get; set; }
        public string InqNo { get; set; }
        public DateTime InqDate { get; set; }
        public int SourceId { get; set; }
        public string SourceName { get; set; }
        //public int ContactId { get; set; }
        public string ContactPersonname { get; set; }
        public string BuyerName { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public string Requirement { get; set; }
        public string Address { get; set; }
        public string Remark { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public string Subject { get; set; }
        public int DeletedBy { get; set; }
        public Nullable<int> AssignTo { get; set; }
        //public List<InquiryItemModel> InquiryItemDetails { get; set; }
    }
}
