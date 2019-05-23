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
    public class EmailSpeech_Repository : IEmailSpeech_Repository, IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;

        public EmailSpeech_Repository(elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void AddEmailSpeech(EmailSpeechMaster obj)
        {
            try
            {
                context.EmailSpeechMasters.Add(obj);
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public bool CheckEmailSpeechExist(EmailSpeechMaster obj)
        {
            try
            {
                if (obj.SpeechId != default(int))
                {
                    SqlParameter[] para = new SqlParameter[2];
                    para[0] = new SqlParameter().CreateParameter("@Title", obj.Title);
                    para[1] = new SqlParameter().CreateParameter("@SpeechId", obj.SpeechId);
                    return new dalc().GetDataTable_Text("SELECT * FROM EmailSpeechMaster with(nolock) WHERE RTRIM(LTRIM(Title)) = RTRIM(LTRIM(@Title)) AND SpeechId <> @SpeechId AND IsActive = 1", para).Rows.Count > 0 ? true : false;


                }
                else
                {
                    SqlParameter[] para = new SqlParameter[1];
                    para[0] = new SqlParameter().CreateParameter("@Title", obj.Title);
                    return new dalc().GetDataTable_Text("SELECT * FROM EmailSpeechMaster with(nolock) WHERE RTRIM(LTRIM(Title)) = RTRIM(LTRIM(@Title)) AND IsActive = 1", para).Rows.Count > 0 ? true : false;

                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        public EmailSpeechMaster CheckEmailSpeech(string title)
        {
            try
            {
                    SqlParameter[] para = new SqlParameter[1];
                    para[0] = new SqlParameter().CreateParameter("@Title", title);
                    return new dalc().GetDataTable_Text("SELECT * FROM EmailSpeechMaster with(nolock) WHERE RTRIM(LTRIM(Title)) = RTRIM(LTRIM(@Title)) AND IsActive = 1", para).ConvertToList<EmailSpeechMaster>().FirstOrDefault();

            }
            catch (Exception)
            {

                throw;
            }

        }
        public IQueryable<EmailSpeechMaster> GetAllEmailSpeech()
        {
            try
            {
                return new dalc().selectbyquerydt("SELECT * FROM EmailSpeechMaster with(nolock) ").ConvertToList<EmailSpeechMaster>().AsQueryable();

            }
            catch (Exception)
            {

                throw;
            }

        }

        public EmailSpeechMaster GetEmailSpeechById(int SpeechId)
        {
            try
            {
               SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@SpeechId", SpeechId);
                return new dalc().GetDataTable_Text("SELECT * FROM EmailSpeechMaster with(nolock) WHERE SpeechId=@SpeechId", para).ConvertToList<EmailSpeechMaster>().FirstOrDefault();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateEmailSpeech(EmailSpeechMaster obj)
        {
            try
            {
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
