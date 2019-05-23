using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;

namespace CRM_Repository.ServiceContract
{
    public interface IVoltage_Repository:IDisposable
    {
        void AddVoltage(VoltageMaster obj);
        void UpdateVoltage(VoltageMaster obj);
        void DeleteVoltage(int id);
        VoltageMaster GetVoltageByID(int id);
        IQueryable<VoltageMaster> GetAllVoltage();
        IQueryable<VoltageMaster> DuplicateVoltage(string Voltage);
        IQueryable<VoltageMaster> DuplicateEditVoltage(int VoltageId, string Voltage);
    }
}
