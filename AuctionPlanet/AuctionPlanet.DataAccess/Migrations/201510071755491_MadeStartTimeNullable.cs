namespace AuctionPlanet.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
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
