using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;

namespace CRM_Repository.ServiceContract
{
    public interface ISalesDocumentName_Repository: IDisposable
    {
        void AddSalesDocument(SalesDocumentNameMaster obj);
        void UpdateSalesDocument(SalesDocumentNameMaster obj);
        void DeleteSalesDocument(int Id);
        SalesDocumentNameMaster GetSalesDocumentById(int Id);
        IQueryable<SalesDocumentNameMaster> GetAllSalesDocument();
        IQueryable<SalesDocumentNameMaster> DuplicateSalesDocument(string SalesDocument);
        IQueryable<SalesDocumentNameMaster> DuplicateEditSalesDocument(int SalesDocId, string SalesDocument);
    }
}
