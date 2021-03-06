using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
using RealEstate.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly RealEstateDbContext _context;

        public GenericRepository(RealEstateDbContext context)
        {
            _context = context;
        }
        public async Task<T> Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public Task<bool> Delete(T entity)
        {
            _context.Set<T>().Remove(entity);

            return Task.FromResult(true);
        }

        public async Task<bool> Exists(Guid id)
        {
            var item = await Get(id);
            return item != null;
        }

        public async Task<T> Get(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public Task<bool> Update(T entity)
        {
            _context.Set<T>().Update(entity);

            return Task.FromResult(true);
        }
    }
}
