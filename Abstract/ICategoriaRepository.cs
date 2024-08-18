using SecondaApp.Model;

namespace SecondaApp.Abstract
{
    public interface ICategoriaRepository
    {
        Task AddAsync(Categoria categoria);
    }
}
