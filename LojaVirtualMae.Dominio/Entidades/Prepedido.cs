using System;
using System.Collections.Generic;

namespace LojaVirtualMae.Dominio.Entidades
{
    public class Prepedido
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public ICollection<ProdutoPrepedido> ProdutoPrepedidos { get; set; }
        public DateTime DataCadastro { get; set; }

    }
}