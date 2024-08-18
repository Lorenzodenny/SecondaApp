using SecondaApp.Model;

namespace SecondaApp.Abstract
{
    public interface IProdottoRepository
    {
        Task<IEnumerable<Prodotto>> GetAllAsync();
        Task<Prodotto> GetByIdAsync(int id);
        Task AddAsync(Prodotto prodotto);
        Task UpdateAsync(Prodotto prodotto);
        Task DeleteAsync(int id);
    }
}
