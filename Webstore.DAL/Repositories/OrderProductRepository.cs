using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webstore.DAL.Interfaces;
using Webstore.Domain.Entity;

namespace Webstore.DAL.Repositories
{
    public class OrderProductRepository : IBaseRepository<OrderProduct>
    {
        private readonly ApplicationDbContext _context;
        public OrderProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(OrderProduct entity)
        {
            await _context.OrderProduct.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public IQueryable<OrderProduct> ReadAll()
        {
            return _context.OrderProduct;
        }
        public async Task Delete(OrderProduct entity)
        {
            _context.OrderProduct.Remove(entity);
            await _context.SaveChangesAsync();
        }
        public async Task<OrderProduct> Update(OrderProduct entity)
        {
            _context.OrderProduct.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
