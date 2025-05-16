
namespace MultiShop.SignalRRealTimeApi.Services.SignalRCommentServices
{
    public class SignalRCommentServices : ISignalRCommentServices
    {
        private readonly HttpClient _httpClient;

        public SignalRCommentServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> GetTotalCommentCountAsync()
        {
            var responseMessage = await _httpClient.GetAsync("UserMessage/GetTotalMessageCountByRecieverId?id=");
            var values = await responseMessage.Content.ReadFromJsonAsync<int>();
            return values;
        }
    }
}
