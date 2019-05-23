using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
    public interface IFrequency_Repository : IDisposable
    {
        void AddFrequency(FrequencyMaster obj);
        void UpdateFrequency(FrequencyMaster obj);
        void DeleteFrequency(int id);
        FrequencyMaster GetFrequencyByID(int id);
        IQueryable<FrequencyMaster> GetAllFrequency();
        IQueryable<FrequencyMaster> DuplicateFrequency(string Frequency);
        IQueryable<FrequencyMaster> DuplicateEditFrequency(int FrequencyId, string Frequency);
    }
}
