using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;
using System.Transactions;
using System.Data.SqlClient;
using CRM_Repository.DataServices;
namespace CRM_Repository.Service
{
    public class ShippingMark_Repository : IShippingMark_Repository, IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;
        public ShippingMark_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }


        public void AddShippingMark(ShipmentMarkMaster ship)
        {
            try
            {
                context.ShipmentMarkMasters.Add(ship);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool CheckShippingMark(ShipmentMarkMaster obj, bool isUpdate)
        {

            try
            {
                if (isUpdate)
                {
                    //var data = context.ShippingMarkMasters.Where(e => e.ShippingMark.Trim() == obj.ShippingMark.Trim() && e.ShippingMarkId != obj.ShippingMarkId && e.IsActive == true).ToList();
                    //return data.Count > 0 ? true : false;

                    SqlParameter[] para = new SqlParameter[6];
                    para[0] = new SqlParameter().CreateParameter("@ShipmentMarkId", obj.ShipmentMarkId);
                    para[1] = new SqlParameter().CreateParameter("@BuyerName", obj.BuyerName);
                    para[2] = new SqlParameter().CreateParameter("@ShipperName", obj.ShipperName);
                    para[3] = new SqlParameter().CreateParameter("@ConsigneeName", obj.ConsigneeName);
                    para[4] = new SqlParameter().CreateParameter("@POL", obj.POL);
                    para[5] = new SqlParameter().CreateParameter("@POD", obj.POD);
                    return new dalc().GetDataTable_Text(@"SELECT * FROM ShipmentMarkMaster with(nolock) WHERE ShipmentMarkId <> @ShipmentMarkId AND RTRIM(LTRIM(ConsigneeName)) = RTRIM(LTRIM(@ConsigneeName)) 
                                                        AND RTRIM(LTRIM(BuyerName)) = RTRIM(LTRIM(@BuyerName))  AND RTRIM(LTRIM(ShipperName)) = RTRIM(LTRIM(@ShipperName))
                                                        AND RTRIM(LTRIM(POL)) = RTRIM(LTRIM(@POL))  AND RTRIM(LTRIM(POD)) = RTRIM(LTRIM(@POD)) AND IsActive = 1", para).Rows.Count > 0 ? true : false;


                }
                else
                {
                    //var data = context.ShippingMarkMasters.Where(e => e.ShippingMark.Trim() == obj.ShippingMark.Trim() && e.IsActive == true).ToList();
                    //return data.Count > 0 ? true : false;

                    SqlParameter[] para = new SqlParameter[5];
                    para[0] = new SqlParameter().CreateParameter("@BuyerName", obj.BuyerName);
                    para[1] = new SqlParameter().CreateParameter("@ShipperName", obj.ShipperName);
                    para[2] = new SqlParameter().CreateParameter("@ConsigneeName", obj.ConsigneeName);
                    para[3] = new SqlParameter().CreateParameter("@POL", obj.POL);
                    para[4] = new SqlParameter().CreateParameter("@POD", obj.POD);
                    return new dalc().GetDataTable_Text(@"SELECT * FROM ShipmentMarkMaster with(nolock) WHERE  RTRIM(LTRIM(ConsigneeName)) = RTRIM(LTRIM(@ConsigneeName)) 
                                                        AND RTRIM(LTRIM(BuyerName)) = RTRIM(LTRIM(@BuyerName))  AND RTRIM(LTRIM(ShipperName)) = RTRIM(LTRIM(@ShipperName))
                                                        AND RTRIM(LTRIM(POL)) = RTRIM(LTRIM(@POL))  AND RTRIM(LTRIM(POD)) = RTRIM(LTRIM(@POD)) AND IsActive = 1", para).Rows.Count > 0 ? true : false;

                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IQueryable<ShipmentMarkMaster> GetAllShippingMark()
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var ship = context.ShippingMarkMasters.Where(e => e.IsActive == true);
                //    scope.Complete();
                //    return ship.AsQueryable();
                //}
                return new dalc().selectbyquerydt("SELECT * FROM ShipmentMarkMaster with(nolock) WHERE  IsActive = 1").ConvertToList<ShipmentMarkMaster>().AsQueryable();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public ShipmentMarkMaster GetShippingMarkById(int ShippingMarkId)
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var ship = context.ShippingMarkMasters.Find(id);
                //    scope.Complete();
                //    return ship;
                //}
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@ShipmentMarkId", ShippingMarkId);
                return new dalc().GetDataTable_Text("SELECT * FROM ShipmentMarkMaster with(nolock) WHERE ShipmentMarkId = @ShipmentMarkId ", para).ConvertToList<ShipmentMarkMaster>().FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateShippingMark(ShipmentMarkMaster ship)
        {
            try
            {
                context.Entry(ship).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
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
