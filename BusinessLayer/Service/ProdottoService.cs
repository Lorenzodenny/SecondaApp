using SecondaApp.Abstract;
using SecondaApp.Model;

namespace SecondaApp.BusinessLayer.Service
{
    public class ProdottoService : IProdottoService
    {
        private readonly IProdottoRepository _prodottoRepository;

        public ProdottoService(IProdottoRepository prodottoRepository)
        {
            _prodottoRepository = prodottoRepository;
        }

        public async Task<IEnumerable<Prodotto>> GetAllProdottiAsync()
        {
            return await _prodottoRepository.GetAllAsync();
        }

        public async Task<Prodotto> GetProdottoByIdAsync(int id)
        {
            return await _prodottoRepository.GetByIdAsync(id);
        }

        public async Task AddProdottoAsync(Prodotto prodotto)
        {
            await _prodottoRepository.AddAsync(prodotto);
        }

        public async Task UpdateProdottoAsync(Prodotto prodotto)
        {
            await _prodottoRepository.UpdateAsync(prodotto);
        }

        public async Task DeleteProdottoAsync(int id)
        {
            await _prodottoRepository.DeleteAsync(id);
        }
    }
}
