using System;
using System.Collections.Generic;

namespace LojaVirtualMae.Dominio.Entidades
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public string Senha { get; set; }
        public string CPF { get; set; }
        public DateTime Nascimento { get; set; }
        public string TelefoneFixo { get; set; }
        public string TelefoneMovel { get; set; }
        public bool ReceberMensagem { get; set; }
        public string Email { get; set; }
        public bool ReceberEmail { get; set; }
        public DateTime DataCadastro { get; set; }
        public int? EnderecoId { get; set; }
        public Endereco Endereco { get; set; }
        public Prepedido Prepedido { get; set; }
    }
}
