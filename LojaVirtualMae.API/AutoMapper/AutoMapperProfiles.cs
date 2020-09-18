using AutoMapper;
using LojaVirtualMae.API.Modelos;
using LojaVirtualMae.Dominio.Entidades;

namespace LojaVirtualMae.API.AutoMapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Produto, ProdutoModelo>().ReverseMap();
            CreateMap<Categoria, CategoriaModelo>().ReverseMap();
            CreateMap<Destaque, DestaqueModelo>().ReverseMap();
        }
    }
}