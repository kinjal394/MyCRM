using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;

namespace CRM_Repository.ServiceContract
{
    public interface IPort_Repository : IDisposable
    {
        void AddPort(PortMaster obj);
        void UpdatePort(PortMaster obj); 
        PortMaster GetPortById(int PortId); 
        IQueryable<PortMaster> DuplicateEditPort(int PortId, string PortName);
        IQueryable<PortMaster> DuplicatePort(string Portname);
        IQueryable<PortMaster> GetAllPort();
    }
}
