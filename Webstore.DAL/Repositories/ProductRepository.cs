using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webstore.DAL.Interfaces;
using Webstore.Domain.Entity;

namespace Webstore.DAL.Repositories
{
    public class ProductRepository : IBaseRepository<Product>
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Create(Product entity)
        {
            await _context.Product.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public IQueryable<Product> ReadAll()
        {
            return _context.Product;
        }
        public async Task Delete(Product product)
        {
            product.Trashed = true;
            _context.Product.Update(product);
            await _context.SaveChangesAsync();
        }
        public async Task<Product> Update(Product entity)
        {
            _context.Product.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

    }
}
