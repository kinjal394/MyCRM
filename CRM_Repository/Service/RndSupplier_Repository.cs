using CRM_Repository.Data;
using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using CRM_Repository.DataServices;
using CRM_Repository.DTOModel;

using System.Transactions;

namespace CRM_Repository.Service
{
    public class RndSupplier_Repository : IRndSupplier_Repository, IDisposable
    {
        dalc odal = new dalc();
        private Data.elaunch_crmEntities context;
        public RndSupplier_Repository(Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public RNDSupplierMaster GetRndSupplierById(int id)
        {
            try
            {
                return context.RNDSupplierMasters.Find(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<RNDSupplierMaster>GetRndSupplierByProductId(int id)
        {
            try
            {
                return context.RNDSupplierMasters.Where(p => p.RNDProductId == id).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public SupplierDetailByIdModel GetCompanyDetailById(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@supplierid", id);
                return odal.GetDataTable_Text(@"SELECT A.supplierid,Website,Address,MobileNo,Email FROM SupplierMaster AS A
                                                LEFT JOIN SupplierAddressMaster  AS B ON A.supplierid=B.supplierid
                                                LEFT JOIN SupplierContactDetail AS C ON A.supplierid=C.supplierid WHERE A.supplierid=@supplierid
                                                AND ISNULL(A.IsActive,0)=1", para).ConvertToList<SupplierDetailByIdModel>().AsQueryable().FirstOrDefault();


            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }
      

        private bool disposedValue = false;
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
    }
}
