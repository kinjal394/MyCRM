using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
   public interface IProductDocument_Repository : IDisposable
    {
        void AddProductDocument(ProductDocumentMaster obj);
        void UpdateProductDocument(ProductDocumentMaster obj);
        void DeleteProductDocument(int id);
        ProductDocumentMaster GetProductDocumentByID(int id);
        IQueryable<ProductDocumentMaster> GetAllProductDocument();
        IQueryable<ProductDocumentMaster> DuplicatProductDocument(string PrdDocName);
        IQueryable<ProductDocumentMaster> DuplicateEditProductDocument(int PrdDocId, string PrdDocName);
    }
}
