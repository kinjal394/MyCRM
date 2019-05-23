using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.Data
{
    [MetadataType(typeof(UserModel))]
    public partial class UserMaster
    {
        public string QualificationName { get; set; }
        public string SourceName { get; set; }
        public string MobCode1 { get; set; }
        public string UserContactcode { get; set; }
        public string DesignationName { get; set; }
        public string RoleName { get; set; }
        public string ReportingName { get; set; }
        public string AccountType { get; set; }
        public string AgencyTypeReferanceName { get; set; }
        public string ReferanceName { get; set; }
        public string DepartmentName { get; set; }
        public string BloodGroup { get; set; }
        public string BirthPlaceCityName { get; set; }
        public string BirthPlaceStateId { get; set; }
        public string BirthPlaceStateName { get; set; }
        public string BirthPlaceCountryId { get; set; }
        public string BirthPlaceCountryName { get; set; }
        public string HomeTownCityName { get; set; }
        public string HomeTownStateId { get; set; }
        public string HomeTownStateName { get; set; }
        public string HomeTownCountryId { get; set; }
        public string HomeTownCountryName { get; set; }
        public string PresentResiCityName { get; set; }
        public string PresentResiStateId { get; set; }
        public string PresentResiStateName { get; set; }
        public string PresentResiCountryId { get; set; }
        public string PresentResiCountryName { get; set; }
        public string PermenantResiCityName { get; set; }
        public string PermenantResiStateId { get; set; }
        public string PermenantResiStateName { get; set; }
        public string PermenantResiCountryId { get; set; }
        public string PermenantResiCountryName { get; set; }
        public string RelativeCityName { get; set; }
        public string RelativeStateId { get; set; }
        public string RelativeStateName { get; set; }
        public string RelativeCountryId { get; set; }
        public string RelativeCountryName { get; set; }
        public string RelativeCountryCode { get; set; }
        public string FriendCityName { get; set; }
        public string FriendStateId { get; set; }
        public string FriendStateName { get; set; }
        public string FriendCountryId { get; set; }
        public string FriendCountryName { get; set; }
        public string FriendCountryCode { get; set; }
        public string WorkMateCityName { get; set; }
        public string WorkMateStateId { get; set; }
        public string WorkMateStateName { get; set; }
        public string WorkMateCountryId { get; set; }
        public string WorkMateCountryName { get; set; }
        public string WorkMateCountryCode { get; set; }
        public string Bank { get; set; }

        //public int BirthcountryId { get; set; }
        //public int BirthstateId { get; set; }
        public List<UserReferenceRelationMaster> UserContactDetails { get; set; }
        public List<UserRefferenceDetail> UserReferanceDetails { get; set; }
        public List<UserSalaryDetail> UserSalDetails { get; set; }
        public List<UserDocDetail> UserDocumentDetails { get; set; }
        public List<UserExperienceDetail> UserExperDetails { get; set; }
        public List<UserEducationDetail> UserEduDetails { get; set; }
    }
    public partial class UserReferenceRelationMaster
    {
        public string Relation { get; set; }
        public string UserContactcode { get; set; }
        public int Status { get; set; }
    }
    public partial class UserRefferenceDetail
    {
        public string ReffTypeName { get; set; }
        public string MobileNoId { get; set; }
        public string MobileNoCode { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public string CountryName { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public int Status { get; set; }
    }
    public partial class UserSalaryDetail
    {
        public string SalaryHead { get; set; }
        public string Currency { get; set; }
        public int Status { get; set; }
    }
    public partial class UserDocDetail
    {
        public string Documents { get; set; }
        public int Status { get; set; }
    }
    public partial class UserExperienceDetail
    {
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public string CountryName { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public int Status { get; set; }
        public string DesignationName { get; set; }
    }

    public partial class UserEducationDetail
    {
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public string CountryName { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public int Status { get; set; }
        public string QualificationName { get; set; }
    }
    public class UserModel
    {

    }
}
