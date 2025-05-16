using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.SignalR;
using MultiShop.SignalRRealTimeApi.Services;
using MultiShop.SignalRRealTimeApi.Services.SignalRCommentServices;
using MultiShop.SignalRRealTimeApi.Services.SignalRMessageServices;

namespace MultiShop.SignalRRealTimeApi.Hubs
{
    public class SignalRHub : Hub
    {
        private readonly ISignalRCommentServices _signalRCommentServices;
        private readonly ISignalRMessageService _signalRMessageService;

        public SignalRHub(ISignalRCommentServices signalRCommentServices, ISignalRMessageService signalRMessageService)
        {
            _signalRCommentServices = signalRCommentServices;
            _signalRMessageService = signalRMessageService;
        }

        public async Task SendStatisticCount(string id)
        {
            var getTotalCommentCount = _signalRCommentServices.GetTotalCommentCountAsync();
            await Clients.All.SendAsync("RecieveCommentCount", getTotalCommentCount);

            var getTotalMessageCount = _signalRMessageService.GetTotalMessageCountByRecieverId(id);
            await Clients.All.SendAsync("RevieceMessageCount", getTotalMessageCount);
        }
    }
}
