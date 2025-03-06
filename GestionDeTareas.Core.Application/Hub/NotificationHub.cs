using Microsoft.AspNetCore.SignalR;
namespace GestionDeTareas.Core.Application.Hub;

public class NotificationHub : Microsoft.AspNetCore.SignalR.Hub
{
    public async Task SendMessage(string message)
    {
        await Clients.All.SendAsync("SendNotification", message);
    }
}