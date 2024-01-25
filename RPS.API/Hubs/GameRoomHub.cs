using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RPS.Application.Features.Match.GetMatchResult;
using RPS.Application.Features.Match.MakeMove;
using RPS.Domain.Entities;
using RPS.Infrastructure.Database;

namespace RPS.API.Hubs
{
    public class GameRoomHub : Hub
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly IMediator _mediator;

        public GameRoomHub(ApplicationDbContext dbContext, UserManager<User> userManager, IMediator mediator)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _mediator = mediator;
        }
        
        public async Task GetGroupMessages(string gameRoomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, gameRoomId);

            var room = _dbContext.GameRooms.FirstOrDefault(r => r.Id == gameRoomId);

            if (room is null)
                return;

            var messages = await _dbContext.Messages.Where(m => m.GameRoomId == room.Id).OrderBy(m => m.Timestamp).ToListAsync();
            
            foreach (var message in messages)
            {
                await Clients.Client(Context.ConnectionId).SendAsync("ReceivePrivateMessage", 
                    await GetUserName(message.SenderId), 
                    message.Content);
            }
        }
        
        public async Task SendPrivateMessage(string senderUserName, 
            string message,
            string gameRoomId,
            string groupName)
        {
            var room = _dbContext.GameRooms.FirstOrDefault(r => r.Id == gameRoomId);
            
            var sender = await _userManager.FindByNameAsync(senderUserName);
            
            if (sender is null || room is null)
                return;

            var newMessage = new Message
            {
                Id = Guid.NewGuid().ToString(),
                Content = message,
                Timestamp = DateTime.Now,
                SenderId = sender.Id,
                GameRoomId = gameRoomId,
            };

            _dbContext.Messages.Add(newMessage);
            await _dbContext.SaveChangesAsync();
            
            await Clients.Group(groupName).SendAsync("ReceivePrivateMessage", senderUserName, 
                message);
        }
        
        public async Task MakeMove(string matchId, string moveId, string userId)
        {
            await _mediator.Send(new MakeMoveCommand(matchId, moveId, userId));
        }

        public async Task SendResultOfGame(string groupName, string matchId)
        {
            var res = await _mediator.Send(new GetMatchResultQuery(matchId));
            await Clients.Group(groupName).SendAsync("ReceiveGameResult", res.Value);
        }

        public async Task SendCountDownTick(int timer, 
            string groupName)
        {
            while (timer > 0)
            {
                await Clients.Group(groupName).SendAsync("ReceivePrivateMessage", timer);
                timer--;
                Thread.Sleep(1000);
            }
        }
        
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
        }
        
        private async Task<string?> GetUserName(string id)
        {
            return (await _userManager.FindByIdAsync(id))?.UserName;
        }
    }
}