using System.Data.Entity.Migrations;

namespace AuctionPlanet.DataAccess.Migrations
{
    public partial class ReplacedEndTimeWithDuration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lots", "Duration", c => c.Time(nullable: false, precision: 7));
            DropColumn("dbo.Lots", "EndTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Lots", "EndTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Lots", "Duration");
        }
    }
}
