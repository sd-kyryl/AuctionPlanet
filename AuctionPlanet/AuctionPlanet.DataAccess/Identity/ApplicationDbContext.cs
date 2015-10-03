using System.Data.Entity;
using AuctionPlanet.DataAccess.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AuctionPlanet.DataAccess.Identity
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private DbSet<Lot> PendingLots { get; set; }
        private DbSet<Lot> AvailableLots { get; set; }
        private DbSet<Lot> UnavailableLots { get; set; }
        private DbSet<AuctionUser> AuctionUsers { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
