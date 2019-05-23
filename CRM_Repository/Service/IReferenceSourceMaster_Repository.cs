using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.Service
{
    public interface IReferenceSourceMaster_Repository : IDisposable
    {
        void Add(ReferenceSourceMaster obj);
        void Update(ReferenceSourceMaster obj);
        IQueryable<ReferenceSourceMaster> GetAll();
        ReferenceSourceMaster GetByID(int id);
        IQueryable<ReferenceSourceMaster> CheckForDuplicate(int SourceId, string SourceName);
    }
}
