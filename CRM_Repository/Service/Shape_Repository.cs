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
    public class Shape_Repository : IShape_Repository,IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;

        public Shape_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void SaveShape(ShapeMaster objShape)
        {
            try
            {
                context.ShapeMasters.Add(objShape);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateShape(ShapeMaster objShape)
        {
            try
            {
                context.Entry(objShape).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteShape(int ShapeId)
        {
            try
            {
                ShapeMaster objShape = context.ShapeMasters.Where(z => z.ShapeId == ShapeId).SingleOrDefault();
                objShape.IsActive = false;
                context.Entry(objShape).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ShapeMaster GetByShapeId(int ShapeId)
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var data = context.ShapeMasters.Find(ShapeId);
                //    scope.Complete();
                //    return data;
                //}
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@ShapeId", ShapeId);
                return new dalc().GetDataTable_Text("SELECT * FROM ShapeMaster with(nolock) WHERE ShapeId = @ShapeId ", para).ConvertToList<ShapeMaster>().FirstOrDefault();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<ShapeMaster> GetShape()
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var data = context.ShapeMasters.Where(x => x.IsActive == true);
                //    scope.Complete();
                //    return data;
                //}

                return new dalc().selectbyquerydt("SELECT * FROM ShapeMaster with(nolock) WHERE  IsActive = 1").ConvertToList<ShapeMaster>().AsQueryable();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool IsExist(int ShapeId, string ShapeName)
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var data = context.ShapeMasters.Where(z => z.ShapeId != ShapeId && z.ShapeName == ShapeName && z.IsActive == true).Count();
                //    if (data > 0)
                //        return true;
                //    else
                //        return false;
                //}

                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@ShapeName", ShapeName);
                para[1] = new SqlParameter().CreateParameter("@ShapeId", ShapeId);
                return new dalc().GetDataTable_Text("SELECT * FROM ShapeMaster with(nolock) WHERE RTRIM(LTRIM(ShapeName)) = RTRIM(LTRIM(@ShapeName)) AND ShapeId <> @ShapeId AND IsActive = 1", para).Rows.Count > 0 ? true : false;

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
