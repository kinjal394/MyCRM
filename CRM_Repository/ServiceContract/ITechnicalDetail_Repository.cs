using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;
namespace CRM_Repository.ServiceContract
{
    public interface ITechnicalDetail_Repository : IDisposable
    {
        void AddTechnicalDetail(TOTechnicalDetail obj);
    }
}
