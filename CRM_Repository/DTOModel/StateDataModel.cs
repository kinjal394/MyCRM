using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.Data
{
    public partial class StateMaster
    {       
        public string CountryName { get; set; }
    }

    public partial class ExpenseMaster
    {
        public string ExTypeName { get; set; }
    }

    public partial class CityMaster
    { 
        public int CountryId { get; set; }
        public string StateName { get; set; }
        public string CountryName { get; set; }
    }
}
