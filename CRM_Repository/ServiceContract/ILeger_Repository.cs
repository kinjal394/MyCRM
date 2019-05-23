using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;

namespace CRM_Repository.ServiceContract
{
    public interface ILeger_Repository:IDisposable
    {
        void AddLeger(LegerMaster obj);
        void UpdateLeger(LegerMaster obj);
        IQueryable<LegerMaster> GetAllLeger();
        IQueryable<LegerMaster> DuplicateLeger(string LegerName);
        LegerMaster GetLegerById(int LegerId);
        IQueryable<LegerMaster> DuplicateEditLeger(int LegerId, string LegerName);
        
    }
}
