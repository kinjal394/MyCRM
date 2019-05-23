using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
    public interface IPhase_Repository : IDisposable
    {
        void AddPhase(PhaseMaster obj);
        void UpdatePhase(PhaseMaster obj);
        void DeletePhase(int id);
        PhaseMaster GetPhaseByID(int id);
        IQueryable<PhaseMaster> GetAllPhase();
        IQueryable<PhaseMaster> DuplicatePhase(string Phase);
        IQueryable<PhaseMaster> DuplicateEditPhase(int PhaseId, string Phase);
    }
}
