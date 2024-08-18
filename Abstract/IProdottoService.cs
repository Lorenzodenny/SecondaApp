using SecondaApp.Model;

namespace SecondaApp.Abstract
{
    public interface IProdottoService
    {
        Task<IEnumerable<Prodotto>> GetAllProdottiAsync();
        Task<Prodotto> GetProdottoByIdAsync(int id);
        Task AddProdottoAsync(Prodotto prodotto);
        Task UpdateProdottoAsync(Prodotto prodotto);
        Task DeleteProdottoAsync(int id);
    }
}
