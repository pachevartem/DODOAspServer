using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            Trace.WriteLine("Create hub");
        }

        public void ClientRegistration(String json)
        {
            User user = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(json);

            Trace.WriteLine($"Client Registration Userkey: {user.keyRoom}, guid: {user.guid}");

            Program.UnityClients.Add(user, Clients.Caller);
            Program.room.AddUser(user);
        }
    }
}