using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRChat.Models
{
    public class User
    {
        public string ConnectionId { get; set; }
        public string Name { get; set; }
        public List<User> Friends { get; set; } = new List<User>();
        public List<FriendRequest> Requests { get; set; } = new List<FriendRequest>();
    }
}