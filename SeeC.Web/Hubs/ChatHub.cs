using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SeeC.Web.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task JoinRoom(string roomName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
            await Clients.Group(roomName).SendAsync("another user joined.");
        }

        public Task LeaveRoom(string roomName)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
        }

        public async Task SendMachineInformation(string roomName, string message)
        {
            await Clients.OthersInGroup(roomName).SendAsync("OnReceiveMachineInformation", message);
        }
    }
}
