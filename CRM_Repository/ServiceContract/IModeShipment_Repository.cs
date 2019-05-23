using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;

namespace CRM_Repository.ServiceContract
{
    public interface IModeShipment_Repository : IDisposable
    {
        void AddShip(ShipmentMaster obj);
        void UpdateShip(ShipmentMaster obj); 
        ShipmentMaster GetShipById(int ShipmentId);
        IQueryable<ShipmentMaster> GetAllShipment();
        IQueryable<ShipmentMaster> DuplicateShip(string Shipname);
        IQueryable<ShipmentMaster> DuplicateEditShip(int ShipID, string Shipname);
    }
}
