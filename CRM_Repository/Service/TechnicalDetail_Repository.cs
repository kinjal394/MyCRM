using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;
using CRM_Repository.DataServices;
using CRM_Repository.ServiceContract;
namespace CRM_Repository.Service
{
    public class TechnicalDetail_Repository : ITechnicalDetail_Repository, IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;

        public TechnicalDetail_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void AddTechnicalDetail(TOTechnicalDetail obj)
        {
            context.TOTechnicalDetails.Add(obj);
            context.SaveChanges();
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
           
        }
        #endregion

    }
}
