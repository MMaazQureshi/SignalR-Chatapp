using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatApp_With_SignalR.Data;
using ChatApp_With_SignalR.Data.Entitities;
using ChatApp_With_SignalR.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRHub.Hubs;

namespace ChatApp_With_SignalR
{
    public class ChatController : Controller
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public ChatController(IHubContext<ChatHub> hubCOntext, ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _hubContext = hubCOntext;
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<IActionResult> SendMessage(ChatModel model)
        {
            var user = await _userManager.FindByIdAsync(model.ReceiverId);
            List<string> connectionIds;
            ChatHub.ConnectedUsers.TryGetValue(user.UserName, out connectionIds);

            var sender = _userManager.GetUserAsync(User).Result;
            Message m = new Message()
            {
                SenderId = sender.Id,
                ReceiverId = model.ReceiverId,
                MessageText = model.Message
            };
            _dbContext.Messages.Add(m);
            foreach (var item in connectionIds)
            {
                await _hubContext.Clients.Client(item).SendAsync("ReceiveMessage", model.User, model.Message);
            }

            //await _hubContext.Clients.All.SendAsync("ReceiveMessage", model.User, model.Message);

            return Ok();
        }

        public async Task UserTyping(TypingModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            await _hubContext.Clients.All.SendAsync("IsTyping", user.UserName, model.IsTyping);
        }
    }
}
