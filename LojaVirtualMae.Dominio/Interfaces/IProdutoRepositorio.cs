using LojaVirtualMae.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LojaVirtualMae.Dominio.Interfaces
{
    public interface IProdutoRepositorio
    {
        Task<bool> AdicionarAsync(Produto produto);
        Task<bool> AtualizarAsync(Produto produto);
        
        Task<IEnumerable<Produto>> ObterTodosAsync();
        Task<Produto> ObterPorIdAsync(int idProduto);
        Task<bool> DeletarAsync(Produto produto);
        Task<bool> Existe(int idProduto);
    }
}
