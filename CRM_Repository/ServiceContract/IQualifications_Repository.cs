using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;

namespace CRM_Repository.ServiceContract
{
   public interface IQualifications_Repository : IDisposable
    {
        void AddQualification(QualificationsMaster ObjQual);
        void UpdateQualification(QualificationsMaster ObjQual);
        //void DeleteQualification(int id);
        QualificationsMaster GetQauliByID(int id);
        IQueryable<QualificationsMaster> GetAllQuali();
        IQueryable<QualificationsMaster> DuplicateQualification(string QualificationName);
        IQueryable<QualificationsMaster> DuplicateEditQualification(int QualificationId, string QualificationName);
    }
}
