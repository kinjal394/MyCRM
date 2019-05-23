using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using CRM_Repository.Data;
namespace CRM_Repository.Data
{
    [MetadataType(typeof(BankMasterModel))]
    public partial class BankMaster
    {
        public string AccountType { get; set; }
        public string BankName { get; set; }
        public string AcNickName { get; set; }
    }
    public class BankMasterModel
    {
        [Required(ErrorMessage = "Required")]
        public string BeneficiaryName { get; set; }
   
        public string BankName { get; set; }
       
        public string BranchName { get; set; }
        [Required(ErrorMessage = "Required")]
        public string AccountNo { get; set; }

        public string IFSCCode { get; set; }
       
        public string NickName { get; set; }
    }
}
