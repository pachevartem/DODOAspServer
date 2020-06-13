using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using AspServerDodo.Hubs;
using AspServerDodo.Logic;
using DodoDataModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Timer = System.Timers.Timer;

namespace AspServerDodo
{
    public class Program
    {
        public static Dictionary<User, MyClientProxy> UnityClients = new Dictionary<User, MyClientProxy>();
        public static Room room;
        public static void Main(string[] args)
        {
            room = new Room("lol");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }


    public static class Helper
    {
        public static void Log(string w)
        {
            Trace.WriteLine(w);
            Console.WriteLine(w);
        }
    }
}
