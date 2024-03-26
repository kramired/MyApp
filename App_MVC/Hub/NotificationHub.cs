using Microsoft.AspNetCore.SignalR;

namespace App_MVC.Hub
{
    public class NotificationHub : Hub
    {
        public Async Task SendNotification(string msg)
        {
            await DynamicHubClients.All.SendAsync("Recieve Notification", msg);
        }
    }
}
