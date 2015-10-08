using System.Data.Entity.Migrations;
using AuctionPlanet.DataAccess.Identity;

namespace AuctionPlanet.DataAccess.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            ////Creation of two roles
            //var role1 = new IdentityRole { Name = "admin" };
            //var role2 = new IdentityRole { Name = "user" };

            ////Addition of roles to the database
            //IdentityResult adminRoleCreationResult = roleManager.Create(role1);

            //if (!adminRoleCreationResult.Succeeded)
            //{
            //    throw new Exception("Creation of admin role failed");
            //}

            //IdentityResult userRoleCreationResult = roleManager.Create(role2);

            //if (!userRoleCreationResult.Succeeded)
            //{
            //    throw new Exception("Creation of user role failed");
            //}
        }
    }
}
