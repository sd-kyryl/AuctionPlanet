using System;
using System.Collections.Generic;
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
            CreateMap<IEnumerable<Lot>, IEnumerable<LotInfo>>().ReverseMap();
            CreateMap<IEnumerable<LotInfo>, IEnumerable<LotViewModel>>().ReverseMap();
            
            CreateMap<AuctionUser, AuctionUserInfo>().ReverseMap();
            CreateMap<IEnumerable<AuctionUser>, IEnumerable<AuctionUserInfo>>().ReverseMap();
        }
    }
}
