using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;
using CRM_Repository.ServiceContract;
using System.Data.SqlClient;
using CRM_Repository.DataServices;

namespace CRM_Repository.Service
{
    public class ProductDocument_Repository:IProductDocument_Repository
    {
        private CRM_Repository.Data.elaunch_crmEntities context;

        public ProductDocument_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }
        public void AddProductDocument(ProductDocumentMaster obj)
        {
            try
            {
                context.ProductDocumentMasters.Add(obj);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public void UpdateProductDocument(ProductDocumentMaster obj)
        {
            try
            {
                context.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public void DeleteProductDocument(int id)
        {
            try
            {
                //AgencyTypeMaster AgencyType = context.AgencyTypeMasters.Find(id);
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@PrdDocId", id);
                ProductDocumentMaster ProductDocument = new dalc().GetDataTable_Text("SELECT * FROM ProductDocumentMaster with(nolock) WHERE PrdDocId=@PrdDocId", para).ConvertToList<ProductDocumentMaster>().FirstOrDefault();
                if (ProductDocument != null)
                {
                    context.ProductDocumentMasters.Remove(ProductDocument);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public ProductDocumentMaster GetProductDocumentByID(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@PrdDocId", id);
                var ProductDocument = new dalc().GetDataTable_Text("SELECT * FROM ProductDocumentMaster with(nolock) WHERE PrdDocId=@PrdDocId", para).ConvertToList<ProductDocumentMaster>().FirstOrDefault();
                return ProductDocument;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public IQueryable<ProductDocumentMaster> GetAllProductDocument()
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@IsActive", "true");
                var ProductDocument = new dalc().GetDataTable_Text("SELECT * FROM ProductDocumentMaster with(nolock) WHERE IsActive=@IsActive", para).ConvertToList<ProductDocumentMaster>().AsQueryable();
                return ProductDocument;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public IQueryable<ProductDocumentMaster> DuplicatProductDocument(string PrdDocName)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@PrdDocName", PrdDocName);
                para[1] = new SqlParameter().CreateParameter("@IsActive", "true");
                var ProductDocument = new dalc().GetDataTable_Text("SELECT * FROM ProductDocumentMaster with(nolock) WHERE PrdDocName=@PrdDocName and IsActive=@IsActive", para).ConvertToList<ProductDocumentMaster>().AsQueryable();
                //var AgencyType = context.AgencyTypeMasters.Where(x => x.AgencyType == AgencyTypeName && x.IsActive == true);
                return ProductDocument.AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public IQueryable<ProductDocumentMaster> DuplicateEditProductDocument(int PrdDocId, string PrdDocName)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[3];
                para[0] = new SqlParameter().CreateParameter("@PrdDocId", PrdDocId);
                para[1] = new SqlParameter().CreateParameter("@PrdDocName", PrdDocName);
                para[2] = new SqlParameter().CreateParameter("@IsActive", "true");
                var ProductDocument = new dalc().GetDataTable_Text("SELECT * FROM ProductDocumentMaster with(nolock) WHERE PrdDocId!=@PrdDocId and PrdDocName=@PrdDocName and IsActive=@IsActive", para).ConvertToList<ProductDocumentMaster>().AsQueryable();
                //var AgencyType = context.AgencyTypeMasters.Where(x => x.AgencyTypeId != AgencyTypeId && x.AgencyType == AgencyTypeName && x.IsActive == true);
                return ProductDocument.AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    context.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
