using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
    public partial interface IEmployeeShift_Repository:IDisposable
    {
        IEnumerable<EmployeeShitfMaster> GetEmployeeShits();
        EmployeeShitfMaster GetShiftByID(int shiftID);
        void InsertShist(EmployeeShitfMaster shift);
        void UpdateShift(EmployeeShitfMaster shift);
        void Save();
        bool CheckShiftExist(EmployeeShitfMaster bankObj, bool isUpdate);
    }
}
