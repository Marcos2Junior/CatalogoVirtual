using LojaVirtualMae.Dominio.DbContexto;
using LojaVirtualMae.Dominio.Entidades;
using LojaVirtualMae.Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtualMae.Dominio.Repositorios
{
    public class UsuarioRepositorio : LojaVirtualRepositorio, IUsuarioRepositorio
    {
        public UsuarioRepositorio(LojaVirtualDbContexto lojaVirtualContexto) : base(lojaVirtualContexto)
        {
        }

        public async Task<Usuario> GetUsuarioByIdAsync(int id) =>
             await _lojaContext.Users
                .Include(x => x.Endereco)
                .Include(x => x.Prepedido)
                .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<List<Usuario>> GetAllUsuariosAsync() =>
            await _lojaContext.Users
                 .Include(x => x.Endereco)
                 .Include(x => x.Prepedido).ToListAsync();

        public async Task<Usuario> GetUsuarioByCPFAsync(string cpf) =>
            await _lojaContext.Users.FirstOrDefaultAsync(x => x.CPF == cpf);

        public async Task<string> GetNewUserNameAsync(string nome)
        {
            var allUsers = await _lojaContext.Users.ToListAsync();
            var last = allUsers.LastOrDefault();

            int id = 1;
            if (last != null)
                id += last.Id;

            var _firstName = nome.Split(' ', System.StringSplitOptions.RemoveEmptyEntries)[0];
            _firstName = _firstName.ToLower().PadLeft(4, 'T').Substring(0, 4);

            return _firstName + id.ToString().PadLeft(5, '0');
        }
    }
}
