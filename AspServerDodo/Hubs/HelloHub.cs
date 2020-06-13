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
            Helper.Log("Create hub");
        }

        public void ClientRegistration(String json)
        {
            Helper.Log($" ConnectionID:     {Context.ConnectionId}");
            User user = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(json);

            Helper.Log($"Client Registration Userkey: {user.keyRoom}, guid: {user.guid}");

            Program.UnityClients.Add(user, new MyClientProxy() {ClientProxy = Clients.Caller, ConnectionID = Context.ConnectionId});
            Program.room.AddUser(user);
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Helper.Log("_____________Disconnect");
            Helper.Log("ConnectionID: "+Context.ConnectionId);

            foreach (KeyValuePair<User, MyClientProxy> pair in Program.UnityClients)
            {
                //Trace.WriteLine($"\n Key:           {pair.Key.guid},\n Value:         {pair.Value},\n Client.Calle:  {Clients.Caller} \n");

                if (pair.Value.ConnectionID == Context.ConnectionId)
                {
                    Helper.Log("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
                    Program.UnityClients.Remove(pair.Key);
                    Program.room.RemoveUser(pair.Key);
                }
            }
            //Program.UnityClients.Remove()
            return base.OnDisconnectedAsync(exception);
        }
    }

    public class MyClientProxy
    {
        public IClientProxy ClientProxy;
        public string ConnectionID;
    }
}