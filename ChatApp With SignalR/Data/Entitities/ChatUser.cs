using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp_With_SignalR.Data.Entitities
{
    public class ChatUser
    {
        public ChatUser()
        {

        }
        public ChatUser(string userName, string connectionId)
        {
            this.UserName = userName;
            this.ConnectionId = connectionId;
        }
        public string UserName { get; set; }

        public string ConnectionId { get; set; }
    }
}
