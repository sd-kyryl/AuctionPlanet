using System.Data.Entity;
using AuctionPlanet.DataAccess.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AuctionPlanet.DataAccess.Identity
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Lot> Lots { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{

        //}
    }
}
