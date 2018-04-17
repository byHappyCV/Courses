using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ChatWhitAuth.Interfaces;
using ChatWhitAuth.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.AspNet.SignalR;

namespace ChatWhitAuth.Hubs
{
    public class ChatHub : Hub
    {
        private static List<UserDTO> Users;
        private IUnitOfWork _unitOfWork;

        public ChatHub(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        private UserDTO GetUser()
        {
            {
                var user = new UserDTO
                {
                    UserName =_unitOfWork.UsersRepo.GetAll().ToList().Find(c => c.Email == Context.User.Identity.Name).Email,
                    ConnectionId = Context.ConnectionId
                };

                return user;
            }

        }
        public override Task OnConnected()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();
            try
            {
                var fr = dbContext.Friendships.ToList();
            }
            catch (Exception e)
            {
                var a = e.Message;
            }
            
            var friends = _unitOfWork.FriendsRepo.GetAll().ToList();
            if (Users == null)
            {
                Users = new List<UserDTO>();
            }
            Users.Add(GetUser());
            //Clients.All.showUsers(Users);
            Clients.Caller.showFriends(ShowUsersFriends());
            Clients.All.OnMessage(
                "[server]", "Welcome to the chat room, " + Context.User.Identity.Name);
            return base.OnConnected();
        }

        public void Send(string message)
        {
            Clients.All.message(Context.User.Identity.GetUserName() + message);

        }

        public override Task OnDisconnected(bool stopCalled)
        {
            Users.Remove(Users.Find(c => c.ConnectionId == Context.ConnectionId));
            Clients.All.showUsers(Users);
            return base.OnDisconnected(stopCalled);
        }

        
        public void SendPrivate(string userConnectionId, string message)
        {
            var s = Users.Any(c => c.ConnectionId == userConnectionId);
            if (Users.Any(c => c.ConnectionId == userConnectionId))
            {
                Clients.Client(userConnectionId).sendPrivate(message);
            }
        }

        public void SendRequest(string toId)
        {
            var user = _unitOfWork.UsersRepo.GetAll().ToList()
                .Find(c => c.Email == Users.Find(a => a.ConnectionId == toId).UserName).Id;
            FriendRequest req = new FriendRequest
            {
                FromId = Context.User.Identity.GetUserId(),
                ToId = user
            };
            
            _unitOfWork.UsersRepo
                .GetAll()
                .ToList()
                .Find(c => c.Email == Users.Find(a => a.ConnectionId == toId).UserName)
                .FriendRequests.Add(req);
            _unitOfWork.Save();
            var reqId = _unitOfWork.RequestsRepo.GetAll().ToList().Find(c => c.FromId == req.FromId && c.ToId == req.ToId).Id;
            Clients.Client(toId).sendRequest(req.FromId, reqId);

        }

        public void Answer(string id, int reqId,bool answer)
        {
            if (answer)
            {
                var list = _unitOfWork.UsersRepo.GetAll().ToList()
                    .Find(c => c.Email == Users.Find(a => a.ConnectionId == id).UserName).Friendships.ToList();
                Friendship user = new Friendship
                {
                    UserId = Context.User.Identity.GetUserId(),
                    FriendId = _unitOfWork.UsersRepo.GetAll().ToList().Find(c => c.Email == Users.Find(a => a.ConnectionId == id).UserName).Id
                };
                list.Add(user);
                var delreq = _unitOfWork.RequestsRepo.GetById(reqId);
                _unitOfWork.RequestsRepo.Delete(delreq);
                _unitOfWork.Save();
            }
            else
            {
                var delreq = _unitOfWork.RequestsRepo.GetById(reqId);
                _unitOfWork.RequestsRepo.Delete(delreq);
                _unitOfWork.Save();
            }

        }
        public void ShowPrivateChat(string userConnectionId)
        {
            Clients.Caller.openChat(userConnectionId, "Chat with:" + Users.Find(c => c.ConnectionId == userConnectionId).UserName);
        }

        public void ShowUsers()
        {
            Clients.All.showUsers(Users);
        }

        public List<Friendship> ShowUsersFriends()
        {
            var friendsList = _unitOfWork.UsersRepo
                .GetAll()
                .ToList()
                .Find(c => c.Id == Context.User.Identity.GetUserId())
                .Friendships.ToList();
            return friendsList;
        }


    }
}