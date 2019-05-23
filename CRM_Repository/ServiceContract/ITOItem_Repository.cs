using CRM_Repository.DTOModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
    public interface ITOItem_Repository
    {
        TOItemModel GetTOItemById(int id);
        IQueryable<TOItemModel> GetAllTOItem();
        IQueryable<TOItemModel> GetTOItembyTOId(int TOId);

    }
}
