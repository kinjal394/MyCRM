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
    public class PlugShape_Repository : IPlugShape_Repository, IDisposable
    {

        private CRM_Repository.Data.elaunch_crmEntities context;

        public PlugShape_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void SavePlugShape(PlugShapeMaster objPlugShape)
        {
            try
            {
                context.PlugShapeMasters.Add(objPlugShape);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void UpdatePlugShape(PlugShapeMaster objPlugShape)
        {
            try
            {
                context.Entry(objPlugShape).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeletePlugShape(int PlugShapeId)
        {
            try
            {
                PlugShapeMaster objPlugShape = context.PlugShapeMasters.Where(z => z.PlugShapeId == PlugShapeId).SingleOrDefault();
                objPlugShape.IsActive = false;
                context.Entry(objPlugShape).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public PlugShapeMaster GetByPlugShapeId(int Id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@PlugShapeId", Id);
                return new dalc().GetDataTable_Text("SELECT * FROM PlugShapeMaster with(nolock) WHERE PlugShapeId = @PlugShapeId AND IsActive = 1", para).ConvertToList<PlugShapeMaster>().FirstOrDefault();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<PlugShapeMaster> GetPlugShape()
        {
            try
            {
                return new dalc().selectbyquerydt("SELECT * FROM PlugShapeMaster with(nolock) WHERE  IsActive = 1 ").ConvertToList<PlugShapeMaster>().AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool IsExist(int PlugShapeId, string Title)
        {
            try
            {

                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@PlugShapeId", PlugShapeId);
                para[1] = new SqlParameter().CreateParameter("@Title", Title);
                return new dalc().GetDataTable_Text(" SELECT * FROM PlugShapeMaster WHERE PlugShapeId <> @PlugShapeId AND RTRIM(LTRIM(Title)) = RTRIM(LTRIM(@Title)) AND IsActive = 1", para).Rows.Count > 0 ? true : false;
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
