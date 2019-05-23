using CRM_Repository.Data;
using CRM_Repository.DataServices;
using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.Service
{
    public class UserRefferenceDetail_Repository : IUserRefferenceDetail_Repository, IDisposable
    {
        dalc odal = new dalc();
        private CRM_Repository.Data.elaunch_crmEntities context;
        public UserRefferenceDetail_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }
        public IQueryable<UserRefferenceDetail> GetUserRefferencebyid(int UserId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@UserId", UserId);
                return odal.GetDataTable_Text(@"Select from UserRefferenceDetail 
                                        Where UserId =@UserId", para).ConvertToList<UserRefferenceDetail>().AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public IQueryable<UserRefferenceDetail> GetUserRefferencebyuserid(int UserId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@UserId", UserId);
                return odal.GetDataTable_Text(@"Select ur.*,rl.ReffTypeName,ct.CityName,st.StateId,st.StateName,cm.CountryId,cm.CountryName 
                                                ,ccm.CountryId MobileNoId from UserRefferenceDetail ur
                                                left join EmpReffTypeMaster rl with(nolock) on rl.ReffTypeId=ur.ReffType 
                                                left join gurjari_crmuser.CityMaster ct with(nolock) on ct.CityId=ur.CityId
                                                left join gurjari_crmuser.StateMaster st with(nolock) on st.StateId=ct.StateId
                                                left join CountryMaster cm with(nolock) on cm.CountryId=st.CountryId 
                                                left join CountryMaster ccm with(nolock) on ccm.CountryCallCode=LEFT(MobileNo,CHARINDEX(' ',MobileNo)-1)
                                                Where ur.UserId =@UserId", para).ConvertToList<UserRefferenceDetail>().AsQueryable();
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
