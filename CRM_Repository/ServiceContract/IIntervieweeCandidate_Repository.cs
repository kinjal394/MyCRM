using CRM_Repository.Data;
using CRM_Repository.DTOModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
    public interface IIntervieweeCandidate_Repository : IDisposable
    {
        void AddIntervieweeCandidate(InterviweeCandidateMaster obj);
        void UpdateIntervieweeCandidate(InterviweeCandidateMaster obj);
        void DeleteIntervieweeCandidate(int id);
        IntervieweeCandidateModel GetIntervieweeCandidateById(int id);
        IQueryable<InterviweeCandidateMaster> GetAllIntervieweeCandidate();
        int CreateUpdate(IntervieweeCandidateModel objInputIntervieweeCandidateMaster);
        int Delete(int IntCandId);
        IntervieweeCandidateModel GetIntervieweeCandidateByCode(string id);
    }
}
