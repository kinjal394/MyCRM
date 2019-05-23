using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
    public interface ISMSSpeech_Repository : IDisposable
    {
        void AddSMSSpeech(SMSSpeechMaster obj);
        void UpdateSMSSpeech(SMSSpeechMaster obj);
        SMSSpeechMaster GetSMSSpeechById(int id);
        IQueryable<SMSSpeechMaster> GetAllSMSSpeech();
        IQueryable<SMSSpeechMaster> DuplicateSMSSpeech(string SMSTitle);
        IQueryable<SMSSpeechMaster> DuplicateEditSMSSpeech(int SMSId, string SMSTitle);
    }
}
