using CRM_Repository.Data;
using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Data.SqlClient;
using CRM_Repository.DataServices;
namespace CRM_Repository.Service
{
    public class TypeOfShipment_Repository : ITypeOfShipment_Repository, IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;

        public TypeOfShipment_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }
     
        public void AddTypeOfShipment(TypeOfShipmentMaster obj)
        {
            try
            {
                context.TypeOfShipmentMasters.Add(obj);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateTypeOfShipment(TypeOfShipmentMaster obj)
        {
            try
            {
                context.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteTypeOfShipment(int TypeOfShipmentid)
        {
            try
            {
                //TypeOfShipmentMaster TOS = context.TypeOfShipmentMasters.Single(X => X.ShipmentTypeId == TypeOfShipmentid);
                //TOS.IsActive = false;
                //context.Entry(TOS).State = System.Data.Entity.EntityState.Modified;
                //context.SaveChanges();
                TypeOfShipmentMaster TOS = context.TypeOfShipmentMasters.Find(TypeOfShipmentid);
                if (TOS != null)
                {
                    context.TypeOfShipmentMasters.Remove(TOS);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<TypeOfShipmentMaster> GetTypeOfShipmentById(int id)
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var TypeOfShipmentById = context.TypeOfShipmentMasters.Where(X => X.ShipmentTypeId == id);
                //    scope.Complete();
                //    return TypeOfShipmentById.AsQueryable();
                //}
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@ShipmentTypeId", id);
                return new dalc().GetDataTable_Text("SELECT * FROM TypeOfShipmentMaster with(nolock) WHERE  ShipmentTypeId = @ShipmentTypeId AND IsActive = 1", para).ConvertToList<TypeOfShipmentMaster>().AsQueryable();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<TypeOfShipmentMaster> DuplicateTypeOfShipment(TypeOfShipmentMaster Data)
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var DuplicateTypeOfShipment = context.TypeOfShipmentMasters.Where(X => X.ShipmentType == Data.ShipmentType && X.ShipmentTypeId != Data.ShipmentTypeId && X.IsActive == true);
                //    scope.Complete();
                //    return DuplicateTypeOfShipment.AsQueryable();
                //}

                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@ShipmentType", Data.ShipmentType);
                para[1] = new SqlParameter().CreateParameter("@ShipmentTypeId", Data.ShipmentTypeId);
                return new dalc().GetDataTable_Text("SELECT * FROM TypeOfShipmentMaster with(nolock) WHERE ShipmentType = @ShipmentType AND ShipmentTypeId <> @ShipmentTypeId AND IsActive = 1", para).ConvertToList<TypeOfShipmentMaster>().AsQueryable();

            }
            catch (Exception)
            {

                throw;
            }
          
        }
        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    context.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
