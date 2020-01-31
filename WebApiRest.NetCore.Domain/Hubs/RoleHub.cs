using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using WebApiRest.NetCore.Domain.Models;

namespace WebApiRest.NetCore.Domain.Hubs
{
    public class RoleHub : Hub
    {
        public async Task Create(RoleModel obj)  
        {  
            await Clients.All.SendAsync("RoleCreated", obj);
        } 

        public async Task Update(RoleModel obj)  
        {  
            await Clients.All.SendAsync("RoleUpdated", obj);
        } 

        public async Task Delete(int id)  
        {  
            await Clients.All.SendAsync("RoleDeleted", id);
        } 

        public async Task DeleteArray(int[] id)  
        {  
            await Clients.All.SendAsync("RolesDeleted", id);
        } 
    }
}