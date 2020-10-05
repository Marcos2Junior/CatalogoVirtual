using System;
using System.Collections.Generic;

namespace LojaVirtualMae.Dominio.Entidades
{
    public class Prepedido
    {
        public int Id { get; set; }
        public int UsuarioID { get; set; }
        public Usuario Usuario { get; set; }
        public ICollection<ProdutoPrepedido> ProdutoPrepedidos { get; set; }
        public DateTime DataCadastro { get; set; }

    }
}