using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;

namespace CRM_Repository.ServiceContract
{
    public interface IShippingMark_Repository : IDisposable
    {
        void AddShippingMark(ShipmentMarkMaster unit);
        void UpdateShippingMark(ShipmentMarkMaster unit);
        ShipmentMarkMaster GetShippingMarkById(int id);
        IQueryable<ShipmentMarkMaster> GetAllShippingMark();
        bool CheckShippingMark(ShipmentMarkMaster obj, bool isUpdate);
    }
}
