using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;
using System.Data.Entity;
using CRM_Repository.ServiceContract;
using CRM_Repository.DataServices;
using System.Transactions;
using System.Data.SqlClient;

namespace CRM_Repository.Service
{
    public class City_Repository : ICity_Repository, IDisposable
    {
        dalc odal = new dalc();
        private elaunch_crmEntities context;
        public City_Repository(elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void InsertCity(CityMaster objcity)
        {
            context.CityMasters.Add(objcity);
            context.SaveChanges();
            
        }

        public void UpdateCity(CityMaster objcity)
        {
            try
            {
                context.Entry(objcity).State = EntityState.Modified;
                CityMaster objArea = context.CityMasters.Where(z => z.CityId == objcity.CityId).SingleOrDefault();
                objArea.CityName = objcity.CityName;
                context.Entry(objArea).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
            
        }



        public CityMaster GetCityByID(int CityId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@CityId", CityId);
                return odal.GetDataTable_Text(@"SELECT CI.CityId,S.StateId,C.CountryId,C.CountryName,S.StateName,
                                            CI.CityName,CI.IsActive 
                                            FROM CountryMaster as C
                                            INNER JOIN StateMaster as S on C.CountryId = S.CountryId
                                            INNER JOIN CityMaster as CI on CI.StateId = S.StateId 
                                            WHERE CI.CityId =@CityId",para).ConvertToList<CityMaster>().AsQueryable().FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IQueryable<CityMaster> GetAllCities()
        {
            try
            {
               
                return new dalc().selectbyquerydt("SELECT * FROM CityMaster with(nolock) where IsActive=1").ConvertToList<CityMaster>().AsQueryable();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<CityMaster> GetCityByStateID(int StateID)
        {
            try
            {
               
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@StateID", StateID);
                return new dalc().GetDataTable_Text("SELECT * FROM RoleMaster with(nolock) WHERE StateID =StateID AND IsActive = 1", para).ConvertToList<CityMaster>().AsQueryable(); 


            }
            catch (Exception)
            {
                return null;
            }
        }


        public IQueryable<CityMaster> CheckForDuplicateCity(int CityId, string CityName)
        {
            try
            {
                
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@CityName", CityName);
                para[1] = new SqlParameter().CreateParameter("@CityId", CityId);
                return new dalc().GetDataTable_Text("SELECT * FROM CityMaster with(nolock) WHERE RTRIM(LTRIM(CityName)) = RTRIM(LTRIM(@CityName)) AND CityId<>@CityId", para).ConvertToList<CityMaster>().AsQueryable();

            }
            catch (Exception ex)
            {
                throw ex;
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
