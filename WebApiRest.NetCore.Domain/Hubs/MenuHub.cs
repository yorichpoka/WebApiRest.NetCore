using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using WebApiRest.NetCore.Domain.Models;

namespace WebApiRest.NetCore.Domain.Hubs
{
    public class MenuHub : Hub
    {
        public async Task Create(MenuModel obj)
        {
            await Clients.All.SendAsync("MenuCreated", obj);
        }

        public async Task Update(MenuModel obj)
        {
            await Clients.All.SendAsync("MenuUpdated", obj);
        }

        public async Task Delete(int id)
        {
            await Clients.All.SendAsync("MenuDeleted", id);
        }

        public async Task DeleteArray(int[] id)
        {
            await Clients.All.SendAsync("MenusDeleted", id);
        }
    }
}