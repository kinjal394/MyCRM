using CRM_Repository.Data;
using CRM_Repository.DataServices;
using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Data;
using CRM_Repository.DTOModel;

namespace CRM_Repository.Service
{
    public class User_Repository : IUser_Repository, IDisposable
    {
        dalc odal = new dalc();
        private elaunch_crmEntities context;
        // private CRM_Repository.Data.elaunch_crmEntities context;

        private IUserContactDetail_Repository _IUserContactDetail_Repository;
        public User_Repository(elaunch_crmEntities _context)
        {
            context = _context;
            this._IUserContactDetail_Repository = new UserContactDetail_Repository(_context);
        }
        public User_Repository()
        {

        }

        public UserMaster AddUser(UserMaster objuser)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {

                    objuser.Name = (objuser.Name == null || objuser.Name == "") ? "" : objuser.Name;
                    objuser.Email = (objuser.Email == null || objuser.Email == "") ? "" : objuser.Email;
                    objuser.FatherName = (objuser.FatherName == null || objuser.FatherName == "") ? "" : objuser.FatherName;
                    objuser.Gender = (objuser.Gender == null) ? 0 : objuser.Gender;
                    objuser.MaritalStatus = (objuser.MaritalStatus == null) ? 0 : objuser.MaritalStatus;
                    context.UserMasters.Add(objuser);
                    context.SaveChanges();
                    if (objuser.UserContactDetails != null)
                    {
                        foreach (var item in objuser.UserContactDetails)
                        {
                            UserReferenceRelationMaster ObjUserContactDetail = new UserReferenceRelationMaster();
                            ObjUserContactDetail.UserId = objuser.UserId;
                            ObjUserContactDetail.RelationId = item.RelationId;
                            ObjUserContactDetail.Name = item.Name;
                            ObjUserContactDetail.Email = item.Email;
                            ObjUserContactDetail.ContactCode = 101;
                            ObjUserContactDetail.ContactNo = item.ContactNo;
                            ObjUserContactDetail.CreatedBy = objuser.CreatedBy;
                            ObjUserContactDetail.CreatedDate = objuser.CreatedDate;
                            ObjUserContactDetail.IsActive = true;
                            context.UserReferenceRelationMasters.Add(ObjUserContactDetail);
                            context.SaveChanges();
                        }
                    }
                    if (objuser.UserReferanceDetails != null)
                    {
                        foreach (var item in objuser.UserReferanceDetails)
                        {
                            UserRefferenceDetail ObjUserRefDetail = new UserRefferenceDetail();
                            ObjUserRefDetail.UserId = objuser.UserId;
                            ObjUserRefDetail.ReffType = item.ReffType;
                            ObjUserRefDetail.ReffName = item.ReffName;
                            ObjUserRefDetail.Email = item.Email;
                            ObjUserRefDetail.MobileNo = item.MobileNo;
                            ObjUserRefDetail.Address = item.Address;
                            ObjUserRefDetail.CityId = item.CityId;
                            ObjUserRefDetail.Pincode = item.Pincode;
                            context.UserRefferenceDetails.Add(ObjUserRefDetail);
                            context.SaveChanges();
                        }
                    }
                    if (objuser.UserSalDetails != null)
                    {
                        foreach (var item in objuser.UserSalDetails)
                        {
                            UserSalaryDetail ObjUserSalaryDetail = new UserSalaryDetail();
                            ObjUserSalaryDetail.UserId = objuser.UserId;
                            ObjUserSalaryDetail.SalaryHeadId = item.SalaryHeadId;
                            ObjUserSalaryDetail.Amount = item.Amount;
                            ObjUserSalaryDetail.CurrencyId = item.CurrencyId;
                            ObjUserSalaryDetail.ExchangeRate = item.ExchangeRate;
                            ObjUserSalaryDetail.INRAmount = item.INRAmount;
                            context.UserSalaryDetails.Add(ObjUserSalaryDetail);
                            context.SaveChanges();
                        }
                    }
                    if (objuser.UserDocumentDetails != null)
                    {
                        foreach (var item in objuser.UserDocumentDetails)
                        {
                            UserDocDetail ObjUserDocDetail = new UserDocDetail();
                            ObjUserDocDetail.UserId = objuser.UserId;
                            ObjUserDocDetail.DocId = item.DocId;
                            ObjUserDocDetail.DocValue = item.DocValue;
                            if (item.DocUpload != null)
                            {
                                ObjUserDocDetail.DocUpload = item.DocUpload;
                            }
                            context.UserDocDetails.Add(ObjUserDocDetail);
                            context.SaveChanges();
                        }
                    }
                    if (objuser.UserExperDetails != null)
                    {
                        foreach (var item in objuser.UserExperDetails)
                        {
                            //EduId, UserId, OrganizationName, CityId, PinCode, Address, FromDate, ToDate, TotalWorkExperience, Designation, Description
                            UserExperienceDetail ObjUserExperienceDetail = new UserExperienceDetail();
                            ObjUserExperienceDetail.UserId = objuser.UserId;
                            ObjUserExperienceDetail.OrganizationName = item.OrganizationName;
                            ObjUserExperienceDetail.CityId = item.CityId;
                            ObjUserExperienceDetail.PinCode = item.PinCode;
                            ObjUserExperienceDetail.Address = item.Address;
                            ObjUserExperienceDetail.FromDate = item.FromDate;
                            ObjUserExperienceDetail.ToDate = item.ToDate;
                            ObjUserExperienceDetail.TotalWorkExperience = item.TotalWorkExperience;
                            ObjUserExperienceDetail.Designation = item.Designation;
                            ObjUserExperienceDetail.Description = item.Description;
                            context.UserExperienceDetails.Add(ObjUserExperienceDetail);
                            context.SaveChanges();
                        }
                    }
                    if (objuser.UserEduDetails != null)
                    {
                        foreach (var item in objuser.UserEduDetails)
                        {
                            //EducationId, UserId, QualificationId, InstituteName, CityId, PinCode, Address, FromDate, Todate, EduDescription
                            UserEducationDetail ObjUserEducationDetail = new UserEducationDetail();
                            ObjUserEducationDetail.UserId = objuser.UserId;
                            ObjUserEducationDetail.QualificationId = item.QualificationId;
                            ObjUserEducationDetail.InstituteName = item.InstituteName;
                            ObjUserEducationDetail.CityId = item.CityId;
                            ObjUserEducationDetail.Address = item.Address;
                            ObjUserEducationDetail.PinCode = item.PinCode;
                            ObjUserEducationDetail.FromDate = item.FromDate;
                            ObjUserEducationDetail.Todate = item.Todate;
                            ObjUserEducationDetail.EduDescription = item.EduDescription;
                            context.UserEducationDetails.Add(ObjUserEducationDetail);
                            context.SaveChanges();
                        }
                    }
                    dbContextTransaction.Commit();
                    //context.UserReferenceRelationMasters.Add(objuser)
                }
                catch (Exception ex)
                {
                    string msg = "";
                    if (ex is System.Data.Entity.Validation.DbEntityValidationException)
                    {
                        foreach (var validationErrors in ((System.Data.Entity.Validation.DbEntityValidationException)ex).EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                msg += ";" + string.Format("{0}:{1}",
                                    validationErrors.Entry.Entity.ToString(),
                                    validationError.ErrorMessage);
                            }
                        }
                    }
                    dbContextTransaction.Rollback();
                }
            }
            return objuser;

        }
        public UserMaster UpdateUser(UserMaster objuser)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    objuser.Name = (objuser.Name == null || objuser.Name == "") ? "" : objuser.Name;
                    objuser.FatherName = (objuser.FatherName == null || objuser.FatherName == "") ? "" : objuser.FatherName;
                    objuser.Gender = (objuser.Gender == null) ? 0 : objuser.Gender;
                    objuser.MaritalStatus = (objuser.MaritalStatus == null) ? 0 : objuser.MaritalStatus;                  
                    context.Entry(objuser).State = System.Data.Entity.EntityState.Modified;//EntityState.Modified;
                    if (objuser.UserContactDetails != null)
                    {
                        foreach (var item in objuser.UserContactDetails)
                        {
                            if (item.Status == 1)
                            {
                                //ADD
                                UserReferenceRelationMaster ObjUserContactDetail = new UserReferenceRelationMaster();
                                //ObjUserContactDetail.EmpRelationId = item.EmpRelationId;
                                ObjUserContactDetail.UserId = objuser.UserId;
                                ObjUserContactDetail.RelationId = item.RelationId;
                                ObjUserContactDetail.Name = item.Name;
                                ObjUserContactDetail.Email = item.Email;
                                ObjUserContactDetail.ContactCode = 101;
                                ObjUserContactDetail.ContactNo = item.ContactNo;
                                ObjUserContactDetail.CreatedBy = objuser.CreatedBy;
                                ObjUserContactDetail.CreatedDate = objuser.CreatedDate;
                                ObjUserContactDetail.ModifyBy = objuser.ModifyBy;
                                ObjUserContactDetail.ModifyDate = objuser.ModifyDate;
                                ObjUserContactDetail.IsActive = true;
                                context.UserReferenceRelationMasters.Add(ObjUserContactDetail);
                            }
                            else if (item.Status == 2 || item.Status == 3)
                            {
                                UserReferenceRelationMaster ObjUserContactDetail = new UserReferenceRelationMaster();
                                ObjUserContactDetail.EmpRelationId = item.EmpRelationId;
                                ObjUserContactDetail.UserId = objuser.UserId;
                                ObjUserContactDetail.RelationId = item.RelationId;
                                ObjUserContactDetail.Name = item.Name;
                                ObjUserContactDetail.Email = item.Email;
                                ObjUserContactDetail.ContactCode = 101;
                                ObjUserContactDetail.ContactNo = item.ContactNo;
                                ObjUserContactDetail.CreatedBy = objuser.CreatedBy;
                                ObjUserContactDetail.CreatedDate = objuser.CreatedDate;
                                ObjUserContactDetail.ModifyBy = objuser.ModifyBy;
                                ObjUserContactDetail.ModifyDate = objuser.ModifyDate;
                                ObjUserContactDetail.IsActive = true;
                                if (item.Status == 3)
                                {
                                    ObjUserContactDetail.DeletedBy = objuser.CreatedBy;
                                    ObjUserContactDetail.DeletedDate = objuser.CreatedDate;
                                    ObjUserContactDetail.IsActive = false;
                                }

                                //_IBuyerContactDetail_Repository.UpdateBuyerContactDetail(ObjBuyerContactDetail);
                                context.Entry(ObjUserContactDetail).State = System.Data.Entity.EntityState.Modified;

                            }

                        }
                    }
                    if (objuser.UserReferanceDetails != null)
                    {
                        foreach (var item in objuser.UserReferanceDetails)
                        {
                            if (item.Status == 1)
                            {
                                //ADD
                                UserRefferenceDetail ObjUserRefDetail = new UserRefferenceDetail();
                                //ObjUserContactDetail.EmpRelationId = item.EmpRelationId;
                                ObjUserRefDetail.UserId = objuser.UserId;
                                ObjUserRefDetail.ReffType = item.ReffType;
                                ObjUserRefDetail.ReffName = item.ReffName;
                                ObjUserRefDetail.Email = item.Email;
                                ObjUserRefDetail.MobileNo = item.MobileNo;
                                ObjUserRefDetail.Address = item.Address;
                                ObjUserRefDetail.CityId = item.CityId;
                                ObjUserRefDetail.Pincode = item.Pincode;
                                context.UserRefferenceDetails.Add(ObjUserRefDetail);
                            }
                            else if (item.Status == 2 || item.Status == 3)
                            {
                                UserRefferenceDetail ObjUserRefDetail = new UserRefferenceDetail();
                                ObjUserRefDetail.ReffId = item.ReffId;
                                ObjUserRefDetail.UserId = objuser.UserId;
                                ObjUserRefDetail.ReffType = item.ReffType;
                                ObjUserRefDetail.ReffName = item.ReffName;
                                ObjUserRefDetail.Email = item.Email;
                                ObjUserRefDetail.MobileNo = item.MobileNo;
                                ObjUserRefDetail.Address = item.Address;
                                ObjUserRefDetail.CityId = item.CityId;
                                ObjUserRefDetail.Pincode = item.Pincode;
                                context.Entry(ObjUserRefDetail).State = System.Data.Entity.EntityState.Modified;
                                if (item.Status == 3)
                                {
                                    context.UserRefferenceDetails.Remove(ObjUserRefDetail);

                                    //_IBuyerContactDetail_Repository.UpdateBuyerContactDetail(ObjBuyerContactDetail);

                                }

                            }
                        }
                    }
                    if (objuser.UserSalDetails != null)
                    {
                        foreach (var item in objuser.UserSalDetails)
                        {
                            if (item.Status == 1)
                            {
                                //ADD
                                UserSalaryDetail ObjUserSalaryDetail = new UserSalaryDetail();
                                //ObjUserSalaryDetail.EmpSalId = item.EmpSalId;
                                ObjUserSalaryDetail.UserId = objuser.UserId;
                                ObjUserSalaryDetail.SalaryHeadId = item.SalaryHeadId;
                                ObjUserSalaryDetail.Amount = item.Amount;
                                ObjUserSalaryDetail.CurrencyId = item.CurrencyId;
                                ObjUserSalaryDetail.ExchangeRate = item.ExchangeRate;
                                ObjUserSalaryDetail.INRAmount = item.INRAmount;
                                context.UserSalaryDetails.Add(ObjUserSalaryDetail);
                            }
                            else if (item.Status == 2 || item.Status == 3)
                            {
                                UserSalaryDetail ObjUserSalaryDetail = new UserSalaryDetail();
                                ObjUserSalaryDetail.EmpSalId = item.EmpSalId;
                                ObjUserSalaryDetail.UserId = objuser.UserId;
                                ObjUserSalaryDetail.SalaryHeadId = item.SalaryHeadId;
                                ObjUserSalaryDetail.Amount = item.Amount;
                                ObjUserSalaryDetail.CurrencyId = item.CurrencyId;
                                ObjUserSalaryDetail.ExchangeRate = item.ExchangeRate;
                                ObjUserSalaryDetail.INRAmount = item.INRAmount;
                                context.Entry(ObjUserSalaryDetail).State = System.Data.Entity.EntityState.Modified;
                                if (item.Status == 3)
                                {
                                    context.UserSalaryDetails.Remove(ObjUserSalaryDetail);
                                }

                            }
                        }
                    }
                    if (objuser.UserDocumentDetails != null)
                    {
                        foreach (var item in objuser.UserDocumentDetails)
                        {
                            if (item.Status == 1)
                            {
                                //ADD
                                UserDocDetail ObjUserDocDetail = new UserDocDetail();
                                //ObjUserDocDetail.EmpDocId = item.EmpDocId;
                                ObjUserDocDetail.UserId = objuser.UserId;
                                ObjUserDocDetail.DocId = item.DocId;
                                ObjUserDocDetail.DocValue = item.DocValue;
                                if (item.DocUpload != null)
                                {
                                    ObjUserDocDetail.DocUpload = item.DocUpload;
                                }
                                context.UserDocDetails.Add(ObjUserDocDetail);
                            }
                            else if (item.Status == 2 || item.Status == 3)
                            {
                                UserDocDetail ObjUserDocDetail = new UserDocDetail();
                                ObjUserDocDetail.EmpDocId = item.EmpDocId;
                                ObjUserDocDetail.UserId = objuser.UserId;
                                ObjUserDocDetail.DocId = item.DocId;
                                ObjUserDocDetail.DocValue = item.DocValue;
                                if (item.DocUpload != null)
                                {
                                    ObjUserDocDetail.DocUpload = item.DocUpload;
                                }
                                context.Entry(ObjUserDocDetail).State = System.Data.Entity.EntityState.Modified;
                                if (item.Status == 3)
                                {
                                    context.UserDocDetails.Remove(ObjUserDocDetail);
                                }

                            }
                        }
                    }
                    if (objuser.UserExperDetails != null)
                    {
                        foreach (var item in objuser.UserExperDetails)
                        {
                            if (item.Status == 1)
                            {
                                //ADD
                                //EduId, UserId, OrganizationName, CityId, PinCode, Address, FromDate, ToDate, TotalWorkExperience, Designation, Description
                                UserExperienceDetail ObjUserExperienceDetail = new UserExperienceDetail();
                                ObjUserExperienceDetail.UserId = objuser.UserId;
                                ObjUserExperienceDetail.OrganizationName = item.OrganizationName;
                                ObjUserExperienceDetail.CityId = item.CityId;
                                ObjUserExperienceDetail.Address = item.Address;
                                ObjUserExperienceDetail.FromDate = item.FromDate;
                                ObjUserExperienceDetail.ToDate = item.ToDate;
                                ObjUserExperienceDetail.PinCode = item.PinCode;
                                ObjUserExperienceDetail.TotalWorkExperience = item.TotalWorkExperience;
                                ObjUserExperienceDetail.Designation = item.Designation;
                                ObjUserExperienceDetail.Description = item.Description;
                                context.UserExperienceDetails.Add(ObjUserExperienceDetail);
                                context.SaveChanges();
                            }
                            else if (item.Status == 2 || item.Status == 3)
                            {
                                UserExperienceDetail ObjUserExperienceDetail = new UserExperienceDetail();
                                ObjUserExperienceDetail.EduId = item.EduId;
                                ObjUserExperienceDetail.UserId = objuser.UserId;
                                ObjUserExperienceDetail.OrganizationName = item.OrganizationName;
                                ObjUserExperienceDetail.CityId = item.CityId;
                                ObjUserExperienceDetail.Address = item.Address;
                                ObjUserExperienceDetail.FromDate = item.FromDate;
                                ObjUserExperienceDetail.ToDate = item.ToDate;
                                ObjUserExperienceDetail.PinCode = item.PinCode;
                                ObjUserExperienceDetail.TotalWorkExperience = item.TotalWorkExperience;
                                ObjUserExperienceDetail.Designation = item.Designation;
                                ObjUserExperienceDetail.Description = item.Description;
                                context.Entry(ObjUserExperienceDetail).State = System.Data.Entity.EntityState.Modified;
                                if (item.Status == 3)
                                {
                                    context.UserExperienceDetails.Remove(ObjUserExperienceDetail);
                                }
                            }
                        }
                    }
                    if (objuser.UserEduDetails != null)
                    {
                        foreach (var item in objuser.UserEduDetails)
                        {
                            if (item.Status == 1)
                            {
                                //ADD
                                //EducationId, UserId, QualificationId, InstituteName, CityId, PinCode, Address, FromDate, Todate, EduDescription
                                UserEducationDetail ObjUserEducationDetail = new UserEducationDetail();
                                ObjUserEducationDetail.UserId = objuser.UserId;
                                ObjUserEducationDetail.QualificationId = item.QualificationId;
                                ObjUserEducationDetail.InstituteName = item.InstituteName;
                                ObjUserEducationDetail.CityId = item.CityId;
                                ObjUserEducationDetail.Address = item.Address;
                                ObjUserEducationDetail.PinCode = item.PinCode;
                                ObjUserEducationDetail.FromDate = item.FromDate;
                                ObjUserEducationDetail.Todate = item.Todate;
                                ObjUserEducationDetail.EduDescription = item.EduDescription;
                                context.UserEducationDetails.Add(ObjUserEducationDetail);
                                context.SaveChanges();
                            }
                            else if (item.Status == 2 || item.Status == 3)
                            {
                                UserEducationDetail ObjUserEducationDetail = new UserEducationDetail();
                                ObjUserEducationDetail.EducationId = item.EducationId;
                                ObjUserEducationDetail.UserId = objuser.UserId;
                                ObjUserEducationDetail.QualificationId = item.QualificationId;
                                ObjUserEducationDetail.InstituteName = item.InstituteName;
                                ObjUserEducationDetail.CityId = item.CityId;
                                ObjUserEducationDetail.Address = item.Address;
                                ObjUserEducationDetail.PinCode = item.PinCode;
                                ObjUserEducationDetail.FromDate = item.FromDate;
                                ObjUserEducationDetail.Todate = item.Todate;
                                ObjUserEducationDetail.EduDescription = item.EduDescription;
                                context.Entry(ObjUserEducationDetail).State = System.Data.Entity.EntityState.Modified;
                                if (item.Status == 3)
                                {
                                    context.UserEducationDetails.Remove(ObjUserEducationDetail);
                                }
                            }
                        }
                    }
                    context.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception ex)
                {
                    string msg = "";
                    if (ex is System.Data.Entity.Validation.DbEntityValidationException)
                    {
                        foreach (var validationErrors in ((System.Data.Entity.Validation.DbEntityValidationException)ex).EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                msg += ";" + string.Format("{0}:{1}",
                                    validationErrors.Entry.Entity.ToString(),
                                    validationError.ErrorMessage);
                            }
                        }
                    }
                    dbContextTransaction.Rollback();
                }
            }
            return objuser;
        }
        public void DeleteUser(int id)
        {
            try
            {
                UserMaster user = context.UserMasters.Find(id);
                if (user != null)
                {
                    context.UserMasters.Remove(user);
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        //public IQueryable<StateMaster> GetAllState()
        //{
        //    try
        //    {
        //        return odal.selectbyquerydt(@"select DepartmentId,DepartmentName from DepartmentMaster").ConvertToList<StateMaster>().AsQueryable();
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        public IQueryable<UserMaster> getAllUser()
        {
            try
            {
                return new dalc().selectbyquerydt("SELECT * FROM UserMaster with(nolock)").ConvertToList<UserMaster>().AsQueryable();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public IQueryable<UserMaster> CheckPasswordById(int UserId, string oldpass)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@UserId", UserId);
                para[1] = new SqlParameter().CreateParameter("@oldpass", oldpass);
                return new dalc().GetDataTable_Text("SELECT * FROM UserMaster with(nolock) WHERE UserId = @UserId AND RTRIM(LTRIM(PASSWORD)) = RTRIM(LTRIM(@oldpass)) AND IsActive = 1", para).ConvertToList<UserMaster>().AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<UserMaster> GetuserById(int UserId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@UserId", UserId);
                return odal.GetDataTable_Text(@" select C.*,bnk.BankName[Bank],dpt.DepartmentName[DepartmentName],dsgm.DesignationName,ac.AccountType,rl.RoleName,uc.Name+' '+uc.Surname[ReportingName],blg.BloodGroup,cm.cityname[BirthPlaceCityName],sm.stateid[BirthPlaceStateId]
                                                ,sm.statename[BirthPlaceStateName],ct.countryid[BirthPlaceCountryId],ct.countryname[BirthPlaceCountryName] 
                                                ,hcm.cityname[HomeTownCityName],hsm.stateid[HomeTownStateId],so.SourceName
                                                ,hsm.statename[HomeTownStateName],hct.countryid[HomeTownCountryId],hct.countryname[HomeTownCountryName] 
                                                ,prcm.cityname[PresentResiCityName],prsm.stateid[PresentResiStateId]
                                                ,prsm.statename[PresentResiStateName],prct.countryid[PresentResiCountryId],prct.countryname[PresentResiCountryName] 
                                                ,pmcm.cityname[PermenantResiCityName],pmsm.stateid[PermenantResiStateId]
                                                ,pmsm.statename[PermenantResiStateName],pmct.countryid[PermenantResiCountryId],pmct.countryname[PermenantResiCountryName] 
                                                ,(case when c.ReferenceTypeId=2 then ag.AgencyType end)AgencyTypeReferanceName,
                                                (case when c.ReferenceTypeId=1 and c.ReferenceSubType=1 then vsg.CompanyName 
                                                when c.ReferenceTypeId=1 and c.ReferenceSubType=2 then ssg.CompanyName 
                                                when c.ReferenceTypeId=1 and c.ReferenceSubType=3 then usg.UserName
                                                when c.ReferenceTypeId=1 and c.ReferenceSubType=4 then bsg.CompanyName
                                                end)ReferanceName
                                                from UserMaster as C with(nolock)
                                                left join SourceMaster as so with(nolock) on  C.SourceId = so.SourceId
                                                left join BankNameMaster as bnk with(nolock) on  C.BankName = bnk.BankId
                                                left join DesignationMaster as dsgm with(nolock) on  C.DesignationId = dsgm.DesignationId
                                                left join RoleMaster as rl with(nolock) on  C.RoleId = rl.RoleId
                                                left join AgencyTypeMaster as ag with(nolock) on  C.ReferenceId = ag.AgencyTypeId
                                                left join UserMaster as usg with(nolock) on  C.ReferenceId = usg.UserId
                                                left join BuyerMaster as bsg with(nolock) on  C.ReferenceId = bsg.BuyerId
                                                left join SupplierMaster as ssg with(nolock) on  C.ReferenceId = ssg.SupplierId
                                                left join VendorMaster as vsg with(nolock) on  C.ReferenceId = vsg.VendorId
                                                left join usermaster as uc with(nolock) on C.ReportingId = uc.UserId 
                                                left join BloodGroupMaster as blg with(nolock) on C.BloodGroupId = blg.BloodGroupId 
                                                left join DepartmentMaster as dpt with(nolock) on C.DepartmentId = dpt.DepartmentId 
                                                left join accounttypemaster as ac with(nolock) on C.AccountTypeId = ac.AccountTypeId 

                                                left join citymaster as cm with(nolock) on cm.cityid=c.BirthPlaceCityId 
                                                left join statemaster as sm with(nolock) on sm.stateid=cm.stateid
                                                left join countrymaster as ct with(nolock) on ct.countryid=sm.countryid

                                                left join citymaster as hcm with(nolock) on hcm.cityid=c.HomeTownCityId 
                                                left join statemaster as hsm with(nolock) on hsm.stateid=hcm.stateid
                                                left join countrymaster as hct with(nolock) on hct.countryid=hsm.countryid

                                                left join citymaster as prcm with(nolock) on prcm.cityid=c.PresentCityId 
                                                left join statemaster as prsm with(nolock) on prsm.stateid=prcm.stateid
                                                left join countrymaster as prct with(nolock) on prct.countryid=prsm.countryid

                                                left join citymaster as pmcm with(nolock) on pmcm.cityid=c.PermanentCityId 
                                                left join statemaster as pmsm with(nolock) on pmsm.stateid=pmcm.stateid
                                                left join countrymaster as pmct with(nolock) on pmct.countryid=pmsm.countryid
                                                where C.UserId =@UserId ", para).ConvertToList<UserMaster>().AsQueryable();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IQueryable<UserMaster> GetReportingUser(int UserId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@UserId", UserId);
                return new dalc().GetDataTable_Text("SELECT * FROM UserMaster with(nolock) WHERE ReportingId = @UserId", para).ConvertToList<UserMaster>().AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<UserMaster> GetInquiryReportingUser(int UserId)
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var reportuser = context.UserMasters.Where(x => x.ReportingId == UserId || x.UserId == UserId);
                //    scope.Complete();
                //    return reportuser.AsQueryable();
                //}
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@UserId", UserId);
                return new dalc().GetDataTable_Text("SELECT * FROM UserMaster with(nolock) WHERE (UserId=@UserId OR ReportingId=@UserId ) AND IsActive = 1", para).ConvertToList<UserMaster>().AsQueryable();

            }
            catch (Exception)
            {
                return null;
            }
        }

        public IQueryable<UserMaster> DuplicateEditUser(int UserId, string UserName)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@UserId", UserId);
                para[1] = new SqlParameter().CreateParameter("@UserName", UserName);
                return new dalc().GetDataTable_Text("SELECT * FROM UserMaster with(nolock) WHERE UserId != @UserId AND UserName = @UserName AND IsActive = 1", para).ConvertToList<UserMaster>().AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IQueryable<UserMaster> CheckLogin(string UserName, string Password)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@UserName", UserName);
                para[1] = new SqlParameter().CreateParameter("@Password", Password);
                return new dalc().GetDataTable_Text("SELECT * FROM UserMaster with(nolock) WHERE UserName = @UserName AND Password = @Password AND IsActive = 1", para).ConvertToList<UserMaster>().AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<UserMaster> DuplicateUser(string UserName)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@UserName", UserName);
                return new dalc().GetDataTable_Text("SELECT * FROM UserMaster with(nolock) WHERE UserName =@UserName AND IsActive = 1", para).ConvertToList<UserMaster>().AsQueryable();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public UserMaster GetUserById(int UserId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@UserId", UserId);
                return new dalc().GetDataTable_Text("SELECT * FROM UserMaster with(nolock) WHERE UserId = @UserId AND IsActive = 1", para).ConvertToList<UserMaster>().FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }

        }
        public void AddLoginHistory(LoginHistory objloguser)
        {
            try
            {
                context.LoginHistories.Add(objloguser);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public AttendanceMaster GetAttendancebyUserid(int UserId, DateTime ondate)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@UserId", UserId);
                para[1] = new SqlParameter().CreateParameter("@OnDate", ondate.Date);
                return new dalc().GetDataTable_Text("SELECT * FROM AttendanceMaster with(nolock) WHERE UserId = @UserId and OnDate=@OnDate AND IsActive = 1", para).ConvertToList<AttendanceMaster>().FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }

        }
        public void AddAttendance(AttendanceMaster objattendance)
        {
            try
            {
                context.AttendanceMasters.Add(objattendance);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void UpdateAttendance(AttendanceMaster objattendance)
        {
            try
            {
                context.Entry(objattendance).State = System.Data.Entity.EntityState.Modified; //EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public System.Data.DataSet GetEmployeeReportData(ReportPara obj)
        {
            DataSet ds = new DataSet();
            int ID = 0;
            try
            {
                ID = Convert.ToInt32(obj.ID);
            }
            catch
            {
                ID = 0;
            }
            SqlParameter[] para = new SqlParameter[9];
            para[0] = new SqlParameter().CreateParameter("@ID", ID);
            para[1] = new SqlParameter().CreateParameter("@UserId", obj.UserId);
            para[2] = new SqlParameter().CreateParameter("@UserType", obj.UserType);
            para[3] = new SqlParameter().CreateParameter("@A", obj.A);
            para[4] = new SqlParameter().CreateParameter("@B", obj.B);
            para[5] = new SqlParameter().CreateParameter("@C", obj.C);
            para[6] = new SqlParameter().CreateParameter("@D", obj.D);
            para[7] = new SqlParameter().CreateParameter("@E", obj.E);
            para[8] = new SqlParameter().CreateParameter("@F", obj.F);
            ds = odal.GetDataset("Employee_Report", para);
            ds.Tables[0].TableName = "EmployeeMain";
            // ds.Tables[1].TableName = "QuotationItems";
            return ds;

        }
        //public IQueryable<StateMaster> GetStateByCountryID(int CountryID)
        //{
        //    try
        //    {
        //        using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
        //        {
        //            var state = context.StateMasters.Where(x => x.CountryId == CountryID && x.IsActive == true);
        //            scope.Complete();
        //            return state.AsQueryable();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

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
