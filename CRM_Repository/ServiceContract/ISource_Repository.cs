using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;

namespace CRM_Repository.ServiceContract
{
    public interface ISource_Repository : IDisposable
    {
        void AddSource(SourceMaster obj);
        void UpdateSource(SourceMaster obj); 
        SourceMaster GetSourceById(int id);
        IQueryable<SourceMaster> GetAllSource();
        IQueryable<SourceMaster> DuplicateSource(string SourceName);
        IQueryable<SourceMaster> DuplicateEditSource(int SourceId, string SourceName);
    }
}
