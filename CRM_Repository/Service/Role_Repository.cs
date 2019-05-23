using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;
using System.Transactions;
using System.Data.SqlClient;
using CRM_Repository.DataServices;

namespace CRM_Repository.Service
{
    public class Role_Repository : IRole_Repository, IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;
        public Role_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void AddRole(RoleMaster Role)
        {
            try
            {
                context.RoleMasters.Add(Role);
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool CheckRoleType(RoleMaster obj, bool isUpdate)
        {
            try
            {
                if (isUpdate)
                {
                    SqlParameter[] para = new SqlParameter[2];
                    para[0] = new SqlParameter().CreateParameter("@RoleName", obj.RoleName);
                    para[1] = new SqlParameter().CreateParameter("@RoleId", obj.RoleId);
                    return new dalc().GetDataTable_Text("SELECT * FROM RoleMaster with(nolock) WHERE RTRIM(LTRIM(RoleName)) = RTRIM(LTRIM(@RoleName)) AND RoleId <> @RoleId AND IsActive = 1", para).Rows.Count > 0 ? true : false;

                }
                else
                {
                     SqlParameter[] para = new SqlParameter[1];
                    para[0] = new SqlParameter().CreateParameter("@RoleName", obj.RoleName);
                    return new dalc().GetDataTable_Text("SELECT * FROM RoleMaster with(nolock) WHERE RTRIM(LTRIM(RoleName)) = RTRIM(LTRIM(@RoleName)) AND IsActive = 1", para).Rows.Count > 0 ? true : false;

                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IQueryable<RoleMaster> GetAllRole()
        {
            try
            {
                return new dalc().selectbyquerydt("SELECT * FROM RoleMaster with(nolock) ").ConvertToList<RoleMaster>().AsQueryable();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public RoleMaster GetRoleById(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@RoleId", id);
                return new dalc().GetDataTable_Text("SELECT * FROM RoleMaster with(nolock) WHERE RoleId=@RoleId", para).ConvertToList<RoleMaster>().FirstOrDefault();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateRole(RoleMaster Role)
        {
            try
            {
                context.Entry(Role).State = System.Data.Entity.EntityState.Modified;
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
