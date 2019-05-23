using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;

namespace CRM_Repository.ServiceContract
{
    public interface IAskcustomerDetails_Repository : IDisposable
    {
        void AddAskCustomerDetail(AskCustomerDetail obj);
        void UpdateAskCustomerDetail(AskCustomerDetail obj);
        void DeleteAskCustomerDetail(int id);
        AskCustomerDetail GetAskCustomerDetailByID(int id);
        IQueryable<AskCustomerDetail> GetAllAskCustomerDetail();
        
    }
}
