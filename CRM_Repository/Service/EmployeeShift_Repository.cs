using CRM_Repository.Data;
using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using CRM_Repository.DataServices;

namespace CRM_Repository.Service
{
    public partial class EmployeeShift_Repository : IEmployeeShift_Repository, IDisposable

    {
        private elaunch_crmEntities context;

        public EmployeeShift_Repository(elaunch_crmEntities context)
        {
            this.context = context;
        }

        public IEnumerable<EmployeeShitfMaster> GetEmployeeShits()
        {
            try
            {
                //return context.EmployeeShitfMasters.ToList();
                return new dalc().selectbyquerydt("SELECT * FROM EmployeeShitfMaster with(nolock) WHERE  IsActive = 1").ConvertToList<EmployeeShitfMaster>().AsQueryable();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public EmployeeShitfMaster GetShiftByID(int id)
        {
            try
            {
               // return context.EmployeeShitfMasters.Find(id);
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@ShiftId", id);
                return new dalc().GetDataTable_Text("SELECT * FROM EmployeeShitfMaster with(nolock) WHERE ShiftId=@ShiftId ", para).ConvertToList<EmployeeShitfMaster>().FirstOrDefault();

            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool CheckShiftExist(EmployeeShitfMaster obj, bool isUpdate)
        {
            try
            {
                if (isUpdate)
                {
                   
                    SqlParameter[] para = new SqlParameter[2];
                    para[0] = new SqlParameter().CreateParameter("@ShiftName", obj.ShiftName);
                    para[1] = new SqlParameter().CreateParameter("@ShiftId", obj.ShiftId);
                    return new dalc().GetDataTable_Text("SELECT * FROM EmployeeShitfMaster with(nolock) WHERE RTRIM(LTRIM(ShiftName)) = RTRIM(LTRIM(@ShiftName)) AND ShiftId <> @ShiftId AND IsActive = 1", para).Rows.Count > 0 ? true : false;

                }
                else
                {
                  
                    SqlParameter[] para = new SqlParameter[1];
                    para[0] = new SqlParameter().CreateParameter("@ShiftName", obj.ShiftName);
                    return new dalc().GetDataTable_Text("SELECT * FROM EmployeeShitfMaster with(nolock) WHERE RTRIM(LTRIM(ShiftName)) = RTRIM(LTRIM(@ShiftName))  AND IsActive = 1", para).Rows.Count > 0 ? true : false;

                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void InsertShist(EmployeeShitfMaster student)
        {
            try
            {
                context.EmployeeShitfMasters.Add(student);
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
          
        }

        public void UpdateShift(EmployeeShitfMaster shift)
        {
            try
            {
                context.Entry(shift).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
          
        }

        public void Save()
        {
            try
            {
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
