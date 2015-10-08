using System.Data.Entity.Migrations;

namespace AuctionPlanet.DataAccess.Migrations
{
    public partial class ChangeOfDuration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Lots", "Duration", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Lots", "Duration", c => c.Time(nullable: false, precision: 7));
        }
    }
}
