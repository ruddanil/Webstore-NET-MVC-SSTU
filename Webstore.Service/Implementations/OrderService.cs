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
using Webstore.Service.Interfaces;

namespace Webstore.Service.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IBaseRepository<Order> _orderRepository;
        public OrderService(IBaseRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<IBaseResponse<Order>> CreateOrder(Guid id_user, DateTime date)
        {
            Order order = new(id_user, date);
            try
            {
                await _orderRepository.Create(order);
                return new BaseResponse<Order>()
                {
                    StatusCode = StatusCode.OK,
                    Data = order
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Order>()
                {
                    Description = $"[CreateOrder] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteOrder(Guid id)
        {
            try
            {
                var order = await _orderRepository.ReadAll().FirstOrDefaultAsync(x => x.IdOrder == id);
                if (order == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "Order not found",
                        StatusCode = StatusCode.ProductNotFound,
                        Data = false
                    };
                }
                await _orderRepository.Delete(order);
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
                    Description = $"[DeleteOrder] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponse<List<Order>> ReadOrders()
        {
            try
            {
                var orders = _orderRepository.ReadAll().ToList();
                if (!orders.Any())
                {
                    return new BaseResponse<List<Order>>()
                    {
                        Description = "Orders not found",
                        StatusCode = StatusCode.OK
                    };

                }
                return new BaseResponse<List<Order>>()
                {
                    Data = orders,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Order>>()
                {
                    Description = $"[ReadOrders] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        public async Task<Order> GetOrderById(Guid id)
        {
            return await _orderRepository.ReadAll().FirstOrDefaultAsync(x => x.IdOrder == id);
        }
    }
}
