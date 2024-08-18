using SecondaApp.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecondaApp.Abstract
{
    public interface IProdottoCategoriaViewRepository
    {
        Task<IEnumerable<ProdottoCategoriaView>> GetAllAsync();
    }
}
