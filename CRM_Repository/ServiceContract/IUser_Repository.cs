using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
    public interface IUser_Repository : IDisposable
    {
        UserMaster AddUser(UserMaster obj);
        UserMaster UpdateUser(UserMaster obj);
        void DeleteUser(int id);
        UserMaster GetUserById(int id);
        IQueryable<UserMaster> getAllUser();
        IQueryable<UserMaster> GetuserById(int UserId);
        IQueryable<UserMaster> DuplicateUser(string UserName);
        IQueryable<UserMaster> DuplicateEditUser(int UserId, string UserName);
        IQueryable<UserMaster> GetReportingUser(int UserId);
        IQueryable<UserMaster> CheckLogin(string UserName, string Password);
        IQueryable<UserMaster> CheckPasswordById(int UserId, string oldpass);
        AttendanceMaster GetAttendancebyUserid(int id, DateTime ondate);
        void AddLoginHistory(LoginHistory obj);
        void AddAttendance(AttendanceMaster obj);
        void UpdateAttendance(AttendanceMaster obj);
        IQueryable<UserMaster> GetInquiryReportingUser(int UserId);
    }
}
