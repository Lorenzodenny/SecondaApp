using Microsoft.EntityFrameworkCore;
using SecondaApp.Abstract;

namespace SecondaApp.DataAcessLayer.Repository
{
    public class Repository<T> : IRepository<T>  where T : class
    {
        private readonly ApplicationDbContext _context;
        readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Insert(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if(entity != null)
            {
                _dbSet.Remove(entity);
            }
        }
    }
}
