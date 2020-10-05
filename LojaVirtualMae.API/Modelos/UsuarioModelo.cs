using System;
using System.ComponentModel.DataAnnotations;

namespace LojaVirtualMae.API.Modelos
{
    public class UsuarioModelo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório"), StringLength(50, MinimumLength = 5, ErrorMessage = "Nome deve ter 4 a 50 caracteres")]
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Apelido { get; set; }
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

        [Required(ErrorMessage = "Senha é obrigatório")]
        public string Password { get; set; }
        public EnderecoModelo Endereco { get; set; }
    }
}
