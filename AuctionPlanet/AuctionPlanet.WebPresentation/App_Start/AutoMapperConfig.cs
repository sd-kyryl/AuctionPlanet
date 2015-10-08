using System;
using AuctionPlanet.BusinessLogic.DataTransferObjects;
using AuctionPlanet.DataAccess.Entities;
using AuctionPlanet.WebPresentation.Models;
using static AutoMapper.Mapper;

namespace AuctionPlanet.WebPresentation
{
    public static class AutoMapperConfig
    {
        public static void CreateMappings()
        {
            CreateMap<Lot, LotInfo>().ForMember(dest => dest.Duration, opts => opts.MapFrom(src => new TimeSpan(src.Duration)));
            CreateMap<LotInfo, Lot>().ForMember(dest => dest.Duration, opts => opts.MapFrom(src => src.Duration.Ticks));
            CreateMap<LotInfo, LotViewModel>().ReverseMap();
        }
    }
}
