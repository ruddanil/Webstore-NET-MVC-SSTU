using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webstore.DAL.Interfaces;
using Webstore.Domain.Entity;

namespace Webstore.DAL.Repositories
{
    public class OrderRepository : IBaseRepository<Order>
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(Order entity)
        {
            await _context.Order.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public IQueryable<Order> ReadAll()
        {
            return _context.Order;
        }
        public async Task Delete(Order product)
        {
            _context.Order.Remove(product);
            await _context.SaveChangesAsync();
        }
        public async Task<Order> Update(Order entity)
        {
            _context.Order.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
