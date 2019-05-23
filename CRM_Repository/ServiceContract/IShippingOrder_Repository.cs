using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;

namespace CRM_Repository.ServiceContract
{
    public interface  IShippingOrder_Repository : IDisposable
    {
        void AddShippingOrder(ShippingOrderMaster obj);
        void UpdateShippingOrder(ShippingOrderMaster obj);
        void DeleteShippingOrder(int id);
        ShippingOrderMaster GetShippingOrderID(int id);
        IQueryable<ShippingOrderMaster> GetAllShippingOrder();
        IQueryable<ShippingOrderMaster> DuplicateShippingOrder(string TypeofShipment);
        IQueryable<ShippingOrderMaster> DuplicateEditShippingOrder(int ShippingOrdId, string TypeofShipment);
    }
}
