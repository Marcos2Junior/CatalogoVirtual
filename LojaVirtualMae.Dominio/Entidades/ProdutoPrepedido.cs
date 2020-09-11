using System;

namespace LojaVirtualMae.Dominio.Entidades
{
    public class ProdutoPrepedido
    {
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public int PrepedidoId { get; set; }
        public Prepedido Prepedido { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}