using CRM_Repository.Data;
using CRM_Repository.DataServices;
using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Transactions;
using System.Data.SqlClient;


namespace CRM_Repository.Service
{
    public class Unit_Repository : IUnit_Repository,IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;
        public Unit_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }


        public void AddUnit(UnitMaster unit)
        {
            try
            {
                context.UnitMasters.Add(unit);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool CheckUnit(UnitMaster obj, bool isUpdate)
        {

            try
            {
                if (isUpdate)
                {
                    //var data = context.UnitMasters.Where(e => e.UnitName.Trim() == obj.UnitName.Trim() && e.UnitId != obj.UnitId && e.IsActive==true).ToList();
                    //return data.Count > 0 ? true : false;
                    SqlParameter[] para = new SqlParameter[2];
                    para[0] = new SqlParameter().CreateParameter("@UnitName", obj.UnitName);
                    para[1] = new SqlParameter().CreateParameter("@UnitId", obj.UnitId);
                    return new dalc().GetDataTable_Text("SELECT * FROM UnitMaster with(nolock) WHERE RTRIM(LTRIM(UnitName))=RTRIM(LTRIM(@UnitName)) AND UnitId<>@UnitId AND IsActive=1", para).Rows.Count > 0 ? true : false;

                }
                else
                {
                  
                    SqlParameter[] para = new SqlParameter[1];
                    para[0] = new SqlParameter().CreateParameter("@UnitName", obj.UnitName);
                    return new dalc().GetDataTable_Text("SELECT * FROM UnitMaster with(nolock) WHERE RTRIM(LTRIM(UnitName))=RTRIM(LTRIM(@UnitName)) AND IsActive=1", para).Rows.Count > 0 ? true : false;

                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IQueryable<UnitMaster> GetAllUnit()
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var unittype = context.UnitMasters;
                //    scope.Complete();
                //    return unittype.AsQueryable();
                //}
                return new dalc().selectbyquerydt("SELECT * FROM UnitMaster with(nolock) WHERE IsActive = 1").ConvertToList<UnitMaster>().AsQueryable();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public UnitMaster GetUnitById(int id)
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var unit = context.UnitMasters.Find(id);
                //    scope.Complete();
                //    return unit;
                //}

                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@UnitId", id);
                return new dalc().GetDataTable_Text("SELECT * FROM UnitMaster with(nolock) WHERE UnitId=@UnitId AND IsActive = 1", para).ConvertToList<UnitMaster>().FirstOrDefault();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateUnit(UnitMaster unit)
        {
            try
            {
                context.Entry(unit).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
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
