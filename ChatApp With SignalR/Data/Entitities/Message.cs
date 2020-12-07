using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp_With_SignalR.Data.Entitities
{
    public class Message
    {
        public int Id { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }

        public string MessageText { get; set; }

        public string DateCreated{ get; set; }
    
    }
}
