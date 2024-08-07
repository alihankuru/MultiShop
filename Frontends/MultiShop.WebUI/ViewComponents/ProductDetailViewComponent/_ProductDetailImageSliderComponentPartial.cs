﻿using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Services.ProductImageServices;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using MultiShop.DtoLayer.CatalogDtos.ProductImageDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.ProductDetailViewComponent
{
    public class _ProductDetailImageSliderComponentPartial : ViewComponent
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public _ProductDetailImageSliderComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7070/api/ProductImages/ProductImagesByProductId?id=" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<GetByIdProductImageDto>(jsonData);

                if (values != null)
                {
                    return View(values);
                }
            }
            return View();
          
        }


        //private readonly IProductImageService _productImageService;
        //public _ProductDetailImageSliderComponentPartial(IProductImageService productImageService)
        //{
        //    _productImageService = productImageService;
        //}
        //public async Task<IViewComponentResult> InvokeAsync(string id)
        //{
        //    var values = await _productImageService.GetByProductIdProductImageAsync(id);
        //    return View(values);
        //}

    }
}
