using LojaVirtualMae.Dominio.DbContexto;
using LojaVirtualMae.Dominio.Entidades;
using LojaVirtualMae.Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

    }
}
