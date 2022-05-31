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
    public class OrderProductService : IOrderProductService
    {
        private readonly IBaseRepository<OrderProduct> _orderProductRepository;
        public OrderProductService(IBaseRepository<OrderProduct> orderProductRepository)
        {
            _orderProductRepository = orderProductRepository;
        }
        public async Task<IBaseResponse<OrderProduct>> CreateOrderProduct(Guid id_order, Guid id_product, int quantity, decimal pricePerUnit)
        {
            try
            {
                var orderProduct = new OrderProduct(id_order, id_product, quantity, pricePerUnit);
                await _orderProductRepository.Create(orderProduct);
                return new BaseResponse<OrderProduct>()
                {
                    StatusCode = StatusCode.OK,
                    Data = orderProduct
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<OrderProduct>()
                {
                    Description = $"[CreateOrder] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteOrderProduct(Guid id)
        {
            try
            {
                var orderProduct = await _orderProductRepository.ReadAll().FirstOrDefaultAsync(x => x.IdOrder == id);
                if (orderProduct == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "Order not found",
                        StatusCode = StatusCode.ProductNotFound,
                        Data = false
                    };
                }
                await _orderProductRepository.Delete(orderProduct);
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

        public IBaseResponse<List<OrderProduct>> ReadOrderProducts()
        {
            try
            {
                var orderProducts = _orderProductRepository.ReadAll().ToList();
                if (!orderProducts.Any())
                {
                    return new BaseResponse<List<OrderProduct>>()
                    {
                        Description = "Orders not found",
                        StatusCode = StatusCode.OK
                    };

                }
                return new BaseResponse<List<OrderProduct>>()
                {
                    Data = orderProducts,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<OrderProduct>>()
                {
                    Description = $"[ReadOrders] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
