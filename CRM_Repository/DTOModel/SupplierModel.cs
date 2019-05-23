using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.Data
{
    public partial class SupplierBankMaster
    {
        public string BankName { get; set; }
        public string AccountType { get; set; }
        public int Status { get; set; }
    }

    public partial class SupplierAddressMaster
    {
        public string CityName { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int Status { get; set; }
    }
    public partial class SupplierChatMaster
    {
        public string ChatName { get; set; }
        public int Status { get; set; }
    }
    public class vmSupplierContactDetail
    {
        public int ContactId { get; set; }
        public int SupplierId { get; set; }
        public string ContactName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string QQCode { get; set; }
        public string Skype { get; set; }
        public int Status { get; set; }
    }

    public class SupplierDetailByIdModel
    {
        public int SupplierId { get; set; }
        public string Website { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }

    }



    public class SupplierModel
    {
        public int SupplierId { get; set; }
        public string CompanyName { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public int AreaId { get; set; }
        public string AreaName { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string PinCode { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public int DeleteBy { get; set; }
        public List<vmSupplierContactDetail> SupplierContactDetails { get; set; }
        public List<SupplierBankMaster> SupplierBankDetails { get; set; }
        public List<SupplierAddressMaster> SupplierAddressDetails { get; set; }

        public List<SupplierChatMaster> SupplierChatDetails { get; set; }
    }
}
