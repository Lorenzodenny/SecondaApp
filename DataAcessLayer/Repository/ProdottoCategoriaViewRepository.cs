using Microsoft.EntityFrameworkCore;
using SecondaApp.Abstract;
using SecondaApp.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecondaApp.DataAcessLayer.Repository
{
    public class ProdottoCategoriaViewRepository : IProdottoCategoriaViewRepository
    {
        private readonly ApplicationDbContext _context;
        private DbSet<ProdottoCategoriaView> _dbSet;

        public ProdottoCategoriaViewRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<ProdottoCategoriaView>();
        }

        public async Task<IEnumerable<ProdottoCategoriaView>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
    }
}
