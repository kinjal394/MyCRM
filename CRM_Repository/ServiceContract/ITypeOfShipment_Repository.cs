using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
    interface ITypeOfShipment_Repository : IDisposable
    {
        void AddTypeOfShipment(TypeOfShipmentMaster obj);
        void UpdateTypeOfShipment(TypeOfShipmentMaster obj);
        void DeleteTypeOfShipment(int TypeOfShipmentid);
        IQueryable<TypeOfShipmentMaster> GetTypeOfShipmentById(int ShipmentId);
        IQueryable<TypeOfShipmentMaster> DuplicateTypeOfShipment(TypeOfShipmentMaster data);
        //IQueryable<TypeOfShipmentMaster> DeleteTypeOfShipment(int ShipID, string Shipname);
    }
}
