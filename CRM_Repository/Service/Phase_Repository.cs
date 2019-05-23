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
    public class Phase_Repository : IPhase_Repository, IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;

        public Phase_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void AddPhase(PhaseMaster obj)
        {
            try
            {
                context.PhaseMasters.Add(obj);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public void UpdatePhase(PhaseMaster obj)
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

        public void DeletePhase(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@PhaseId", id);
                PhaseMaster Phase = new dalc().GetDataTable_Text("SELECT * FROM PhaseMaster with(nolock) WHERE PhaseId=@PhaseId", para).ConvertToList<PhaseMaster>().FirstOrDefault();
                if (Phase != null)
                {
                    context.PhaseMasters.Remove(Phase);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public PhaseMaster GetPhaseByID(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@PhaseId", id);
                var Phase = new dalc().GetDataTable_Text("SELECT * FROM PhaseMaster with(nolock) WHERE PhaseId=@PhaseId", para).ConvertToList<PhaseMaster>().FirstOrDefault();
                return Phase;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public IQueryable<PhaseMaster> GetAllPhase()
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@IsActive", "true");
                var Phase = new dalc().GetDataTable_Text("SELECT * FROM PhaseMaster with(nolock) WHERE IsActive=@IsActive", para).ConvertToList<PhaseMaster>().AsQueryable();
                return Phase;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public IQueryable<PhaseMaster> DuplicatePhase(string Phase)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@Phase", Phase);
                para[1] = new SqlParameter().CreateParameter("@IsActive", "true");
                var Phasedata = new dalc().GetDataTable_Text("SELECT * FROM Phasemaster with(nolock) WHERE Phase=@Phase and IsActive=@IsActive", para).ConvertToList<PhaseMaster>().AsQueryable();
                return Phasedata.AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public IQueryable<PhaseMaster> DuplicateEditPhase(int PhaseId, string Phase)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[3];
                para[0] = new SqlParameter().CreateParameter("@PhaseId", PhaseId);
                para[1] = new SqlParameter().CreateParameter("@Phase", Phase);
                para[2] = new SqlParameter().CreateParameter("@IsActive", "true");
                var Phasedata = new dalc().GetDataTable_Text("SELECT * FROM PhaseMaster with(nolock) WHERE PhaseId!=@PhaseId and Phase=@Phase and IsActive=@IsActive", para).ConvertToList<PhaseMaster>().AsQueryable();
                return Phasedata.AsQueryable();
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
