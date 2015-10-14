using System;
using System.ComponentModel.DataAnnotations;

namespace AuctionPlanet.WebPresentation.Models
{
    public class BidViewModel
    {
        public Guid LotId { get; set; }
        public string LotTitle { get; set; }
        public decimal CurrentPrice { get; set; }
        public string CurrentBidder { get; set; }
        [Required]
        public decimal NewPrice { get; set; }
        public string NewBidder { get; set; }
    }
}
