using LojaVirtualMae.Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LojaVirtualMae.Dominio.Interfaces
{
    public interface IProdutoRepositorio : ILojaVirtualMaeRepositorio
    {
        Task<Produto> GetProdutoByIdAsync(int idProduto);
        Task<List<Produto>> GetAllProdutosAsync();
    }
}
