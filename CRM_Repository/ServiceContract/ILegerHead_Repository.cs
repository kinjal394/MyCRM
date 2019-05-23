using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;

namespace CRM_Repository.ServiceContract
{
    public interface ILegerHead_Repository:IDisposable
    {

        void AddLegerHead(LegerHeadMaster obj);
        void UpdateLegerHead(LegerHeadMaster obj);
        IQueryable<LegerHeadMaster> GetAllLegerHead();
        IQueryable<LegerHeadMaster> DuplicateLegerHead(string LegerHeadName);
        LegerHeadMaster GetLegerHeadById(int LegerHeadId);
        IQueryable<LegerHeadMaster> DuplicateEditLegerHead(int LegerHeadId, string LegerHeadName);
    }
}
