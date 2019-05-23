using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.DTOModel;

namespace CRM_Repository.ServiceContract
{
  public interface IExhibition_Repository : IDisposable
    {
        ExhibitionMaster AddExhibition(ExhibitionMaster exhibitionObj);
        void DeleteExhibition(int id);
        IQueryable<ExhibitionMaster> getAllExhibition();
        ExhibitionMaster GetExhibitionById(int id);
        void UpdateExhibition(ExhibitionMaster bankobj);
        int CreateUpdate(ExhibitionModel objInputExhibition);
        bool CheckExhibitionType(ExhibitionMaster ExkObj, bool isUpdate);
    }
}
