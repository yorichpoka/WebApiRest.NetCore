using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using WebApiRest.NetCore.Domain.Enums;
using WebApiRest.NetCore.Domain.Interfaces.Bussiness;
using WebApiRest.NetCore.Domain.Models;

namespace WebApiRest.NetCore.Domain.Hubs
{
    public class UserHub : Hub
    {
        private readonly IUserBussiness _Bussiness;

        public UserHub(IUserBussiness dao)
        {
            this._Bussiness = dao;
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            // Remove user connected
            LogOutServer(this.Context.ConnectionId);

            return base.OnDisconnectedAsync(exception);
        }

        public void Create(UserModel obj)
        {
            Clients.All.SendAsync(HubClientMethods.UserCreated.ToString(), obj)
                       .Start();
        }

        public void Update(UserModel obj)
        {
            Clients.All.SendAsync(HubClientMethods.UserUpdated.ToString(), obj)
                       .Start();
        }

        public void Delete(int id)
        {
            Clients.All.SendAsync(HubClientMethods.UserDeleted.ToString(), id)
                       .Start();
        }

        public void DeleteArray(int[] id)
        {
            Clients.All.SendAsync(HubClientMethods.UsersDeleted.ToString(), id)
                       .Start();
        }

        public Task LogInServer(string connectionId, int id)
        {
            return
                Task.Factory.StartNew(
                    () =>
                    {
                        this._Bussiness.SetHubConnectionId(connectionId, id);
                    }
                );
        }

        public Task<bool> LogOutServer(string hubConnectionId, int? id = null)
        {
            return this._Bussiness.RemoveHubConnectionId(hubConnectionId, id);
        }

        public Task<string> GetHubConnectionIdServer()
        {
            return
                Task.Factory.StartNew<string>(
                    () =>
                    {
                        return this.Context.ConnectionId;
                    }
                );
        }
    }
}