namespace AuctionPlanet.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BanFlagForUsers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "BanFlag", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "BanFlag");
        }
    }
}
