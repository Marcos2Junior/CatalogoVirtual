using System;
using System.Collections.Generic;

namespace LojaVirtualMae.Dominio.Entidades
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal PrecoAtual { get; set; }
        public decimal? PrecoAntigo { get; set; }
        public decimal? DescontoPorcentagem { get; set; }
        public int Estoque { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
        public int? DestaqueId { get; set; }
        public Destaque Destaque { get; set; }
        public ICollection<ProdutoPedido> ProdutoPedidos { get; set; }
        public ICollection<ProdutoPrepedido> ProdutoPrepedidos { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }
    }
}
