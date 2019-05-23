using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;

namespace CRM_Repository.ServiceContract
{
    public interface ITermsAndCondition_Repository : IDisposable
    {
        void AddTermsAndCondition(TermsAndConditionMaster obj);
        void UpdateTermsAndCondition(TermsAndConditionMaster obj); 
        TermsAndConditionMaster GetTermsAndConditionById(int id);
        IQueryable<TermsAndConditionMaster> GetAllTermsAndCondition();
        IQueryable<TermsAndConditionMaster> DuplicateTermsAndCondition(string Description);
        IQueryable<TermsAndConditionMaster> DuplicateEditTermsAndCondition(int TermsId, string Description);
    }
}
