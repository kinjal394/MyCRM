using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;

namespace CRM_Repository.ServiceContract
{
    public interface IDepartment_Repository : IDisposable
    {
        void AddDepartment(DepartmentMaster obj);
        void UpdateDepartment(DepartmentMaster obj);
        void DeleteDepartment(int id);
        DepartmentMaster GetDepartmentById(int id);
        IQueryable<DepartmentMaster> getAllDepartment();
        IQueryable<DepartmentMaster> GetdepartmentById(int DepartmentId);
        IQueryable<DepartmentMaster> DuplicateDepartment(string DepartmentName);
        IQueryable<DepartmentMaster> DuplicateEditDepartment(int DepartmentId, string DepartmentName);
        IQueryable<UserMaster> GetUserbyDepartment(int Id);
    }
}
