﻿using Microsoft.AspNet.SignalR.Client;
using Microsoft.Owin;
using Microsoft.Owin.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ivr_test
{
    class Program
    {

        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .CreateLogger();

            string baseAddress = "http://localhost:8623/";

            using (WebApp.Start<Startup>("http://*:8623/"))
            {
                // Let's wire up a SignalR client here to easily inspect what
                //  calls are happening
                //
                var hubConnection = new HubConnection(baseAddress);
                IHubProxy eventHubProxy = hubConnection.CreateHubProxy("IvrHub");
                eventHubProxy.On<string, CallDetails>("onNewCall", (channel, ev) =>
                    Log.Information("Event received on {channel} channel - {@ev}", channel, ev));
                hubConnection.Start().Wait();

                // Join the channel for task updates in our console window
                //
                //eventHubProxy.Invoke("Subscribe", Constants.AdminChannel);
                //eventHubProxy.Invoke("Subscribe", Constants.TaskChannel);

                Console.WriteLine($"Server is running on {baseAddress}");
                Console.WriteLine("Press <enter> to stop server");
                Console.ReadLine();
            }
        }
    }
}
