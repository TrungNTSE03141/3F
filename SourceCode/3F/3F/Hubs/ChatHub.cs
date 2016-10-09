using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using _3F.Utils;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        private static List<string> Users = new List<string>();

        public override Task OnConnected()
        {
            if (Users.Contains(Context.User.Identity.Name))
                return base.OnConnected();
            Users.Add(Context.ConnectionId);
            Clients.All.addNewMessageToPage("User online", Users.Count);
            return base.OnConnected();
        }

        public override Task OnReconnected()
        {
            if (Users.Contains(Context.User.Identity.Name))
                return base.OnReconnected();
            Users.Add(Context.User.Identity.Name);
            Clients.All.addNewMessageToPage("User online", Users.Count);
            return base.OnReconnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            Users.Remove(Context.User.Identity.Name);
            Clients.All.addNewMessageToPage("User online", Users.Count);
            return base.OnDisconnected(stopCalled);
        }

        public void Send(string name, string message)
        {
            if (!Context.User.Identity.Name.Equals("ABC"))
                // Call the addNewMessageToPage method to update clients.
                Clients.All.addNewMessageToPage(name, message);

            Console.WriteLine(Context.User.Identity.Name);

        }
    }
}