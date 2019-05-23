using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
    public interface IPlugShape_Repository : IDisposable
    {
        void SavePlugShape(PlugShapeMaster objPlugShape);
        void UpdatePlugShape(PlugShapeMaster objPlugShape);
        void DeletePlugShape(int PlugShapeId);
        PlugShapeMaster GetByPlugShapeId(int Id);
        IQueryable<PlugShapeMaster> GetPlugShape();
        bool IsExist(int PlugShapeId, string Title);
    }
}
