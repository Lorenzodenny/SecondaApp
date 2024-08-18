using SecondaApp.Abstract;
using SecondaApp.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecondaApp.Service
{
    public class ProdottoCategoriaViewService : IProdottoCategoriaViewService
    {
        private readonly IProdottoCategoriaViewRepository _repository;

        public ProdottoCategoriaViewService(IProdottoCategoriaViewRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProdottoCategoriaView>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}
