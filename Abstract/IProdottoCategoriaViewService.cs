using SecondaApp.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecondaApp.Abstract
{
    public interface IProdottoCategoriaViewService
    {
        Task<IEnumerable<ProdottoCategoriaView>> GetAllAsync();
    }
}
