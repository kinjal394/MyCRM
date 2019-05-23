using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
    public interface IEmailSignature_Repository : IDisposable
    {
        void AddEmailSignature(SignatureMaster obj);
        void UpdateEmailSignature(SignatureMaster obj);
        SignatureMaster GetEmailSignatureById(int id);
        IQueryable<SignatureMaster> GetAllSignature();
        bool CheckEmailSignature(SignatureMaster obj);
    }
}
