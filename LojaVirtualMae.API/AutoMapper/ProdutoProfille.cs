using AutoMapper;
using LojaVirtualMae.API.Modelos;
using LojaVirtualMae.Dominio.Entidades;

namespace LojaVirtualMae.API.AutoMapper
{
    public class ProdutoProfille : Profile
    {
        public ProdutoProfille()
        {
            CreateMap<Produto, ProdutoModelo>();
            CreateMap<ProdutoModelo, Produto>();
        }
    }
}