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
   public class ChatName_Repository : IChatName_Repository,IDisposable
    {

        private CRM_Repository.Data.elaunch_crmEntities context;
        public ChatName_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }
        public void AddChatName(ChatNameMaster obj)
        {
            try
            {
                context.ChatNameMasters.Add(obj);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

        }
        public void DeleteChatName(int id)
        {
            try
            {
                ChatNameMaster Chat = context.ChatNameMasters.Find(id);
                if (Chat != null)
                {
                    context.ChatNameMasters.Remove(Chat);
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IQueryable<ChatNameMaster> getAllChatName()
        {
            try
            {
                return new dalc().selectbyquerydt("SELECT * FROM ChatNameMaster with(nolock)").ConvertToList<ChatNameMaster>().AsQueryable();
            }
            catch (Exception)
            {

                throw;
            }

        }
        public ChatNameMaster GetChatNameById(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@ChatId", id);
                return new dalc().GetDataTable_Text("SELECT * FROM ChatNameMaster with(nolock) WHERE ChatId=@ChatId AND IsActive = 1", para).ConvertToList<ChatNameMaster>().FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }

        }
        public IQueryable<ChatNameMaster> GetchatNameById(int ChatId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@ChatId", ChatId);
                return new dalc().GetDataTable_Text("SELECT * FROM ChatNameMaster with(nolock) WHERE ChatId=@ChatId AND IsActive = 1", para).ConvertToList<ChatNameMaster>().AsQueryable();

            }
            catch (Exception)
            {

                throw;
            }

        }

        public IQueryable<ChatNameMaster> DuplicateEditChatName(int ChatId, string ChatName)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@ChatName", ChatName);
                para[1] = new SqlParameter().CreateParameter("@ChatId", ChatId);
                return new dalc().GetDataTable_Text("SELECT * FROM ChatNameMaster with(nolock) WHERE RTRIM(LTRIM(ChatName))=RTRIM(LTRIM(@ChatName))  AND ChatId<>@ChatId  AND IsActive = 1", para).ConvertToList<ChatNameMaster>().AsQueryable();

            }
            catch (Exception)
            {

                throw;
            }

        }
        public IQueryable<ChatNameMaster> DuplicateChatName(string ChatName)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@ChatName", ChatName);
                return new dalc().GetDataTable_Text("SELECT * FROM ChatNameMaster with(nolock) WHERE RTRIM(LTRIM(ChatName))=RTRIM(LTRIM(@ChatName))  AND IsActive = 1", para).ConvertToList<ChatNameMaster>().AsQueryable();

            }
            catch (Exception)
            {

                throw;
            }

        }


        public void UpdateChatName(ChatNameMaster obj)
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
