using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AspServerDodo.Hubs;
using DodoDataModel;

namespace AspServerDodo.Logic
{
    public class Room
    {
        public Room(string keyRoom)
        {
            this.keyRoom = keyRoom;
        }

        private string keyRoom;
        public List<User> Users = new List<User>();
       

        public void AddUser(User u)
        {
            Trace.WriteLine($"Room key: {keyRoom}, Userkey: {u.keyRoom}");
            if (!Users.Contains(u) && u.keyRoom == keyRoom)
            {
                Users.Add(u);
            }

            Trace.WriteLine($"Count client: {Users.Count}");

            if (Users.Count ==2)
            {
                foreach (var user in Users)
                {
                    Program.UnityClients[user].ClientProxy.SendCoreAsync("OnRoomComlete", new[] { "1" });
                }
            }
        }

        public void RemoveUser(User u)
        {
                Trace.WriteLine("REMOVE USER");
            foreach (User user in Users)
            {
                Trace.WriteLine($"UserID: {user.guid} \nu: {u.guid}");
            }
            if (Users.Contains(u))
            {
                Trace.WriteLine("Contains USER");
                Users.Remove(u);
            }
        }

    }
}
