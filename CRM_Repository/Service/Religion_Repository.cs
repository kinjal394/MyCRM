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
    public class Religion_Repository : IReligion_Repository, IDisposable
    {

        private CRM_Repository.Data.elaunch_crmEntities context;
        public Religion_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }
        public void AddReligion(ReligionMaster obj)
        {
            try
            {
                context.ReligionMasters.Add(obj);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public ReligionMaster GetReligionById(int ReligionId)
        {
            try
            {
                // return context.ReligionMasters.Find(id);

                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@ReligionId", ReligionId);
                return new dalc().GetDataTable_Text("SELECT * FROM ReligionMaster with(nolock) WHERE ReligionId=@ReligionId", para).ConvertToList<ReligionMaster>().FirstOrDefault();

            }
            catch (Exception)
            {

                throw;
            }

        }

        public IQueryable<ReligionMaster> DuplicateEditReligion(int ReligionId, string ReligionName)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@ReligionName", ReligionName);
                para[1] = new SqlParameter().CreateParameter("@ReligionId", ReligionId);
                return new dalc().GetDataTable_Text("SELECT * FROM ReligionMaster with(nolock) WHERE  RTRIM(LTRIM(ReligionName))=RTRIM(LTRIM(@ReligionName)) AND ReligionId<>@ReligionId AND IsActive = 1 ", para).ConvertToList<ReligionMaster>().AsQueryable();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IQueryable<ReligionMaster> DuplicateReligion(string ReligionName)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@ReligionName", ReligionName);
                return new dalc().GetDataTable_Text("SELECT * FROM ReligionMaster with(nolock) WHERE  RTRIM(LTRIM(ReligionName))=RTRIM(LTRIM(@ReligionName)) AND  IsActive = 1 ", para).ConvertToList<ReligionMaster>().AsQueryable();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void UpdateReligion(ReligionMaster obj)
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

        public IQueryable<ReligionMaster> GetAllReligion()
        {
            try
            {
              
                return new dalc().selectbyquerydt("SELECT * FROM ReligionMaster with(nolock) WHERE IsActive=1").ConvertToList<ReligionMaster>().AsQueryable();
            }
            catch
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
