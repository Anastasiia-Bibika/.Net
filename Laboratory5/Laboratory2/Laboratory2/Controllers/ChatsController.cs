using System;
using Laboratory2.Services.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Laboratory2.Models;
using Microsoft.Extensions.Hosting;

namespace Laboratory2.Controllers
{
    [ApiController]
    [Authorize]
    public class ChatsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ChatsController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpGet("api/chats")]
        public IActionResult Read(
            [FromBody] User user,
            [FromQuery] string orderBy = "Id",
            [FromQuery] string order = "asc",
            [FromQuery] int page = 1,
            [FromQuery] int perPage = 25
            )
        {
            if (user.isAdmin == false)
            {
                var userChats = _context.Chats.Where(chat => chat.User.isAdmin == false);
                 return Ok(userChats);
            }
            else {
                var userChats = _context.Chats.Where(chat => chat.User.isAdmin == true);
                return Ok(userChats);
            }
        }

        [HttpPost("api/chats")]
        public IActionResult Create([FromBody] Chat chat)
        {
            var user = Helpers.AuthHelper.GetUser(HttpContext.User);
            chat.UserId = user.Id;
            _context.Chats.Add(chat);

            return Ok(chat);

        }

        [HttpDelete("api/chats/{id}")]
        public IActionResult Delete(int id)
        {
            // to-do: додати можливість видаляти чат
            var chat = _context.Chats.FirstOrDefault(c => c.Id == id);
            if (chat == null)
            {
                return NotFound();
            }

            _context.Chats.Remove(chat);
            return Ok();
        }

        [HttpGet("api/chats/{chatId}/messages")]
        public IActionResult ReadMessages(
            int chatId,
            [FromQuery] string orderBy = "Id",
            [FromQuery] string order = "asc",
            [FromQuery] int page = 1,
            [FromQuery] int perPage = 25)
        {
            // to-do: додати можливість отримувати список повідомлень у чаті

            var chat = _context.Chats.FirstOrDefault(c => c.Id == chatId);
            if (chat == null)
            {
                return NotFound();
            }

            var messages = chat.Messages;
            return Ok(messages);
        }
    }
}
