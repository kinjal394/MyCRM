using CRM_Repository.Data;
using CRM_Repository.DataServices;
using CRM_Repository.DTOModel;
using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


using System.Data.SqlClient;
namespace CRM_Repository.Service
{
    public class ProductParameterMaster_Repository : IProductParameterMaster_Repository,IDisposable
    {
        dalc odal = new dalc();
        private CRM_Repository.Data.elaunch_crmEntities context;

        public ProductParameterMaster_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void SaveProductParameter(ProductParameterMaster objProductParameterMaster)
        {
            try
            {
                context.ProductParameterMasters.Add(objProductParameterMaster);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateProductParameter(ProductParameterMaster objProductParameterMaster)
        {
            try
            {
                context.Entry(objProductParameterMaster).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteProductParameter(int TechDetailId)
        {
            try
            {
                ProductParameterMaster objProductParameterMaster = context.ProductParameterMasters.Find(TechDetailId);
                if (objProductParameterMaster != null)
                {
                    context.ProductParameterMasters.Remove(objProductParameterMaster);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProductParameterMaster GetByTechDetailId(int TechDetailId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@TechDetailId", TechDetailId);
                return new dalc().GetDataTable_Text("SELECT * FROM ProductParameterMaster with(nolock) WHERE TechDetailId = @TechDetailId", para).ConvertToList<ProductParameterMaster>().FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<ProductParameterMaster> GetProductParameter()
        {
            try
            {
                return new dalc().selectbyquerydt("SELECT * FROM ProductParameterMaster with(nolock) ").ConvertToList<ProductParameterMaster>().AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<vmProductParameterMaster> GetTechDetailByProductId(int ProductId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@ProductId", ProductId);
                return odal.GetDataTable_Text(@"select PPM.TechDetailId,PPM.ProductId,PPM.TechParaId,TSM.TechSpec,PPM.Value,PPM.CatalogId
                                              from  ProductParameterMaster as PPM  with(nolock)
                                              inner join TechnicalSpecMaster TSM   with(nolock)  on TSM.SpecificationId = PPM.TechParaId
                                              Where PPM.ProductId =@ProductId ", para).ConvertToList<vmProductParameterMaster>().AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<vmProductParameterMaster> GetTechDetailByProductSupplierId(int ProductId, int CatalogId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@ProductId", ProductId);
                para[1] = new SqlParameter().CreateParameter("@CatalogId", CatalogId);
                return odal.GetDataTable_Text(@"select PPM.TechDetailId,PPM.ProductId,PPM.TechParaId,TSM.TechSpec,PPM.Value,PPM.CatalogId,T.TechHeadId,T.TechHead
                                              from  ProductParameterMaster as PPM  with(nolock)
                                              inner join TechnicalSpecMaster TSM   with(nolock)  on TSM.SpecificationId = PPM.TechParaId
											  left join TechnicalSpecHeadMaster T   with(nolock)  on T.TechHeadId = TSM.TechHeadId
                                              Where PPM.ProductId =@ProductId And PPM.CatalogId = @CatalogId", para).ConvertToList<vmProductParameterMaster>().AsQueryable();
            }
            catch (Exception)
            {
                throw;
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
        public ProductParameterMaster GetByTechparaid(int pid,int TechParaId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@pid", pid);
                para[1] = new SqlParameter().CreateParameter("@TechParaId", TechParaId);
                return new dalc().GetDataTable_Text("SELECT * FROM ProductParameterMaster with(nolock) WHERE ProductId=@pid and TechParaId = @TechParaId", para).ConvertToList<ProductParameterMaster>().FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
       
    }
}
