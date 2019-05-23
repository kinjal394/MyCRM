using CRM_Repository.Data;
using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

using System.Data.SqlClient;
using CRM_Repository.DataServices;
namespace CRM_Repository.Service
{
    public class ProductCatalogMaster_Repository : IProductCatalogMaster_Repository , IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;

        public ProductCatalogMaster_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public ProductCatalogMaster SaveProductCatalog(ProductCatalogMaster objProductCatalogMaster)
        {
            try
            {
                context.ProductCatalogMasters.Add(objProductCatalogMaster);
                context.SaveChanges();
                return objProductCatalogMaster;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateProductCatalog(ProductCatalogMaster objProductCatalogMaster)
        {
            try
            {
                context.Entry(objProductCatalogMaster).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProductPrice SaveProductPrice(ProductPrice objProductPrice)
        {
            try
            {
                context.ProductPrices.Add(objProductPrice);
                context.SaveChanges();
                return objProductPrice;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateProductPrice(ProductPrice objProductPrice)
        {
            try
            {
                context.Entry(objProductPrice).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void DeleteProductCatalog(int CatalogId)
        {
            try
            {
                ProductCatalogMaster objProductCatalogMaster = context.ProductCatalogMasters.Where(z => z.CatalogId == CatalogId).SingleOrDefault();
                //objProductCatalogMaster.IsActive = false;
                context.Entry(objProductCatalogMaster).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProductCatalogMaster GetByCatalogId(int CatalogId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@CatalogId", CatalogId);
                return new dalc().GetDataTable_Text("SELECT * FROM ProductCatalogMaster with(nolock) WHERE CatalogId = @CatalogId AND IsActive=1", para).ConvertToList<ProductCatalogMaster>().FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool CheckProductModenolDuplication(string Obj)
        {
            try
            {
                    SqlParameter[] para = new SqlParameter[1];
                    para[0] = new SqlParameter().CreateParameter("@ProductModelNo", Obj);
                    return new dalc().GetDataTable_Text("SELECT * FROM ProductCatalogMaster with(nolock) WHERE RTRIM(LTRIM(ProductModelNo)) = RTRIM(LTRIM(@ProductModelNo)) AND  IsActive = 1", para).Rows.Count > 0 ? true : false;

               
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool CheckSupplierModelNolDuplication(string Obj)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@SupplierModelNo", Obj);
                return new dalc().GetDataTable_Text("SELECT * FROM ProductCatalogMaster with(nolock) WHERE RTRIM(LTRIM(SupplierModelNo)) = RTRIM(LTRIM(@SupplierModelNo)) AND  IsActive = 1", para).Rows.Count > 0 ? true : false;


            }
            catch (Exception)
            {
                return false;
            }
        }
        public IQueryable<ProductCatalogMaster> GetProductCatalog()
        {
            try
            {
                return new dalc().selectbyquerydt("SELECT * FROM ProductCatalogMaster with(nolock) ").ConvertToList<ProductCatalogMaster>().AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<ProductCatalogMaster> GetCatalogByProductId(int ProductId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@ProductId", ProductId);
                return new dalc().GetDataTable_Text(@"SELECT pc.CatalogId, pc.ProductId, pc.CatalogPath, pc.SupplierId, pc.SupplierModelNo, pc.ProductModelNo, pc.IsActive, pc.CountryOfOriginId,pc.Capacity,
                                                    co.CountryOfOrigin As CountryOfOriginName,sp.CompanyName[SupplierName]
                                                    ,count(ps.CatalogId) As NoofDoc
                                                    FROM ProductCatalogMaster pc with(nolock)
                                                    left join BuyerMaster sp with(nolock) on sp.BuyerId = pc.SupplierId
                                                    left join CountryOfOriginMaster co with(nolock) on co.OriginId = pc.CountryOfOriginId
                                                    left join ProductSuppDocumentDetail ps with(nolock) on ps.ProductId = pc.ProductId And  ps.CatalogID = pc.CatalogID
                                                    WHERE pc.IsActive = 1 and pc.ProductId = @ProductId
                                                    group by co.CountryOfOrigin, sp.CompanyName,
                                                    pc.CatalogId, pc.ProductId, pc.CatalogPath, pc.SupplierId, pc.SupplierModelNo, pc.ProductModelNo, pc.IsActive, pc.CountryOfOriginId,pc.Capacity
                                                     ", para).ConvertToList<ProductCatalogMaster>().AsQueryable();
                

                //return new dalc().GetDataTable_Text(@"SELECT pc.*,co.CountryOfOrigin As CountryOfOriginName,sp.CompanyName[SupplierName]        
                //                                    ,len(pc.CatalogPath) - len(replace(pc.CatalogPath, '|', '')) + 1  as NoofDoc
                //                                    FROM ProductCatalogMaster pc with(nolock)
                //                                    left join BuyerMaster sp with(nolock) on sp.BuyerId = pc.SupplierId
                //                                    left join CountryOfOriginMaster co with(nolock) on co.OriginId = pc.CountryOfOriginId
                //                                    WHERE pc.IsActive = 1 and pc.ProductId = @ProductId ", para).ConvertToList<ProductCatalogMaster>().AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IQueryable<ProductSuppDocumentDetail> GetSuppDocumentbyId(int ProductId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@ProductId", ProductId);
                return new dalc().GetDataTable_Text(@"SELECT pc.* FROM ProductSuppDocumentDetail pc with(nolock) WHERE pc.ProductId = @ProductId AND pc.IsActive=1 ", para).ConvertToList<ProductSuppDocumentDetail>().AsQueryable();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IQueryable<ProductCatalogMaster> GetCatalogByProductSupplierId(int ProductId, int CatalogId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@ProductId", ProductId);
                para[1] = new SqlParameter().CreateParameter("@CatalogId", CatalogId);
                return new dalc().GetDataTable_Text("SELECT pc.*,sp.CompanyName[SupplierName] FROM ProductCatalogMaster pc with(nolock) left join BuyerMaster sp with(nolock) on sp.BuyerId=pc.SupplierId WHERE pc.ProductId = @ProductId And pc.CatalogId=@CatalogId AND pc.IsActive=1", para).ConvertToList<ProductCatalogMaster>().AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<ProductPackingDetail> GetPackingByProductSupplierId(int ProductId, int CatalogId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@ProductId", ProductId);
                para[1] = new SqlParameter().CreateParameter("@CatalogId", CatalogId);
                return new dalc().GetDataTable_Text(@"Select pc.*,cm.CurrencyName,pm.PackingType,ps.Title As PlugShape,sp.Phase,vm.Voltage,fm.Frequency
                                                    --,tm.TaxName
                                                    from ProductPackingDetail pc with(nolock) 
                                                    left join CurrencyMaster cm with(nolock) on cm.CurrencyId = pc.CurrencyId
                                                    left join PlugShapeMaster ps with(nolock) on ps.PlugShapeId = pc.PlugShapeId  
                                                    left join PhaseMaster sp with(nolock) on sp.PhaseId = pc.PhaseId  
                                                    left join VoltageMaster vm with(nolock) on vm.VoltageId = pc.VoltageId  
                                                    left join FrequencyMaster fm with(nolock) on fm.FrequencyId = pc.FrequencyId  
                                                    --left join TaxMaster tm with(nolock) on tm.TaxId = pc.TaxId    
                                                    left join PackingTypeMaster pm with(nolock) on pm.PackingTypeId = pc.PackingTypeId                      
                                                    WHERE ProductId = @ProductId And CatalogId=@CatalogId", para).ConvertToList<ProductPackingDetail>().AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IQueryable<ProductSuppDocumentDetail> GetSuppDocumentByProductSuppId(int ProductId, int CatalogId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@ProductId", ProductId);
                para[1] = new SqlParameter().CreateParameter("@CatalogId", CatalogId);
                return new dalc().GetDataTable_Text(@"SELECT pc.*,pd.prdDocName FROM ProductSuppDocumentDetail pc with(nolock)
                                                    inner join ProductDocumentMaster as pd on pd.PrdDocId=pc.PrdDocId
                                                    WHERE pc.ProductId =@ProductId And pc.CatalogId=@CatalogId AND pc.IsActive=1", para).ConvertToList<ProductSuppDocumentDetail>().AsQueryable();
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
    }
}
