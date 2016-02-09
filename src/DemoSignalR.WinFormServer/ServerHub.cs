using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DemoSignalR.Model;
using Microsoft.AspNet.SignalR;

namespace DemoSignalR.WinFormServer
{
    public class ServerHub : Hub
    {
        public void AddCustomer(Customer customer)
        {            
            Clients.All.RefreshData(customer);            
        }

        public override Task OnConnected()
        {
            Program.MainForm.WriteToConsole("Client connected: " + Context.ConnectionId);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            Program.MainForm.WriteToConsole("Client disconnected: " + Context.ConnectionId);
            return base.OnDisconnected(stopCalled);

            return base.OnDisconnected(stopCalled);
        }
    }
}
