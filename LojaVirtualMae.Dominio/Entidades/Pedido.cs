using System.Collections.Generic;

namespace LojaVirtualMae.Dominio.Entidades
{
    public class Pedido
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public string Observacao { get; set; }
        public int MetodoEntregaId { get; set; }
        public MetodoEntrega MetodoEntrega { get; set; }
        public ICollection<ProdutoPedido> ProdutoPedidos{ get; set; }
    }
}