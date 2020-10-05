using LojaVirtualMae.Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LojaVirtualMae.Dominio.Interfaces
{
    public interface IUsuarioRepositorio : ILojaVirtualMaeRepositorio
    {
        Task<Usuario> GetUsuarioByIdAsync(int id);
        Task<Usuario> GetUsuarioByCPFAsync(string cpf);
        Task<List<Usuario>> GetAllUsuariosAsync();
        Task<string> GetNewUserNameAsync(string nome);
    }
}
