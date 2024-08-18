using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SecondaApp.Abstract;
using SecondaApp.Model;

namespace SecondaApp.DataAcessLayer.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly ApplicationDbContext _context;
        private DbSet<Categoria> _dbSet;

        public CategoriaRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Categoria>();
        }

        public async Task AddAsync(Categoria categoria)
        {
            var parameters = new[]
            {
                new SqlParameter("@Nome", categoria.Nome)
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC AddCategoria @Nome", parameters);
        }
    }
}
