using AutoMapper;
using DotnetBoilerplate.Application.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace DotnetBoilerplate.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private bool _disposed = false;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task CommitAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                _transaction = null; // Reset the transaction after committing changes
            }
        }

        public void Rollback()
        {
            _transaction?.Rollback();
        }

        private void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                _transaction?.Dispose();
                _context.Dispose();
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
