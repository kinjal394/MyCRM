using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
    public interface IEmailSpeech_Repository : IDisposable
    {
        void AddEmailSpeech(EmailSpeechMaster obj);
        void UpdateEmailSpeech(EmailSpeechMaster obj);
        EmailSpeechMaster GetEmailSpeechById(int id);
        IQueryable<EmailSpeechMaster> GetAllEmailSpeech();
        bool CheckEmailSpeechExist(EmailSpeechMaster obj);
        EmailSpeechMaster CheckEmailSpeech(string title);
    }
}
