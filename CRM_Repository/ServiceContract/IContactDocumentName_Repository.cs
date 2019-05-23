using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;

namespace CRM_Repository.ServiceContract
{
    public interface IContactDocumentName_Repository:IDisposable
    {
        void AddContactDocumentName(ContactDocumentNameMaster obj);
        void UpdateContactDocumentName(ContactDocumentNameMaster obj);
        void DeleteContactDocumentName(int id);
        IQueryable<ContactDocumentNameMaster> getAllContactDocumentName();
        ContactDocumentNameMaster GetContactDocumentNameById(int id);
        IQueryable<ContactDocumentNameMaster> GetContactDocumentById(int Id);
       IQueryable<ContactDocumentNameMaster> DuplicateEditContactDocumentName(int ContactDocId, string ContactDocName);
        IQueryable<ContactDocumentNameMaster> DuplicateContactDocumentName(string ContactDocName);
    }
}
