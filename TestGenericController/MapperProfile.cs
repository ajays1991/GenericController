using System;
using TestGenericController.Entities;
using AutoMapper;
using Data.entities;

namespace TestGenericController
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<AlbumRequest, Albums>().ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<Albums, AlbumResponse>();
        }

        public static string MapEnum(Enum anyEnum)
        {
            return anyEnum.ToString();
        }
    }
}
