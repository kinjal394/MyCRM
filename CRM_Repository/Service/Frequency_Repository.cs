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
   public class Frequency_Repository:IFrequency_Repository,IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;

        public Frequency_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void AddFrequency(FrequencyMaster obj)
        {
            try
            {
                context.FrequencyMasters.Add(obj);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public void UpdateFrequency(FrequencyMaster obj)
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

        public void DeleteFrequency(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@FrequencyId", id);
                FrequencyMaster Frequency = new dalc().GetDataTable_Text("SELECT * FROM FrequencyMaster with(nolock) WHERE FrequencyId=@FrequencyId", para).ConvertToList<FrequencyMaster>().FirstOrDefault();
                if (Frequency != null)
                {
                    context.FrequencyMasters.Remove(Frequency);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public FrequencyMaster GetFrequencyByID(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@FrequencyId", id);
                var Frequency = new dalc().GetDataTable_Text("SELECT * FROM FrequencyMaster with(nolock) WHERE FrequencyId=@FrequencyId", para).ConvertToList<FrequencyMaster>().FirstOrDefault();
                return Frequency;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public IQueryable<FrequencyMaster> GetAllFrequency()
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@IsActive", "true");
                var Frequency = new dalc().GetDataTable_Text("SELECT * FROM FrequencyMaster with(nolock) WHERE IsActive=@IsActive", para).ConvertToList<FrequencyMaster>().AsQueryable();
                return Frequency;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public IQueryable<FrequencyMaster> DuplicateFrequency(string Frequency)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@Frequency", Frequency);
                para[1] = new SqlParameter().CreateParameter("@IsActive", "true");
                var Frequencydata = new dalc().GetDataTable_Text("SELECT * FROM FrequencyMaster with(nolock) WHERE Frequency=@Frequency and IsActive=@IsActive", para).ConvertToList<FrequencyMaster>().AsQueryable();
                return Frequencydata.AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public IQueryable<FrequencyMaster> DuplicateEditFrequency(int FrequencyId, string Frequency)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[3];
                para[0] = new SqlParameter().CreateParameter("@FrequencyId", FrequencyId);
                para[1] = new SqlParameter().CreateParameter("@Frequency", Frequency);
                para[2] = new SqlParameter().CreateParameter("@IsActive", "true");
                var Frequencydata = new dalc().GetDataTable_Text("SELECT * FROM FrequencyMaster with(nolock) WHERE FrequencyId!=@FrequencyId and Frequency=@Frequency and IsActive=@IsActive", para).ConvertToList<FrequencyMaster>().AsQueryable();
                //var AgencyType = context.AgencyTypeMasters.Where(x => x.AgencyTypeId != AgencyTypeId && x.AgencyType == AgencyTypeName && x.IsActive == true);
                return Frequencydata.AsQueryable();
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
