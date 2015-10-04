using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionPlanet.DataAccess.Entities;
using AuctionPlanet.DataAccess.Identity;

namespace AuctionPlanet.WebPresentation.Models
{
    public class AuctionUserViewModel
    {
        public Guid Id { get; set; }

        public bool BannedFlag { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<Lot> PublishedLots { get; set; }

        public virtual ICollection<Lot> AcquiredLots { get; set; }
    }
}
