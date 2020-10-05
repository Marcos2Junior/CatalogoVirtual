using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace LojaVirtualMae.Dominio.Entidades
{
    public class Usuario : IdentityUser<int>
    {
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public string CPF { get; set; }
        public string Imagem { get; set; }
        public DateTime Nascimento { get; set; }
        public string TelefoneFixo { get; set; }
        public bool ReceberMensagem { get; set; }
        public bool ReceberEmail { get; set; }
        public DateTime DataCadastro { get; set; }
        public int? EnderecoId { get; set; }
        public Endereco Endereco { get; set; }
        public Prepedido Prepedido { get; set; }
        public List<UserRole> UserRoles { get; set; }
    }
}
