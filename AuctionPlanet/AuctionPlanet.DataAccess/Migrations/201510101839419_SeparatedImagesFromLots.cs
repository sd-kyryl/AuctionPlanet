namespace AuctionPlanet.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeparatedImagesFromLots : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LotImages",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ImageType = c.String(),
                        ImageData = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Lots", "Image_Id", c => c.Guid());
            CreateIndex("dbo.Lots", "Image_Id");
            AddForeignKey("dbo.Lots", "Image_Id", "dbo.LotImages", "Id");
            DropColumn("dbo.Lots", "ImageType");
            DropColumn("dbo.Lots", "ImageData");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Lots", "ImageData", c => c.Binary());
            AddColumn("dbo.Lots", "ImageType", c => c.String());
            DropForeignKey("dbo.Lots", "Image_Id", "dbo.LotImages");
            DropIndex("dbo.Lots", new[] { "Image_Id" });
            DropColumn("dbo.Lots", "Image_Id");
            DropTable("dbo.LotImages");
        }
    }
}
