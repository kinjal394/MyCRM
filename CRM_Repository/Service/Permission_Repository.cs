using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.ServiceContract;
using System.Data.Entity;
using CRM_Repository.Data;
using System.Data;
using System.Transactions;
using System.Data.SqlClient;
using CRM_Repository.DataServices;
using CRM_Repository.DTOModel;

namespace CRM_Repository.Service
{
    public class Permission_Repository : IPermission_Repository, IDisposable
    {
        private elaunch_crmEntities context;
        public Permission_Repository(elaunch_crmEntities _context)
        {
            context = _context;
        }
        public Permission_Repository()
        {
            context = new elaunch_crmEntities();
        }
        public List<MenuDTO> GetPages()
        {
            try
            {
                return new dalc().selectbyquerydt(@"SELECT * FROM MenuMaster").ConvertToList<MenuDTO>().ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<PermissionDTO> GetPermissions(int UserId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@UserId", UserId);
                return new dalc().GetDataTable_Text("SELECT * FROM AccountEntryMaster with(nolock) WHERE AccountId=@AccountId", para).ConvertToList<PermissionDTO>().ToList();
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
