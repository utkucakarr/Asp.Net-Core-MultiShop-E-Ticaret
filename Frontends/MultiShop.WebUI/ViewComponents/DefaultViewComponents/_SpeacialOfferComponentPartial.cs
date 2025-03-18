using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.SpecialOfferDtos;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.ViewComponents.DefaultViewComponents
{
    public class _SpeacialOfferComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientBuilder;

        public _SpeacialOfferComponentPartial(IHttpClientFactory httpClientBuilder)
        {
            _httpClientBuilder = httpClientBuilder;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientBuilder.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7070/api/SpecialOffers");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultSpecialOfferDto>>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}
