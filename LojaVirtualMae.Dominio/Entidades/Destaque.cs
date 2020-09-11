using System.Collections.Generic;

namespace LojaVirtualMae.Dominio.Entidades
{
    public class Destaque
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public ICollection<Produto> Produtos{ get; set; }
    }
}