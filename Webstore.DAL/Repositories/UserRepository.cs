using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webstore.DAL.Interfaces;
using Webstore.Domain.Entity;

namespace Webstore.DAL.Repositories
{
    public class UserRepository : IBaseRepository<User>
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Create(User entity)
        {
            await _context.User.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public IQueryable<User> ReadAll()
        {
            return _context.User;
        }
        public async Task Delete(User entity)
        {
            _context.User.Remove(entity);
            await _context.SaveChangesAsync();
        }
        public async Task<User> Update(User entity)
        {
            _context.User.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
