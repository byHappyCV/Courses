using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using SignalRChat.Models;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        static List<User> Users = new List<User>();

        // Отправка сообщений
        public void Send(string name, string message)
        {
            Clients.All.addMessage(name, message);
        }

        public void Show()
        {
            Clients.Caller.showAll(Users);
        }

        public void SendPrivate(string name, string message, string id)
        {
            Clients.Client(id).sendPrivate(name, message);
        }

        public void SendFriendRequest(string userId, string friendId)
        {
            Users.Find(c => c.ConnectionId == friendId).Requests.Add(new FriendRequest{FromId = userId});
            Clients.Client(friendId).sendFriendRequest(userId);

        }
        public void AddFriend(string userId, string id, string answer)
        {
            if (answer == "yes")
            {
                var user = Users.Find(c => c.ConnectionId == id);
                var friend = Users.Find(c => c.ConnectionId == userId);
                user.Friends.Add(friend);
            }
            if (answer == " no")
            {
                var request = Users.Find(c => c.ConnectionId == id).Requests.Find(c => c.FromId == userId);
                Users.Find(c => c.ConnectionId == id).Requests.Remove(request);
            }
        }

        public void ShowFriends(string id)
        {
            Clients.Caller.showFriends(Users.Find(c => c.ConnectionId == id).Friends);
        }

        // Подключение нового пользователя
        public void Connect(string userName)
        {
            var id = Context.ConnectionId;


            if (!Users.Any(x => x.ConnectionId == id))
            {
                Users.Add(new User { ConnectionId = id, Name = userName });

                // Посылаем сообщение текущему пользователю
                Clients.Caller.onConnected(id, userName, Users);

                // Посылаем сообщение всем пользователям, кроме текущего
                Clients.AllExcept(id).onNewUserConnected(id, userName);
            }
        }

        // Отключение пользователя
        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            var item = Users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                Users.Remove(item);
                var id = Context.ConnectionId;
                Clients.All.onUserDisconnected(id, item.Name);
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}