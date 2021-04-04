using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FavoritesSurvey.Misc
{
    public class SignalRHub : Hub
    {
        public async Task SendSurveyChanges()
        {
            await Clients.All.SendAsync("ReceiveSurveyChanges");
        }
    }
}
