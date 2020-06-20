using SAED.ApplicationCore.Interfaces;
using SAED.Infrastructure.Data;
using System;
using System.Threading.Tasks;

namespace SAED.ApplicationCore.Services
{
    public class UnityOfWorkService : IUnityOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnityOfWorkService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }
    }
}
