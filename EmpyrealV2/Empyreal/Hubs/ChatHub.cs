using Empyreal.ViewModels.Display;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Empyreal.Hubs
{
    public class ChatHub : Hub
    {
        public async Task GetRequest(ProductBasicViewModel model)
        {
            await Clients.All.SendAsync("ReloadQuantity", model);
        }
    }
}
