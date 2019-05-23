using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;

namespace CRM_Repository.ServiceContract
{
    public interface ILicense_Repository: IDisposable
    {
        void AddLicense(LicenseMaster obj);
        void UpdateLicense(LicenseMaster obj);
        void DeleteLicense(int id);
        LicenseMaster GetLicenseByID(int id);
        IQueryable<LicenseMaster> GetAllLicense();
        IQueryable<LicenseMaster> DuplicateLicense(string LicenseName);
        IQueryable<LicenseMaster> DuplicateEditLicense(int LicenseId, string LicenseName);

    }
}
