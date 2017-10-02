using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ivr_test
{
    public class IvrHub: Hub
    {
        public void NewCall(CallDetails details)
        {
            Clients.Group(details.Advisor).onNewCall(details);
            Console.WriteLine($"New Call: {details}");
        }

        public async Task Subscribe(string channel)
        {
            Console.WriteLine($"Subscribe: {Context.ConnectionId} into {channel}");
            await Groups.Add(Context.ConnectionId, channel);
        }

        public Task Unsubscribe(string channel)
        {
            Console.WriteLine($"Unsubscribe: {Context.ConnectionId} from {channel}");
            return Groups.Remove(Context.ConnectionId, channel);
        }

        public override Task OnConnected()
        {
            Console.WriteLine($"Connected: {Context.ConnectionId}");
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            Console.WriteLine($"Disonnected: {Context.ConnectionId}");
            return base.OnDisconnected(stopCalled);
        }
    }
}
