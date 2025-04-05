using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CommentDtos;
using MultiShop.WebUI.Services.CommentServices;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Controllers
{
    public class ProductListController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ICommentService _commentService;

        public ProductListController(IHttpClientFactory httpClientFactory, ICommentService commentService)
        {
            _httpClientFactory = httpClientFactory;
            _commentService = commentService;
        }

        public IActionResult Index(string id)
        {
            @ViewBag.directory1 = "Ana Sayfa";
            @ViewBag.directory2 = "Ürünler";
            @ViewBag.directory3 = "Ürün Listesi";
            ViewBag.i = id;
            return View();
        }

        public IActionResult ProductDetail(string id)
        {
            @ViewBag.directory1 = "Ana Sayfa";
            @ViewBag.directory2 = "Ürünler Listesi";
            @ViewBag.directory3 = "Ürün Detayları";
            ViewBag.x = id;
            return View();
        }

        [HttpGet]
        public PartialViewResult AddComment()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(CreateCommentDto createCommentDto, string pid)
        {
            createCommentDto.ImageUrl = "test";
            createCommentDto.Rating = 1;
            createCommentDto.CreatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            createCommentDto.Status = false;
            //createCommentDto.ProductId = "67c89f7bcc1038b62692c1c9";
            await _commentService.CreateCommentAsync(createCommentDto);
            return RedirectToAction("Index", "Default");
            //var client = _httpClientFactory.CreateClient();
            //var jsonData = JsonConvert.SerializeObject(createCommentDto);
            //StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            //var responseMessage = await client.PostAsync("https://localhost:7059/api/Comments", stringContent);
            //if (responseMessage.IsSuccessStatusCode)
            //{
            //    return RedirectToAction("Index", "Default");
            //}
            //return View();
        }
    }
}
