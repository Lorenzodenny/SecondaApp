using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SecondaApp.Abstract;
using SecondaApp.Model;

namespace SecondaApp.DataAcessLayer.Repository
{
    public class ProdottoRepository : IProdottoRepository
    {
        private readonly ApplicationDbContext _context;
        private DbSet<Prodotto> _dbSet;

        public ProdottoRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Prodotto>();
        }

        public async Task<IEnumerable<Prodotto>> GetAllAsync()
        {
            return await _dbSet.FromSqlRaw("EXEC GetAllProdotti").ToListAsync();
        }

        public async Task<Prodotto> GetByIdAsync(int id)
        {
            var idParameter = new SqlParameter("@Id", id);
            return await _dbSet.FromSqlRaw("EXEC GetProdottoById @Id", idParameter).FirstOrDefaultAsync();
        }

        public async Task AddAsync(Prodotto prodotto)
        {
            var parameters = new[]
            {
            new SqlParameter("@Nome", prodotto.Nome),
            new SqlParameter("@Descrizione", prodotto.Descrizione),
            new SqlParameter("@Prezzo", prodotto.Prezzo)
        };
            await _context.Database.ExecuteSqlRawAsync("EXEC AddProdotto @Nome, @Descrizione, @Prezzo", parameters);
        }

        public async Task UpdateAsync(Prodotto prodotto)
        {
            var parameters = new[]
            {
            new SqlParameter("@Id", prodotto.Id),
            new SqlParameter("@Nome", prodotto.Nome),
            new SqlParameter("@Descrizione", prodotto.Descrizione),
            new SqlParameter("@Prezzo", prodotto.Prezzo)
        };
            await _context.Database.ExecuteSqlRawAsync("EXEC UpdateProdotto @Id, @Nome, @Descrizione, @Prezzo", parameters);
        }

        public async Task DeleteAsync(int id)
        {
            var idParameter = new SqlParameter("@Id", id);
            await _context.Database.ExecuteSqlRawAsync("EXEC DeleteProdotto @Id", idParameter);
        }
    }
}
