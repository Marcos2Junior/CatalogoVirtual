using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace LojaVirtualMae.Dominio.Entidades
{
    public class Role : IdentityRole<int>
    {
        public List<UserRole> UserRoles { get; set; }
    }
}