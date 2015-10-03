using System;
using System.Collections.Generic;
using AuctionPlanet.DataAccess.Identity;

namespace AuctionPlanet.DataAccess.Entities
{
    public class AuctionUser
    {
        public Guid Id { get; set; }

        public Guid ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<Lot> PublishedLots { get; set; }

        public virtual ICollection<Lot> AcquiredLots { get; set; }
    }
}
