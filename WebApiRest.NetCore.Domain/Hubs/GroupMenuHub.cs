using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using WebApiRest.NetCore.Domain.Models;

namespace WebApiRest.NetCore.Domain.Hubs
{
    public class GroupMenuHub : Hub
    {
        public async Task Create(GroupMenuModel obj)  
        {  
            await Clients.All.SendAsync("GroupMenuCreated", obj);
        } 

        public async Task Update(GroupMenuModel obj)  
        {  
            await Clients.All.SendAsync("GroupMenuUpdated", obj);
        } 

        public async Task Delete(int id)  
        {  
            await Clients.All.SendAsync("GroupMenuDeleted", id);
        } 

        public async Task DeleteArray(int[] id)  
        {  
            await Clients.All.SendAsync("GroupMenusDeleted", id);
        } 
    }
}