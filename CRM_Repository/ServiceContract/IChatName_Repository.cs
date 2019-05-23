using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;

namespace CRM_Repository.ServiceContract
{
    public interface IChatName_Repository :IDisposable
    {
        void AddChatName(ChatNameMaster obj);
        void UpdateChatName(ChatNameMaster obj);
        void DeleteChatName(int id);
        ChatNameMaster GetChatNameById(int id);
        IQueryable<ChatNameMaster> getAllChatName();
        IQueryable<ChatNameMaster> GetchatNameById(int ChatId);
        IQueryable<ChatNameMaster> DuplicateChatName(string ChatName);
        IQueryable<ChatNameMaster> DuplicateEditChatName(int ChatId, string ChatName);
    }
}
