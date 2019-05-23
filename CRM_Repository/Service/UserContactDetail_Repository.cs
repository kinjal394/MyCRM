using CRM_Repository.Data;
using CRM_Repository.DataServices;
using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace CRM_Repository.Service
{
   public class UserContactDetail_Repository : IUserContactDetail_Repository,IDisposable
    {
        dalc odal = new dalc();
        private CRM_Repository.Data.elaunch_crmEntities context;
        public UserContactDetail_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }
        public IQueryable<UserReferenceRelationMaster> GetUserContactbyuserid(int UserId)
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var BuyerContact = context.BuyerContactDetails.Find(id);
                //    scope.Complete();
                //    return BuyerContact;
                //}
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@UserId", UserId);
                return odal.GetDataTable_Text(@"Select from UserReferenceRelationMaster 
                                        Where UserId =@UserId  And ISNULL(IsActive,0)=1", para).ConvertToList<UserReferenceRelationMaster>().AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }

        }
        public IQueryable<UserReferenceRelationMaster> GetUserContactbyid(int UserId)
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var BuyerContact = context.BuyerContactDetails.Find(id);
                //    scope.Complete();
                //    return BuyerContact;
                //}
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@UserId", UserId);
                return odal.GetDataTable_Text(@"Select ur.*,rl.RelationName[Relation],cm.CountryCallCode[UserContactcode] from UserReferenceRelationMaster ur
                                        left join RelationMaster rl with(nolock) on rl.RelationId=ur.RelationId 
                                        left join CountryMaster cm with(nolock) on cm.CountryId=ur.ContactCode 
                                        Where ur.UserId =@UserId  And ISNULL(ur.IsActive,0)=1",para).ConvertToList<UserReferenceRelationMaster>().AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }

        }
        public IQueryable<UserSalaryDetail> GetUserSalarybyuserid(int UserId)
        {
            try
            {
              
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@UserId", UserId);
                return odal.GetDataTable_Text(@"Select * from UserSalaryDetail 
                                        Where UserId =@UserId", para).ConvertToList<UserSalaryDetail>().AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }

        }
        public IQueryable<UserSalaryDetail> GetUserSalarybyid(int UserId)
        {
            try
            {
            
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@UserId", UserId);
                return odal.GetDataTable_Text(@"Select ur.*,rl.SalaryHeadName[SalaryHead],cr.CurrencyName[Currency] from UserSalaryDetail ur with(nolock)
                                        left join SalaryHeadMaster rl with(nolock) on rl.SalaryHeadId=ur.SalaryHeadId 
                                        left join CurrencyMaster cr with(nolock) on cr.CurrencyId=ur.CurrencyId 
                                        Where ur.UserId =@UserId", para).ConvertToList<UserSalaryDetail>().AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }

        }
        public IQueryable<UserDocDetail> GetUserDocbyid(int UserId)
        {
            try
            {

                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@UserId", UserId);
                return odal.GetDataTable_Text(@"Select ur.*,rl.DocName[Documents] from UserDocDetail ur with(nolock)
                                        left join DocumentNameMaster rl with(nolock) on rl.DocId=ur.DocId 
                                        Where ur.UserId =@UserId", para).ConvertToList<UserDocDetail>().AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }

        }
        public IQueryable<UserExperienceDetail> GetUserExperbyid(int UserId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@UserId", UserId);
                return odal.GetDataTable_Text(@"Select UE.*,D.DesignationName,ct.CityName,st.StateId,st.StateName,cm.CountryId,cm.CountryName from UserExperienceDetail As UE with(nolock)
                                                Left Join DesignationMaster AS D On D.DesignationId = UE.Designation
                                                left join CityMaster ct with(nolock) on ct.CityId=UE.CityId
                                                left join StateMaster st with(nolock) on st.StateId=ct.StateId
                                                left join CountryMaster cm with(nolock) on cm.CountryId=st.CountryId 
                                                Where UE.UserId =@UserId", para).ConvertToList<UserExperienceDetail>().AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }

        }
        public IQueryable<UserEducationDetail> GetUserEducationid(int UserId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@UserId", UserId);
                return odal.GetDataTable_Text(@"Select UE.*,D.QualificationName,ct.CityName,st.StateId,st.StateName,cm.CountryId,cm.CountryName 
                                                from UserEducationDetail As UE with(nolock)
                                                Left Join QualificationsMaster AS D On D.QualificationId = UE.QualificationId
                                                left join CityMaster ct with(nolock) on ct.CityId=UE.CityId
                                                left join StateMaster st with(nolock) on st.StateId=ct.StateId
                                                left join CountryMaster cm with(nolock) on cm.CountryId=st.CountryId 
                                                Where UE.UserId =@UserId", para).ConvertToList<UserEducationDetail>().AsQueryable();
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
