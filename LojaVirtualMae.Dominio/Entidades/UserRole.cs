using Microsoft.AspNetCore.Identity;

namespace LojaVirtualMae.Dominio.Entidades
{
    public class UserRole : IdentityUserRole<int>
    {
        public Usuario Usuario { get; set; }
        public Role Role { get; set; }
    }
}