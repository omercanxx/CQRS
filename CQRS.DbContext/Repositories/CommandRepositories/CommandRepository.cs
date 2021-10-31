using CQRS.Core.Entities;
using CQRS.Core.Interfaces.CommandInterfaces;
using CQRS.Core.Interfaces.QueryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Infrastructure.Repositories.CommandRepositories
{
    public class CommandRepository<TEntity> : ICommandRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly AppDbContext _context;
        private readonly DbSet<TEntity> _dbSet;
        public CommandRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }
        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }
        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
        public void Remove(TEntity entity)
        {
            entity.Delete();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
