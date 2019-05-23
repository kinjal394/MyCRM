using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
    public interface IRelation_Repository : IDisposable
    {
        void SaveRelation(RelationMaster objRelation);
        void UpdateRelation(RelationMaster objRelation);
        void DeleteRelation(int RelationId);
        RelationMaster GetByRelationId(int RelationId);
        IQueryable<RelationMaster> GetRelation();
        bool IsExist(int RelationId, string RelationName);
        IQueryable<UserReferenceRelationMaster> GetRelationById(int Id);
    }
}
