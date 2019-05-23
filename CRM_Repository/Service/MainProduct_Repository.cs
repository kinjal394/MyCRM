using CRM_Repository.Data;
using CRM_Repository.DataServices;
using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CRM_Repository.Service
{
    public class MainProduct_Repository : IMainProduct_Repository
    {
        private CRM_Repository.Data.elaunch_crmEntities context;
        public MainProduct_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void SaveMainProduct(MainProductMaster mainProduct)
        {
            try
            {
                context.MainProductMasters.Add(mainProduct);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateMainProduct(MainProductMaster product)
        {
            try
            {
                context.Entry(product).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void DeleteMainProduct(int UserId, int mainProductId)
        {
            try
            {
                MainProductMaster Cat = context.MainProductMasters.Single(u => u.MainProductId == mainProductId);
                Cat.DeletedBy = UserId;
                Cat.IsActive = false;
                Cat.DeletedDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                context.Entry(Cat).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsProductExist(MainProductMaster mainProduct,string mode)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[3];
                para[0] = new SqlParameter().CreateParameter("@SubCategoryId", mainProduct.SubCategoryId);
                para[1] = new SqlParameter().CreateParameter("@MainProductName", mainProduct.MainProductName);
                para[2] = new SqlParameter().CreateParameter("@MainProductId", mainProduct.MainProductId);
                if (mode == "ADD")
                {
                    return new dalc().GetDataTable_Text(" SELECT * FROM MainProductMaster WHERE SubCategoryId = @SubCategoryId AND MainProductName = @MainProductName AND IsActive = 1", para).Rows.Count > 0 ? true : false;
                }
                else if (mode == "EDIT")
                {
                    return new dalc().GetDataTable_Text(" SELECT * FROM MainProductMaster WHERE SubCategoryId = @SubCategoryId AND MainProductName = @MainProductName AND MainProductId != @MainProductId AND IsActive = 1", para).Rows.Count > 0 ? true : false;
                }
                return true;

                
            }
            catch (Exception)
            {

                throw;
            }
          
            //return context.MainProductMasters.Where(x => x.MainProductName == mainProduct.MainProductName && x.MainProductId != mainProduct.MainProductId&& x.IsActive == true);

        }

        public MainProductMaster GetProductById(int productId)
        {
            try
            {
              
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@SubCategoryId", productId);
                return new dalc().GetDataTable_Text("SELECT * FROM MainProductMaster with(nolock) WHERE SubCategoryId=@SubCategoryId ", para).ConvertToList<MainProductMaster>().FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public IQueryable<MainProductMaster> GetProductBySubcategoryId(int subcatId)
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var mainproduct = context.MainProductMasters.Where(z => z.SubCategoryId == subcatId && z.IsActive == true);
                //    scope.Complete();
                //    return mainproduct.AsQueryable();
                //}

                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@SubCategoryId", subcatId);
                return new dalc().GetDataTable_Text("SELECT * FROM MainProductMaster with(nolock) WHERE  SubCategoryId<>@SubCategoryId AND IsActive = 1", para).ConvertToList<MainProductMaster>().AsQueryable();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public IQueryable<MainProductMaster> GetMainProducts()
        {
            try
            {
                //return context.MainProductMasters;
                return new dalc().selectbyquerydt("SELECT * FROM MainProductMaster with(nolock) WHERE IsActive = 1").ConvertToList<MainProductMaster>().AsQueryable();

            }
            catch (Exception ex)
            {
                throw ex;
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
