using System.Data.Entity.Migrations;

namespace AuctionPlanet.DataAccess.Migrations
{
    public partial class MadeStartTimeNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Lots", "StartTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Lots", "StartTime", c => c.DateTime(nullable: false));
        }
    }
}
