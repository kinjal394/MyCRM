using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;

namespace CRM_Repository.ServiceContract
{
    public interface IDesignation_Repository : IDisposable
    {
        void AddDesignation(DesignationMaster obj);
        void UpdateDesignation(DesignationMaster obj);
        void DeleteDesignation(int id);
        DesignationMaster GetDesignationByID(int id);
        IQueryable<DesignationMaster> GetAllDesignation();
        IQueryable<DesignationMaster> DuplicateDesignation(string DesignationName);
        IQueryable<DesignationMaster> DuplicateEditDesignation(int DesignationId, string DesignationName);
    }
}
