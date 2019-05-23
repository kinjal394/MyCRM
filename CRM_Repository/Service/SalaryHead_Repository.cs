using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;
using CRM_Repository.ServiceContract;
using System.Data.SqlClient;
using CRM_Repository.DataServices;

namespace CRM_Repository.Service
{
    public class SalaryHead_Repository:ISalaryHead_Repository, IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;
        public SalaryHead_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }
        public void AddSalaryHead(SalaryHeadMaster obj)
        {
            try
            {
                context.SalaryHeadMasters.Add(obj);
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }
        public void UpdateSalaryHead(SalaryHeadMaster obj)
        {
            try
            {
                context.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }
        public void DeleteSalaryHead(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@SalaryHeadId", id);
                SalaryHeadMaster SalaryHead = new dalc().GetDataTable_Text("SELECT * FROM SalaryHeadMaster with(nolock) WHERE SalaryHeadId=@SalaryHeadId", para).ConvertToList<SalaryHeadMaster>().FirstOrDefault();
                if(SalaryHead!=null)
                {
                    context.SalaryHeadMasters.Remove(SalaryHead);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }
        public SalaryHeadMaster GetSalaryHeadById(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@SalaryHeadId", id);
                var SalaryHead = new dalc().GetDataTable_Text("SELECT * FROM SalaryHeadMaster with(nolock) WHERE SalaryHeadId=@SalaryHeadId", para).ConvertToList<SalaryHeadMaster>().FirstOrDefault();
                return SalaryHead;
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }
        public IQueryable<SalaryHeadMaster> GetAllSalaryHead()
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@IsActive", "true");
                var SalaryHead = new dalc().GetDataTable_Text("SELECT * FROM SalaryHeadMaster with(nolock) WHERE IsActive=@IsActive", para).ConvertToList<SalaryHeadMaster>().AsQueryable();
                return SalaryHead;
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }
        public IQueryable<SalaryHeadMaster> DuplicateSalaryHead(string SalaryHeadName)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@SalaryHeadName", SalaryHeadName);
                para[1] = new SqlParameter().CreateParameter("@IsActive", "true");
                var SalaryHead = new dalc().GetDataTable_Text("SELECT * FROM SalaryHeadMaster with(nolock) WHERE SalaryHeadName=@SalaryHeadName and IsActive=@IsActive", para).ConvertToList<SalaryHeadMaster>().AsQueryable();
                
                return SalaryHead.AsQueryable();
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }
        public IQueryable<SalaryHeadMaster> DuplicateEditSalaryHead(int SalaryHeadId, string SalaryHeadName)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[3];
                para[0] = new SqlParameter().CreateParameter("@SalaryHeadId", SalaryHeadId);
                para[1] = new SqlParameter().CreateParameter("@SalaryHeadName", SalaryHeadName);
                para[2] = new SqlParameter().CreateParameter("@IsActive", "true");
                var AgencyType = new dalc().GetDataTable_Text("SELECT * FROM SalaryHeadMaster with(nolock) WHERE SalaryHeadId!=@SalaryHeadId and SalaryHeadName=@SalaryHeadName and IsActive=@IsActive", para).ConvertToList<SalaryHeadMaster>().AsQueryable();
                //var AgencyType = context.AgencyTypeMasters.Where(x => x.AgencyTypeId != AgencyTypeId && x.AgencyType == AgencyTypeName && x.IsActive == true);
                return AgencyType.AsQueryable();
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
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
