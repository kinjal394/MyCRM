using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.Data
{
    public partial class VendorBankMaster
    {
        public string BankName { get; set; }
        public string AccountType { get; set; }
        public int Status { get; set; }
    }
    public class vmVendorContactDetail
    {
        public int VendorId { get; set; }
        public int ContactId { get; set; }
        public string ContactName { get; set; }
        public string Surname { get; set; }
        public int DesignationId { get; set; }
        public string DesignationName { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string Skype { get; set; }
        public string QQ { get; set; }
        public int Status { get; set; }
    }
    public partial class VendorChatMaster
    {
        public string ChatName { get; set; }
        public int Status { get; set; }
    }
    public partial class VendorAddressDetail
    {
        public string CityName { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int Status { get; set; }
    }
    public class VendorModel
    {
        public int VendorId { get; set; }
        public int AgencyTypeId { get; set; }
        public string AgencyType { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public int AreaId { get; set; }
        public string AreaName { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string Fax { get; set; }
        public string Website { get; set; }
        public string Remark { get; set; }
        public string VAT { get; set; }
        public string GST { get; set; }
        public string PAN { get; set; }
        public string TAN { get; set; }
        public string ServiceTaxNo { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifyBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public List<vmVendorContactDetail> VendorContactDetails { get; set; }
        public List<VendorBankMaster> VendorBankDetails { get; set; }
        public List<VendorAddressDetail> VendorAddressDetails { get; set; }

        public List<VendorChatMaster> VendorChatDetails { get; set; }
    }
}
