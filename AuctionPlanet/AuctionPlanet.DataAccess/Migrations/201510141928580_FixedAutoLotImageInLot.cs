namespace AuctionPlanet.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedAutoLotImageInLot : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "BanFlag");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "BanFlag", c => c.Boolean(nullable: false));
        }
    }
}
