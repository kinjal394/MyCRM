using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;
using System.Transactions;
using CRM_Repository.DataServices;
using System.Data.SqlClient;

namespace CRM_Repository.Service
{
    public class EmailSignature_Repository : IEmailSignature_Repository, IDisposable
    {
        dalc odal = new dalc();
        private CRM_Repository.Data.elaunch_crmEntities context;

        public EmailSignature_Repository(elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void AddEmailSignature(SignatureMaster obj)
        {
            try
            {
                odal.updatedata(@"INSERT INTO[gurjari_crmuser].[SignatureMaster]
           ([Title]
           ,[Signature]
           ,[UserId]
           ,[CreatedBy]
           ,[CreatedDate] 
           ,[IsActive])
     VALUES 
('" + obj.Title + "',N'" + obj.Signature.Trim().Replace("'", "''") + "','" + obj.UserId + "','" + obj.CreatedBy + "','" + obj.CreatedDate + "','1')");

                //context.SignatureMasters.Add(obj);
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public bool CheckEmailSignature(SignatureMaster obj)
        {
            try
            {
                if (obj.SignatureId != default(int))
                {
                    SqlParameter[] para = new SqlParameter[2];
                    para[0] = new SqlParameter().CreateParameter("@Title", obj.Title);
                    para[1] = new SqlParameter().CreateParameter("@SignatureId", obj.SignatureId);
                    return new dalc().GetDataTable_Text("SELECT * FROM SignatureMaster with(nolock) WHERE RTRIM(LTRIM(Title)) = RTRIM(LTRIM(@Title)) AND SignatureId <> @SignatureId AND IsActive = 1", para).Rows.Count > 0 ? true : false;

                }
                else
                {
                    SqlParameter[] para = new SqlParameter[1];
                    para[0] = new SqlParameter().CreateParameter("@Title", obj.Title);
                    return new dalc().GetDataTable_Text("SELECT * FROM SignatureMaster with(nolock) WHERE RTRIM(LTRIM(Title)) = RTRIM(LTRIM(@Title)) AND IsActive = 1", para).Rows.Count > 0 ? true : false;

                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public IQueryable<SignatureMaster> GetAllSignature()
        {
            try
            {
                return new dalc().selectbyquerydt("SELECT * FROM SignatureMaster with(nolock) ").ConvertToList<SignatureMaster>().AsQueryable();
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        public SignatureMaster GetEmailSignatureById(int SignatureId)
        {
            try
            {
               SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@SignatureId", SignatureId);
                return new dalc().GetDataTable_Text("SELECT * FROM SignatureMaster with(nolock) WHERE SignatureId=@SignatureId", para).ConvertToList<SignatureMaster>().FirstOrDefault();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateEmailSignature(SignatureMaster obj)
        {
            try
            {
                //odal.updatedata(@"UPDATE[gurjari_crmuser].[SignatureMaster] SET Title='" + obj.Title + "',Signature=N'" + obj.Signature.Replace("'","''") + "',ModifyBy='" + obj.ModifyBy + "',ModifyDate=getdate() where SignatureId='" + obj.SignatureId + "'");
                context.Entry(obj).State = System.Data.Entity.EntityState.Modified;

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
