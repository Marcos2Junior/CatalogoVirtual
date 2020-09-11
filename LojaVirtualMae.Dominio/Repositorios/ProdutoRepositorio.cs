using LojaVirtualMae.Dominio.DbContexto;
using LojaVirtualMae.Dominio.Entidades;
using LojaVirtualMae.Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LojaVirtualMae.Dominio.Repositorios
{
    public class ProdutoRepositorio : IProdutoRepositorio
    {
        private readonly LojaVirtualDbContexto _lojaContext;

        public ProdutoRepositorio(LojaVirtualDbContexto lojaVirtualContexto)
        {
            _lojaContext = lojaVirtualContexto;
        }

        public async Task<bool> AdicionarAsync(Produto produto)
        {
            await _lojaContext.AddAsync(produto);
            await _lojaContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AtualizarAsync(Produto produto)
        {
            if (_lojaContext.Entry(produto).State == EntityState.Detached)
            {
                _lojaContext.Attach(produto);
                _lojaContext.Entry(produto).State = EntityState.Modified;
            }
            else
            {
                _lojaContext.Update(produto);
            }

            await _lojaContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeletarAsync(Produto produto)
        {
            _lojaContext.Remove(produto);
            await _lojaContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Existe(int idProduto)
        {
            return await _lojaContext.Produtos.AnyAsync(x => x.Id == idProduto);
        }

        public async Task<Produto> ObterPorIdAsync(int idProduto)
        {
            return await _lojaContext.Produtos.FirstOrDefaultAsync(x => x.Id == idProduto);
        }

        public async Task<IEnumerable<Produto>> ObterTodosAsync()
        {
            return await _lojaContext.Produtos.ToListAsync();
        }
    }
}
