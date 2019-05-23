using CRM_Repository.Data;
using CRM_Repository.DataServices;
using CRM_Repository.DTOModel;
using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.Service
{
    public class WorkProfile_Repository : IWorkProfile_Repository, IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;

        public WorkProfile_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }
        public void AddWorkProfile(WorkProfileMaster obj)
        {
            try
            {
                context.WorkProfileMasters.Add(obj);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateWorkProfile(WorkProfileMaster obj)
        {
            try
            {
                context.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteWorkProfile(int id)
        {
            try
            {
                WorkProfileMaster objWP = context.WorkProfileMasters.Where(z => z.WorkProfileId == id).SingleOrDefault();
                objWP.IsActive = false;
                context.Entry(objWP).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public WorkProfileModle GetWorkProfileById(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@WorkProfileId", id);
                return new dalc().GetDataTable_Text("Select * from WorkProfileMaster As W WITH(nolock) Inner Join DepartmentMaster As D WITH(nolock) On W.DepartmentId = D.DepartmentId WHERE W.WorkProfileId=@WorkProfileId AND W.IsActive=1", para).ConvertToList<WorkProfileModle>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IQueryable<WorkProfileModle> GetAllWorkProfile(int DepartmentId, int UserTypeId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@DepartmentId", DepartmentId);
                //if (UserTypeId == 1)
                //{
                //    return new dalc().GetDataTable_Text("SELECT * FROM WorkProfileMaster with(nolock) WHERE IsActive=1", para).ConvertToList<WorkProfileModle>().AsQueryable();
                //}
                //else
                //{
                    return new dalc().GetDataTable_Text("SELECT * FROM WorkProfileMaster with(nolock) WHERE IsActive=1 AND DepartmentId=@DepartmentId", para).ConvertToList<WorkProfileModle>().AsQueryable();
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
