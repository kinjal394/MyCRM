using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.DTOModel
{
    public partial class ExhibitionModel
    {
        public int ExId { get; set; }
        public string ExName { get; set; }
        public int AreaId { get; set; }
        public string Venue { get; set; }
        public string NoofYears { get; set; }
        public string ExProfile { get; set; }
        public string OrganizerDetail { get; set; }
        public string BankDetail { get; set; }
        public string Address { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifyBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime ExDate { get; set; }
        public string AreaName { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public string Tel { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Web { get; set; }
        public string ContactPerson { get; set; }
        public string Chat { get; set; }
    }
    
}
