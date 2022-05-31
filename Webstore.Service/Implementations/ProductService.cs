using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webstore.DAL.Interfaces;
using Webstore.Domain.Entity;
using Webstore.Domain.Enum;
using Webstore.Domain.Response;
using Webstore.Domain.ViewModel.Product;
using Webstore.Service.Interfaces;

namespace Webstore.Service.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IBaseRepository<Product> _productRepository;
        public ProductService(IBaseRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IBaseResponse<Product>> CreateProduct(ProductViewModel model)
        {
            try
            {
                var product = new Product()
                {
                    Id_product = model.Id_product,
                    Title = model.Title,
                    Description = model.Description,
                    Img = model.Img,
                    Price = model.Price,
                    Quantity = model.Quantity
                };
                await _productRepository.Create(product);
                return new BaseResponse<Product>()
                {
                    StatusCode = StatusCode.OK,
                    Data = product
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Product>()
                {
                    Description = $"[CreateProduct] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponse<List<Product>> ReadProducts()
        {
            try
            {
                var products = _productRepository.ReadAll().Where(x => x.Trashed == false).ToList();
                if (!products.Any())
                {
                    return new BaseResponse<List<Product>>()
                    {
                        Description = "Products not found",
                        StatusCode = StatusCode.OK
                    };
                    
                }
                return new BaseResponse<List<Product>>()
                {
                    Data = products,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Product>>()
                {
                    Description = $"[ReadProducts] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Product>> UpdateProduct(Guid id, ProductViewModel model)
        {
            try
            {
                var product = await _productRepository.ReadAll().FirstOrDefaultAsync(x => x.Id_product == id);
                if (product == null)
                {
                    return new BaseResponse<Product>()
                    {
                        Description = "Product not found",
                        StatusCode = StatusCode.ProductNotFound
                    };
                }

                product.Title = model.Title;
                product.Description = model.Description;
                product.Img = model.Img;
                product.Price = model.Price;
                product.Quantity = model.Quantity;

                await _productRepository.Update(product);

                return new BaseResponse<Product>()
                {
                    Data = product,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Product>()
                {
                    Description = $"[Update] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteProduct(Guid id)
        {
            try
            {
                var product = await _productRepository.ReadAll().FirstOrDefaultAsync(x => x.Id_product == id);
                if (product == null || product.Trashed == true)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "Product not found",
                        StatusCode = StatusCode.ProductNotFound,
                        Data = false
                    };
                }
                await _productRepository.Delete(product);
                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteProduct] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<ProductViewModel>> ReadProductByID(Guid id)
        {
            try
            {
                var product = await _productRepository.ReadAll().FirstOrDefaultAsync(x => x.Id_product == id);
                if (product == null)
                {
                    return new BaseResponse<ProductViewModel>()
                    {
                        Description = "Пользователь не найден",
                        StatusCode = StatusCode.ProductNotFound
                    };
                }
                var data = new ProductViewModel()
                {
                    Id_product = product.Id_product,
                    Title = product.Title,
                    Description = product.Description,
                    Img = product.Img,
                    Price = product.Price,
                    Quantity = product.Quantity
                };

                return new BaseResponse<ProductViewModel>()
                {
                    StatusCode = StatusCode.OK,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ProductViewModel>()
                {
                    Description = $"[ReadProductById] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        public Product GetProductById(Guid id)
        {
            return _productRepository.ReadAll().FirstOrDefault(x => x.Id_product == id);
        }
    }
}
