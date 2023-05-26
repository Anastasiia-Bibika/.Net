using Laboratory2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Laboratory2.Hubs
{
    [Authorize]
    public class ChatHub : Microsoft.AspNetCore.SignalR.Hub
    {
        private readonly ApplicationDbContext _context;
        public ChatHub(ApplicationDbContext context)
        {
            this._context = context;
        }
        public override Task OnConnectedAsync()
        {
            // Отримуємо ідентифікатор користувача, який намагається приєднатися до чату
            var userId = _context.Users.Name;

            // Перевіряємо права доступу користувача
            if (HasPermission(userId))
            {
                // Користувач має право надсилати повідомлення, дозволяємо йому приєднатися до чату
                return base.OnConnectedAsync();
            }
            else
            {
                // Користувач не має права надсилати повідомлення, відмовляємо йому в приєднанні
                throw new HubException("У вас немає прав доступу до цього чату.");
            }
        }
        private bool HasPermission(string userId)
        {
            return true;
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        public async Task Join([FromQuery] int chatId)
        {
            /* to-do: додати валідацію чи має право користувач під'єднатися до чату */

            await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
        }

        public async Task Leave([FromQuery] int chatId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId.ToString());
        }

        public async Task SendMessage([FromQuery] int chatId, string message)
        {
            /* to-do: додати валідацію чи має право користувач надсилати повідомлення у чаті */

            var user = Helpers.AuthHelper.GetUser(Context.User);
            await Clients.Group(chatId.ToString()).SendAsync("Message", new { user, message });

            /* to-do: зберегти меседж у БД у таблиці меседжів */
        }
    }
}
