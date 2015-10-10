using System;
using AuctionPlanet.DataAccess.Entities;
using AuctionPlanet.DataAccess.Utility;

namespace AuctionPlanet.BusinessLogic.DataTransferObjects
{
    public class LotInfo
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? StartTime { get; set; }

        public TimeSpan Duration { get; set; }

        public decimal StartPrice { get; set; }

        public decimal CurrentPrice { get; set; }

        public string OriginalOwner { get; set; }

        public string CurrentBidder { get; set; }

        public LotStatus Status { get; set; }

        public virtual LotImage Image { get; set; }
    }
}
