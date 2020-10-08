using System.Collections.Generic;
using System.Threading.Tasks;

namespace LojaVirtualMae.Dominio.Interfaces
{
    public interface ILojaVirtualMaeRepositorio
    {
        Task<bool> AdicionarAsync<T>(T entity) where T : class;
        Task<bool> AtualizarAsync<T>(T entity) where T : class;
        Task<bool> DeletarAsync<T>(T entity) where T : class;
        Task<bool> DeletarRangeAsync<T>(T[] entityarray) where T : class;
        Task<List<T>> SelecionarTodosAsync<T>() where T : class; 
    }
}
