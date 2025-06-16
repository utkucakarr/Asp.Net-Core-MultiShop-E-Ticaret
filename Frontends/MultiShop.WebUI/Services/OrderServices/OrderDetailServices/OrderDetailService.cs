using MultiShop.DtoLayer.OrderDtos.OrderDetailDtos;
using MultiShop.DtoLayer.OrderDtos.OrderOrderingDtos;

namespace MultiShop.WebUI.Services.OrderServices.OrderDetailServices
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly HttpClient _httpClient;

        public OrderDetailService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateOrderDetailAsync(CreateOrderDetailDto createOrderDetailDto)
        {
            var response = await _httpClient.PostAsJsonAsync<CreateOrderDetailDto>("orderDetails", createOrderDetailDto);
        }
    }
}
