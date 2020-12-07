using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp_With_SignalR.Models
{
    public class ChatModel
    {
        public string SenderId  { get; set; }
        public string ReceiverId{ get; set; }
        public string Message { get; set; }
        public string User { get; set; }
    }
}
