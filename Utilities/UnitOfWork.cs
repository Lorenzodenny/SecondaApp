using Microsoft.EntityFrameworkCore.Storage;
using SecondaApp.Abstract;
using SecondaApp.DataAcessLayer;

namespace SecondaApp.Utilities
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction _transaction;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                _context.SaveChanges();
                _transaction.Commit();
            }
            catch 
            {
                _transaction.Rollback();
                throw;
            }
        }

        public void Rollback()
        {
            _transaction?.Rollback();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
    }
}
