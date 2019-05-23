using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
    public interface IDocumentUpload_Repository : IDisposable
    {

        void AddDocumentUpload(EmpDocumentMaster empdocobj);
        void UpdateDocumentUpload(EmpDocumentMaster empdocobj);
        EmpDocumentMaster GetDocUploadById(int id);
        IQueryable<EmpDocumentMaster> GetdocUploadById(int id);
        IQueryable<EmpDocumentMaster> GetAllUploadDoc();
        IQueryable<EmpDocumentMaster> CheckForDuplicateDoc(int EmpId, int DocId);
    }
}
