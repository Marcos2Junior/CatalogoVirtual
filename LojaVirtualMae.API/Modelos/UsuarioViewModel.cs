using System;
using System.ComponentModel.DataAnnotations;

namespace LojaVirtualMae.API.Modelos
{
    public class UsuarioViewModel
    {
        [Required(ErrorMessage = "Id é obrigatório")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório"), StringLength(50, MinimumLength = 5, ErrorMessage = "Nome deve ter 4 a 50 caracteres")]
        public string Nome { get; set; }
        public string CPF { get; set; }
        [StringLength(10, ErrorMessage = "Apelido deve ter no máximo 10 caracteres.")]
        public string Apelido { get; set; }
        [DataType(DataType.Date, ErrorMessage = "Insira uma data de nascimento válida.")]
        public DateTime Nascimento { get; set; }
        public string TelefoneFixo { get; set; }

        [Required]
        public bool ReceberEmail { get; set; }

        [Required]
        public bool ReceberMensagem { get; set; }

        [Required(ErrorMessage = "Email é obrigatório"), DataType(DataType.EmailAddress, ErrorMessage = "Email inválido")]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Imagem { get; set; }
        public EnderecoModelo Endereco { get; set; }
    }
}