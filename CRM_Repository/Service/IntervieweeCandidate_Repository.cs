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
    public class IntervieweeCandidate_Repository : IIntervieweeCandidate_Repository, IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;

        public IntervieweeCandidate_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }
        public void AddIntervieweeCandidate(InterviweeCandidateMaster obj)
        {
            try
            {
                context.InterviweeCandidateMasters.Add(obj);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateIntervieweeCandidate(InterviweeCandidateMaster obj)
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
        public void DeleteIntervieweeCandidate(int id)
        {
            try
            {
                InterviweeCandidateMaster objWP = context.InterviweeCandidateMasters.Where(z => z.IntCandId == id).SingleOrDefault();
                //objWP.IsActive = false;
                context.Entry(objWP).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IntervieweeCandidateModel GetIntervieweeCandidateById(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@IntervieweeCandidateId", id);
                return new dalc().GetDataTable_Text(@"Select C.*,CI.CityId,CI.CityName,S.StateId,S.StateName,CO.CountryId,CO.CountryName,Q.QualificationName
                                                    ,(case when c.ReferenceTypeId=2 then ag.AgencyType end)AgencyTypeReferanceName,
                                                    (case when c.ReferenceTypeId=1 and c.ReferenceSubType=1 then vsg.CompanyName 
                                                    when c.ReferenceTypeId=1 and c.ReferenceSubType=2 then ssg.CompanyName 
                                                    when c.ReferenceTypeId=1 and c.ReferenceSubType=3 then usg.UserName
                                                    when c.ReferenceTypeId=1 and c.ReferenceSubType=4 then bsg.CompanyName
                                                    end)ReferanceName,SC.SourceName[Source]
                                                    FROM InterviweeCandidateMaster As C WITH(nolock)
                                                    Inner join citymaster as CI with(nolock) on CI.CityId = C.CityId
                                                    Inner join statemaster as S with(nolock) on S.StateId = CI.StateId
                                                    Inner join countrymaster as CO with(nolock) on CO.CountryId = S.CountryId
                                                    left join QualificationsMaster as Q with(nolock) on Q.QualificationId = C.QualificationId
                                                    left join UserMaster as usg with(nolock) on  C.ReferenceId = usg.UserId
                                                    left join AgencyTypeMaster as ag with(nolock) on  C.ReferenceId = ag.AgencyTypeId
                                                    left join BuyerMaster as bsg with(nolock) on  C.ReferenceId = bsg.BuyerId
                                                    left join SupplierMaster as ssg with(nolock) on  C.ReferenceId = ssg.SupplierId
                                                    left join VendorMaster as vsg with(nolock) on  C.ReferenceId = vsg.VendorId
                                                    left join Sourcemaster as SC with(nolock) on  C.SourceId = SC.SourceId
                                                    Where C.IntCandId = @IntervieweeCandidateId AND C.IsActive=1", para).ConvertToList<IntervieweeCandidateModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<InterviweeCandidateMaster> GetAllIntervieweeCandidate()
        {
            try
            {
                //SqlParameter[] para = new SqlParameter[1];
                //para[0] = new SqlParameter().CreateParameter("@DepartmentId", DepartmentId);
                //return new dalc().GetDataTable_Text("SELECT * FROM IntervieweeCandidateMaster with(nolock) WHERE IsActive=1 AND DepartmentId=@DepartmentId").ConvertToList<InterviweeCandidateMaster>().AsQueryable();
                return new dalc().selectbyquerydt("SELECT * FROM InterviweeCandidateMaster with(nolock) where IsActive=1").ConvertToList<InterviweeCandidateMaster>().AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int CreateUpdate(IntervieweeCandidateModel objInputIntervieweeCandidateMaster)
        {
            int resVal;
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    //IntCandId, ReferenceTypeId, ReferenceId, ReferenceSubType, ReferenceMannualEntry, CityId, Address, Pincode, MobileNo, 
                    //Email, CommunicateDate, FirstName, SurName, Gender, MaritalStatus, Birthdate, TotalWorkExperience, CurrentCTC, 
                    //CurrentExpected, NoticePeriod, UploadResume, IsActive
                    InterviweeCandidateMaster ObjIntervieweeCandidateMaster = new InterviweeCandidateMaster();
                    if (objInputIntervieweeCandidateMaster.IntCandId <= 0)
                    {
                        #region "INSERT"
                        ObjIntervieweeCandidateMaster.ReferenceTypeId = objInputIntervieweeCandidateMaster.ReferenceTypeId;
                        ObjIntervieweeCandidateMaster.ReferenceId = objInputIntervieweeCandidateMaster.ReferenceId;
                        ObjIntervieweeCandidateMaster.ReferenceSubType = objInputIntervieweeCandidateMaster.ReferenceSubType;
                        ObjIntervieweeCandidateMaster.ReferenceMannualEntry = objInputIntervieweeCandidateMaster.ReferenceMannualEntry;
                        ObjIntervieweeCandidateMaster.CityId = objInputIntervieweeCandidateMaster.CityId;
                        ObjIntervieweeCandidateMaster.Address = objInputIntervieweeCandidateMaster.Address;
                        ObjIntervieweeCandidateMaster.Pincode = objInputIntervieweeCandidateMaster.Pincode;
                        ObjIntervieweeCandidateMaster.MobileNo = objInputIntervieweeCandidateMaster.MobileNo;
                        ObjIntervieweeCandidateMaster.Email = objInputIntervieweeCandidateMaster.Email;
                        ObjIntervieweeCandidateMaster.CommunicateDate = objInputIntervieweeCandidateMaster.CommunicateDate;
                        ObjIntervieweeCandidateMaster.FirstName = objInputIntervieweeCandidateMaster.FirstName;
                        ObjIntervieweeCandidateMaster.SurName = objInputIntervieweeCandidateMaster.SurName;
                        ObjIntervieweeCandidateMaster.Gender = objInputIntervieweeCandidateMaster.Gender;
                        ObjIntervieweeCandidateMaster.MaritalStatus = objInputIntervieweeCandidateMaster.MaritalStatus;
                        ObjIntervieweeCandidateMaster.QualificationId = objInputIntervieweeCandidateMaster.QualificationId;
                        ObjIntervieweeCandidateMaster.Birthdate = objInputIntervieweeCandidateMaster.Birthdate;
                        ObjIntervieweeCandidateMaster.TotalWorkExperience = objInputIntervieweeCandidateMaster.TotalWorkExperience;
                        ObjIntervieweeCandidateMaster.CurrentCTC = objInputIntervieweeCandidateMaster.CurrentCTC;
                        ObjIntervieweeCandidateMaster.CurrentExpected = objInputIntervieweeCandidateMaster.CurrentExpected;
                        ObjIntervieweeCandidateMaster.NoticePeriod = objInputIntervieweeCandidateMaster.NoticePeriod;
                        ObjIntervieweeCandidateMaster.UploadResume = objInputIntervieweeCandidateMaster.UploadResume;
                        ObjIntervieweeCandidateMaster.SourceId = objInputIntervieweeCandidateMaster.SourceId;
                        ObjIntervieweeCandidateMaster.CandidateRefNo = objInputIntervieweeCandidateMaster.CandidateRefno;
                        ObjIntervieweeCandidateMaster.Chat = objInputIntervieweeCandidateMaster.Chat;
                        ObjIntervieweeCandidateMaster.IsActive = true;
                        AddIntervieweeCandidate(ObjIntervieweeCandidateMaster);

                        resVal = 1;
                        #endregion
                    }
                    else
                    {
                        #region "UPDATE"
                        //EDIT
                        ObjIntervieweeCandidateMaster = context.InterviweeCandidateMasters.Find(objInputIntervieweeCandidateMaster.IntCandId);
                        ObjIntervieweeCandidateMaster.ReferenceTypeId = objInputIntervieweeCandidateMaster.ReferenceTypeId;
                        ObjIntervieweeCandidateMaster.ReferenceId = objInputIntervieweeCandidateMaster.ReferenceId;
                        ObjIntervieweeCandidateMaster.ReferenceSubType = objInputIntervieweeCandidateMaster.ReferenceSubType;
                        ObjIntervieweeCandidateMaster.ReferenceMannualEntry = objInputIntervieweeCandidateMaster.ReferenceMannualEntry;
                        ObjIntervieweeCandidateMaster.CityId = objInputIntervieweeCandidateMaster.CityId;
                        ObjIntervieweeCandidateMaster.Address = objInputIntervieweeCandidateMaster.Address;
                        ObjIntervieweeCandidateMaster.Pincode = objInputIntervieweeCandidateMaster.Pincode;
                        ObjIntervieweeCandidateMaster.MobileNo = objInputIntervieweeCandidateMaster.MobileNo;
                        ObjIntervieweeCandidateMaster.Email = objInputIntervieweeCandidateMaster.Email;
                        ObjIntervieweeCandidateMaster.CommunicateDate = objInputIntervieweeCandidateMaster.CommunicateDate;
                        ObjIntervieweeCandidateMaster.FirstName = objInputIntervieweeCandidateMaster.FirstName;
                        ObjIntervieweeCandidateMaster.SurName = objInputIntervieweeCandidateMaster.SurName;
                        ObjIntervieweeCandidateMaster.Gender = objInputIntervieweeCandidateMaster.Gender;
                        ObjIntervieweeCandidateMaster.MaritalStatus = objInputIntervieweeCandidateMaster.MaritalStatus;
                        ObjIntervieweeCandidateMaster.QualificationId = objInputIntervieweeCandidateMaster.QualificationId;
                        ObjIntervieweeCandidateMaster.Birthdate = objInputIntervieweeCandidateMaster.Birthdate;
                        ObjIntervieweeCandidateMaster.TotalWorkExperience = objInputIntervieweeCandidateMaster.TotalWorkExperience;
                        ObjIntervieweeCandidateMaster.CurrentCTC = objInputIntervieweeCandidateMaster.CurrentCTC;
                        ObjIntervieweeCandidateMaster.CurrentExpected = objInputIntervieweeCandidateMaster.CurrentExpected;
                        ObjIntervieweeCandidateMaster.NoticePeriod = objInputIntervieweeCandidateMaster.NoticePeriod;
                        ObjIntervieweeCandidateMaster.UploadResume = objInputIntervieweeCandidateMaster.UploadResume;
                        ObjIntervieweeCandidateMaster.SourceId = objInputIntervieweeCandidateMaster.SourceId;
                        ObjIntervieweeCandidateMaster.CandidateRefNo = objInputIntervieweeCandidateMaster.CandidateRefno;
                        ObjIntervieweeCandidateMaster.Chat = objInputIntervieweeCandidateMaster.Chat;
                        UpdateIntervieweeCandidate(ObjIntervieweeCandidateMaster);

                        resVal = 2;
                        #endregion 
                    }
                    context.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception ex )
                {
                    ex.SetLog("test");
                    dbContextTransaction.Rollback();
                    resVal = 0;
                }
            }
            return resVal;
        }
        public IntervieweeCandidateModel GetIntervieweeCandidateByCode(string id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@CandidateRefNo", id);
                return new dalc().GetDataTable_Text(@"Select C.*,CI.CityId,CI.CityName,S.StateId,S.StateName,CO.CountryId,CO.CountryName,Q.QualificationName
                                                    ,(case when c.ReferenceTypeId=2 then ag.AgencyType end)AgencyTypeReferanceName,
                                                    (case when c.ReferenceTypeId=1 and c.ReferenceSubType=1 then vsg.CompanyName 
                                                    when c.ReferenceTypeId=1 and c.ReferenceSubType=2 then ssg.CompanyName 
                                                    when c.ReferenceTypeId=1 and c.ReferenceSubType=3 then usg.UserName
                                                    when c.ReferenceTypeId=1 and c.ReferenceSubType=4 then bsg.CompanyName
                                                    end)ReferanceName,SC.SourceName[Source]
                                                    FROM InterviweeCandidateMaster As C WITH(nolock)
                                                    Inner join citymaster as CI with(nolock) on CI.CityId = C.CityId
                                                    Inner join statemaster as S with(nolock) on S.StateId = CI.StateId
                                                    Inner join countrymaster as CO with(nolock) on CO.CountryId = S.CountryId
                                                    left join QualificationsMaster as Q with(nolock) on Q.QualificationId = C.QualificationId
                                                    left join UserMaster as usg with(nolock) on  C.ReferenceId = usg.UserId
                                                    left join AgencyTypeMaster as ag with(nolock) on  C.ReferenceId = ag.AgencyTypeId
                                                    left join BuyerMaster as bsg with(nolock) on  C.ReferenceId = bsg.BuyerId
                                                    left join SupplierMaster as ssg with(nolock) on  C.ReferenceId = ssg.SupplierId
                                                    left join VendorMaster as vsg with(nolock) on  C.ReferenceId = vsg.VendorId
                                                    left join Sourcemaster as SC with(nolock) on  C.SourceId = SC.SourceId
                                                    Where C.CandidateRefNo = @CandidateRefNo AND C.IsActive=1", para).ConvertToList<IntervieweeCandidateModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Delete(int IntCandId)
        {
            int resVal;
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    InterviweeCandidateMaster ObjInterviweeCandidate = context.InterviweeCandidateMasters.Find(IntCandId);
                    ObjInterviweeCandidate.IsActive = false;
                    UpdateIntervieweeCandidate(ObjInterviweeCandidate);

                    resVal = 1;
                    context.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                    resVal = 0;
                }
            }
            return resVal;
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
