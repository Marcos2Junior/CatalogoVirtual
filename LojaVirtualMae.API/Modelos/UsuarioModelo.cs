using System;

namespace LojaVirtualMae.API.Modelos
{
    public class UsuarioModelo
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Apelido { get; set; }
        public DateTime Nascimento { get; set; }
        public string TelefoneFixo { get; set; }
        public bool ReceberEmail { get; set; }
        public bool ReceberMensagem { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Imagem { get; set; }
        public string Password { get; set; }
        public EnderecoModelo Endereco { get; set; }
    }
}
