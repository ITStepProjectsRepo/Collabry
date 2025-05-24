using Microsoft.AspNet.SignalR;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collabry
{
    public class TextHub : Hub
    {
        private static readonly ConcurrentDictionary<string, UserIntroPacket> ConnectedUsers = new ConcurrentDictionary<string, UserIntroPacket>();

        public void Register(UserIntroPacket user)
        {
            ConnectedUsers[Context.ConnectionId] = user;
            BroadcastUsers();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            ConnectedUsers.TryRemove(Context.ConnectionId, out _);
            BroadcastUsers();
            return base.OnDisconnected(stopCalled);
        }

        public void UpdateUserList(List<UserIntroPacket> list)
        {
            BroadcastUsers(list);
        }

        public void SendMessage(Message_S message)
        {
            new Message_SService().AddMessage(message);
            Clients.All.ReceiveMessage(message);
        }

        private void BroadcastUsers(List<UserIntroPacket> overrideList = null)
        {
            var list = overrideList ?? ConnectedUsers.Values.ToList();
            Clients.All.UserListUpdated(list);
        }
    }
}
