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
    public class Department_Repository : IDepartment_Repository, IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;
        public Department_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }
        public void AddDepartment(DepartmentMaster obj)
        {
            try
            {
                context.DepartmentMasters.Add(obj);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

        }
        public void DeleteDepartment(int id)
        {
            try
            {
                DepartmentMaster dept = context.DepartmentMasters.Find(id);
                if (dept != null)
                {
                    context.DepartmentMasters.Remove(dept);
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IQueryable<DepartmentMaster> getAllDepartment()
        {
            try
            {
                    return new dalc().selectbyquerydt("SELECT * FROM DepartmentMaster with(nolock) WHERE IsActive = 1").ConvertToList<DepartmentMaster>().AsQueryable();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public DepartmentMaster GetDepartmentById(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@DepartmentId", id);
                return new dalc().GetDataTable_Text("SELECT * FROM DepartmentMaster with(nolock) WHERE DepartmentId=@DepartmentId ", para).ConvertToList<DepartmentMaster>().FirstOrDefault();

            }
            catch (Exception)
            {

                throw;
            }

        }
        public IQueryable<DepartmentMaster> GetdepartmentById(int DepartmentId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@DepartmentId", DepartmentId);
                return new dalc().GetDataTable_Text("SELECT * FROM DepartmentMaster with(nolock) WHERE DepartmentId=@DepartmentId ", para).ConvertToList<DepartmentMaster>().AsQueryable();

            }
            catch (Exception)
            {

                throw;
            }

        }
        public IQueryable<DepartmentMaster> DuplicateEditDepartment(int DepartmentId, string DepartmentName)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@DepartmentName", DepartmentName);
                para[1] = new SqlParameter().CreateParameter("@DepartmentId", DepartmentId);
                return new dalc().GetDataTable_Text("SELECT * FROM DepartmentMaster with(nolock) WHERE RTRIM(LTRIM(DepartmentName))=RTRIM(LTRIM(@DepartmentName))  AND DepartmentId<>@DepartmentId  AND IsActive = 1", para).ConvertToList<DepartmentMaster>().AsQueryable();

            }
            catch (Exception)
            {

                throw;
            }

        }
        public IQueryable<DepartmentMaster> DuplicateDepartment(string DepartmentName)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@DepartmentName", DepartmentName);
                return new dalc().GetDataTable_Text("SELECT * FROM DepartmentMaster with(nolock) WHERE RTRIM(LTRIM(DepartmentName))=RTRIM(LTRIM(@DepartmentName))  AND IsActive = 1", para).ConvertToList<DepartmentMaster>().AsQueryable();

            }
            catch (Exception)
            {

                throw;
            }

        }
        public void UpdateDepartment(DepartmentMaster obj)
        {
            try
            {
                context.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IQueryable<UserMaster> GetUserbyDepartment(int Id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@DeptId", Id);
                return new dalc().GetDataTable_Text("SELECT * FROM UserMaster with(nolock) WHERE DepartmentId=@DeptId", para).ConvertToList<UserMaster>().AsQueryable();
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
