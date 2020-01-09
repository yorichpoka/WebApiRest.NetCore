using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using WebApiRest.NetCore.Domain.Models;

namespace WebApiRest.NetCore.Domain.Hubs
{
    public class AuthorizationHub : Hub
    {
        public async Task Create(AuthorizationModel obj)  
        {  
            await Clients.All.SendAsync("AuthorizationCreated", obj);
        } 

        public async Task Update(AuthorizationModel obj)  
        {  
            await Clients.All.SendAsync("AuthorizationUpdated", obj);
        } 

        public async Task Delete(object ids)  
        {  
            await Clients.All.SendAsync("AuthorizationDeleted", ids);
        } 

        public async Task DeleteArray(object[] ids)  
        {  
            await Clients.All.SendAsync("AuthorizationsDeleted", ids);
        } 
    }
}