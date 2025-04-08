using AutoMapper;
using POSApi.Application.DTO.KupacDTO;
using POSApi.Application.DTO.ProizvodDTO;
using POSApi.Application.DTO.Stavke_racunaDTO;
using POSApi.Application.DTO.Zaglavlje_racunaDTO;
using POSApi.Domain.Models;

namespace POSApi.WebApi.Mappings
{
    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {

            CreateMap<Kupac, CreateKupacDTO>().ReverseMap();
            CreateMap<Kupac, GetKupacDTO>().ReverseMap();
            CreateMap<Kupac, UpdateKupacDTO>().ReverseMap();

            CreateMap<Proizvod, CreateProizvodDTO>().ReverseMap();
            CreateMap<Proizvod, GetProizvodDTO>().ReverseMap();
            CreateMap<Proizvod, UpdateProizvodDTO>().ReverseMap();

            CreateMap<Zaglavlje_racuna, CreateZaglavlje_racunaDTO>().ReverseMap();
            CreateMap<Zaglavlje_racuna, GetZaglavlje_racunaDTO>().ReverseMap();
            CreateMap<Zaglavlje_racuna, UpdateZaglavlje_racunaDTO>().ReverseMap();

            CreateMap<Stavke_racuna, CreateStavke_racunaDTO>().ReverseMap();
            CreateMap<Stavke_racuna, GetStavke_racunaDTO>()
                .ForMember(dest => dest.BROJ, opt => opt.MapFrom(src => src.ZAGLAVLJE_RACUNA.BROJ));
            CreateMap<Stavke_racuna, UpdateStavke_racunaDTO>().ReverseMap();

        }
    }
}
