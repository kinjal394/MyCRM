using CRM_Repository.Data;
using CRM_Repository.DTOModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
    public interface IWorkProfile_Repository : IDisposable
    {
        void AddWorkProfile(WorkProfileMaster obj);
        void UpdateWorkProfile(WorkProfileMaster obj);
        void DeleteWorkProfile(int id);
        WorkProfileModle GetWorkProfileById(int id);
        IQueryable<WorkProfileModle> GetAllWorkProfile(int DepartmentId, int UserTypeId);
    }
}
