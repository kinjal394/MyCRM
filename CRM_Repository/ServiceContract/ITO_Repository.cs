using CRM_Repository.Data;
using CRM_Repository.DTOModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
    public interface ITO_Repository:IDisposable
    {
        int CreateUpdate(TOModel obj);
        TOModel GetTOById(int id);
        IQueryable<TOModel> GetAllTO();
        int Delete(TOModel obj);

        TechnicalSpecMaster getSpec(int id);
        IQueryable<TechnicleDetail> GetTechnicalDetailbyToid(int id);

        //delete technical detail
        void deletetechnicaldetail(int pid,int spectype);

    }
}
