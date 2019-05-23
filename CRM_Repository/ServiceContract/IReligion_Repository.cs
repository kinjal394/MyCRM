using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
    public interface IReligion_Repository : IDisposable
    {
        void AddReligion(ReligionMaster obj);
        void UpdateReligion(ReligionMaster obj);
        ReligionMaster GetReligionById(int id);
        IQueryable<ReligionMaster> GetAllReligion();
        IQueryable<ReligionMaster> DuplicateReligion(string ReligionName);
        IQueryable<ReligionMaster> DuplicateEditReligion(int ReligionId, string ReligionName);
    }
}
