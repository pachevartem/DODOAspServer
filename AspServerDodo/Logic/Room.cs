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
            Helper.Log($"Room key: {keyRoom}, Userkey: {u.keyRoom}");
            if (!Users.Contains(u) && u.keyRoom == keyRoom)
            {
                Users.Add(u);
            }

            Helper.Log($"Count client: {Users.Count}");

            if (Users.Count == 2)
            {
                foreach (var user in Users)
                {
                    Program.UnityClients[user].ClientProxy.SendCoreAsync("OnRoomComlete", new[] { "1" });
                }
            }
        }

        public void RemoveUser(User u)
        {
            Helper.Log("REMOVE USER");
            foreach (User user in Users)
            {
                Helper.Log($"UserID: {user.guid} \nu: {u.guid}");
            }
            if (Users.Contains(u))
            {
                Helper.Log("Contains USER");
                Users.Remove(u);
            }
        }

    }
}
