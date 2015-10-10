using System;
using System.ComponentModel.DataAnnotations;
using AuctionPlanet.DataAccess.Entities;
using AuctionPlanet.DataAccess.Utility;

namespace AuctionPlanet.WebPresentation.Models
{
    public class LotViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
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
