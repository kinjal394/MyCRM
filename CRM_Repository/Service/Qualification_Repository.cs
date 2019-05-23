using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using CRM_Repository.Data;
using CRM_Repository.ServiceContract;
using CRM_Repository.DataServices;
using System.Data.SqlClient;

namespace CRM_Repository.Service
{
    public class Qualification_Repository : IQualifications_Repository, IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;

        public Qualification_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }
        public void AddQualification(QualificationsMaster ObjQual)
        {
            try
            {
                context.QualificationsMasters.Add(ObjQual);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        //public void DeleteQualification(int id)
        //{
        //    try
        //    {
        //        SqlParameter[] para = new SqlParameter[1];
        //        para[0] = new SqlParameter().CreateParameter("@QualificationId", id);
        //        QualificationsMaster Qual = new dalc().GetDataTable_Text("SELECT * FROM QualificationsMaster with(nolock) WHERE QualificationId=@QualificationId", para).ConvertToList<QualificationsMaster>().FirstOrDefault();
        //        if (Qual != null)
        //        {
        //            context.QualificationsMasters.Remove(Qual);
        //            context.SaveChanges();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex.InnerException;
        //    }
        //}

        public IQueryable<QualificationsMaster> DuplicateEditQualification(int QualificationId, string QualificationName)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[3];
                para[0] = new SqlParameter().CreateParameter("@QualificationId", QualificationId);
                para[1] = new SqlParameter().CreateParameter("@QualificationName", QualificationName);
                para[2] = new SqlParameter().CreateParameter("@IsActive", "true");
                var Qual = new dalc().GetDataTable_Text("SELECT * FROM QualificationsMaster with(nolock) WHERE QualificationId!=@QualificationId and QualificationName=@QualificationName and IsActive=@IsActive", para).ConvertToList<QualificationsMaster>().AsQueryable();
                return Qual.AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public IQueryable<QualificationsMaster> DuplicateQualification(string QualificationName)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@QualificationName", QualificationName);
                return new dalc().GetDataTable_Text("SELECT * FROM QualificationsMaster with(nolock) WHERE RTRIM(LTRIM(QualificationName)) = RTRIM(LTRIM(@QualificationName)) AND IsActive=1 ", para).ConvertToList<QualificationsMaster>().AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public IQueryable<QualificationsMaster> GetAllQuali()
        {
            try
            {
                return new dalc().selectbyquerydt("SELECT * FROM QualificationsMaster with(nolock) WHERE IsActive=1").ConvertToList<QualificationsMaster>().AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public QualificationsMaster GetQauliByID(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@id", id);
                return new dalc().GetDataTable_Text("SELECT * FROM QualificationsMaster with(nolock) WHERE QualificationId=@id", para).ConvertToList<QualificationsMaster>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public void UpdateQualification(QualificationsMaster ObjQual)
        {
            try
            {
                context.Entry(ObjQual).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
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
