using SecondaApp.Model;

namespace SecondaApp.Abstract
{
    public interface ICategoriaService
    {
        Task AddCategoriaAsync(Categoria categoria);
    }
}
