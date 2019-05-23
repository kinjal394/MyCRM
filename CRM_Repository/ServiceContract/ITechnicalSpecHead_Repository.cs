using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
    public interface ITechnicalSpecHead_Repository : IDisposable
    {
        void AddTechnicalSpecHead(TechnicalSpecHeadMaster objTechnicalSpecHead);
        void UpdateTechnicalSpecHead(TechnicalSpecHeadMaster objTechnicalSpecHead);
        void DeleteTechnicalSpecHead(int TechHeadId);
        TechnicalSpecHeadMaster GetByTechHeadId(int TechHeadId);
        IQueryable<TechnicalSpecHeadMaster> GetTechHead();
        IQueryable<TechnicalSpecHeadMaster> DuplicateTechnicalSpecHead(string TechHead);
        IQueryable<TechnicalSpecHeadMaster> DuplicateEdiTechnicalSpecHead(int TechHeadId, string TechHead);
    }
}
