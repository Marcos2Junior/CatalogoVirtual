using LojaVirtualMae.Dominio.DbContexto;
using LojaVirtualMae.Dominio.Entidades;
using LojaVirtualMae.Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LojaVirtualMae.Dominio.Repositorios
{
    public class ProdutoRepositorio : LojaVirtualRepositorio, IProdutoRepositorio
    {
        public ProdutoRepositorio(LojaVirtualDbContexto lojaVirtualContexto) : base(lojaVirtualContexto)
        {
        }

        public async Task<Produto> GetProdutoByIdAsync(int idProduto)
        {
            return await _lojaContext.Produtos
                .Include(x => x.Categoria)
                .Include(x => x.Destaque)
                .FirstOrDefaultAsync(x => x.Id == idProduto);
        }

        public async Task<List<Produto>> GetAllProdutosAsync()
        {
            return await _lojaContext.Produtos
                .Include(x => x.Categoria)
                .Include(x => x.Destaque)
                .ToListAsync();
        }
    }
}
