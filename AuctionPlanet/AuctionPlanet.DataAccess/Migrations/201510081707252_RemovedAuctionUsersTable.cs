namespace AuctionPlanet.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedAuctionUsersTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Lots", "AuctionUser_Id", "dbo.AuctionUsers");
            DropForeignKey("dbo.AuctionUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Lots", "AuctionUser_Id1", "dbo.AuctionUsers");
            DropIndex("dbo.AuctionUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Lots", new[] { "AuctionUser_Id" });
            DropIndex("dbo.Lots", new[] { "AuctionUser_Id1" });
            DropColumn("dbo.Lots", "AuctionUser_Id");
            DropColumn("dbo.Lots", "AuctionUser_Id1");
            DropTable("dbo.AuctionUsers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AuctionUsers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BannedFlag = c.Boolean(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Lots", "AuctionUser_Id1", c => c.Guid());
            AddColumn("dbo.Lots", "AuctionUser_Id", c => c.Guid());
            CreateIndex("dbo.Lots", "AuctionUser_Id1");
            CreateIndex("dbo.Lots", "AuctionUser_Id");
            CreateIndex("dbo.AuctionUsers", "ApplicationUser_Id");
            AddForeignKey("dbo.Lots", "AuctionUser_Id1", "dbo.AuctionUsers", "Id");
            AddForeignKey("dbo.AuctionUsers", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Lots", "AuctionUser_Id", "dbo.AuctionUsers", "Id");
        }
    }
}
