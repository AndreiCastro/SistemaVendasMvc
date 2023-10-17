using AutoMapper;
using SistemaVendas.Dtos;
using SistemaVendas.Models;

namespace SistemaVendas.Mappings
{
    public class EntitiesDtoProfile : Profile
    {
        /*Esse classe serve para mapear as models com o dtos. No Startup tem a sua configuração da injeção de dependencia*/
        public EntitiesDtoProfile()
        {
            CreateMap<ClienteModel, ClienteDto>().ReverseMap();
            //ReverseMap serve para dizer que tanto uma model por ser um dto como um dto pode ser uma model, por causa da key word ReverseMap

            CreateMap<VendedorModel, VendedorDto>().ReverseMap();
            CreateMap<ProdutoModel, ProdutoDto>().ReverseMap();
            CreateMap<VendaModel, VendaDto>().ReverseMap();
        }        
    }
}
