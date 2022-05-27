using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webstore.Domain.Entity;
using Webstore.Domain.Response;
using Webstore.Domain.ViewModel.Product;

namespace Webstore.Service.Interfaces
{
    public interface IProductService
    {
        Task<IBaseResponse<Product>> CreateProduct(ProductViewModel model);
        IBaseResponse<List<Product>> ReadProducts();
        Task<IBaseResponse<ProductViewModel>> ReadProductByID(Guid id);
        Task<IBaseResponse<Product>> UpdateProduct(Guid id, ProductViewModel model);
        Task<IBaseResponse<bool>> DeleteProduct(Guid id);
    }
}
