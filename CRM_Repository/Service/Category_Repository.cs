using CRM_Repository.Data;
using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using CRM_Repository.DataServices;

namespace CRM_Repository.Service
{
    public class Category_Repository : ICategory_Repository, IDisposable
    {

        private CRM_Repository.Data.elaunch_crmEntities context;
        public Category_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        

        public void SaveCategory(CategoryMaster Category)
        {
            try
            {
                context.CategoryMasters.Add(Category);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateCategory(CategoryMaster Category)
        {
            try
            {
                context.Entry(Category).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void DeleteCategory(int UserId, int CategoryId)
        {
            try
            {
                CategoryMaster Cat = context.CategoryMasters.Single(u => u.CategoryId == CategoryId);
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

        public CategoryMaster GetCategoryById(int CategoryId)
        {
            try
            {
               
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@CategoryId", CategoryId);
                return new dalc().GetDataTable_Text("SELECT * FROM CategoryMaster with(nolock) WHERE CategoryId=@CategoryId ", para).ConvertToList<CategoryMaster>().FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public IQueryable<CategoryMaster> GetCategory()
        {
            try
            {
                
                return new dalc().selectbyquerydt("SELECT * FROM CategoryMaster with(nolock)").ConvertToList<CategoryMaster>().AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<CategoryMaster> IsCategoryRepeat(CategoryMaster Category)
        {
            try
            {
               
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@CategoryName", Category.CategoryName);
                para[1] = new SqlParameter().CreateParameter("@CategoryId", Category.CategoryId);
                return new dalc().GetDataTable_Text("SELECT * FROM CategoryMaster with(nolock) WHERE CategoryId<>@CategoryId AND CategoryName=@CategoryName  AND IsActive = 1", para).ConvertToList<CategoryMaster>().AsQueryable();

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
