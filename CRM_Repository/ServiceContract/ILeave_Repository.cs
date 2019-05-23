using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;

namespace CRM_Repository.ServiceContract
{
    public interface ILeave_Repository : IDisposable
    {
        void InsertLeave(LeaveMaster objorigin);
        void UpdateLeave(LeaveMaster objorigin);
        IQueryable<LeaveMaster> GetAllLeave();
        LeaveMaster GetLeaveID(int LeaveID);
        IQueryable<LeaveMaster> DuplicateEditLeave(int LeaveId);
    }
}
