using Microsoft.AspNet.SignalR;
using System.Collections.Generic;

namespace Collabry
{
    public class VoiceHub : Hub
    {
        public void UpdateUserList(List<UserIntroPacket> users)
        {
            Clients.All.UserListUpdated(users);
        }
    }
}
