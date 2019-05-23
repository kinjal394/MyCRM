using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models.Grid
{
    public class UserMain
    {
        public string Email { get; set; }
        public Guid Identifier { get; set; }
        public string MobileNo { get; set; }
        public string ConfirmationCode { get; set; }
        public int UserId { get; set; }
        public string BrokerEmail { get; set; }
        public Int64 RowNumber { get; set; }
    }

    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class ApplicableChargeMaster
    {
        public int ApplicableChargeId { get; set; }
        public string ApplicableChargeName { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class SubCategory
    {
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class WorkReminderMaster
    {
        public int WorkRemindId { get; set; }
        public string Title { get; set; }
        public int DepartmentId { get; set; }
        public string Department { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public DateTime RemindDate { get; set; }
        public TimeSpan RemindTime { get; set; }
        public string RemindMode { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class AskcustomerDetails
    {
        public int AskCustId { get; set; }
        public int SourceId { get; set; }
        public string SourceName { get; set; }
        public DateTime Date { get; set; }
        public DateTime Createddate { get; set; }
        public TimeSpan Createdtime { get; set; }
        public string Name { get; set; }
        public string Mobileno { get; set; }
        public string Email { get; set; }
        public string Requirement { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class AccountTypee
    {
        public int AccountTypeId { get; set; }
        public string AccountType { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class SalesDocumentNameMaster
    {
        public int SalesDocId { get; set; }
        public string SalesDocument { get; set; }
        public string IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class EmployeeShitf
    {
        public int ShiftId { get; set; }
        public System.TimeSpan InTime { get; set; }
        public System.TimeSpan OutTime { get; set; }
        public decimal Hours { get; set; }
        public bool IsActive { get; set; }
        public System.TimeSpan LateEntryCalculate { get; set; }
        public string ShiftName { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class Country
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string CountryFlag { get; set; }
        public string CountryAlphaCode { get; set; }
        public string CountryCallCode { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class PaymentModeMaster
    {
        public int PaymentModeId { get; set; }
        public string PaymentMode { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }

    public class State
    {
        public int StateId { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string StateName { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }

    public class Expense
    {
        public int ExId { get; set; }
        public int ExTypeId { get; set; }
        public string ExTypeName { get; set; }
        public string ContactPerson { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }

    public class CountryOrigin
    {
        public int OriginId { get; set; }
        public string CountryOfOrigin { get; set; }
        public bool IsActive { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public Int64 RowNumber { get; set; }
    }

    public class City
    {
        public int CityId { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public string CountryName { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }

    public class ShipmentMaster
    {
        public int ShipmentId { get; set; }
        public string ModeOfShipment { get; set; }
        public Int64 RowNumber { get; set; }
    }

    public class Designation
    {
        public int DesignationId { get; set; }
        public string DesignationName { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }

    public class PortMaster
    {
        public int PortId { get; set; }
        public string PortName { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class SourceMaster
    {
        public int SourceId { get; set; }
        public string SourceName { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class TermsAndConditionMaster
    {
        public int TermsId { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public Int64 RowNumber { get; set; }
    }

    public class BuyerMaster //Change Class
    {
        public int BuyerId { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public int AgencyTypeId { get; set; }
        public string AgencyType { get; set; }
        public int CityId { get; set; }
        public string Country { get; set; }
        public string StateName { get; set; }
        public string Addres { get; set; }
        public string CityName { get; set; }
        public string PinCode { get; set; }
        public string WebAddress { get; set; }
        public string WeeklyOff { get; set; }
        public string ContactType { get; set; }
        public string Fax { get; set; }
        public string ContectDetails { get; set; }
        public string DocumentsData { get; set; }
        public int CreatedBy { get; set; }
        public string Remark { get; set; }
        public string Person { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string ContactEmail { get; set; }
        public string ChatDetails { get; set; }
        public bool IsActive { get; set; }
        public string MobileNo { get; set; }
        public string ContactPersonDetail { get; set; }
        public Int64 RowNumber { get; set; }
    }


    public class Bank //Change Class
    {
        public int BankId { get; set; }
        public string BeneficiaryName { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string AccountNo { get; set; }
        public string IFSCCode { get; set; }
        public string CRNNo { get; set; }
        public string NickName { get; set; }
        public string SwiftCode { get; set; }
        public string AcNickName { get; set; }
        public int AccountTypeId { get; set; }
        public string RegisterEmail { get; set; }
        public string RegisterMobile { get; set; }
        public string StatementPassword { get; set; }
        public string BankCustomerCareNo { get; set; }
        public string BankCustomerCareEmail { get; set; }
        public int BankNameId { get; set; }
        public string Note { get; set; }
        public string MICRCode { get; set; }
        public Int64 RowNumber { get; set; }
    }

    public class PaymentTerms
    {
        public int PaymentTermId { get; set; }
        public string TermName { get; set; }
        public decimal Terms { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }

    public class DeliveryTerms
    {
        public int TermsId { get; set; }
        public string DeliveryName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class Unit
    {
        public int UnitId { get; set; }
        public string UnitName { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class ExhibitionMaster
    {
        public int ExId { get; set; }
        public string ExName { get; set; }
        public int CityId { get; set; }
        public int StateId { get; set; }
        public int AreaId { get; set; }
        public string AreaName { get; set; }
        public int CountryId { get; set; }
        public string CityName { get; set; }
        public string Venue { get; set; }
        public string NoofYears { get; set; }
        public string ExProfile { get; set; }
        public string OrganizerDetail { get; set; }
        public string BankDetail { get; set; }
        public string Address { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime ExDate { get; set; }
        public string StateName { get; set; }
        public string CountryName { get; set; }
        public string Tel { get; set; }
        public string MobileNo { get; set; }
        public string TelId { get; set; }
        public string MobileId { get; set; }
        public string Email { get; set; }
        public string Web { get; set; }
        public string ContactPerson { get; set; }
        public string Chat { get; set; }
        public string Month { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class Event
    {
        public int EventId { get; set; }
        public int EventTypeId { get; set; }
        public string EventName { get; set; }
        public string EventTypeName { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class EventType
    {
        public int EventTypeId { get; set; }
        public string EventTypeName { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class FinancialYearMaster
    {
        public int FinancialYearId { get; set; }
        public string FinancialYear { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }

    public class AgencyTypeMaster
    {
        public int AgencyTypeId { get; set; }
        public string AgencyType { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class Phasemaster
    {
        public int PhaseId { get; set; }
        public string Phase { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class FrequencyMaster
    {
        public int FrequencyId { get; set; }
        public string Frequency { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class VoltageMaster
    {
        public int VoltageId { get; set; }
        public string Voltage { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class ReportFormatMaster
    {
        public int RotFormatId { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyHeader { get; set; }
        public string CompanyFooter { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class ContactInvitationMaster
    {
        public int ContactInvitationId { get; set; }
        public string ContactInvitation { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class ProductDocumentMaster
    {
        public int PrdDocId { get; set; }
        public string PrdDocName { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class AcHolderMaster
    {
        public int AcHolderCode { get; set; }
        public string AcHolderName { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }

    public class SalaryHeadMaster
    {
        public int SalaryHeadId { get; set; }
        public string SalaryHeadName { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class ITRMaster
    {
        public int ITRId { get; set; }
        public string ITRName { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }

    public class Currency
    {
        public int CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public bool IsActive { get; set; }
        public string CurrencySymbol { get; set; }
        public string CurrencyCode { get; set; }
        public Int64 RowNumber { get; set; }
    }

    public class AreaMaster
    {
        public int AreaId { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string AreaName { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public Int64 RowNumber { get; set; }
    }


    public class Specification
    {
        public int SpecificationId { get; set; }
        public string TechSpec { get; set; }
        public string TechHead { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }

    public class VendorMaster
    {
        public int VendorId { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public int AreaId { get; set; }
        public string AreaName { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string PinCode { get; set; }
        public string WebAddress { get; set; }
        public string WeeklyOff { get; set; }
        public string VAT { get; set; }
        public string CST { get; set; }
        public string PAN { get; set; }
        public string TAN { get; set; }
        public string Remark { get; set; }
        public string Person { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class Company
    {
        public int ComId { get; set; }
        public string ComCode { get; set; }
        public string ComName { get; set; }
        public string RegOffAdd { get; set; }
        public string CorpOffAdd { get; set; }
        public string TelNos { get; set; }
        public string Email { get; set; }
        public string Web { get; set; }
        public string ComLogo { get; set; }
        public string TaxDetails { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }

    public class DepartmentMaster
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public Int64 RowNumber { get; set; }
    }

    public class RNDProduct
    {
        public int RNDProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Keyword { get; set; }
        public int EmailSpeechId { get; set; }
        public string EmailSpeech { get; set; }
        public int SMSSpeechId { get; set; }
        public string SMSSpeech { get; set; }
        public string RMPhotos { get; set; }
        public string MPPhotos { get; set; }
        public string FMPhotos { get; set; }
        public string Videoes { get; set; }
        public string ChatSpeech { get; set; }
        public bool IsActive { get; set; }
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int CreatedBy { get; set; }
        public string ModifyDate { get; set; }

        public Int64 RowNumber { get; set; }
    }
    public class Product
    {
        public int ProductId { get; set; }
        public int MainProductId { get; set; }
        public string MainProductName { get; set; }
        public string ProductName { get; set; }
        public string HSCode { get; set; }
        public string ProductCode { get; set; }
        public string Functionality { get; set; }
        public string FbLink { get; set; }
        public string Dimension { get; set; }
        public string GPlusLink { get; set; }
        public decimal Price { get; set; }
        public decimal DealerPrice { get; set; }
        public string OursModelNo { get; set; }
        public string ModelNo { get; set; }
        public string Height { get; set; }
        public string CBM { get; set; }
        public string Width { get; set; }
        public string Length { get; set; }
        public string GrossWeight { get; set; }
        public string NetWeight { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; }
        public bool IsActive { get; set; }
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class ProductSupllier
    {
        public int CatalogId { get; set; }
        public string CompanyName { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string SupplierModelNo { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class MainProductMaster
    {
        public int MainProductId { get; set; }
        public string MainProductName { get; set; }
        public bool IsActive { get; set; }
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public Int64 RowNumber { get; set; }
    }

    public class Supplier
    {
        public int SupplierId { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public int AreaId { get; set; }
        public string AreaName { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string PinCode { get; set; }
        public string WebAddress { get; set; }
        public string WeeklyOff { get; set; }
        public string VAT { get; set; }
        public string CST { get; set; }
        public string PAN { get; set; }
        public string TAN { get; set; }
        public string Remark { get; set; }
        public string Person { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
        public string CountryAlphaCode { get; set; }
        public string Fax { get; set; }
        public string Website { get; set; }
        public string CountryName { get; set; }
        public string StateName { get; set; }
    }
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class UserMaster
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Employee_Name { get; set; }
        public string FatherName { get; set; }
        public string Email { get; set; }
        public string MobNo { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string ChatDetail { get; set; }
        public string Skype { get; set; }
        public bool IsActive { get; set; }
        public string Status { get; set; }
        public Int64 RowNumber { get; set; }

        public string SourceName { get; set; }
        public string BloodGroup { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public Nullable<int> BirthPlaceCityId { get; set; }
        public string BirthPlaceArea { get; set; }
        public string BirthPlacePincode { get; set; }
        public Nullable<int> HomeTownCityId { get; set; }
        public string HomeTownArea { get; set; }
        public string HomeTownPincode { get; set; }
        public Nullable<int> BloodGroupId { get; set; }
        public string ResidentNo { get; set; }
        public string PresentArea { get; set; }
        public Nullable<int> PresentCityId { get; set; }
        public string PresentPinCode { get; set; }
        public string PresentAddress { get; set; }
        public string PermanentArea { get; set; }
        public Nullable<int> PermanentCityId { get; set; }
        public string PermanentPinCode { get; set; }
        public string PermanentAddress { get; set; }
        public string DrivingLicNo { get; set; }
        public string VoterIdNumber { get; set; }
        public string PassportNo { get; set; }
        public string PANNo { get; set; }
        public string AadharNo { get; set; }
        public string FullNameAsPerBank { get; set; }
        public string BranchName { get; set; }
        public string AccountNo { get; set; }
        public string AccountType { get; set; }
        public string IFSC { get; set; }
        public string ReportingName { get; set; }
        public string Password { get; set; }
        public string AgencyTypeReferanceName { get; set; }
        public Nullable<int> ReferenceSubType { get; set; }
        public string ReferanceName { get; set; }
        public string ReferenceMannualEntry { get; set; }
        public Nullable<System.DateTime> JoiningDate { get; set; }
        public string DesignationName { get; set; }
        public string RoleName { get; set; }
        public Nullable<int> PresentPropType { get; set; }
        public Nullable<int> PermanentPropType { get; set; }
        public string MICRCode { get; set; }
        public string Photo { get; set; }
        public Nullable<System.TimeSpan> ShiftStartTime { get; set; }
        public Nullable<System.TimeSpan> ShiftEndTime { get; set; }
        public Nullable<System.TimeSpan> LunchStartTime { get; set; }
        public Nullable<System.TimeSpan> LunchEndTime { get; set; }
        public Nullable<System.DateTime> CommunicationDate { get; set; }
        //public Nullable<int> QualificationId { get; set; }
        //public string QualificationName { get; set; }
        //public string TotalWorkExperience { get; set; }
        public string ContactMobile { get; set; }
        public string ContactEmail { get; set; }
        public string ContactChat { get; set; }
        public Nullable<int> SourceId { get; set; }
        public string BankName { get; set; }
    }

    //Transaction
    public class SalesOrderMaster
    {
        public int SOId { get; set; }
        public string SoNo { get; set; }
        public string CompanyName { get; set; }
        public string ContactPerson { get; set; }
        public string DeliveryName { get; set; }
        public string TermName { get; set; }
        public bool IsActive { get; set; }
        public DateTime SoDate { get; set; }
        public int BuyerId { get; set; }
        public int BuyerContactId { get; set; }
        public string Remark { get; set; }
        public decimal TotalAmount { get; set; }
        public int TotalAmountCode { get; set; }
        public int DeliveryTermId { get; set; }
        public int PaymentTermId { get; set; }
        public string SoRefNo { get; set; }
        public string ComName { get; set; }
        public Int64 RowNumber { get; set; }
    }

    public class TypeOfShipmentMaster
    {
        public int ShipmentTypeId { get; set; }
        public string ShipmentType { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }

    }
    public class InvoiceTypeMaster
    {
        public int InvoiceTypeId { get; set; }
        public string InvoiceTypeName { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }

    }
    public class Document
    {
        public int DocId { get; set; }
        public string DocName { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }

    public class Inquiry
    {
        public int InqId { get; set; }
        public string InqNo { get; set; }
        public string BuyerName { get; set; }
        public string ContactPersonname { get; set; }
        public string MobileNo { get; set; }
        public int MobileCode1 { get; set; }
        public string MobileNo1 { get; set; }
        public int MobileCode2 { get; set; }
        public string MobileNo2 { get; set; }
        public string Email { get; set; }
        public string Requirement { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int SourceId { get; set; }
        public string SourceName { get; set; }
        public string Address { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public string CountryName { get; set; }
        public int CountryId { get; set; }
        public bool IsActive { get; set; }
        public int Status { get; set; }
        public string TaskStatus { get; set; }
        public string Remark { get; set; }
        public string Subject { get; set; }
        public string AssignFromUser { get; set; }
        public string AssignToUser { get; set; }
        public DateTime InqDate { get; set; }
        public Int64 RowNumber { get; set; }
        public string AssignShow { get; set; }
        public int FollowUserId { get; set; }
        public string FollowStatus { get; set; }
        public DateTime FollowDate { get; set; }
        public TimeSpan FollowTime { get; set; }
        public DateTime LastFollowDate { get; set; }
        public TimeSpan LastFollowTime { get; set; }

    }

    public class TaskMaster
    {
        public int TaskId { get; set; }
        public string TaskNo { get; set; }
        public String Task { get; set; }
        public int Priority { get; set; }
        public String PriorityName { get; set; }
        public string Review { get; set; }
        public int Status { get; set; }
        public string TaskStatus { get; set; }
        public int TaskTypeId { get; set; }
        public string TaskType { get; set; }
        public int TaskGroupId { get; set; }
        public string TaskGroupName { get; set; }
        public string GroupBy { get; set; }
        public int TaskDetailId { get; set; }
        public int FromId { get; set; }
        public string FromUser { get; set; }
        public string ToId { get; set; }
        public string ToUser { get; set; }
        public DateTime ActualDate { get; set; }
        public TimeSpan ActualTime { get; set; }
        public DateTime PlanDateTime { get; set; }
        public TimeSpan PlanTime { get; set; }
        public DateTime NextFollowDate { get; set; }
        public TimeSpan NextFollowTime { get; set; }
        public DateTime lastFollowdate { get; set; }
        public TimeSpan lastFollowTime { get; set; }
        public DateTime FollowCreatedDate { get; set; }
        public TimeSpan FollowCreatedTime { get; set; }
        public string Note { get; set; }
        public string Duration { get; set; }
        public Int64 RowNumber { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string AssignTo { get; set; }
        public decimal Persontage { get; set; }
        public int FollowUserId { get; set; }
        public string FollowStatus { get; set; }
        public string FollowUpTaskStatus { get; set; }
        public string FollowFromUser { get; set; }
        public DateTime DeadlineDate { get; set; }
    }


    public class PerformaInvoice
    {
        public int PerformaInvId { get; set; }
        public string PerformaInvNo { get; set; }
        public int RptFormatId { get; set; }
        public string RptCompany { get; set; }
        public DateTime PerformaInvDate { get; set; }
        public int DeliveryTermId { get; set; }
        public string DeliveryTerm { get; set; }
        public int PaymentTermId { get; set; }
        public string PaymentTerm { get; set; }
        public int ModeOfShipmentId { get; set; }
        public string ModeOfShipment { get; set; }
        public string ShippingMarks { get; set; }
        public int ConsigneId { get; set; }
        public string Contact { get; set; }
        public int LoadingPortId { get; set; }
        public string LoadingPort { get; set; }
        public int DischargePortId { get; set; }
        public string DischargePort { get; set; }
        public string ContactName { get; set; }
        public string ContactTel { get; set; }
        public string Contactmail { get; set; }
        public string ConsigneName { get; set; }
        public string ConsigneAddress { get; set; }
        public string ConsigneEmail { get; set; }
        public string ConsigneTel { get; set; }
        public string ConsigneTax { get; set; }
        public int BankNameId { get; set; }
        public string BankName { get; set; }
        public int AccountTypeId { get; set; }
        public string AccountType { get; set; }
        public string BeneficiaryName { get; set; }
        public string BranchName { get; set; }
        public string BankAddress { get; set; }
        public string AccountNo { get; set; }
        public string IFSCCode { get; set; }
        public string SwiftCode { get; set; }
        public int CreatedBy { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class PurchaseOrder
    {
        public int PoId { get; set; }
        public string PoNo { get; set; }
        public string PoRefNo { get; set; }
        public DateTime PoDate { get; set; }
        public string Remark { get; set; }
        public int TermsConditionId { get; set; }
        public string TermsCondition { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalTax { get; set; }
        public decimal PayableAmount { get; set; }
        public int ModeOfShipmentId { get; set; }
        public string ModeOfShipment { get; set; }
        public int PriceCode { get; set; }
        public int LandingPort { get; set; }
        public string LandingPortName { get; set; }
        public int DischargePort { get; set; }
        public string DischargePortName { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string Address { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Attn { get; set; }
        public string AttnMobile { get; set; }
        public string AttnEmail { get; set; }
        public string SupplierTax { get; set; }
        public int ComId { get; set; }
        public string ComName { get; set; }
        public int BuyerId { get; set; }
        public string BuyerName { get; set; }
        public string BuyerAddress { get; set; }
        public string BuyerTel { get; set; }
        public string BuyerFax { get; set; }
        public string BuyerWebsite { get; set; }
        public string BuyerEmail { get; set; }
        public string BuyerAttn { get; set; }
        public string BuyerAttnMobile { get; set; }
        public string BuyerTax { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedDate { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class QuotationMaster
    {
        public int QuotationId { get; set; }
        public string QuotationNo { get; set; }
        public int BuyerId { get; set; }
        public string CompanyName { get; set; }
        public string DeliveryName { get; set; }
        public string TermName { get; set; }
        public int DeliveryTermId { get; set; }
        public int PaymentTermId { get; set; }
        public int QuotationMadeBy { get; set; }
        public DateTime QuotationDate { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string Note { get; set; }
        public string InqNo { get; set; }
        public System.DateTime OfferValiddate { get; set; }
        public decimal Total { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class HolidayMaster
    {
        public int HolidayId { get; set; }
        public int HolidayNameId { get; set; }
        public string HolidayName { get; set; }
        public int CountryId { get; set; }
        public DateTime OnDate { get; set; }
        public string CountryName { get; set; }
        public bool IsActive { get; set; }
        public string StateIds { get; set; }
        public string ReligionIds { get; set; }
        public string StateName { get; set; }
        public string ReligionName { get; set; }
        public Int64 RowNumber { get; set; }

    }
    public class LicenseMaster
    {
        public int LicenseId { get; set; }
        public string LicenseName { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class LeaveMaster
    {
        public int LeaveId { get; set; }
        public int UserId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool IsHalf { get; set; }
        public decimal TotalDays { get; set; }
        public string Reason { get; set; }
        public int Status { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }

    public class BuyerContactDetail
    {
        public int BuyerId { get; set; }
        public int ContactId { get; set; }
        public string CompanyName { get; set; }
        public string ContactPerson { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class EmailSpeech
    {
        public int SpeechId { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class EmailSignature
    {
        public int SignatureId { get; set; }
        public string Title { get; set; }
        public string Signature { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string DepartmentName { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class Religion
    {
        public int ReligionId { get; set; }
        public string ReligionName { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class SMSSpeech
    {
        public int SMSId { get; set; }
        public string DepartmentName { get; set; }
        public string SMSTitle { get; set; }
        public string SMS { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }

    public class ShapeInfo
    {
        public int ShapeId { get; set; }
        public string ShapeName { get; set; }
        public String Description { get; set; }
        public bool IsActive { get; set; }
        public string Photo { get; set; }
        public Int64 RowNumber { get; set; }
    }


    public class PlugShapeInfo
    {
        public int PlugShapeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }


    public class InwardCourierInfo
    {
        public int CourierId { get; set; }
        public DateTime CourierDate { get; set; }
        public TimeSpan CourierTime { get; set; }
        public int SenderId { get; set; }
        public string Sender { get; set; }
        public int VendorId { get; set; }
        public string Vendor { get; set; }
        public string ShipmentRemark { get; set; }
        public int ReceivedBy { get; set; }
        public string Receive { get; set; }
        public string SenderType { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
        public string MonthYear { get; set; }
        public string CourierType { get; set; }
        public string CourierReffNo { get; set; }
    }


    public class ShippingMarkInfo
    {
        public int ShipmentMarkId { get; set; }
        public string BuyerName { get; set; }
        public string ShipperName { get; set; }
        public string ConsigneeName { get; set; }
        public int POL { get; set; }
        public int POD { get; set; }
        public string POLName { get; set; }
        public string PODName { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }

    public class TaxInfo
    {
        public int TaxId { get; set; }
        public string TaxName { get; set; }
        public decimal Percentage { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class SenderInformation
    {
        public string CompanyName { get; set; }
        public string ID { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class ReceiptVoucher
    {
        public int VoucherId { get; set; }
        public int BuyerId { get; set; }
        public string BuyerName { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public string Naration { get; set; }
        public System.DateTime VoucherDate { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }


    public class OutwardCourierInfo
    {
        public int CourierId { get; set; }
        public DateTime CourierDate { get; set; }
        public TimeSpan CourierTime { get; set; }
        public int AreaId { get; set; }
        public string AreaName { get; set; }
        public int VendorId { get; set; }
        public string Vendor { get; set; }
        public int ReceiverId { get; set; }
        public string Receiver { get; set; }
        public int SenderBy { get; set; }
        public string Sender { get; set; }
        public string ReceiverType { get; set; }
        public string ShipmentRefNo { get; set; }
        public decimal Amount { get; set; }
        public string PaymentBy { get; set; }
        public string Remark { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
        public string MonthYear { get; set; }
        public string CourierType { get; set; }
        public string CourierReffNo { get; set; }
    }

    public class ExpenseTypeInfo
    {
        public int ExTypeId { get; set; }
        public string ExTypeName { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }

    public class BloodGroupInfo
    {
        public int BloodGroupId { get; set; }
        public string BloodGroup { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }

    public class SupplierContactDetail
    {
        public int SupplierId { get; set; }
        public string CompanyName { get; set; }
        public int ContactId { get; set; }
        public string ContactName { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }

    public class TaskStatusInfo
    {
        public int StatusId { get; set; }
        public string TaskStatus { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }

    public class TaskPriorityInfo
    {
        public int PriorityId { get; set; }
        public string PriorityName { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class Relation
    {
        public int RelationId { get; set; }
        public string RelationName { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class HolidayNamee
    {
        public int HolidayId { get; set; }
        public string HolidayName { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class ReportingUser
    {
        public int UserId { get; set; }
        public int ReportingId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }

    public class Percentages
    {
        public int PercentageId { get; set; }
        public decimal Percentage { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public partial class AdvertisementSource
    {
        public int SiteId { get; set; }
        public string SiteName { get; set; }
        public string SiteUrl { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }

    public partial class UploadProductDataMaster
    {
        public string GplusLink { get; set; }
        public string FbLink { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public int ProductId { get; set; }
        public int CatalogId { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string VideoLink { get; set; }
        public string SourceLink { get; set; }
        public Int64 RowNumber { get; set; }
    }

    public partial class TaskTypeMaster
    {
        public int TaskTypeId { get; set; }
        public string TaskType { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public partial class LoginHistoryMaster
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string IP { get; set; }
        public string Browser { get; set; }
        public System.DateTime LoginTime { get; set; }
        public string DeviceTypeName { get; set; }
        public string UserName { get; set; }
        public TimeSpan LogTime { get; set; }
        public Int64 RowNumber { get; set; }
        public string MonthYear { get; set; }

    }
    public partial class Attendance
    {
        public int AtId { get; set; }
        public int UserId { get; set; }
        public string Month { get; set; }
        public System.DateTime OnDate { get; set; }
        public Nullable<System.TimeSpan> WorkStartTime { get; set; }
        public Nullable<System.TimeSpan> WorkEndTime { get; set; }
        public Nullable<System.TimeSpan> LunchStartTime { get; set; }
        public Nullable<System.TimeSpan> LunchEndTime { get; set; }
        public string WorkStartIP { get; set; }
        public string WorkEndIP { get; set; }
        public string LunchStartIP { get; set; }
        public string LunchEndIP { get; set; }
        public bool IsActive { get; set; }
        public string UserName { get; set; }
        public Nullable<System.TimeSpan> TotalLunch { get; set; }
        public Nullable<System.TimeSpan> TotalWork { get; set; }
        public Nullable<System.TimeSpan> TotalWorking { get; set; }
        public Int64 RowNumber { get; set; }

    }
    public class ChatNameMaster
    {
        public int ChatId { get; set; }
        public string ChatName { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class BankNameMaster
    {
        public int BankId { get; set; }
        public string BankName { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class TOTypeMaster
    {
        public int TOTypeId { get; set; }
        public string TOType { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class LegerHead
    {
        public int LegerHeadId { get; set; }
        public string LegerHeadName { get; set; }
        public bool IsActive { get; set; }
        public int ITRId { get; set; }
        public string ITRName { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class Leger
    {
        public int LegerId { get; set; }
        public string LegerName { get; set; }
        public bool IsActive { get; set; }
        public int LegerHeadId { get; set; }
        public string LegerHeadName { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class RndProductMaster
    {
        public int RNDProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Keyword { get; set; }
        public string EmailSpeech { get; set; }
        public string Cataloges { get; set; }
        public string Photoes { get; set; }
        public string Videoes { get; set; }
    }

    public class AccountEntry
    {
        public int AccountId { get; set; }
        public int LegerId { get; set; }
        public int CurrencyId { get; set; }
        public string PartyName { get; set; }
        public decimal Amount { get; set; }
        public decimal INRAmount { get; set; }
        public decimal ExchangeRate { get; set; }
        public string TransactionSlip { get; set; }
        public string Photo { get; set; }
        public string BillPdf { get; set; }
        public string Remark { get; set; }
        public bool IsActive { get; set; }
        public DateTime BillDate { get; set; }
        public string CurrencyName { get; set; }
        public string LegerName { get; set; }
        public string BillNo { get; set; }
        public Int64 RowNumber { get; set; }
        public int LegerHeadId { get; set; }
        public string LegerHeadName { get; set; }
        public int ITRId { get; set; }
        public string ITRName { get; set; }
        public string AccountEntryType { get; set; }
        public string Account_Entry_Type { get; set; }
        public int TaxId { get; set; }
        public string TaxName { get; set; }
        public string TaxValue { get; set; }
    }
    public class TaskGroup
    {
        public int TaskGroupId { get; set; }
        public string TaskGroupName { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class TO
    {
        public int TOId { get; set; }
        public int InqId { get; set; }
        public int ToTypeId { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifyBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public string Remark { get; set; }
        public string InqNo { get; set; }
        public string TOType { get; set; }
        public string BuyerName { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Requirement { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class WorkTask
    {
        public int DailyWorkId { get; set; }
        public int UserId { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime Date { get; set; }
        public Nullable<System.TimeSpan> WorkStartTime { get; set; }
        public Nullable<System.TimeSpan> WorkEndTime { get; set; }
        public Nullable<System.TimeSpan> LunchStartTime { get; set; }
        public Nullable<System.TimeSpan> LunchEndTime { get; set; }
        public string Name { get; set; }
        public string Attandance { get; set; }
        public string TaskStatus { get; set; }
        public string Remark { get; set; }
        public string Persontage { get; set; }
        public string Task { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class WorkProfile
    {
        public int WorkProfileId { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Nullable<System.TimeSpan> WorkTime { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
        public string WorkDay { get; set; }
        public string WorkCycle { get; set; }
        public Nullable<System.DateTime> WorkDate { get; set; }
    }
    public class SalesPurchaseEntry
    {
        public int SalesPurchaseId { get; set; }
        public int FinicialYearId { get; set; }
        public string InvoiceNo { get; set; }
        public System.DateTime InvoiceDate { get; set; }
        public int PartyType { get; set; }
        public int PartyId { get; set; }
        public string PartyName { get; set; }
        public bool IsActive { get; set; }
        public string PartyTypeName { get; set; }
        public string FinancialYear { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifyBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Int64 RowNumber { get; set; }
    }
    //RJ -13-02-2017 start
    public class IntervieweeCandidate
    {
        public int IntCandId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Experience { get; set; }
        public string Education { get; set; }
        public string Location { get; set; }
        public string Gender { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> ReferenceTypeId { get; set; }
        public Nullable<int> ReferenceId { get; set; }
        public Nullable<int> ReferenceSubType { get; set; }
        public string ReferenceMannualEntry { get; set; }
        public Nullable<int> CityId { get; set; }
        public string Address { get; set; }
        public string Pincode { get; set; }
        public System.DateTime CommunicateDate { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public Nullable<int> MaritalStatus { get; set; }
        public System.DateTime Birthdate { get; set; }
        public string TotalWorkExperience { get; set; }
        public Nullable<decimal> CurrentCTC { get; set; }
        public Nullable<decimal> CurrentExpected { get; set; }
        public string NoticePeriod { get; set; }
        public string UploadResume { get; set; }
        public Nullable<int> QualificationId { get; set; }
        public Nullable<int> SourceId { get; set; }
        public string Chat { get; set; }
        public string SourceName { get; set; }
        public string CandidateRefNo { get; set; }

        public Int64 RowNumber { get; set; }
    }
    //RJ -13-02-2017 End
    public class AHolderCodeMaster
    {
        public int AcHolderCode { get; set; }
        public string AcHolderName { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class DailyReporting
    {
        public int DailyWorkId { get; set; }
        public int UserId { get; set; }
        public string DepartmentName { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int TaskInqId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int StatusId { get; set; }
        public string TaskStatus { get; set; }
        public string Remark { get; set; }
        public decimal Persontage { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Totaltime { get; set; }
        public string TaskType { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class Qualifications
    {
        public int QualificationId { get; set; }
        public string QualificationName { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class ContactType
    {
        public int ContactTypeId { get; set; }
        public string ContactTypeName { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }

    public class AddressTypeMaster
    {
        public int AddressTypeId { get; set; }
        public string AddressTypeName { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class InquiryFollowup
    {
        public int FollowupId { get; set; }
        public int InqId { get; set; }
        public string CurrentUpdate { get; set; }
        public DateTime NextFollowDate { get; set; }
        public TimeSpan NextFollowTime { get; set; }
        public string StatusName { get; set; }
        public int Status { get; set; }
        public int CreatedBy { get; set; }
        public string UserName { get; set; }
        public int AssignId { get; set; }
        public string AssignUserName { get; set; }
        public DateTime CurrentDate { get; set; }
        public TimeSpan CurrentTime { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class TaskFollowup
    {
        public int TaskDetailId { get; set; }
        public int TaskId { get; set; }
        public string Note { get; set; }
        public DateTime NextFollowDate { get; set; }
        public TimeSpan NextFollowTime { get; set; }
        public DateTime ActualDate { get; set; }
        public TimeSpan ActualTime { get; set; }
        public DateTime PlanDateTime { get; set; }
        public TimeSpan PlanTime { get; set; }
        public DateTime CreatedDate { get; set; }
        public TimeSpan CreatedTime { get; set; }
        public string StatusName { get; set; }
        public int Status { get; set; }
        public int FromId { get; set; }
        public string UserName { get; set; }
        public int ToId { get; set; }
        public string AssignUserName { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class ShippingOrder
    {
        public int ShippingOrdId { get; set; }
        public string TypeofShipment { get; set; }
        public string Commodity { get; set; }
        public string Nooftotal { get; set; }
        public string NoofBL { get; set; }
        public string CPBuyerName { get; set; }
        public string CPBuyerAddress { get; set; }
        public string CPBuyerTelephone { get; set; }
        public string CPBuyerFax { get; set; }
        public string CPBuyerContactPerson { get; set; }
        public string EDBuyerName { get; set; }
        public string EDBuyerAddress { get; set; }
        public string EDBuyerTelephone { get; set; }
        public string EDBuyerContactPerson { get; set; }
        public string Freight { get; set; }
        public string POL { get; set; }
        public string POD { get; set; }
        public string ProductDescription { get; set; }
        public string ShippingMarksNumber { get; set; }
        public string TotalNOPkgs { get; set; }
        public decimal TotalGross { get; set; }
        public decimal Measurement { get; set; }
        public string Shipmentterms { get; set; }
        public string CompanyName { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class ContactDocumentName
    {
        public int ContactDocId { get; set; }
        public string ContactDocName { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }

    public class BillofLoading
    {
        public int BLId { get; set; }
        public string ShipperName { get; set; }
        public string ShipperAddress { get; set; }
        public string ConsigneeName { get; set; }
        public string ConsigneeAddress { get; set; }
        public string Freight { get; set; }
        public string POL { get; set; }
        public string POD { get; set; }
        public string ProductDescription { get; set; }
        public string ShippingMarksNumber { get; set; }
        public string TotalNOPkgs { get; set; }
        public decimal GrossWeight { get; set; }
        public decimal NetWeight { get; set; }
        public decimal VolMeasurement { get; set; }
        public string CompanyName { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class CourierTypeName
    {
        public int CourierTypeId { get; set; }
        public string CourierType { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class TechnicalSpecHead
    {
        public int TechHeadId { get; set; }
        public string TechHead { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class PackingTypeName
    {
        public int PackingTypeId { get; set; }
        public string PackingType { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class ProductList
    {
        public int CatalogId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string CompanyName { get; set; }
        public string SupplierModelNo { get; set; }
        public string ProductCode { get; set; }
        public string OurCatalogImg { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class EmpReffTypeMaster
    {
        public int ReffTypeId { get; set; }
        public string ReffTypeName { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }



    public class ReferenceSourceMasterGrid
    {
        public int SourceId { get; set; }
        public string SourceName { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }
    public class tblDealerPrice
    {
        public int ProductPriceId { get; set; }
        public string DealerPrice { get; set; }
        public int ProductId { get; set; }
        public int SupplierId { get; set; }
    }
    public class TransactionTypeMaster
    {
        public int TranTypeId { get; set; }
        public string TranType { get; set; }
        public bool IsActive { get; set; }
        public Int64 RowNumber { get; set; }
    }
}
