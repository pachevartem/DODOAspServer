using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using AspServerDodo.Logic;
using DodoDataModel;
using Microsoft.AspNetCore.SignalR;

namespace AspServerDodo.Hubs
{
    public class HelloHub : Hub
    {
        public HelloHub()
        {
            Helper.Log($"Create new hub");
        }

        public void ClientRegistration(String json)
        {
            User user = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(json);

            Helper.Log($"Client Registration Userkey: {user.keyRoom}, guid: {user.guid}");

            Program.UnityClients.Add(user, new MyClientProxy() { ClientProxy = Clients.Caller, ConnectionID = Context.ConnectionId });
            Program.room.AddUser(user);
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Helper.Log($"Client Discnnected with ConnectionID: {Context.ConnectionId}");

            User _user = null;
            foreach (KeyValuePair<User, MyClientProxy> pair in Program.UnityClients)
            {
                if (pair.Value.ConnectionID == Context.ConnectionId)
                {
                    _user = pair.Key;
                }
            }

            if (_user != null)
            {
                Program.UnityClients.Remove(_user);
                Program.room.RemoveUser(_user);
            }

            return base.OnDisconnectedAsync(exception);
        }
    }

    public class MyClientProxy
    {
        public IClientProxy ClientProxy;
        public string ConnectionID;
    }
}