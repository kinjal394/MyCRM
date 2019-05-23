using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
    public interface IUserRefferenceDetail_Repository : IDisposable
    {
        IQueryable<UserRefferenceDetail> GetUserRefferencebyid(int id);
        IQueryable<UserRefferenceDetail> GetUserRefferencebyuserid(int id);
    }
}
