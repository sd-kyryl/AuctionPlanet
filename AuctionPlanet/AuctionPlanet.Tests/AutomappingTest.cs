using System;
using System.Collections.Generic;
using System.Linq;
using AuctionPlanet.BusinessLogic.DataTransferObjects;
using AuctionPlanet.DataAccess.Entities;
using AuctionPlanet.DataAccess.Identity;
using AuctionPlanet.DataAccess.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static AutoMapper.Mapper;

namespace AuctionPlanet.Tests
{
    [TestClass]
    public class AutomappingTest
    {
        [TestMethod]
        public void EnumerableTestMethod()
        {
            CreateMap<Lot, LotInfo>().ReverseMap();
            IEnumerable<Lot> lots = Enumerable.Empty<Lot>().ToList();
            IEnumerable<LotInfo> lotInfos = Map<IEnumerable<LotInfo>>(lots);
        }

        [TestMethod]
        public void LotSetTestMethod()
        {
            CreateMap<Lot, LotInfo>().ReverseMap();

            ApplicationDbContext db = ApplicationDbContext.Create();

            IEnumerable<Lot> lots = db.Lots.Where(lot => lot.Status == LotStatus.Available).ToList();
            IEnumerable<LotInfo> lotInfos = Map<IEnumerable<LotInfo>>(lots);
        }

        [TestMethod]
        public void FilledLotEnumerableMappingTestMethod()
        {
            CreateMap<Lot, LotInfo>().ForMember(dest => dest.Duration, opts => opts.MapFrom(src => new TimeSpan(src.Duration)));
            CreateMap<LotInfo, Lot>().ForMember(dest => dest.Duration, opts => opts.MapFrom(src => src.Duration.Ticks));
            
            //CreateMap<Lot, LotInfo>().ReverseMap();

            List<Lot> lotList = new List<Lot>
            {
                new Lot
                {
                    Id = Guid.NewGuid(),
                    CurrentBidder = null,
                    CurrentPrice = 70M,
                    Description = "",
                    Duration = 3000000,
                    OriginalOwner = "Kyryl Shestakov",
                    StartPrice = 70M,
                    StartTime = null,
                    Status = LotStatus.PendingApproval,
                    Title = "Vase"
                }
            };

            IEnumerable<Lot> lots = lotList;
            IEnumerable<LotInfo> lotInfos = Map<IEnumerable<LotInfo>>(lots);
        }
    }
}
