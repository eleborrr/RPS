using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RPS.Application.Features.GameRoom.AddParticipant;
using RPS.Application.Features.GameRoom.GetGameRoomInfo;
using RPS.Application.Features.Match.CreateNewMatch;
using RPS.Application.Features.Match.MakeMove;
using RPS.Application.Features.Round.GetRoundResult;
using RPS.Domain.Entities;
using RPS.Infrastructure.Database;
using RPS.Shared.Rating;

namespace RPS.API.Hubs
{
    public class GameRoomHub : Hub
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMediator _mediator;
        private readonly IBus _bus;

        public GameRoomHub(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager, IMediator mediator, IBus bus)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _mediator = mediator;
            _bus = bus;
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
            string gameRoomId)
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
            
            await Clients.Group(gameRoomId).SendAsync("ReceivePrivateMessage", senderUserName, 
                message);
        }
        
        public async Task MakeMove(string roundId, string moveId, string userId)
        {
            await _mediator.Send(new MakeMoveCommand(roundId, moveId, userId));
        }

        public async Task JoinLobby(string gameRoomId, string userId)
        {
            var res = await _mediator.Send(new AddParticipantCommand(gameRoomId, userId));
            if (res.IsSuccess)
            {
                await StartRound(gameRoomId);
            }
            //TODO if failure show exception
            else
            {
                Console.WriteLine(res.Error);
            }
        }

        public async Task StartRound(string gameRoomId)
        {
            //MOCK
            // var roomInfo = new GameRoomInfoDto("lol", DateTime.Now, "12", "2", "1", true, true);
            var roomInfo = (await _mediator.Send(new GetGameRoomInfoQuery(gameRoomId))).Value!;
            while (roomInfo.CreatorConnected && roomInfo.ParticipantConnected)
            {
                var newRoundId = (await _mediator.Send(
                        new CreateNewRoundCommand(roomInfo.CreatorId, roomInfo.ParticipantId)))
                    .Value;
                await Clients.Group(gameRoomId).SendAsync("GameStarted", newRoundId);
                
                await SendCountDownTick(7, gameRoomId);
                await SendResultOfRound(gameRoomId, newRoundId);
                await SendCountDownTick(3, gameRoomId);
                // roomInfo = (await _mediator.Send(new GetGameRoomInfoQuery(gameRoomId))).Value!;
            }
        }

        public async Task SendResultOfRound(string gameRoomId, string roundId)
        {
            var res = (await _mediator.Send(new GetRoundResultQuery(roundId))).Value;
            
            if (res.IsDraw)
            {
                await _bus.Publish(new AdjustUserRatingMongoDto() {UserId = res.WinnerId, Adjust = 1});
                await _bus.Publish(new AdjustUserRatingMongoDto(){UserId = res.LoserId, Adjust = 1});
            }
            else
            {
                await _bus.Publish(new AdjustUserRatingMongoDto(){UserId = res.LoserId, Adjust = -1});
                await _bus.Publish(new AdjustUserRatingMongoDto(){UserId = res.WinnerId, Adjust = 3});
            }

            await Clients.Group(gameRoomId).SendAsync("ReceiveGameResult", res.WinnerId);
        }

        public async Task SendCountDownTick(int timer, 
            string gameRoomId)
        {
            while (timer > 0)
            {
                await Clients.Group(gameRoomId).SendAsync("ReceiveTimer", timer);
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
            // _mediator.Send(new RemoveParticipantCommand());
            await base.OnDisconnectedAsync(exception);
        }
        
        private async Task<string?> GetUserName(string id)
        {
            return (await _userManager.FindByIdAsync(id))?.UserName;
        }
    }
}