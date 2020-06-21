using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using SAED.ApplicationCore.Entities;
using SAED.ApplicationCore.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAED.Infrastructure.Data
{
    public class EfRepository<T> : IAsyncRepository<T> where T : BaseEntity, IAggregateRoot
    {
        protected readonly ApplicationDbContext _context;

        public EfRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            var specificationResult = await ApplySpecification(spec);
            return await specificationResult.ToListAsync();
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            var specificationResult = await ApplySpecification(spec);
            return await specificationResult.CountAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);

            return entity;
        }

        public Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            return Task.FromResult(entity);
        }

        public Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);

            return Task.FromResult(entity);
        }

        public async Task<T> FirstAsync(ISpecification<T> spec)
        {
            var specificationResult = await ApplySpecification(spec);
            return await specificationResult.FirstAsync();
        }

        public async Task<T> FirstOrDefaultAsync(ISpecification<T> spec)
        {
            var specificationResult = await ApplySpecification(spec);
            return await specificationResult.FirstOrDefaultAsync();
        }

        private async Task<IQueryable<T>> ApplySpecification(ISpecification<T> spec)
        {
            return await EfSpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }
    }
}