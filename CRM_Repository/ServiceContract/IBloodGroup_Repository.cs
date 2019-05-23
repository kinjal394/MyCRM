using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;

namespace CRM_Repository.ServiceContract
{
    public interface IBloodGroup_Repository : IDisposable
    {
        void AddBloodGroup(BloodGroupMaster bloodgroup);
        void UpdateBloodGroup(BloodGroupMaster bloodgroup);
        BloodGroupMaster GetBloodGroupById(int id);
        IQueryable<BloodGroupMaster> GetAllBloodGroup();
        bool CheckBloodGroup(BloodGroupMaster obj, bool isUpdate);
    }
}
