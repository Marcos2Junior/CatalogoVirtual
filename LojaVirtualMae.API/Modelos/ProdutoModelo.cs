using System;
using System.ComponentModel.DataAnnotations;

namespace LojaVirtualMae.API.Modelos
{
    public class ProdutoModelo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} é de preenchimento Obrigatório")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "{0} tamanho deve ser entre {2} e {1} caracteres")]
        public string Nome { get; set; }

        [StringLength(400, MinimumLength = 10, ErrorMessage = "{0} tamanho deve ser entre {2} e {1} caracteres")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "{0} é Obrigatório")]
        [Range(0.1, 50000.0, ErrorMessage = " {0} deve ser entre {1} a {2}")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public decimal PrecoAtual { get; set; }

        [Range(0.1, 50000.0, ErrorMessage = " {0} deve ser entre {1} a {2}")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public decimal? PrecoAntigo { get; set; }
        public decimal? DescontoPorcentagem { get; set; }

        [Required(ErrorMessage = "{0} Obrigatório")]
        [Range(1, 50000, ErrorMessage = " {0} deve ser entre {1} a {2}")]
        public int Estoque { get; set; }
        public CategoriaModelo Categoria { get; set; }
        public DestaqueModelo Destaque { get; set; }
        public bool Ativo { get; set; }
    }
}
