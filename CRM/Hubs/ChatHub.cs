using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using CRM_Repository.Data;
using Microsoft.AspNet.SignalR.Hubs;

namespace CRM.Hubs
{
    public class ChatHub : Hub
    {
        public void Send(string name, string message)
        {
            Clients.All.addNewMessageToPage(name, message);
        }

        public void Subscribe(string customerId)
        {
            Groups.Add(Context.ConnectionId, customerId);
        }

        [HubMethodName("sendNotifications")]
        public static void SendNotification(string userid,string message)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            context.Clients.All.updateNotificationList(userid, message);
        }

        [HubMethodName("sendInvoiceMessage")]
        public static void SendInvoiceMessage(int userId)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            context.Clients.All.getMessage(userId);
        }
        public void Unsubscribe(string customerId)
        {
            Groups.Remove(Context.ConnectionId, customerId);
        }

    }
}