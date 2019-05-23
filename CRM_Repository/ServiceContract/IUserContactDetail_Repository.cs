using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
    public interface IUserContactDetail_Repository : IDisposable
    {

        IQueryable<UserReferenceRelationMaster> GetUserContactbyid(int id);
        IQueryable<UserReferenceRelationMaster> GetUserContactbyuserid(int id);
        IQueryable<UserSalaryDetail> GetUserSalarybyid(int UserId);
        IQueryable<UserSalaryDetail> GetUserSalarybyuserid(int UserId);
        IQueryable<UserDocDetail> GetUserDocbyid(int UserId);
        IQueryable<UserExperienceDetail> GetUserExperbyid(int UserId);
        IQueryable<UserEducationDetail> GetUserEducationid(int UserId);
    }

}
