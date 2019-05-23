using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;
using System.Data;
using System.Data.Entity;
using CRM_Repository.DataServices;
using CRM_Repository.ServiceContract;
using System.Transactions;
using System.Data.SqlClient;

namespace CRM_Repository.Service
{
    public class State_Repository : IState_Repository, IDisposable
    {
        dalc odal = new dalc();
        private elaunch_crmEntities context;
        public State_Repository(elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void InsertState(StateMaster objstate)
        {
            try
            {
                context.StateMasters.Add(objstate);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void UpdateState(StateMaster objstate)
        {
            try
            {
                context.Entry(objstate).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<StateMaster> GetAllState()
        {
            try
            {
                return odal.selectbyquerydt(@"select S.StateId,C.CountryId,C.CountryName,S.StateName,S.IsActive from CountryMaster as C with(nolock) 
inner join StateMaster as S with(nolock)  on C.CountryId = S.CountryId").ConvertToList<StateMaster>().AsQueryable();
            }
            catch (Exception )
            {
                return null;
            }
        }

        public StateMaster GetStateByID(int StateID)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@StateID", StateID);
                return new dalc().GetDataTable_Text("SELECT * FROM StateMaster with(nolock) WHERE StateID=@StateID", para).ConvertToList<StateMaster>().FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<StateMaster> CheckForDuplicateState(int StateID, string Statename)
        {
            try
            {
               
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@StateName", Statename.ToString());
                para[1] = new SqlParameter().CreateParameter("@StateId", StateID);
                // return new dalc().selectbyquerydt("SELECT * FROM StateMaster with(nolock) where RTRIM(LTRIM(StateName)) = RTRIM(LTRIM('"+ Statename + "')) AND StateId<>" + StateID + " AND IsActive=1").ConvertToList<StateMaster>().AsQueryable();
                return new dalc().GetDataTable_Text("SELECT * FROM StateMaster with(nolock) where RTRIM(LTRIM(StateName)) = RTRIM(LTRIM(@StateName)) AND StateId<>@StateId AND IsActive=1",para).ConvertToList<StateMaster>().AsQueryable();

            }
            catch (Exception)
            {
                return null;
            }
        }

        public IQueryable<StateMaster> GetStateByCountryID(int CountryID)
        {
            try
            {
               SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@CountryId", CountryID);
                return new dalc().GetDataTable_Text("SELECT * FROM StateMaster with(nolock) where  CountryId=@CountryId AND IsActive=1",para).ConvertToList<StateMaster>().AsQueryable();

            }
            catch (Exception)
            {
                return null;
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
