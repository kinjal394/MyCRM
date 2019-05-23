using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;

namespace CRM_Repository.ServiceContract
{
   public interface IApplicableCharge_Repository:IDisposable
    {
        void AddAppliChar(ApplicableChargeMaster obj);
        void UpdateAppliChar(ApplicableChargeMaster obj);
        void DeleteAppllichar(int id);
        ApplicableChargeMaster getApplicharbyId(int id);
        IQueryable<ApplicableChargeMaster> GetAllApplichar();
        IQueryable<ApplicableChargeMaster> DuplicateApplicableChargeName(string ApplicableChargeName);
        IQueryable<ApplicableChargeMaster> DuplicateEditApplicableChargeName(int ApplicableChargeId, string ApplicableChargeName);
    }
}
