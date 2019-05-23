using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.Data
{
    public partial class BuyerBankDetail
    {
        public string BankName { get; set; }
        public string AccountType { get; set; }
        public int Status { get; set; }
    }
    public partial class BuyerChatDetail
    {
        public string ChatName { get; set; }
        public int Status { get; set; }
    }
    public partial class BuyerLicenseDetail
    {
        public string LicenseName { get; set; }
        public int Status { get; set; }
    }
    public partial class BuyerAddressDetail
    {
        public string CityName { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string AddressType { get; set; }
        public int Status { get; set; }
    }
    public class vmBuyerContactDetail
    {
        public int BuyerId { get; set; }
        public int ContactId { get; set; }
        public string ContactPerson { get; set; }
        public string Surname { get; set; }
        public Nullable<int> DesignationId { get; set; }
        public string DesignationName { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string Chat { get; set; }
        public string ChatDetails { get; set; }
        public string QQCode { get; set; }
        public string SkypeId { get; set; }
        public int Status { get; set; }
    }
    public class BuyerModel
    {
        public int BuyerId { get; set; }
        public string Address { get; set; }
        public int AreaId { get; set; }
        public string AreaName { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string CompanyName { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string PinCode { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public string Remark { get; set; }
        public string WebAddress { get; set; }
        public string WeeklyOff { get; set; }
        public string GST { get; set; }
        public string VAT { get; set; }
        public string CST { get; set; }
        public string PAN { get; set; }
        public string TAN { get; set; }
        public string Telephone { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public int DeleteBy { get; set; }
        public string ContactType { get; set; }
        public int AgencyTypeId { get; set; }
        public string Fax { get; set; }
        public string AgencyType { get; set; }
        public string DocumentsData { get; set; }
        public string ConInvId { get; set; }
        public string EmailAddress { get; set; }
        public List<vmBuyerContactDetail> BuyerContactDetails { get; set; }
        public List<BuyerBankDetail> BuyerBankDetails { get; set; }
        public List<BuyerChatDetail> BuyerChatDetails { get; set; }
        public List<BuyerLicenseDetail> BuyerLicenseDetails { get; set; }
        public List<BuyerAddressDetail> BuyerAddressDetails { get; set; }
    }
}
