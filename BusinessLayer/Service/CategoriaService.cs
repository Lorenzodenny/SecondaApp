using SecondaApp.Abstract;
using SecondaApp.Model;

namespace SecondaApp.BusinessLayer.Service
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task AddCategoriaAsync(Categoria categoria)
        {
            await _categoriaRepository.AddAsync(categoria);
        }
    }
}
