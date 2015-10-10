namespace AuctionPlanet.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageData : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lots", "ImageType", c => c.String());
            AddColumn("dbo.Lots", "ImageData", c => c.Binary());
            DropColumn("dbo.Lots", "ImageUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Lots", "ImageUrl", c => c.String());
            DropColumn("dbo.Lots", "ImageData");
            DropColumn("dbo.Lots", "ImageType");
        }
    }
}
