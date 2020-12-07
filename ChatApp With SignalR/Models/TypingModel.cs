using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp_With_SignalR.Models
{
    public class TypingModel
    {
        public string ReceiverId{ get; set; }
        public bool IsTyping{ get; set; }
    }
}
