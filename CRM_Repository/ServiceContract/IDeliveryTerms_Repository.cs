using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;

namespace CRM_Repository.ServiceContract
{
    public interface IDeliveryTerms_Repository : IDisposable
    {
        void AddDeliveryTerms(DeliveryTermsMaster obj);
        void UpdateDeliveryTerms(DeliveryTermsMaster obj);
        void DeleteDeliveryTerms(int id);
        DeliveryTermsMaster GetDeliveryTermsByID(int id);
        IQueryable<DeliveryTermsMaster> GetAllDeliveryTerms();
        IQueryable<DeliveryTermsMaster> DuplicateDeliveryTerms(string DeliveryTermsName);
        IQueryable<DeliveryTermsMaster> DuplicateEditDeliveryTerms(int DeliveryTermsId, string DeliveryTermsName);
    }
}
