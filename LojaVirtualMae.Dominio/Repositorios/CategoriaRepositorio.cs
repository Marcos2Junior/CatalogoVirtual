using System.Threading.Tasks;
using LojaVirtualMae.Dominio.DbContexto;
using LojaVirtualMae.Dominio.Entidades;
using LojaVirtualMae.Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtualMae.Dominio.Repositorios
{
    public class CategoriaRepositorio : LojaVirtualRepositorio, ICategoriaRepositorio
    {
        public CategoriaRepositorio(LojaVirtualDbContexto lojaVirtualContexto) : base(lojaVirtualContexto)
        {
        }

        public async Task<Categoria> GetCategoriaByIdAsync(int idCategoria)
        => await _lojaContext.Categorias.FirstOrDefaultAsync(x => x.Id == idCategoria);
    }
}