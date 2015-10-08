using System.Data.Entity.Migrations;

namespace AuctionPlanet.DataAccess.Migrations
{
    public partial class RemovedUserReferencesFromLots : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Lots", "NewOwner_Id", "dbo.AuctionUsers");
            DropForeignKey("dbo.Lots", "OriginalOwner_Id", "dbo.AuctionUsers");
            DropIndex("dbo.Lots", new[] { "NewOwner_Id" });
            DropIndex("dbo.Lots", new[] { "OriginalOwner_Id" });
            AddColumn("dbo.AuctionUsers", "ApplicationUserId", c => c.Guid(nullable: false));
            AddColumn("dbo.Lots", "OriginalOwner", c => c.String());
            DropColumn("dbo.Lots", "NewOwner_Id");
            DropColumn("dbo.Lots", "OriginalOwner_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Lots", "OriginalOwner_Id", c => c.Guid());
            AddColumn("dbo.Lots", "NewOwner_Id", c => c.Guid());
            DropColumn("dbo.Lots", "OriginalOwner");
            DropColumn("dbo.AuctionUsers", "ApplicationUserId");
            CreateIndex("dbo.Lots", "OriginalOwner_Id");
            CreateIndex("dbo.Lots", "NewOwner_Id");
            AddForeignKey("dbo.Lots", "OriginalOwner_Id", "dbo.AuctionUsers", "Id");
            AddForeignKey("dbo.Lots", "NewOwner_Id", "dbo.AuctionUsers", "Id");
        }
    }
}
