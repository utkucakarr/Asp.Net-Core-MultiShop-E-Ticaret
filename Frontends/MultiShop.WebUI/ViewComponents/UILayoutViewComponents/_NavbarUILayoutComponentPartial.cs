using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using MultiShop.WebUI.Services.CatalogServices.CategoryServices;
using MultiShop.WebUI.Services.Interfaces;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text.Json.Nodes;

namespace MultiShop.WebUI.ViewComponents.UILayoutViewComponents
{
    public class _NavbarUILayoutComponentPartial : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        private readonly IIdentityService _identityService;

        public _NavbarUILayoutComponentPartial(ICategoryService categoryService, IIdentityService identityService)
        {
            _categoryService = categoryService;
            _identityService = identityService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.isLogin = _identityService.IsAuthenticated();
            var values = await _categoryService.GetAllCategoryAsync();
            return View(values);
        }
    }
}