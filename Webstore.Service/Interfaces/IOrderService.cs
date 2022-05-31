using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webstore.Domain.Entity;
using Webstore.Domain.Response;

namespace Webstore.Service.Interfaces
{
    public interface IOrderService
    {
        Task<IBaseResponse<Order>> CreateOrder(Guid id_user, DateTime date);
        IBaseResponse<List<Order>> ReadOrders();
        Task<IBaseResponse<bool>> DeleteOrder(Guid id);
        Task<Order> GetOrderById(Guid id);
    }
}
