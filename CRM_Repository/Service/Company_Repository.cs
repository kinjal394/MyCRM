using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;
using CRM_Repository.ServiceContract;
using System.Transactions;
using System.Data.SqlClient;
using CRM_Repository.DataServices;

namespace CRM_Repository.Service
{
    public class Company_Repository : ICompany_Repository, IDisposable
    {

        private CRM_Repository.Data.elaunch_crmEntities context;
        public Company_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void AddComapny(CompanyMaster companyobj)
        {
            try
            {
                context.CompanyMasters.Add(companyobj);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public void DeleteComapny(int id)
        {
            try
            {
                CompanyMaster comobj = context.CompanyMasters.Find(id);
                if (comobj != null)
                {
                    context.CompanyMasters.Remove(comobj);
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public bool CheckComapnyExist(CompanyMaster obj, bool isUpdate)
        {
            try
            {
                if (isUpdate)
                {
                    SqlParameter[] para = new SqlParameter[2];
                    para[0] = new SqlParameter().CreateParameter("@ComName", obj.ComName);
                    para[1] = new SqlParameter().CreateParameter("@ComId", obj.ComId);
                    return new dalc().GetDataTable_Text("SELECT * FROM CompanyMaster with(nolock) WHERE RTRIM(LTRIM(ComName)) = RTRIM(LTRIM(@ComName)) AND ComId <> @ComId AND IsActive = 1", para).Rows.Count > 0 ? true : false;
                }
                else
                {
                    SqlParameter[] para = new SqlParameter[1];
                    para[0] = new SqlParameter().CreateParameter("@ComName", obj.ComName);
                    return new dalc().GetDataTable_Text("SELECT * FROM CompanyMaster with(nolock) WHERE RTRIM(LTRIM(ComName)) = RTRIM(LTRIM(@ComName)) AND IsActive = 1", para).Rows.Count > 0 ? true : false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public IQueryable<CompanyMaster> GetAllCompay()
        {
            try
            {
                return new dalc().selectbyquerydt("SELECT * FROM CompanyMaster with(nolock) ").ConvertToList<CompanyMaster>().AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public CompanyMaster GetComapnybyid(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@ComId", id);
                return new dalc().GetDataTable_Text("SELECT * FROM CompanyMaster with(nolock) WHERE ComId=@ComId", para).ConvertToList<CompanyMaster>().FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateCompany(CompanyMaster companyobj)
        {
            try
            {
                context.Entry(companyobj).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
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
