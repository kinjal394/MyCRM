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
    public class SubCategory_Repository : ISubCategory_Repository, IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;
        public SubCategory_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void SaveSubCategory(SubCategoryMaster objSubCategory)
        {
            try
            {
                context.SubCategoryMasters.Add(objSubCategory);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateSubCategory(SubCategoryMaster objSubCategory)
        {
            try
            {
                context.Entry(objSubCategory).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteSubCategory(int SubCategoryId, int UserId)
        {
            try
            {
                SubCategoryMaster objSubCategory = context.SubCategoryMasters.Where(u => u.SubCategoryId == SubCategoryId).SingleOrDefault();
                objSubCategory.DeletedBy = UserId;
                objSubCategory.IsActive = false;
                objSubCategory.DeletedDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                context.Entry(objSubCategory).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<SubCategoryMaster> GetSubCategory()
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var data = context.SubCategoryMasters.Where(x => x.IsActive == true);
                //    scope.Complete();
                //    return data;
                //}

                return new dalc().selectbyquerydt("SELECT * FROM SubCategoryMaster with(nolock) where  IsActive=1").ConvertToList<SubCategoryMaster>().AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public SubCategoryMaster GetSubCategorybyId(int SubCategoryId)
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var data = context.SubCategoryMasters.Find(SubCategoryId);
                //    scope.Complete();
                //    return data;
                //}

                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@SubCategoryId", SubCategoryId);
                return new dalc().GetDataTable_Text("SELECT * FROM SubCategoryMaster with(nolock) WHERE SubCategoryId=@SubCategoryId and IsActive=1", para).ConvertToList<SubCategoryMaster>().FirstOrDefault();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<SubCategoryMaster> GetSubCategorybyCategory(int CategoryId)
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var data = context.SubCategoryMasters.Where(X => X.CategoryId == CategoryId && X.IsActive == true);
                //    scope.Complete();
                //    return data;
                //}

                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@CategoryId", CategoryId);
                return new dalc().GetDataTable_Text("SELECT * FROM SubCategoryMaster with(nolock) WHERE CategoryId=@CategoryId and IsActive=1", para).ConvertToList<SubCategoryMaster>().AsQueryable();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool IsExist(SubCategoryMaster objSubCategory,string mode)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[3];
                para[0] = new SqlParameter().CreateParameter("@CategoryId", objSubCategory.CategoryId);
                para[1] = new SqlParameter().CreateParameter("@SubCategoryName", objSubCategory.SubCategoryName);
                para[2] = new SqlParameter().CreateParameter("@SubCategoryId", objSubCategory.SubCategoryId);
                if (mode == "ADD")
                {
                    return new dalc().GetDataTable_Text(" SELECT * FROM gurjari_crmuser.SubCategoryMaster WHERE CategoryId = @CategoryId AND SubCategoryName = @SubCategoryName AND IsActive = 1", para).Rows.Count > 0 ? true : false;
                }else if (mode == "EDIT")
                {
                    return new dalc().GetDataTable_Text(" SELECT * FROM gurjari_crmuser.SubCategoryMaster WHERE CategoryId = @CategoryId AND SubCategoryName = @SubCategoryName AND SubCategoryId != @SubCategoryId AND IsActive = 1", para).Rows.Count > 0 ? true : false;
                }
                return true;
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var data = context.SubCategoryMasters.Where(X => X.CategoryId == objSubCategory.CategoryId && X.SubCategoryName == objSubCategory.SubCategoryName && X.SubCategoryId != objSubCategory.SubCategoryId && objSubCategory.IsActive == true).Count();
                //    if (data > 0)
                //        return true;
                //    else
                //        return false;
                //}
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
