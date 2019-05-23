using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;
namespace CRM_Repository.ServiceContract
{
    public interface IDocument_Repository : IDisposable
    {
        void AddDocument(DocumentNameMaster docobj);
        void UpdateDocument(DocumentNameMaster docobj);
        DocumentNameMaster GetDocById(int id);
        IQueryable<DocumentNameMaster> GetAllDoc();
        bool CheckDocName(DocumentNameMaster obj, bool isUpdate);
    }
}
