using LojaVirtualMae.Dominio.Entidades;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtualMae.Dominio.DbContexto
{
    public class LojaVirtualDbContexto : IdentityDbContext<Usuario, Role, int,
                                                           IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
                                                           IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Destaque> Destaques { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<MetodoEntrega> MetodoEntregas { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Prepedido> Prepedidos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<ProdutoPedido> ProdutoPedidos { get; set; }
        public DbSet<ProdutoPrepedido> ProdutoPrepedidos { get; set; }

        public LojaVirtualDbContexto(DbContextOptions<LojaVirtualDbContexto> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProdutoPedido>().HasKey(x => new { x.PedidoId, x.ProdutoId });
            modelBuilder.Entity<ProdutoPrepedido>().HasKey(x => new { x.ProdutoId, x.PrepedidoId });

            modelBuilder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                      .WithMany(r => r.UserRoles)
                      .HasForeignKey(ur => ur.RoleId)
                      .IsRequired();

                userRole.HasOne(ur => ur.Usuario)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });
        }
    }
}
