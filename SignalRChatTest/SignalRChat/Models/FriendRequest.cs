using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRChat.Models
{
    public class FriendRequest
    {
        public string FromId { get; set; }
        public string ToId { get; set; }
    }
}