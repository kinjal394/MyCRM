using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;

namespace CRM_Repository.ServiceContract
{
    public interface ISpecification_Repository : IDisposable
    {
        void AddSpecification(TechnicalSpecMaster obj);
        void UpdateSpecification(TechnicalSpecMaster obj);
        void DeleteSpecification(int id);
        TechnicalSpecMaster GetSpecificationByID(int id);
        IQueryable<TechnicalSpecMaster> GetAllSpecification();
        IQueryable<TechnicalSpecMaster> DuplicateSpecification(string TechSpec);
        IQueryable<TechnicalSpecMaster> DuplicateEditSpecification(int SpecificationId, string TechSpec);
    }
}
