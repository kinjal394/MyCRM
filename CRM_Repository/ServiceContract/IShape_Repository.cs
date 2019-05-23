using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
    public interface IShape_Repository : IDisposable
    {
        void SaveShape(ShapeMaster objShape);
        void UpdateShape(ShapeMaster objShape);
        void DeleteShape(int ShapeId);
        ShapeMaster GetByShapeId(int ShapeId);
        IQueryable<ShapeMaster> GetShape();
        bool IsExist(int ShapeId, string ShapeName);
    }
}
