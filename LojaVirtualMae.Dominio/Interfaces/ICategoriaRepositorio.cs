using System.Threading.Tasks;
using LojaVirtualMae.Dominio.Entidades;

namespace LojaVirtualMae.Dominio.Interfaces
{
    public interface ICategoriaRepositorio : ILojaVirtualMaeRepositorio
    {
         Task<Categoria> GetCategoriaByIdAsync(int idCategoria);
    }
}