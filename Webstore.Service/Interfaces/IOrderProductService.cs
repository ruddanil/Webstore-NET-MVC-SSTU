using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webstore.Domain.Entity;
using Webstore.Domain.Response;

namespace Webstore.Service.Interfaces
{
    public interface IOrderProductService
    {
        Task<IBaseResponse<OrderProduct>> CreateOrderProduct(Guid id_order, Guid id_product, int quantity, decimal pricePerUnit);
        IBaseResponse<List<OrderProduct>> ReadOrderProducts();
        Task<IBaseResponse<bool>> DeleteOrderProduct(Guid id);
    }
}
