using System.ComponentModel.DataAnnotations;

namespace LojaVirtualMae.API.Modelos
{
    public class UsuarioLoginModelo
    {
        [Required]
        public string EmailCPF { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
