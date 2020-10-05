using LojaVirtualMae.Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LojaVirtualMae.Dominio.Interfaces
{
    public interface IUsuarioRepositorio : ILojaVirtualMaeRepositorio
    {
        Task<Usuario> GetUsuarioByIdAsync(int id);
        Task<List<Usuario>> GetAllUsuariosAsync();
    }
}
