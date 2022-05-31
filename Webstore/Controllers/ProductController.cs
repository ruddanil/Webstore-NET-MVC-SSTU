using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Webstore.Domain.Entity;
using Webstore.Domain.ViewModel.Product;
using Webstore.Service.Interfaces;

namespace Webstore.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult ReadProducts()
        {
            var response = _productService.ReadProducts();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return View("Error", $"{response.Description}");
        }

        [HttpGet]
        public async Task<IActionResult> ReadProductById(Guid id)
        {
            var response = await _productService.ReadProductByID(id);
            return PartialView("ReadProduct", response.Data);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var response = await _productService.DeleteProduct(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("ReadProducts");
            }
            return View("Error", $"{response.Description}");
        }
        

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Save(Guid id)
        {
            if (id == Guid.Empty)
            {
                return PartialView();
            }

            var response = await _productService.ReadProductByID(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return PartialView(response.Data);
            }
            ModelState.AddModelError("Error", response.Description);
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                if (product.Id_product == Guid.Empty || product.Id_product == null)
                {
                    await _productService.CreateProduct(product);
                }
                else
                {
                    await _productService.UpdateProduct(product.Id_product, product);
                }
                return RedirectToAction("ReadProducts");
            }
            return View();
        }
    }

}
