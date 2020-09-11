﻿using System.ComponentModel.DataAnnotations;

namespace LojaVirtualMae.API.Modelos
{
    public class CategoriaModelo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} Obrigatório")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "{0} tamanho deve ser entre {2} e {1} caracteres")]
        public string Nome { get; set; }
        public string Descricao { get; set; }
    }
}