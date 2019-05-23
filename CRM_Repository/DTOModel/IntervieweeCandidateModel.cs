using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.DTOModel
{
    public class IntervieweeCandidateModel
    {
        public int IntCandId { get; set; }
        public int ReferenceTypeId { get; set; }
        public int ReferenceId { get; set; }
        public int ReferenceSubType { get; set; }
        public string ReferenceMannualEntry { get; set; }
        public string Address { get; set; }
        public string Pincode { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public DateTime CommunicateDate { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public int Gender { get; set; }
        public int MaritalStatus { get; set; }
        public DateTime Birthdate { get; set; }
        public string TotalWorkExperience { get; set; }
        public decimal CurrentCTC { get; set; }
        public decimal CurrentExpected { get; set; }
        public string NoticePeriod { get; set; }
        public string UploadResume { get; set; }
        public bool IsActive { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public Nullable<int> QualificationId { get; set; }
        public string QualificationName { get; set; }
        public int StateId { get; set; }
        public string AgencyTypeReferanceName { get; set; }
        public string ReferanceName { get; set; }
        public string StateName { get; set; }
        public Nullable<int> SourceId { get; set; }
        public string CandidateRefno { get; set; }
        public string Chat { get; set; }
        public string Source { get; set; }
        public string CandidateRefNo { get; set; }

    }
}
