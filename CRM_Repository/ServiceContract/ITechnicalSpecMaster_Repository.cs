using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
    public interface ITechnicalSpecMaster_Repository
    {
        void SaveTechnicalSpec(TechnicalSpecMaster objTechnicalSpecMaster);
        void UpdateTechnicalSpec(TechnicalSpecMaster objTechnicalSpecMaster);
        void DeleteTechnicalSpec(int SpecificationId);
        TechnicalSpecMaster GetBySpecificationId(int SpecificationId);
        IQueryable<TechnicalSpecMaster> GetTechnicalSpec();
        bool IsExist(int SpecificationId, string TechSpec);
    }
}
