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
            CreateMap<Proizvod, GetProizvodDTO>()
            .ForMember(dest => dest.proizvodSlikaUrl, opt => opt.MapFrom(src => Path.GetFileName(src.PROIZVODSlikaUrl)));
            CreateMap<Proizvod, UpdateProizvodDTO>().ReverseMap();

            CreateMap<Zaglavlje_racuna, CreateZaglavlje_racunaDTO>().ReverseMap();
            CreateMap<Zaglavlje_racuna, GetZaglavlje_racunaDTO>()
                .ForMember(dest => dest.kupac, opt => opt.MapFrom(src => src.KUPAC));
            CreateMap<Zaglavlje_racuna, UpdateZaglavlje_racunaDTO>().ReverseMap();

            CreateMap<Stavke_racuna, CreateStavke_racunaDTO>().ReverseMap();
            CreateMap<Stavke_racuna, GetStavke_racunaDTO>()
                .ForMember(dest => dest.broj, opt => opt.MapFrom(src => src.ZAGLAVLJE_RACUNA.BROJ))
                .ForMember(dest => dest.proizvod, opt => opt.MapFrom(src => src.PROIZVOD));
            CreateMap<Stavke_racuna, UpdateStavke_racunaDTO>().ReverseMap();

        }
    }
}
